using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    }

    private void btnSave_Click(object sender, EventArgs e)
    {
      bool bNeedRestart = false;
      if (Properties.Default.MySqlServerAddress != edtMySqlServerAddress.Text || Properties.Default.MySqlServerPort != Convert.ToInt32(edtMySqlServerPort.Text) ||
          Properties.Default.MySqlServerDatabase != edtMySqlServerDatabase.Text || Properties.Default.MySqlServerUsername != edtMySqlServerUsername.Text ||
          Properties.Default.MySqlServerPassword != edtMySqlServerPassword.Text)
      {
        bNeedRestart = true;
      }
      Properties.Default.MySqlServerAddress = edtMySqlServerAddress.Text;
      Properties.Default.MySqlServerPort = Convert.ToInt32(edtMySqlServerPort.Text);
      Properties.Default.MySqlServerDatabase = edtMySqlServerDatabase.Text;
      Properties.Default.MySqlServerUsername = edtMySqlServerUsername.Text;
      Properties.Default.MySqlServerPassword = edtMySqlServerPassword.Text;

      Properties.Default.Save();

      // Test MySql connection

      MySqlDB db = new MySqlDB(Properties.Default.MySqlServerUsername, Properties.Default.MySqlServerPassword, Properties.Default.MySqlServerAddress, Properties.Default.MySqlServerPort, Properties.Default.MySqlServerDatabase);
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
