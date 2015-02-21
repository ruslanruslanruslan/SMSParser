using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SMSSpamer
{
  class ModemLogic
  {
    public AutoResetEvent receiveNow;

    private string _ConnectedPortName;

    private string[] _Ports;
    public string[] Ports
    {
      get { return _Ports; }
      set { _Ports = value; }
    }

    private SerialPort _ConnectedPort;
    public SerialPort ConnectedPort
    {
      get { return _ConnectedPort; }
      set { _ConnectedPort = value; }
    }

    private Dictionary<string, string> friendlyPorts;

    public string GetPortNameByIndex(int index)
    {
      try
      {
        return _Ports[index];
      }
      catch
      {
        return String.Empty;
      }
    }

    public string[] LoadPorts()
    {
      try
      {
        _Ports = SerialPort.GetPortNames();
        Array.Sort(_Ports, StringComparer.InvariantCulture);

        friendlyPorts = BuildPortNameHash(_Ports);

        List<string> Names = new List<string>();

        foreach (string port in _Ports)
        {
          string name;
          if (friendlyPorts.TryGetValue(port, out name))
          {
            Names.Add(name);
          }
          else
          {
            Names.Add(port);
          }
        }
        return Names.ToArray();
      }
      catch (Exception ex)
      {
        throw new Exception("Can't enumerate avalable devices: " + ex.Message, ex);
      }
    }

    public void OpenPort(string strPortName)
    {
      _ConnectedPortName = strPortName;
      receiveNow = new AutoResetEvent(false);
      _ConnectedPort = new SerialPort();
      try
      {
        _ConnectedPort.PortName = strPortName;
        _ConnectedPort.BaudRate = 9600;
        _ConnectedPort.DataBits = 8;
        _ConnectedPort.StopBits = StopBits.One;
        _ConnectedPort.Parity = Parity.None;
        _ConnectedPort.ReadTimeout = 300;
        _ConnectedPort.WriteTimeout = 300;
        _ConnectedPort.Encoding = Encoding.GetEncoding("iso-8859-1");
        _ConnectedPort.DataReceived += new SerialDataReceivedEventHandler(port_DataReceived);
        _ConnectedPort.Open();
        _ConnectedPort.DtrEnable = true;
        _ConnectedPort.RtsEnable = true;
      }
      catch (Exception ex)
      {
        throw new Exception("Can't open selected port " + strPortName + ": " + ex.Message, ex);
      }
    }

    //Close Port
    public void ClosePort()
    {
      try
      {
        if (_ConnectedPort != null)
        {
          _ConnectedPort.Close();
          _ConnectedPort.DataReceived -= new SerialDataReceivedEventHandler(port_DataReceived);
          _ConnectedPort = null;
        }
      }
      catch (Exception ex)
      {
        throw new Exception("Can't close port " + ConnectedPort.PortName + ": " + ex.Message, ex);
      }
    }

    public bool SendMessage(string PhoneNo, string Message)
    {
      bool isSend = false;
      try
      {
        if (_ConnectedPort == null)
        {
          OpenPort(_ConnectedPortName);
        }
        string recievedData = ExecCommand("AT", 300);
        recievedData = ExecCommand("AT+CMGF=1", 300);
        String command = "AT+CMGS=\"" + PhoneNo + "\"";
        recievedData = ExecCommand(command, 300);
        command = Message + char.ConvertFromUtf32(26) + "\r";
        recievedData = ExecCommand(command, 3000); //3 seconds
        if (recievedData.EndsWith("\r\nOK\r\n"))
        {
          isSend = true;
        }
        else if (recievedData.Contains("ERROR"))
        {
          isSend = false;
        }
        return isSend;
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    //Execute AT Command
    public string ExecCommand(string command, int responseTimeout)
    {
      try
      {
        _ConnectedPort.DiscardOutBuffer();
        _ConnectedPort.DiscardInBuffer();
        receiveNow.Reset();
        _ConnectedPort.Write(command + "\r");

        string input = ReadResponse(responseTimeout);
        if ((input.Length == 0) || ((!input.EndsWith("\r\n> ")) && (!input.EndsWith("\r\nOK\r\n"))))
          throw new Exception("No success message was received.");
        return input;
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public string ReadResponse(int timeout)
    {
      string buffer = string.Empty;
      try
      {
        do
        {
          if (receiveNow.WaitOne(timeout, false))
          {
            string t = _ConnectedPort.ReadExisting();
            buffer += t;
          }
          else
          {
            if (buffer.Length > 0)
              throw new Exception("Response received is incomplete.");
            else
              throw new Exception("No data received from phone.");
          }
        }
        while (!buffer.EndsWith("\r\nOK\r\n") && !buffer.EndsWith("\r\n> ") && !buffer.EndsWith("\r\nERROR\r\n"));
      }
      catch (Exception ex)
      {
        throw ex;
      }
      return buffer;
    }

    //Receive data from port
    private void port_DataReceived(object sender, SerialDataReceivedEventArgs e)
    {
      try
      {
        if (e.EventType == SerialData.Chars)
        {
          receiveNow.Set();
        }
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    private void MineRegistryForPortName(string startKeyPath, Dictionary<string, string> targetMap, string[] portsToMap)
    {
      if (targetMap.Count >= portsToMap.Length)
        return;
      using (RegistryKey currentKey = Registry.LocalMachine)
      {
        try
        {
          using (RegistryKey currentSubKey = currentKey.OpenSubKey(startKeyPath))
          {
            string[] currentSubkeys = currentSubKey.GetSubKeyNames();
            if (currentSubkeys.Contains("Device Parameters") &&
                startKeyPath != "SYSTEM\\CurrentControlSet\\Enum")
            {
              object portName = Registry.GetValue("HKEY_LOCAL_MACHINE\\" +
                  startKeyPath + "\\Device Parameters", "PortName", null);
              if (portName == null ||
                  portsToMap.Contains(portName.ToString()) == false)
                return;
              object friendlyPortName = Registry.GetValue("HKEY_LOCAL_MACHINE\\" +
                  startKeyPath, "FriendlyName", null);
              string friendlyName = "N/A";
              if (friendlyPortName != null)
                friendlyName = friendlyPortName.ToString();
              if (friendlyName.Contains(portName.ToString()) == false)
                friendlyName = string.Format("{0} ({1})", friendlyName, portName);
              targetMap[portName.ToString()] = friendlyName;
            }
            else
            {
              foreach (string strSubKey in currentSubkeys)
                MineRegistryForPortName(startKeyPath + "\\" + strSubKey, targetMap, portsToMap);
            }
          }
        }
        catch (Exception)
        {
          Console.WriteLine("Error accessing key '{0}'.. Skipping..", startKeyPath);
        }
      }
    }
    private Dictionary<string, string> BuildPortNameHash(string[] portsToMap)
    {
      Dictionary<string, string> oReturnTable = new Dictionary<string, string>();
      MineRegistryForPortName("SYSTEM\\CurrentControlSet\\Enum", oReturnTable, portsToMap);
      return oReturnTable;
    }
  }
}
