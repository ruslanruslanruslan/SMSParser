namespace SMSSpamer
{
  partial class frmSettings
  {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
      if (disposing && (components != null))
      {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.panel1 = new System.Windows.Forms.Panel();
      this.panel2 = new System.Windows.Forms.Panel();
      this.tabControl1 = new System.Windows.Forms.TabControl();
      this.tabMySql = new System.Windows.Forms.TabPage();
      this.label14 = new System.Windows.Forms.Label();
      this.edtMySqlServerPassword = new System.Windows.Forms.TextBox();
      this.edtMySqlServerUsername = new System.Windows.Forms.TextBox();
      this.edtMySqlServerDatabase = new System.Windows.Forms.TextBox();
      this.edtMySqlServerPort = new System.Windows.Forms.TextBox();
      this.edtMySqlServerAddress = new System.Windows.Forms.TextBox();
      this.label10 = new System.Windows.Forms.Label();
      this.label9 = new System.Windows.Forms.Label();
      this.label8 = new System.Windows.Forms.Label();
      this.label7 = new System.Windows.Forms.Label();
      this.label6 = new System.Windows.Forms.Label();
      this.btnCancel = new System.Windows.Forms.Button();
      this.btnSave = new System.Windows.Forms.Button();
      this.panel1.SuspendLayout();
      this.panel2.SuspendLayout();
      this.tabControl1.SuspendLayout();
      this.tabMySql.SuspendLayout();
      this.SuspendLayout();
      // 
      // panel1
      // 
      this.panel1.Controls.Add(this.tabControl1);
      this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.panel1.Location = new System.Drawing.Point(0, 0);
      this.panel1.Name = "panel1";
      this.panel1.Size = new System.Drawing.Size(563, 261);
      this.panel1.TabIndex = 0;
      // 
      // panel2
      // 
      this.panel2.Controls.Add(this.btnCancel);
      this.panel2.Controls.Add(this.btnSave);
      this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
      this.panel2.Location = new System.Drawing.Point(0, 226);
      this.panel2.Name = "panel2";
      this.panel2.Size = new System.Drawing.Size(563, 35);
      this.panel2.TabIndex = 1;
      // 
      // tabControl1
      // 
      this.tabControl1.Controls.Add(this.tabMySql);
      this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tabControl1.Location = new System.Drawing.Point(0, 0);
      this.tabControl1.Name = "tabControl1";
      this.tabControl1.SelectedIndex = 0;
      this.tabControl1.Size = new System.Drawing.Size(563, 261);
      this.tabControl1.TabIndex = 0;
      // 
      // tabMySql
      // 
      this.tabMySql.Controls.Add(this.label14);
      this.tabMySql.Controls.Add(this.edtMySqlServerPassword);
      this.tabMySql.Controls.Add(this.edtMySqlServerUsername);
      this.tabMySql.Controls.Add(this.edtMySqlServerDatabase);
      this.tabMySql.Controls.Add(this.edtMySqlServerPort);
      this.tabMySql.Controls.Add(this.edtMySqlServerAddress);
      this.tabMySql.Controls.Add(this.label10);
      this.tabMySql.Controls.Add(this.label9);
      this.tabMySql.Controls.Add(this.label8);
      this.tabMySql.Controls.Add(this.label7);
      this.tabMySql.Controls.Add(this.label6);
      this.tabMySql.Location = new System.Drawing.Point(4, 22);
      this.tabMySql.Name = "tabMySql";
      this.tabMySql.Padding = new System.Windows.Forms.Padding(3);
      this.tabMySql.Size = new System.Drawing.Size(555, 235);
      this.tabMySql.TabIndex = 0;
      this.tabMySql.Text = "MySql";
      this.tabMySql.UseVisualStyleBackColor = true;
      // 
      // label14
      // 
      this.label14.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      this.label14.Location = new System.Drawing.Point(12, 171);
      this.label14.Name = "label14";
      this.label14.Size = new System.Drawing.Size(535, 23);
      this.label14.TabIndex = 21;
      this.label14.Text = "Need program restart to apply database changes";
      this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // edtMySqlServerPassword
      // 
      this.edtMySqlServerPassword.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.edtMySqlServerPassword.Location = new System.Drawing.Point(66, 110);
      this.edtMySqlServerPassword.Name = "edtMySqlServerPassword";
      this.edtMySqlServerPassword.PasswordChar = '*';
      this.edtMySqlServerPassword.Size = new System.Drawing.Size(503, 20);
      this.edtMySqlServerPassword.TabIndex = 20;
      // 
      // edtMySqlServerUsername
      // 
      this.edtMySqlServerUsername.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.edtMySqlServerUsername.Location = new System.Drawing.Point(66, 84);
      this.edtMySqlServerUsername.Name = "edtMySqlServerUsername";
      this.edtMySqlServerUsername.Size = new System.Drawing.Size(481, 20);
      this.edtMySqlServerUsername.TabIndex = 19;
      // 
      // edtMySqlServerDatabase
      // 
      this.edtMySqlServerDatabase.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.edtMySqlServerDatabase.Location = new System.Drawing.Point(66, 58);
      this.edtMySqlServerDatabase.Name = "edtMySqlServerDatabase";
      this.edtMySqlServerDatabase.Size = new System.Drawing.Size(481, 20);
      this.edtMySqlServerDatabase.TabIndex = 18;
      // 
      // edtMySqlServerPort
      // 
      this.edtMySqlServerPort.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.edtMySqlServerPort.Location = new System.Drawing.Point(66, 32);
      this.edtMySqlServerPort.Name = "edtMySqlServerPort";
      this.edtMySqlServerPort.Size = new System.Drawing.Size(481, 20);
      this.edtMySqlServerPort.TabIndex = 17;
      // 
      // edtMySqlServerAddress
      // 
      this.edtMySqlServerAddress.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.edtMySqlServerAddress.Location = new System.Drawing.Point(66, 6);
      this.edtMySqlServerAddress.Name = "edtMySqlServerAddress";
      this.edtMySqlServerAddress.Size = new System.Drawing.Size(481, 20);
      this.edtMySqlServerAddress.TabIndex = 16;
      // 
      // label10
      // 
      this.label10.AutoSize = true;
      this.label10.Location = new System.Drawing.Point(12, 113);
      this.label10.Name = "label10";
      this.label10.Size = new System.Drawing.Size(53, 13);
      this.label10.TabIndex = 15;
      this.label10.Text = "Password";
      // 
      // label9
      // 
      this.label9.AutoSize = true;
      this.label9.Location = new System.Drawing.Point(12, 87);
      this.label9.Name = "label9";
      this.label9.Size = new System.Drawing.Size(29, 13);
      this.label9.TabIndex = 14;
      this.label9.Text = "User";
      // 
      // label8
      // 
      this.label8.AutoSize = true;
      this.label8.Location = new System.Drawing.Point(12, 61);
      this.label8.Name = "label8";
      this.label8.Size = new System.Drawing.Size(53, 13);
      this.label8.TabIndex = 13;
      this.label8.Text = "Database";
      // 
      // label7
      // 
      this.label7.AutoSize = true;
      this.label7.Location = new System.Drawing.Point(12, 35);
      this.label7.Name = "label7";
      this.label7.Size = new System.Drawing.Size(26, 13);
      this.label7.TabIndex = 12;
      this.label7.Text = "Port";
      // 
      // label6
      // 
      this.label6.AutoSize = true;
      this.label6.Location = new System.Drawing.Point(10, 9);
      this.label6.Name = "label6";
      this.label6.Size = new System.Drawing.Size(38, 13);
      this.label6.TabIndex = 11;
      this.label6.Text = "Server";
      // 
      // btnCancel
      // 
      this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.btnCancel.Location = new System.Drawing.Point(476, 5);
      this.btnCancel.Name = "btnCancel";
      this.btnCancel.Size = new System.Drawing.Size(75, 23);
      this.btnCancel.TabIndex = 3;
      this.btnCancel.Text = "Cancel";
      this.btnCancel.UseVisualStyleBackColor = true;
      // 
      // btnSave
      // 
      this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.btnSave.DialogResult = System.Windows.Forms.DialogResult.OK;
      this.btnSave.Location = new System.Drawing.Point(395, 5);
      this.btnSave.Name = "btnSave";
      this.btnSave.Size = new System.Drawing.Size(75, 23);
      this.btnSave.TabIndex = 2;
      this.btnSave.Text = "Save";
      this.btnSave.UseVisualStyleBackColor = true;
      this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
      // 
      // frmSettings
      // 
      this.AcceptButton = this.btnSave;
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.CancelButton = this.btnCancel;
      this.ClientSize = new System.Drawing.Size(563, 261);
      this.Controls.Add(this.panel2);
      this.Controls.Add(this.panel1);
      this.Name = "frmSettings";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "Settings";
      this.Load += new System.EventHandler(this.frmSettings_Load);
      this.panel1.ResumeLayout(false);
      this.panel2.ResumeLayout(false);
      this.tabControl1.ResumeLayout(false);
      this.tabMySql.ResumeLayout(false);
      this.tabMySql.PerformLayout();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Panel panel1;
    private System.Windows.Forms.Panel panel2;
    private System.Windows.Forms.TabControl tabControl1;
    private System.Windows.Forms.TabPage tabMySql;
    private System.Windows.Forms.Label label14;
    private System.Windows.Forms.TextBox edtMySqlServerPassword;
    private System.Windows.Forms.TextBox edtMySqlServerUsername;
    private System.Windows.Forms.TextBox edtMySqlServerDatabase;
    private System.Windows.Forms.TextBox edtMySqlServerPort;
    private System.Windows.Forms.TextBox edtMySqlServerAddress;
    private System.Windows.Forms.Label label10;
    private System.Windows.Forms.Label label9;
    private System.Windows.Forms.Label label8;
    private System.Windows.Forms.Label label7;
    private System.Windows.Forms.Label label6;
    private System.Windows.Forms.Button btnCancel;
    private System.Windows.Forms.Button btnSave;
  }
}