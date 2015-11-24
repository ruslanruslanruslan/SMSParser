using System;
using System.Windows.Forms;

namespace SMSSpamer
{
  public partial class frmSettings : Form
  {
    public frmSettings()
    {
      InitializeComponent();
    }

    private void frmSettings_Load(object sender, EventArgs e)
    {
      edtMySqlServerAddress.Text = Properties.Default.MySqlServerAddress;
      edtMySqlServerPort.Text = Properties.Default.MySqlServerPort.ToString();
      edtMySqlServerDatabase.Text = Properties.Default.MySqlServerDatabase;
      edtMySqlServerUsername.Text = Properties.Default.MySqlServerUsername;
      edtMySqlServerPassword.Text = Properties.Default.MySqlServerPassword;
      edtTimeoutCommand.Text = Properties.Default.TimeoutCommand.ToString();
      edtTimeoutSMS.Text = Properties.Default.TimeoutSMS.ToString();
      edtTimeoutBatch.Text = Properties.Default.TimeoutBatch.ToString();
      edtSMSDayLimit.Text = Properties.Default.DayLimitSMS.ToString();
      lblLastSMSSent.Text = Properties.Default.LastSMSSent.ToShortDateString();
      lblSMSSent.Text = Properties.Default.SMSSentToday.ToString();
    }

    private void btnSave_Click(object sender, EventArgs e)
    {
      var bNeedRestart = false;
      if (Properties.Default.MySqlServerAddress != edtMySqlServerAddress.Text || Properties.Default.MySqlServerPort != Convert.ToInt32(edtMySqlServerPort.Text) ||
          Properties.Default.MySqlServerDatabase != edtMySqlServerDatabase.Text || Properties.Default.MySqlServerUsername != edtMySqlServerUsername.Text ||
          Properties.Default.MySqlServerPassword != edtMySqlServerPassword.Text)
        bNeedRestart = true;
      Properties.Default.MySqlServerAddress = edtMySqlServerAddress.Text;
      try
      {
        Properties.Default.MySqlServerPort = Convert.ToInt32(edtMySqlServerPort.Text);
      }
      catch
      {
        MessageBox.Show("Can't save port number. Incorrect number", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        return;
      }
      Properties.Default.MySqlServerDatabase = edtMySqlServerDatabase.Text;
      Properties.Default.MySqlServerUsername = edtMySqlServerUsername.Text;
      Properties.Default.MySqlServerPassword = edtMySqlServerPassword.Text;

      try
      {
        Properties.Default.TimeoutCommand = Convert.ToInt32(edtTimeoutCommand.Text);
      }
      catch
      {
        MessageBox.Show("Can't save command timeout. Incorrect number", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        return;
      }
      try
      {
        Properties.Default.TimeoutSMS = Convert.ToInt32(edtTimeoutSMS.Text);
      }
      catch
      {
        MessageBox.Show("Can't save timeout between SMS. Incorrect number", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        return;
      }
      try
      {
        Properties.Default.TimeoutBatch = Convert.ToInt32(edtTimeoutBatch.Text);
      }
      catch
      {
        MessageBox.Show("Can't save timeout between batches. Incorrect number", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        return;
      }
      try
      {
        Properties.Default.DayLimitSMS = Convert.ToInt32(edtSMSDayLimit.Text);
      }
      catch
      {
        MessageBox.Show("Can't save SMS day limit. Incorrect number", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        return;
      }

      Properties.Default.Save();

      // Test MySql connection

      var db = new MySqlDB(Properties.Default.MySqlServerUsername, Properties.Default.MySqlServerPassword, Properties.Default.MySqlServerAddress, Properties.Default.MySqlServerPort, Properties.Default.MySqlServerDatabase);
      try
      {
        var r = db.mySqlConnection;
      }
      catch (Exception)
      {
        MessageBox.Show("Невозможно установить соединение с базой данных", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        DialogResult = DialogResult.Abort;
        bNeedRestart = false;
      }
      db.Close();

      if (bNeedRestart)
      {
        MessageBox.Show("Для внесения изменения программа будет перезапущена", "Перезапуск", MessageBoxButtons.OK, MessageBoxIcon.Information);
        Application.Restart();
      }
    }
  }
}
