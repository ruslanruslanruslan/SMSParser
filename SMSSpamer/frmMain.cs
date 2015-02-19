using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SMSSpamer
{
  public partial class frmMain : Form
  {
    public frmMain()
    {
      InitializeComponent();
    }

    ModemLogic modemLogic = new ModemLogic();

    private void AddLog(string msg, Color msgColor)
    {
      int start = rtbLog.Text.Length - 1;
      if (start < 0)
        start = 0;
      rtbLog.AppendText(DateTime.Now.ToShortTimeString() + " | " + msg + Environment.NewLine);
      rtbLog.Select(start, rtbLog.Text.Length - start + 1);
      rtbLog.SelectionColor = msgColor;
      rtbLog.SelectionStart = rtbLog.Text.Length;
      rtbLog.ScrollToCaret();
    }
    //static List<USBDeviceInfo> GetUSBDevices()
    //{
    //  List<USBDeviceInfo> devices = new List<USBDeviceInfo>();

    //  ManagementObjectCollection collection;
    //  using (var searcher = new ManagementObjectSearcher(@"Select * From Win32_USBHub"))
    //    collection = searcher.Get();

    //  foreach (var device in collection)
    //  {
    //    devices.Add(new USBDeviceInfo(
    //    (string)device.GetPropertyValue("DeviceID"),
    //    (string)device.GetPropertyValue("PNPDeviceID"),
    //    (string)device.GetPropertyValue("Description"),
    //    (string)device.GetPropertyValue("Caption"),
    //    (string)device.GetPropertyValue("CreationClassName"),
    //    (string)device.GetPropertyValue("Name"),
    //    (string)device.GetPropertyValue("SystemCreationClassName"),
    //    (string)device.GetPropertyValue("SystemName")
    //    ));
    //  }

    //  collection.Dispose();
    //  return devices;
    //}

    //class USBDeviceInfo
    //{
    //  public USBDeviceInfo(string deviceID, string pnpDeviceID, string description, string caption, string creationClassName, string name, string systemCreationClassName, string systemName)
    //  {
    //    this.DeviceID = deviceID;
    //    this.PnpDeviceID = pnpDeviceID;
    //    this.Description = description;
    //    this.Caption = caption;
    //    this.CreationClassName = creationClassName;
    //    this.Name = name;
    //    this.SystemCreationClassName = systemCreationClassName;
    //    this.SystemName = systemName;
    //  }
    //  public string DeviceID { get; private set; }
    //  public string PnpDeviceID { get; private set; }
    //  public string Description { get; private set; }
    //  public string Caption { get; private set; }
    //  public string CreationClassName { get; private set; }
    //  public string Name { get; private set; }
    //  public string SystemCreationClassName { get; private set; }
    //  public string SystemName { get; private set; }
    //}
    //private void FindModem()
    //{
    //  //ManagementScope scope = new ManagementScope();
    //  //SelectQuery query = new SelectQuery("SELECT * FROM Win32_USBControllerDevice");
    //  //ManagementObjectSearcher searcher = new ManagementObjectSearcher(scope, query);

    //  //try
    //  //{
    //  //  foreach (ManagementObject item in searcher.Get())
    //  //  {
    //  //    String description = item["Description"].ToString();
    //  //    String deviceID = item["DeviceID"].ToString();

    //  //    AddLog("Porta " + description + " deviceID " + deviceID, Color.Black);

    //  //    /*if (description.Contains("USB Serial Port"))
    //  //      return deviceID;*/
    //  //  }
    //  //}
    //  //catch (ManagementException)
    //  //{
    //  //}

    //  //AddLog("Method 1", Color.Black);
    //  //try
    //  //{
    //  //  ManagementObjectSearcher searcher =
    //  //      new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_USBControllerDevice ");

    //  //  foreach (ManagementObject queryObj in searcher.Get())
    //  //  {
    //  //    AddLog(queryObj["DeviceID"].ToString(), Color.Black);
    //  //    AddLog(queryObj["Description"].ToString(), Color.Black);
    //  //    AddLog(queryObj["Caption"].ToString(), Color.Black);
    //  //    AddLog(queryObj["Name"].ToString(), Color.Black);
    //  //    AddLog(queryObj["ProviderType"].ToString(), Color.Black);
    //  //    AddLog(queryObj["SystemName"].ToString(), Color.Black);
    //  //    AddLog(queryObj["SystemCreationClassName"].ToString(), Color.Black);
    //  //  }
    //  //}
    //  //catch (ManagementException e)
    //  //{
    //  //  MessageBox.Show("An error occurred while querying for WMI data: " + e.Message);
    //  //}
    //  //AddLog("Method 2", Color.Black);
    //  //var usbDevices = GetUSBDevices();

    //  //foreach (var usbDevice in usbDevices)
    //  //{
    //  //  AddLog(String.Format("Device ID: {0}\n PNP Device ID: {1}\n Description: {2}\n Caption: {3}\n CreationClassName: {4}\n Name: {5}\n SystemCreationClassName: {6}\n SystemName: {7}\n SystemCreationClassName: {7}\n",
    //  //    usbDevice.DeviceID, usbDevice.PnpDeviceID, usbDevice.Description, usbDevice.Caption, usbDevice.CreationClassName, usbDevice.Name, usbDevice.SystemCreationClassName, usbDevice.SystemName, usbDevice.SystemCreationClassName), Color.Black);
    //  //}

    //  Dictionary<string, string> friendlyPorts = BuildPortNameHash(SerialPort.GetPortNames());
    //  foreach (KeyValuePair<string, string> kvp in friendlyPorts)
    //  {
    //    AddLog(String.Format("Port '{0}' is better known as '{1}'", kvp.Key, kvp.Value), Color.Black);
    //  }

    //}

    //private void frmMain_Load(object sender, EventArgs e)
    //{
    //  FindModem();
    //}

    //static Dictionary<string, string> BuildPortNameHash(string[] portsToMap)
    //{
    //  Dictionary<string, string> oReturnTable = new Dictionary<string, string>();
    //  MineRegistryForPortName("SYSTEM\\CurrentControlSet\\Enum", oReturnTable, portsToMap);
    //  return oReturnTable;
    //}
    ///// <summary>
    ///// Recursively enumerates registry subkeys starting with startKeyPath looking for 
    ///// "Device Parameters" subkey. If key is present, friendly port name is extracted.
    ///// </summary>
    ///// <param name="startKeyPath">the start key from which to begin the enumeration</param>
    ///// <param name="targetMap">dictionary that will get populated with 
    ///// nonfriendly-to-friendly port names</param>
    ///// <param name="portsToMap">array of port names (i.e. COM1, COM2, etc)</param>
    //static void MineRegistryForPortName(string startKeyPath, Dictionary<string, string> targetMap,
    //    string[] portsToMap)
    //{
    //  if (targetMap.Count >= portsToMap.Length)
    //    return;
    //  using (RegistryKey currentKey = Registry.LocalMachine)
    //  {
    //    try
    //    {
    //      using (RegistryKey currentSubKey = currentKey.OpenSubKey(startKeyPath))
    //      {
    //        string[] currentSubkeys = currentSubKey.GetSubKeyNames();
    //        if (currentSubkeys.Contains("Device Parameters") &&
    //            startKeyPath != "SYSTEM\\CurrentControlSet\\Enum")
    //        {
    //          object portName = Registry.GetValue("HKEY_LOCAL_MACHINE\\" +
    //              startKeyPath + "\\Device Parameters", "PortName", null);
    //          if (portName == null ||
    //              portsToMap.Contains(portName.ToString()) == false)
    //            return;
    //          object friendlyPortName = Registry.GetValue("HKEY_LOCAL_MACHINE\\" +
    //              startKeyPath, "FriendlyName", null);
    //          string friendlyName = "N/A";
    //          if (friendlyPortName != null)
    //            friendlyName = friendlyPortName.ToString();
    //          if (friendlyName.Contains(portName.ToString()) == false)
    //            friendlyName = string.Format("{0} ({1})", friendlyName, portName);
    //          targetMap[portName.ToString()] = friendlyName;
    //        }
    //        else
    //        {
    //          foreach (string strSubKey in currentSubkeys)
    //            MineRegistryForPortName(startKeyPath + "\\" + strSubKey, targetMap, portsToMap);
    //        }
    //      }
    //    }
    //    catch (Exception)
    //    {
    //      Console.WriteLine("Error accessing key '{0}'.. Skipping..", startKeyPath);
    //    }
    //  }
    //}

    private bool TryToConnectToModem(string strPortName)
    {
      try
      {
        modemLogic.ClosePort();
      }
      catch (Exception ex)
      {
        AddLog(ex.Message, LogMessageColor.Error());
      }
      try
      {
        modemLogic.OpenPort(strPortName);
        if(modemLogic.ConnectedPort != null)
        {
          AddLog("Device successfully connected", LogMessageColor.Information());
          return true;
        }
      }
      catch (Exception ex)
      {
        AddLog(ex.Message, LogMessageColor.Error());
      }
      return false;
    }

    private void LoadPortNames()
    {
      try
      {
        string[] ports = modemLogic.LoadPorts();
        foreach (string port in ports)
        {
          cbModem.Items.Add(port);
        }
      }
      catch(Exception ex)
      {
        AddLog(ex.Message, LogMessageColor.Error());
      }
      try
      {
        if (cbModem.Items.Count > 0)
        {
          cbModem.SelectedIndex = 0;
        }
        else
        {
          AddLog("No avalable devices", LogMessageColor.Error());
        }
      }
      catch(Exception ex)
      {
        AddLog(ex.Message, LogMessageColor.Error());
      }
    }

    private void frmMain_Load(object sender, EventArgs e)
    {
      string[] args = Environment.GetCommandLineArgs();
      btnSend.Enabled = false;
      cbModem.SelectedIndexChanged -= cbModem_SelectedIndexChanged;
      LoadPortNames();
      if (cbModem.Items.Count > 0)
      {
        if (cbModem.Items.Contains(Properties.Default.COMPortName))
        {
          cbModem.SelectedIndex = cbModem.Items.IndexOf(Properties.Default.COMPortName);
        }
      }
      edtPhoneNumber.Text = Properties.Default.PhoneNumber;
      edtMessage.Text = Properties.Default.Message;
      if (TryToConnectToModem(cbModem.Text))
      {
        btnSend.Enabled = true;
      }
      cbModem.SelectedIndexChanged += cbModem_SelectedIndexChanged;
      if (args.Count() > 1)
      {
        cbModem.Enabled = false;
        edtPhoneNumber.Enabled = false;
        edtMessage.Enabled = false;
        btnSend.Enabled = false;
        if (args[1] == "--send-sms")
        {
          Text += " 'Send single SMS mode'";
          AddLog("Send single SMS mode", LogMessageColor.Information());
          TryAutoSendMessage(args[2], args[3]);
        }
      }
    }
    private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
    {
      Properties.Default.COMPortName = cbModem.Text;
      Properties.Default.PhoneNumber = edtPhoneNumber.Text;
      Properties.Default.Message = edtMessage.Text;
      Properties.Default.Save();
    }

    private void cbModem_SelectedIndexChanged(object sender, EventArgs e)
    {
      btnSend.Enabled = false;
      TryToConnectToModem(cbModem.Text);
      btnSend.Enabled = true;
    }

    private void btnSend_Click(object sender, EventArgs e)
    {
      if (SendMessage(edtPhoneNumber.Text, edtMessage.Text))
      {
        AddLog("Message '" + edtMessage.Text + "' successfully sent to '" + edtPhoneNumber.Text + "'", LogMessageColor.Error());
      }
      else
      {
        AddLog("Can't send message '" + edtMessage.Text + "' to '" + edtPhoneNumber.Text, LogMessageColor.Error());
      }
    }

    private bool SendMessage(string PhoneNo, string Message)
    {
      try
      {
        AddLog("Sending message '" + edtMessage.Text + "' to '" + edtPhoneNumber.Text, LogMessageColor.Information());
        return modemLogic.SendMessage(PhoneNo, Message);
      }
      catch (Exception ex)
      {
        AddLog("Can't send message '" + Message + "' to '" + PhoneNo + "': " + ex.Message, LogMessageColor.Error());
      }
      return false;
    }

    private void TryAutoSendMessage(string Phone, string Message)
    {
      if (!SendMessage(Phone, Message))
      {
        foreach(string port in modemLogic.Ports)
        {
          modemLogic.OpenPort(port);
          if (SendMessage(Phone, Message))
          {
            Program.Exit(0);
            return;
          }
        }
      }
      Program.Exit(1);
    }

  }

  class LogMessageColor
  {
    public static Color Information()
    {
      return Color.Black;
    }
    public static Color Warning()
    {
      return Color.Gold;
    }
    public static Color Error()
    {
      return Color.Red;
    }
  }
}