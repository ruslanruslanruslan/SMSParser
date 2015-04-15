namespace SMSSpamer
{
  partial class frmMain
  {
    /// <summary>
    /// Требуется переменная конструктора.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Освободить все используемые ресурсы.
    /// </summary>
    /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
    protected override void Dispose(bool disposing)
    {
      if (disposing && (components != null))
      {
        components.Dispose();
      }
      if (modemLogic != null)
        modemLogic.Dispose();
      base.Dispose(disposing);
    }

    #region Код, автоматически созданный конструктором форм Windows

    /// <summary>
    /// Обязательный метод для поддержки конструктора - не изменяйте
    /// содержимое данного метода при помощи редактора кода.
    /// </summary>
    private void InitializeComponent()
    {
      this.panel1 = new System.Windows.Forms.Panel();
      this.btnSendFromDB = new System.Windows.Forms.Button();
      this.btnSettings = new System.Windows.Forms.Button();
      this.btnSend = new System.Windows.Forms.Button();
      this.edtMessage = new System.Windows.Forms.TextBox();
      this.edtPhoneNumber = new System.Windows.Forms.TextBox();
      this.cbModem = new System.Windows.Forms.ComboBox();
      this.label3 = new System.Windows.Forms.Label();
      this.label2 = new System.Windows.Forms.Label();
      this.label1 = new System.Windows.Forms.Label();
      this.splitter1 = new System.Windows.Forms.Splitter();
      this.panel2 = new System.Windows.Forms.Panel();
      this.rtbLog = new System.Windows.Forms.RichTextBox();
      this.panel3 = new System.Windows.Forms.Panel();
      this.panel4 = new System.Windows.Forms.Panel();
      this.rtbModemLog = new System.Windows.Forms.RichTextBox();
      this.edtCommand = new System.Windows.Forms.TextBox();
      this.btnSendCommand = new System.Windows.Forms.Button();
      this.splitter2 = new System.Windows.Forms.Splitter();
      this.panel1.SuspendLayout();
      this.panel2.SuspendLayout();
      this.panel3.SuspendLayout();
      this.panel4.SuspendLayout();
      this.SuspendLayout();
      // 
      // panel1
      // 
      this.panel1.Controls.Add(this.btnSendFromDB);
      this.panel1.Controls.Add(this.btnSettings);
      this.panel1.Controls.Add(this.btnSend);
      this.panel1.Controls.Add(this.edtMessage);
      this.panel1.Controls.Add(this.edtPhoneNumber);
      this.panel1.Controls.Add(this.cbModem);
      this.panel1.Controls.Add(this.label3);
      this.panel1.Controls.Add(this.label2);
      this.panel1.Controls.Add(this.label1);
      this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
      this.panel1.Location = new System.Drawing.Point(0, 0);
      this.panel1.Name = "panel1";
      this.panel1.Size = new System.Drawing.Size(319, 302);
      this.panel1.TabIndex = 1;
      // 
      // btnSendFromDB
      // 
      this.btnSendFromDB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.btnSendFromDB.Location = new System.Drawing.Point(10, 273);
      this.btnSendFromDB.Name = "btnSendFromDB";
      this.btnSendFromDB.Size = new System.Drawing.Size(105, 23);
      this.btnSendFromDB.TabIndex = 8;
      this.btnSendFromDB.Text = "Send from DB";
      this.btnSendFromDB.UseVisualStyleBackColor = true;
      this.btnSendFromDB.Click += new System.EventHandler(this.btnSendFromDB_Click);
      // 
      // btnSettings
      // 
      this.btnSettings.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.btnSettings.Location = new System.Drawing.Point(208, 273);
      this.btnSettings.Name = "btnSettings";
      this.btnSettings.Size = new System.Drawing.Size(105, 23);
      this.btnSettings.TabIndex = 7;
      this.btnSettings.Text = "Settings";
      this.btnSettings.UseVisualStyleBackColor = true;
      this.btnSettings.Click += new System.EventHandler(this.btnSettings_Click);
      // 
      // btnSend
      // 
      this.btnSend.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.btnSend.Location = new System.Drawing.Point(10, 231);
      this.btnSend.Name = "btnSend";
      this.btnSend.Size = new System.Drawing.Size(303, 23);
      this.btnSend.TabIndex = 6;
      this.btnSend.Text = "Send message";
      this.btnSend.UseVisualStyleBackColor = true;
      this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
      // 
      // edtMessage
      // 
      this.edtMessage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.edtMessage.Location = new System.Drawing.Point(10, 101);
      this.edtMessage.Multiline = true;
      this.edtMessage.Name = "edtMessage";
      this.edtMessage.ScrollBars = System.Windows.Forms.ScrollBars.Both;
      this.edtMessage.Size = new System.Drawing.Size(303, 124);
      this.edtMessage.TabIndex = 5;
      // 
      // edtPhoneNumber
      // 
      this.edtPhoneNumber.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.edtPhoneNumber.Location = new System.Drawing.Point(10, 62);
      this.edtPhoneNumber.Name = "edtPhoneNumber";
      this.edtPhoneNumber.Size = new System.Drawing.Size(303, 20);
      this.edtPhoneNumber.TabIndex = 4;
      // 
      // cbModem
      // 
      this.cbModem.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.cbModem.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cbModem.FormattingEnabled = true;
      this.cbModem.Location = new System.Drawing.Point(10, 19);
      this.cbModem.Name = "cbModem";
      this.cbModem.Size = new System.Drawing.Size(303, 21);
      this.cbModem.TabIndex = 3;
      this.cbModem.SelectedIndexChanged += new System.EventHandler(this.cbModem_SelectedIndexChanged);
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Location = new System.Drawing.Point(7, 85);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(50, 13);
      this.label3.TabIndex = 2;
      this.label3.Text = "Message";
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(7, 43);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(76, 13);
      this.label2.TabIndex = 1;
      this.label2.Text = "Phone number";
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(7, 3);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(42, 13);
      this.label1.TabIndex = 0;
      this.label1.Text = "Modem";
      // 
      // splitter1
      // 
      this.splitter1.Location = new System.Drawing.Point(319, 0);
      this.splitter1.Name = "splitter1";
      this.splitter1.Size = new System.Drawing.Size(3, 302);
      this.splitter1.TabIndex = 2;
      this.splitter1.TabStop = false;
      // 
      // panel2
      // 
      this.panel2.Controls.Add(this.splitter2);
      this.panel2.Controls.Add(this.rtbLog);
      this.panel2.Controls.Add(this.panel3);
      this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
      this.panel2.Location = new System.Drawing.Point(322, 0);
      this.panel2.Name = "panel2";
      this.panel2.Size = new System.Drawing.Size(375, 302);
      this.panel2.TabIndex = 3;
      // 
      // rtbLog
      // 
      this.rtbLog.Dock = System.Windows.Forms.DockStyle.Fill;
      this.rtbLog.Location = new System.Drawing.Point(0, 0);
      this.rtbLog.Name = "rtbLog";
      this.rtbLog.ReadOnly = true;
      this.rtbLog.Size = new System.Drawing.Size(375, 202);
      this.rtbLog.TabIndex = 0;
      this.rtbLog.Text = "";
      // 
      // panel3
      // 
      this.panel3.Controls.Add(this.rtbModemLog);
      this.panel3.Controls.Add(this.panel4);
      this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
      this.panel3.Location = new System.Drawing.Point(0, 202);
      this.panel3.Name = "panel3";
      this.panel3.Size = new System.Drawing.Size(375, 100);
      this.panel3.TabIndex = 1;
      // 
      // panel4
      // 
      this.panel4.Controls.Add(this.btnSendCommand);
      this.panel4.Controls.Add(this.edtCommand);
      this.panel4.Dock = System.Windows.Forms.DockStyle.Bottom;
      this.panel4.Location = new System.Drawing.Point(0, 65);
      this.panel4.Name = "panel4";
      this.panel4.Size = new System.Drawing.Size(375, 35);
      this.panel4.TabIndex = 0;
      // 
      // rtbModemLog
      // 
      this.rtbModemLog.BackColor = System.Drawing.SystemColors.ButtonFace;
      this.rtbModemLog.Dock = System.Windows.Forms.DockStyle.Fill;
      this.rtbModemLog.Location = new System.Drawing.Point(0, 0);
      this.rtbModemLog.Name = "rtbModemLog";
      this.rtbModemLog.ReadOnly = true;
      this.rtbModemLog.Size = new System.Drawing.Size(375, 65);
      this.rtbModemLog.TabIndex = 1;
      this.rtbModemLog.Text = "";
      // 
      // edtCommand
      // 
      this.edtCommand.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.edtCommand.Location = new System.Drawing.Point(3, 8);
      this.edtCommand.Name = "edtCommand";
      this.edtCommand.Size = new System.Drawing.Size(261, 20);
      this.edtCommand.TabIndex = 0;
      this.edtCommand.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.edtCommand_KeyPress);
      // 
      // btnSendCommand
      // 
      this.btnSendCommand.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.btnSendCommand.Location = new System.Drawing.Point(270, 6);
      this.btnSendCommand.Name = "btnSendCommand";
      this.btnSendCommand.Size = new System.Drawing.Size(102, 23);
      this.btnSendCommand.TabIndex = 1;
      this.btnSendCommand.Text = "Send command";
      this.btnSendCommand.UseVisualStyleBackColor = true;
      this.btnSendCommand.Click += new System.EventHandler(this.btnSendCommand_Click);
      // 
      // splitter2
      // 
      this.splitter2.Dock = System.Windows.Forms.DockStyle.Bottom;
      this.splitter2.Location = new System.Drawing.Point(0, 199);
      this.splitter2.Name = "splitter2";
      this.splitter2.Size = new System.Drawing.Size(375, 3);
      this.splitter2.TabIndex = 2;
      this.splitter2.TabStop = false;
      // 
      // frmMain
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(697, 302);
      this.Controls.Add(this.panel2);
      this.Controls.Add(this.splitter1);
      this.Controls.Add(this.panel1);
      this.Name = "frmMain";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "SMS Spamer";
      this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
      this.Load += new System.EventHandler(this.frmMain_Load);
      this.panel1.ResumeLayout(false);
      this.panel1.PerformLayout();
      this.panel2.ResumeLayout(false);
      this.panel3.ResumeLayout(false);
      this.panel4.ResumeLayout(false);
      this.panel4.PerformLayout();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Panel panel1;
    private System.Windows.Forms.Splitter splitter1;
    private System.Windows.Forms.Panel panel2;
    private System.Windows.Forms.Button btnSend;
    private System.Windows.Forms.TextBox edtMessage;
    private System.Windows.Forms.TextBox edtPhoneNumber;
    private System.Windows.Forms.ComboBox cbModem;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Button btnSettings;
    private System.Windows.Forms.Button btnSendFromDB;
    private System.Windows.Forms.Panel panel3;
    private System.Windows.Forms.RichTextBox rtbModemLog;
    private System.Windows.Forms.Panel panel4;
    private System.Windows.Forms.Button btnSendCommand;
    private System.Windows.Forms.TextBox edtCommand;
    private System.Windows.Forms.RichTextBox rtbLog;
    private System.Windows.Forms.Splitter splitter2;

  }
}

