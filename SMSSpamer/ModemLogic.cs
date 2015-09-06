using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;

namespace SMSSpamer
{
  class ModemLogic : IDisposable
  {
    public AddModemLogDelegate AddModemLog;

    public AutoResetEvent receiveNow;

    private string _ConnectedPortName;

    private string[] ports;
    public string[] Ports
    {
      get { return ports; }
      set { ports = value; }
    }

    private SerialPort connectedPort;
    public SerialPort ConnectedPort
    {
      get { return connectedPort; }
      set { connectedPort = value; }
    }

    private Dictionary<string, string> friendlyPorts;

    public string GetPortNameByIndex(int index)
    {
      try
      {
        return ports[index];
      }
      catch
      {
        return string.Empty;
      }
    }

    public string[] LoadPorts()
    {
      try
      {
        ports = SerialPort.GetPortNames();
        Array.Sort(ports, StringComparer.InvariantCulture);

        friendlyPorts = BuildPortNameHash(ports);

        var Names = new List<string>();

        foreach (var port in ports)
        {
          string name;
          if (friendlyPorts.TryGetValue(port, out name))
            Names.Add(name);
          else
            Names.Add(port);
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
      connectedPort = new SerialPort();
      try
      {
        connectedPort.PortName = strPortName;
        connectedPort.BaudRate = 9600;
        connectedPort.DataBits = 8;
        connectedPort.StopBits = StopBits.One;
        connectedPort.Parity = Parity.None;
        connectedPort.ReadTimeout = 300;
        connectedPort.WriteTimeout = 300;
        connectedPort.Encoding = Encoding.GetEncoding("iso-8859-1");
        connectedPort.DataReceived += new SerialDataReceivedEventHandler(port_DataReceived);
        connectedPort.Open();
        connectedPort.DtrEnable = true;
        connectedPort.RtsEnable = true;
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
        if (connectedPort != null)
        {
          connectedPort.Close();
          connectedPort.DataReceived -= new SerialDataReceivedEventHandler(port_DataReceived);
          connectedPort = null;
        }
      }
      catch (Exception ex)
      {
        throw new Exception("Can't close port " + ConnectedPort.PortName + ": " + ex.Message, ex);
      }
    }

    public string SendMessage(string PhoneNo, string Message)
    {
      const string OK = "\r\nOK\r\n";
      const int maxMessageLength = 70;
      var timeout = Properties.Default.TimeoutCommand * 1000;
      try
      {
        if (connectedPort == null)
          OpenPort(_ConnectedPortName);

        var command = "AT";
        Console.WriteLine(command);
        AddModemLog(command, null);
        var receivedData = ExecCommand(command, timeout);
        AddModemLog(null, receivedData);
        if (!receivedData.EndsWith(OK))
          throw new Exception("Request: '" + command + "' Responce: '" + receivedData + "'");

        command = "AT+CMEE=1\r";
        Console.WriteLine(command);
        AddModemLog(command, null);
        receivedData = ExecCommand(command, timeout);
        AddModemLog(null, receivedData);
        if (!receivedData.EndsWith(OK))
          throw new Exception("Request: '" + command + "' Responce: '" + receivedData + "'");

        command = "AT+CMGF=0";
        Console.WriteLine(command);
        AddModemLog(command, null);
        receivedData = ExecCommand(command, timeout);
        AddModemLog(null, receivedData);
        if (!receivedData.EndsWith(OK))
          throw new Exception("Request: '" + command + "' Responce: '" + receivedData + "'");

        if (Message.Length <= maxMessageLength)
        {
          var PDUMessage = new StringBuilder();
          PDUMessage.Append("000100" + string.Format("{0:X2}", PhoneNo.Length + 1) + "91");
          PDUMessage.Append(ConvertPhoneNumber(PhoneNo));
          PDUMessage.Append("0008" + string.Format("{0:X2}", Message.Length * 2) + ConvertTextToUCS(Message));
          command = "AT+CMGS=" + Convert.ToString((PDUMessage.Length / 2) - 1) + "\r";
          Console.WriteLine(command);
          AddModemLog(command, null);
          receivedData = ExecCommand(command, timeout);
          AddModemLog(null, receivedData);
          command = PDUMessage.ToString() + Convert.ToChar(26);
          Console.WriteLine(command);
          AddModemLog(command, null);
          receivedData = ExecCommand(command, timeout);
          AddModemLog(null, receivedData);
          if (!receivedData.EndsWith(OK))
            throw new Exception("Request: '" + command + "' Responce: '" + receivedData + "'");
          else
            return null;
        }
        else
        {
          var partcount = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(Message.Length) / Convert.ToDouble(maxMessageLength)));
          Console.WriteLine("Message.Length: " + Message.Length.ToString());
          Console.WriteLine("partcount: " + partcount.ToString());
          for (var part = 0; part < partcount; part++)
          {
            const int headerLength = 6;
            var PDUMessage = new StringBuilder();
            PDUMessage.Append("004100" + string.Format("{0:X2}", PhoneNo.Length + 1) + "91");
            PDUMessage.Append(ConvertPhoneNumber(PhoneNo));
            var partLength = part < (partcount - 1) ? maxMessageLength : Message.Length - maxMessageLength * part;
            var partMessage = Message.Substring(part * maxMessageLength, partLength);
            PDUMessage.Append("0008" + string.Format("{0:X2}", partLength * 2 + headerLength) + "05000300" + String.Format("{0:X2}", partcount) + String.Format("{0:X2}", part + 1) + ConvertTextToUCS(partMessage));
            command = "AT+CMGS=" + Convert.ToString((PDUMessage.Length / 2) - 1) + "\r";
            Console.WriteLine(command);
            AddModemLog(command, null);
            receivedData = ExecCommand(command, timeout);
            AddModemLog(null, receivedData);
            command = PDUMessage.ToString() + Convert.ToChar(26);
            Console.WriteLine(command);
            AddModemLog(command, null);
            receivedData = ExecCommand(command, timeout);
            AddModemLog(null, receivedData);
            if (!receivedData.EndsWith(OK))
              throw new Exception("Request: '" + command + "' Responce: '" + receivedData + "'");
          }
          return null;
        }
      }
      catch
      {
        throw;
      }
    }

    //Execute AT Command
    public string ExecCommand(string command, int responseTimeout)
    {
      try
      {
        connectedPort.DiscardOutBuffer();
        connectedPort.DiscardInBuffer();
        receiveNow.Reset();
        connectedPort.Write(command + "\r");

        var input = ReadResponse(responseTimeout);
        if ((input.Length == 0) || ((!input.EndsWith("\r\n> ")) && (!input.EndsWith("\r\nOK\r\n"))))
          throw new Exception("No success message was received. Modem responce is: " + input);
        return input;
      }
      catch
      {
        throw;
      }
    }

    public string ReadResponse(int timeout)
    {
      var buffer = string.Empty;
      try
      {
        do
        {
          if (receiveNow.WaitOne(timeout, false))
          {
            var t = connectedPort.ReadExisting();
            buffer += t;
          }
          else
          {
            if (buffer.Length > 0)
              throw new Exception("Response received is incomplete. Received responce: '" + buffer + "'");
            else
              throw new Exception("No data received from phone.");
          }
        }
        while (!buffer.EndsWith("\r\nOK\r\n") && !buffer.EndsWith("\r\n> ") && !buffer.EndsWith("\r\nERROR\r\n"));
      }
      catch
      {
        throw;
      }
      return buffer;
    }

    //Receive data from port
    private void port_DataReceived(object sender, SerialDataReceivedEventArgs e)
    {
      try
      {
        if (e.EventType == SerialData.Chars)
          receiveNow.Set();
      }
      catch
      {
        throw;
      }
    }

    private void MineRegistryForPortName(string startKeyPath, Dictionary<string, string> targetMap, string[] portsToMap)
    {
      if (targetMap.Count >= portsToMap.Length)
        return;
      using (var currentKey = Registry.LocalMachine)
      {
        try
        {
          using (var currentSubKey = currentKey.OpenSubKey(startKeyPath))
          {
            var currentSubkeys = currentSubKey.GetSubKeyNames();
            if (currentSubkeys.Contains("Device Parameters") &&
                startKeyPath != "SYSTEM\\CurrentControlSet\\Enum")
            {
              var portName = Registry.GetValue("HKEY_LOCAL_MACHINE\\" +
                  startKeyPath + "\\Device Parameters", "PortName", null);
              if (portName == null || portsToMap.Contains(portName.ToString()) == false)
                return;
              var friendlyPortName = Registry.GetValue("HKEY_LOCAL_MACHINE\\" + startKeyPath, "FriendlyName", null);
              var friendlyName = "N/A";
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

    private const string strAlphabet = "АБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЬЪЭЮЯабвгдеёжзийклмнопрстуфхцчшщэюяABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789'-* :;)(.,!=_ыЫъь+";
    private string[] ArrayUCSCode = new string[142]{            
            "0410","0411","0412","0413","0414","0415","00A8","0416","0417",
            "0418","0419","041A","041B","041C","041D","041E","041F","0420",
            "0421","0422","0423","0424","0425","0426","0427","0428","0429",
            "042C","042A","042D","042E","042F","0430","0431","0432","0433",
            "0434","0435","00B8","0436","0437","0438","0439","043A","043B",
            "043C","043D","043E","043F","0440","0441","0442","0443","0444",
            "0445","0446","0447","0448","0449","044D","044E","044F","0041",
            "0042","0043","0044","0045","0046","0047","0048","0049","004A",
            "004B","004C","004D","004E","004F","0050","0051","0052","0053",
            "0054","0055","0056","0057","0058","0059","005A","0061","0062",
            "0063","0064","0065","0066","0067","0068","0069","006A","006B",
            "006C","006D","006E","006F","0070","0071","0072","0073","0074",
            "0075","0076","0077","0078","0079","007A","0030","0031","0032",
            "0033","0034","0035","0036","0037","0038","0039","0027","002D",
            "002A","0020","003A","003B","0029","0028","002E","002C","0021",
            "003D","005F","044B","042B", "044A","044C","002B"};

    private string ConvertTextToUCS(string InputText)
    {
      var UCS = new StringBuilder(InputText.Length);
      var intLetterIndex = 0;
      for (var i = 0; i < InputText.Length; i++)
      {
        intLetterIndex = strAlphabet.IndexOf(InputText[i]);
        if (intLetterIndex != -1)
          UCS.Append(ArrayUCSCode[intLetterIndex]);
      }
      return UCS.ToString();
    }

    private string ConvertPhoneNumber(string PhoneNumber)
    {
      var NewNumber = new StringBuilder(PhoneNumber.Length);
      if (PhoneNumber.Length / 2 == PhoneNumber.Length / 2.0) // число четное
      {
        for (var i = 0; i < PhoneNumber.Length / 2; i++)
        {
          NewNumber.Append(PhoneNumber[2 * i + 1].ToString());
          NewNumber.Append(PhoneNumber[2 * i].ToString());
        }
      }
      else // номер с нечетным кол-вом символом
      {
        for (var i = 0; i < PhoneNumber.Length / 2; i++)
        {
          NewNumber.Append(PhoneNumber[2 * i + 1].ToString());
          NewNumber.Append(PhoneNumber[2 * i].ToString());
        }
        NewNumber.Append("F");
        NewNumber.Append(PhoneNumber[PhoneNumber.Length - 1]);
      }
      return NewNumber.ToString();
    }

    ~ModemLogic()
    {
      Dispose(false);
    }

    public void Dispose()
    {
      Dispose(true);
      GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
      ClosePort();
      if (receiveNow != null)
        receiveNow.Dispose();
    }

  }
}
