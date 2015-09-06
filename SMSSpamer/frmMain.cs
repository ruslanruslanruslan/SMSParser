using System;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Extensions;

namespace SMSSpamer
{
  public partial class frmMain : Form
  {
    public frmMain()
    {
      InitializeComponent();
      CheckForIllegalCrossThreadCalls = true;
      modemLogic.AddModemLog = AddModemLog;
      if ((DateTime.Now - Properties.Default.LastSMSSent).TotalDays >= 1)
      {
        Properties.Default.LastSMSSent = DateTime.Now;
        Properties.Default.SMSSentToday = 0;
        Properties.Default.Save();
      }
    }

    private ModemLogic modemLogic = new ModemLogic();
    private bool bStop = false;
    private bool bStopped = true;
    private object thislock = new object();

    private void AddLog(string msg, Color msgColor)
    {
      if (rtbLog.InvokeRequired)
        rtbLog.Invoke(new MethodInvoker(() => AddLog(msg, msgColor)));
      else
      {
        try
        {
          lock (thislock)
          {
            var start = rtbLog.Text.Length - 1;
            if (start < 0)
              start = 0;
            rtbLog.AppendText(DateTime.Now.ToShortTimeString() + " | " + msg + Environment.NewLine);
            rtbLog.Select(start, rtbLog.Text.Length - start + 1);
            rtbLog.SelectionColor = msgColor;
            rtbLog.SelectionStart = rtbLog.Text.Length;
            rtbLog.ScrollToCaret();
          }
        }
        catch (Exception ex)
        {
          MessageBox.Show(ex.Message, "Error adding log", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
      }
    }

    public void AddModemLog(string request, string responce)
    {
      if (rtbModemLog.InvokeRequired)
        rtbModemLog.Invoke(new MethodInvoker(() => AddModemLog(request, responce)));
      else
      {
        try
        {
          lock (thislock)
          {
            if (request != null)
              rtbModemLog.AppendText(DateTime.Now.ToShortTimeString() + " | " + " Request: " + request + Environment.NewLine + Environment.NewLine);
            if (responce != null)
              rtbModemLog.AppendText(DateTime.Now.ToShortTimeString() + " | " + " Responce: " + responce + Environment.NewLine + Environment.NewLine);
            rtbModemLog.ScrollToCaret();
          }
        }
        catch (Exception ex)
        {
          MessageBox.Show(ex.Message, "Error adding modem log", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
      }
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
        if (modemLogic.ConnectedPort != null)
        {
          AddLog("Device successfully connected", LogMessageColor.Success());
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
        foreach (var port in modemLogic.LoadPorts())
          cbModem.Items.Add(port);
      }
      catch (Exception ex)
      {
        AddLog(ex.Message, LogMessageColor.Error());
      }
      try
      {
        if (cbModem.Items.Count > 0)
          cbModem.SelectedIndex = 0;
        else
          AddLog("No avalable devices", LogMessageColor.Error());
      }
      catch (Exception ex)
      {
        AddLog(ex.Message, LogMessageColor.Error());
      }
    }

    private void frmMain_Load(object sender, EventArgs e)
    {
      var args = Environment.GetCommandLineArgs();
      btnSend.Enabled = false;
      cbModem.SelectedIndexChanged -= cbModem_SelectedIndexChanged;
      LoadPortNames();
      if (cbModem.Items.Count > 0)
        if (cbModem.Items.Contains(Properties.Default.ModemName))
          cbModem.SelectedIndex = cbModem.Items.IndexOf(Properties.Default.ModemName);

      edtPhoneNumber.Text = Properties.Default.PhoneNumber;
      edtMessage.Text = Properties.Default.Message;
      if (TryToConnectToModem(modemLogic.GetPortNameByIndex(cbModem.SelectedIndex)))
        btnSend.Enabled = true;
      cbModem.SelectedIndexChanged += cbModem_SelectedIndexChanged;
      if (args.Count() > 1)
      {
        cbModem.Enabled = false;
        edtPhoneNumber.Enabled = false;
        edtMessage.Enabled = false;
        btnSend.Enabled = false;
        btnSettings.Enabled = false;
        btnSendFromDB.Enabled = false;
        if (args[1] == "--send-sms")
        {
          Text += " 'Send single SMS mode'";
          AddLog("Send single SMS mode", LogMessageColor.Information());
          Task.Factory.StartNew(() =>
            {
              TryAutoSendMessage(args[2], args[3]);
            }
          );
        }
        else if (args[1] == "--send-sms-from-db")
        {
          Text += " 'Send SMS from DB mode'";
          AddLog("Send SMS from DB mode", LogMessageColor.Information());
          string server = "", login = "", password = "", database = "";
          var port = 3306;
          for (var i = 2; i < args.Count(); i += 2)
          {
            if (args[i] == "-server")
              server = args[i + 1];
            else if (args[i] == "-database")
              database = args[i + 1];
            else if (args[i] == "-login")
              login = args[i + 1];
            else if (args[i] == "-password")
              password = args[i + 1];
            else if (args[i] == "-port")
              port = Convert.ToInt32(args[i + 1]);
          }
          var db = new MySqlDB(login, password, server, port, database);
          try
          {
            var testConnection = db.mySqlConnection;
          }
          catch (Exception ex)
          {
            MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            Program.Exit(1);
          }
          Task.Factory.StartNew(() =>
            {
              SendMessageFromDatabase(db);
              db.Close();
            }
          );
        }
      }
      else
      {
        if (Properties.Default.MySqlServerAddress.Length == 0 ||
            Properties.Default.MySqlServerDatabase.Length == 0 ||
            Properties.Default.MySqlServerPassword.Length == 0 ||
            Properties.Default.MySqlServerPort == 0 ||
            Properties.Default.MySqlServerUsername.Length == 0)
        {
          var frm = new frmSettings();
          frm.ShowDialog();
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
        AddLog("Message '" + edtMessage.Text + "' successfully sent to '" + edtPhoneNumber.Text + "'", LogMessageColor.Success());
      else
        AddLog("Can't send message '" + edtMessage.Text + "' to '" + edtPhoneNumber.Text + "'", LogMessageColor.Error());
    }

    private bool SendMessage(string PhoneNo, string Message)
    {
      try
      {
        if (Properties.Default.SMSSentToday >= Properties.Default.DayLimitSMS)
        {
          AddLog("You reached day SMS Limit of " + Properties.Default.DayLimitSMS.ToString() + " SMS", LogMessageColor.Error());
          return false;
        }
        AddLog("Sending message '" + Message + "' to '" + PhoneNo + "'", LogMessageColor.Information());
        var error = modemLogic.SendMessage(PhoneNo, Message);
        if (error != null)
        {
          AddLog("Send message '" + Message + "' to '" + PhoneNo + "' failed with error: " + error, LogMessageColor.Error());
          bStop = true;
        }
        else
        {
          Properties.Default.SMSSentToday += 1;
          Properties.Default.Save();
        }
        return error == null;
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
        foreach (var port in modemLogic.Ports)
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

    private void SendMessageFromDatabase(MySqlDB db)
    {
      while (true)
      {
        if (bStop)
          return;
        try
        {
          var messages = db.GetMessagePacket();
          var Sent = 0;
          foreach (var message in messages)
          {
            if (bStop)
              return;
            try
            {
              if (SendMessage(message.number, message.message))
              {
                AddLog("Success", LogMessageColor.Success());
                try
                {
                  db.SetMessageSent(message.id);
                  Sent++;
                  AddLog("Mark [" + message.id + "] as sent", LogMessageColor.Information());
                }
                catch (Exception ex)
                {
                  AddLog("Can't mark [" + message.id + "] as sent: '" + ex.Message, LogMessageColor.Error());
                }
              }
            }
            catch (Exception ex)
            {
              AddLog("Can't send message '" + message.message + "' to '" + message.number + "': " + ex.Message, LogMessageColor.Error());
            }
            AddLog("Sleeping for " + Properties.Default.TimeoutSMS.ToString() + " sec", LogMessageColor.Information());
            for (var i = 0; i < Properties.Default.TimeoutSMS; i++)
            {
              if (bStop)
                return;
              System.Threading.Thread.Sleep(1000);
            }
          }
          if (Sent == 0)
          {
            AddLog("Nothing sent. Sleeping for " + Properties.Default.TimeoutBatch.ToString() + " sec", LogMessageColor.Error());
            for (var i = 0; i < Properties.Default.TimeoutBatch; i++)
            {
              if (bStop)
                return;
              System.Threading.Thread.Sleep(1000);
            }
          }
        }
        catch (Exception ex)
        {
          AddLog("Can't send messages '" + ex.Message, LogMessageColor.Error());
        }
      }
    }

    private void btnSettings_Click(object sender, EventArgs e)
    {
      var frm = new frmSettings();
      frm.ShowDialog();
    }

    private void btnSendFromDB_Click(object sender, EventArgs e)
    {
      if (bStopped)
      {
        var db = new MySqlDB(Properties.Default.MySqlServerUsername, Properties.Default.MySqlServerPassword, Properties.Default.MySqlServerAddress, Properties.Default.MySqlServerPort, Properties.Default.MySqlServerDatabase);
        Task.Factory.StartNew(() =>
          {
            bStopped = false;
            btnSendFromDB.SetPropertyThreadSafe(() => btnSendFromDB.Text, "Stop");
            SendMessageFromDatabase(db);
            db.Close();
            bStopped = true;
            bStop = false;
            btnSendFromDB.SetPropertyThreadSafe(() => btnSendFromDB.Text, "Send from DB");
            AddLog("Stopped", LogMessageColor.Information());
          }
        );
      }
      else
      {
        bStop = true;
        AddLog("Stopping", LogMessageColor.Information());
      }
    }

    private void btnSendCommand_Click(object sender, EventArgs e)
    {
      if (edtCommand.Text != String.Empty)
      {
        try
        {
          var data = modemLogic.ExecCommand(edtCommand.Text, Properties.Default.TimeoutCommand * 1000);
          AddModemLog(edtCommand.Text, data);
          edtCommand.Clear();
        }
        catch (Exception ex)
        {
          AddLog(ex.Message, LogMessageColor.Error());
        }
      }
    }

    private void edtCommand_KeyPress(object sender, KeyPressEventArgs e)
    {
      if (e.KeyChar == (char)Keys.Enter)
        btnSendCommand_Click(sender, e);
    }

    private void edtMessage_TextChanged(object sender, EventArgs e)
    {
      const int maxMessageLength = 70;
      var smsCount = (edtMessage.Text.Length - 1) / maxMessageLength + 1;
      lblMessageLength.Text = string.Format("{0}/{1} ({2})", edtMessage.Text.Length, smsCount == 0 ? maxMessageLength : maxMessageLength * smsCount, smsCount);
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
    public static Color Success()
    {
      return Color.LimeGreen;
    }
  }

  delegate void AddModemLogDelegate(string request, string responce);
}