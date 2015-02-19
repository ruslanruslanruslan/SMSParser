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
        if (cbModem.Items.Contains(Properties.Default.ModemName))
        {
          cbModem.SelectedIndex = cbModem.Items.IndexOf(Properties.Default.ModemName);
        }
      }
      edtPhoneNumber.Text = Properties.Default.PhoneNumber;
      edtMessage.Text = Properties.Default.Message;
      if (TryToConnectToModem(modemLogic.GetPortNameByIndex(cbModem.SelectedIndex)))
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
      modemLogic.ClosePort();
      Properties.Default.ModemName = cbModem.Text;
      Properties.Default.PhoneNumber = edtPhoneNumber.Text;
      Properties.Default.Message = edtMessage.Text;
      Properties.Default.Save();
    }

    private void cbModem_SelectedIndexChanged(object sender, EventArgs e)
    {
      btnSend.Enabled = false;
      TryToConnectToModem(modemLogic.GetPortNameByIndex(cbModem.SelectedIndex));
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