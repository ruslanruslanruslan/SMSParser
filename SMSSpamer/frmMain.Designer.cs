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
      base.Dispose(disposing);
    }

    #region Код, автоматически созданный конструктором форм Windows

    /// <summary>
    /// Обязательный метод для поддержки конструктора - не изменяйте
    /// содержимое данного метода при помощи редактора кода.
    /// </summary>
    private void InitializeComponent()
    {
      this.rtbLog = new System.Windows.Forms.RichTextBox();
      this.panel1 = new System.Windows.Forms.Panel();
      this.btnSend = new System.Windows.Forms.Button();
      this.edtMessage = new System.Windows.Forms.TextBox();
      this.edtPhoneNumber = new System.Windows.Forms.TextBox();
      this.cbModem = new System.Windows.Forms.ComboBox();
      this.label3 = new System.Windows.Forms.Label();
      this.label2 = new System.Windows.Forms.Label();
      this.label1 = new System.Windows.Forms.Label();
      this.splitter1 = new System.Windows.Forms.Splitter();
      this.panel2 = new System.Windows.Forms.Panel();
      this.panel1.SuspendLayout();
      this.panel2.SuspendLayout();
      this.SuspendLayout();
      // 
      // rtbLog
      // 
      this.rtbLog.Dock = System.Windows.Forms.DockStyle.Fill;
      this.rtbLog.Location = new System.Drawing.Point(0, 0);
      this.rtbLog.Name = "rtbLog";
      this.rtbLog.ReadOnly = true;
      this.rtbLog.Size = new System.Drawing.Size(375, 302);
      this.rtbLog.TabIndex = 0;
      this.rtbLog.Text = "";
      // 
      // panel1
      // 
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
      this.panel2.Controls.Add(this.rtbLog);
      this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
      this.panel2.Location = new System.Drawing.Point(322, 0);
      this.panel2.Name = "panel2";
      this.panel2.Size = new System.Drawing.Size(375, 302);
      this.panel2.TabIndex = 3;
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
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.RichTextBox rtbLog;
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

  }
}

