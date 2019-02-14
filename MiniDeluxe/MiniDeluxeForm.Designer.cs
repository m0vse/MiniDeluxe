namespace MiniDeluxe
{
    partial class MiniDeluxeForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MiniDeluxeForm));
            this.btnSave = new System.Windows.Forms.Button();
            this.lblStatus = new System.Windows.Forms.Label();
            this.cbLocalOnly = new System.Windows.Forms.CheckBox();
            this.btnAbout = new System.Windows.Forms.Button();
            this.btnCheckForUpdate = new System.Windows.Forms.Button();
            this.Tabs = new System.Windows.Forms.TabControl();
            this.HRDPage = new System.Windows.Forms.TabPage();
            this.tbLogPort = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbPort = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.PowerSDRPage = new System.Windows.Forms.TabPage();
            this.gbPSDR = new System.Windows.Forms.GroupBox();
            this.lbPort = new System.Windows.Forms.Label();
            this.cbSerialport = new System.Windows.Forms.ComboBox();
            this.gbPSDR2 = new System.Windows.Forms.GroupBox();
            this.tbLow = new System.Windows.Forms.TextBox();
            this.tbHigh = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.cbPSDR = new System.Windows.Forms.CheckBox();
            this.SmartSDRPage = new System.Windows.Forms.TabPage();
            this.gbSSDR = new System.Windows.Forms.GroupBox();
            this.SSDRsmeter = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.SSDRmode = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.SSDRfreq = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.SSDRserial = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.SSDRname = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.SSDRip = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.SSDRmodel = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cbSSDR = new System.Windows.Forms.CheckBox();
            this.DDUtilPage = new System.Windows.Forms.TabPage();
            this.gbRIOX = new System.Windows.Forms.GroupBox();
            this.btnRIOXTest = new System.Windows.Forms.Button();
            this.txtRIOXIP = new System.Windows.Forms.TextBox();
            this.txtRIOXport = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.cbRIOX = new System.Windows.Forms.CheckBox();
            this.N1MMPage = new System.Windows.Forms.TabPage();
            this.cbN1MM = new System.Windows.Forms.CheckBox();
            this.gbN1MM = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.radioPort = new System.Windows.Forms.TextBox();
            this.Log4OM = new System.Windows.Forms.TabPage();
            this.cbLog4OM = new System.Windows.Forms.CheckBox();
            this.gbLog4OM = new System.Windows.Forms.GroupBox();
            this.label21 = new System.Windows.Forms.Label();
            this.log4omDBPass = new System.Windows.Forms.TextBox();
            this.label22 = new System.Windows.Forms.Label();
            this.log4omDBUser = new System.Windows.Forms.TextBox();
            this.label20 = new System.Windows.Forms.Label();
            this.log4omDBName = new System.Windows.Forms.TextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.log4omDBServer = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.log4omService = new System.Windows.Forms.TextBox();
            this.QRZcom = new System.Windows.Forms.TabPage();
            this.cbQRZcom = new System.Windows.Forms.CheckBox();
            this.gbQRZcom = new System.Windows.Forms.GroupBox();
            this.QRZPassword = new System.Windows.Forms.TextBox();
            this.QRZUsername = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.QRZdburl = new System.Windows.Forms.TextBox();
            this.SaveClose = new System.Windows.Forms.Button();
            this.Tabs.SuspendLayout();
            this.HRDPage.SuspendLayout();
            this.PowerSDRPage.SuspendLayout();
            this.gbPSDR.SuspendLayout();
            this.gbPSDR2.SuspendLayout();
            this.SmartSDRPage.SuspendLayout();
            this.gbSSDR.SuspendLayout();
            this.DDUtilPage.SuspendLayout();
            this.gbRIOX.SuspendLayout();
            this.N1MMPage.SuspendLayout();
            this.gbN1MM.SuspendLayout();
            this.Log4OM.SuspendLayout();
            this.gbLog4OM.SuspendLayout();
            this.QRZcom.SuspendLayout();
            this.gbQRZcom.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnSave
            // 
            this.btnSave.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnSave.Location = new System.Drawing.Point(218, 410);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(64, 23);
            this.btnSave.TabIndex = 4;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(15, 251);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(47, 13);
            this.lblStatus.TabIndex = 6;
            this.lblStatus.Text = "lblStatus";
            // 
            // cbLocalOnly
            // 
            this.cbLocalOnly.AutoSize = true;
            this.cbLocalOnly.Checked = true;
            this.cbLocalOnly.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbLocalOnly.Location = new System.Drawing.Point(18, 215);
            this.cbLocalOnly.Name = "cbLocalOnly";
            this.cbLocalOnly.Size = new System.Drawing.Size(135, 17);
            this.cbLocalOnly.TabIndex = 13;
            this.cbLocalOnly.Text = "Local connections only";
            this.cbLocalOnly.UseVisualStyleBackColor = true;
            // 
            // btnAbout
            // 
            this.btnAbout.Location = new System.Drawing.Point(135, 246);
            this.btnAbout.Name = "btnAbout";
            this.btnAbout.Size = new System.Drawing.Size(44, 23);
            this.btnAbout.TabIndex = 14;
            this.btnAbout.Text = "About";
            this.btnAbout.UseVisualStyleBackColor = true;
            this.btnAbout.Click += new System.EventHandler(this.btnAbout_Click);
            // 
            // btnCheckForUpdate
            // 
            this.btnCheckForUpdate.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCheckForUpdate.Enabled = false;
            this.btnCheckForUpdate.Location = new System.Drawing.Point(135, 275);
            this.btnCheckForUpdate.Name = "btnCheckForUpdate";
            this.btnCheckForUpdate.Size = new System.Drawing.Size(113, 23);
            this.btnCheckForUpdate.TabIndex = 15;
            this.btnCheckForUpdate.Text = "Check for Update";
            this.btnCheckForUpdate.UseVisualStyleBackColor = true;
            this.btnCheckForUpdate.Click += new System.EventHandler(this.btnCheckForUpdate_Click);
            // 
            // Tabs
            // 
            this.Tabs.Controls.Add(this.HRDPage);
            this.Tabs.Controls.Add(this.PowerSDRPage);
            this.Tabs.Controls.Add(this.SmartSDRPage);
            this.Tabs.Controls.Add(this.DDUtilPage);
            this.Tabs.Controls.Add(this.N1MMPage);
            this.Tabs.Controls.Add(this.Log4OM);
            this.Tabs.Controls.Add(this.QRZcom);
            this.Tabs.Location = new System.Drawing.Point(6, 1);
            this.Tabs.Name = "Tabs";
            this.Tabs.SelectedIndex = 0;
            this.Tabs.Size = new System.Drawing.Size(431, 194);
            this.Tabs.TabIndex = 11;
            // 
            // HRDPage
            // 
            this.HRDPage.Controls.Add(this.tbLogPort);
            this.HRDPage.Controls.Add(this.label2);
            this.HRDPage.Controls.Add(this.tbPort);
            this.HRDPage.Controls.Add(this.label8);
            this.HRDPage.Location = new System.Drawing.Point(4, 22);
            this.HRDPage.Name = "HRDPage";
            this.HRDPage.Padding = new System.Windows.Forms.Padding(3);
            this.HRDPage.Size = new System.Drawing.Size(423, 168);
            this.HRDPage.TabIndex = 4;
            this.HRDPage.Text = "HRD";
            this.HRDPage.UseVisualStyleBackColor = true;
            // 
            // tbLogPort
            // 
            this.tbLogPort.Location = new System.Drawing.Point(113, 55);
            this.tbLogPort.Name = "tbLogPort";
            this.tbLogPort.Size = new System.Drawing.Size(100, 20);
            this.tbLogPort.TabIndex = 19;
            this.tbLogPort.Text = "7825";
            this.tbLogPort.TextChanged += new System.EventHandler(this.textBox1_TextChanged_1);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(33, 55);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(74, 13);
            this.label2.TabIndex = 18;
            this.label2.Text = "Logbook Port:";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // tbPort
            // 
            this.tbPort.Location = new System.Drawing.Point(113, 25);
            this.tbPort.Name = "tbPort";
            this.tbPort.Size = new System.Drawing.Size(100, 20);
            this.tbPort.TabIndex = 17;
            this.tbPort.Text = "7809";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(17, 25);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(90, 13);
            this.label8.TabIndex = 16;
            this.label8.Text = "HRD Server Port:";
            // 
            // PowerSDRPage
            // 
            this.PowerSDRPage.Controls.Add(this.gbPSDR);
            this.PowerSDRPage.Controls.Add(this.cbPSDR);
            this.PowerSDRPage.Location = new System.Drawing.Point(4, 22);
            this.PowerSDRPage.Name = "PowerSDRPage";
            this.PowerSDRPage.Padding = new System.Windows.Forms.Padding(3);
            this.PowerSDRPage.Size = new System.Drawing.Size(423, 168);
            this.PowerSDRPage.TabIndex = 0;
            this.PowerSDRPage.Text = "PowerSDR";
            this.PowerSDRPage.UseVisualStyleBackColor = true;
            // 
            // gbPSDR
            // 
            this.gbPSDR.Controls.Add(this.lbPort);
            this.gbPSDR.Controls.Add(this.cbSerialport);
            this.gbPSDR.Controls.Add(this.gbPSDR2);
            this.gbPSDR.Location = new System.Drawing.Point(8, 27);
            this.gbPSDR.Name = "gbPSDR";
            this.gbPSDR.Size = new System.Drawing.Size(366, 135);
            this.gbPSDR.TabIndex = 18;
            this.gbPSDR.TabStop = false;
            // 
            // lbPort
            // 
            this.lbPort.AutoSize = true;
            this.lbPort.Location = new System.Drawing.Point(6, 16);
            this.lbPort.Name = "lbPort";
            this.lbPort.Size = new System.Drawing.Size(114, 13);
            this.lbPort.TabIndex = 13;
            this.lbPort.Text = "PowerSDR Serial Port:";
            // 
            // cbSerialport
            // 
            this.cbSerialport.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSerialport.FormattingEnabled = true;
            this.cbSerialport.Location = new System.Drawing.Point(120, 13);
            this.cbSerialport.Name = "cbSerialport";
            this.cbSerialport.Size = new System.Drawing.Size(100, 21);
            this.cbSerialport.TabIndex = 12;
            this.cbSerialport.SelectedIndexChanged += new System.EventHandler(this.cbSerialport_SelectedIndexChanged);
            // 
            // gbPSDR2
            // 
            this.gbPSDR2.Controls.Add(this.tbLow);
            this.gbPSDR2.Controls.Add(this.tbHigh);
            this.gbPSDR2.Controls.Add(this.label3);
            this.gbPSDR2.Controls.Add(this.label4);
            this.gbPSDR2.Location = new System.Drawing.Point(9, 32);
            this.gbPSDR2.Name = "gbPSDR2";
            this.gbPSDR2.Size = new System.Drawing.Size(220, 63);
            this.gbPSDR2.TabIndex = 16;
            this.gbPSDR2.TabStop = false;
            this.gbPSDR2.Text = "Update Intervals (msec)";
            // 
            // tbLow
            // 
            this.tbLow.Location = new System.Drawing.Point(100, 38);
            this.tbLow.Name = "tbLow";
            this.tbLow.Size = new System.Drawing.Size(51, 20);
            this.tbLow.TabIndex = 10;
            this.tbLow.Text = "5000";
            // 
            // tbHigh
            // 
            this.tbHigh.Location = new System.Drawing.Point(100, 15);
            this.tbHigh.Name = "tbHigh";
            this.tbHigh.Size = new System.Drawing.Size(51, 20);
            this.tbHigh.TabIndex = 9;
            this.tbHigh.Text = "1000";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(28, 18);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(66, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "High Priority:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(30, 41);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(64, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Low Priority:";
            // 
            // cbPSDR
            // 
            this.cbPSDR.AutoSize = true;
            this.cbPSDR.Checked = true;
            this.cbPSDR.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbPSDR.Location = new System.Drawing.Point(6, 4);
            this.cbPSDR.Name = "cbPSDR";
            this.cbPSDR.Size = new System.Drawing.Size(65, 17);
            this.cbPSDR.TabIndex = 17;
            this.cbPSDR.Text = "Enabled";
            this.cbPSDR.UseVisualStyleBackColor = true;
            this.cbPSDR.CheckedChanged += new System.EventHandler(this.cbPSDR_CheckedChanged);
            // 
            // SmartSDRPage
            // 
            this.SmartSDRPage.Controls.Add(this.gbSSDR);
            this.SmartSDRPage.Controls.Add(this.cbSSDR);
            this.SmartSDRPage.Location = new System.Drawing.Point(4, 22);
            this.SmartSDRPage.Name = "SmartSDRPage";
            this.SmartSDRPage.Padding = new System.Windows.Forms.Padding(3);
            this.SmartSDRPage.Size = new System.Drawing.Size(423, 168);
            this.SmartSDRPage.TabIndex = 3;
            this.SmartSDRPage.Text = "SmartSDR";
            this.SmartSDRPage.UseVisualStyleBackColor = true;
            // 
            // gbSSDR
            // 
            this.gbSSDR.Controls.Add(this.SSDRsmeter);
            this.gbSSDR.Controls.Add(this.label14);
            this.gbSSDR.Controls.Add(this.SSDRmode);
            this.gbSSDR.Controls.Add(this.label13);
            this.gbSSDR.Controls.Add(this.SSDRfreq);
            this.gbSSDR.Controls.Add(this.label12);
            this.gbSSDR.Controls.Add(this.SSDRserial);
            this.gbSSDR.Controls.Add(this.label10);
            this.gbSSDR.Controls.Add(this.SSDRname);
            this.gbSSDR.Controls.Add(this.label11);
            this.gbSSDR.Controls.Add(this.SSDRip);
            this.gbSSDR.Controls.Add(this.label9);
            this.gbSSDR.Controls.Add(this.SSDRmodel);
            this.gbSSDR.Controls.Add(this.label1);
            this.gbSSDR.Location = new System.Drawing.Point(8, 29);
            this.gbSSDR.Name = "gbSSDR";
            this.gbSSDR.Size = new System.Drawing.Size(376, 133);
            this.gbSSDR.TabIndex = 19;
            this.gbSSDR.TabStop = false;
            // 
            // SSDRsmeter
            // 
            this.SSDRsmeter.Location = new System.Drawing.Point(72, 96);
            this.SSDRsmeter.Name = "SSDRsmeter";
            this.SSDRsmeter.ReadOnly = true;
            this.SSDRsmeter.Size = new System.Drawing.Size(91, 20);
            this.SSDRsmeter.TabIndex = 18;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(19, 96);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(44, 13);
            this.label14.TabIndex = 17;
            this.label14.Text = "S Meter";
            // 
            // SSDRmode
            // 
            this.SSDRmode.Location = new System.Drawing.Point(222, 70);
            this.SSDRmode.Name = "SSDRmode";
            this.SSDRmode.ReadOnly = true;
            this.SSDRmode.Size = new System.Drawing.Size(112, 20);
            this.SSDRmode.TabIndex = 11;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(183, 73);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(34, 13);
            this.label13.TabIndex = 10;
            this.label13.Text = "Mode";
            // 
            // SSDRfreq
            // 
            this.SSDRfreq.Location = new System.Drawing.Point(72, 70);
            this.SSDRfreq.Name = "SSDRfreq";
            this.SSDRfreq.ReadOnly = true;
            this.SSDRfreq.Size = new System.Drawing.Size(91, 20);
            this.SSDRfreq.TabIndex = 9;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(9, 70);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(57, 13);
            this.label12.TabIndex = 8;
            this.label12.Text = "Frequency";
            // 
            // SSDRserial
            // 
            this.SSDRserial.Location = new System.Drawing.Point(222, 18);
            this.SSDRserial.Name = "SSDRserial";
            this.SSDRserial.ReadOnly = true;
            this.SSDRserial.Size = new System.Drawing.Size(136, 20);
            this.SSDRserial.TabIndex = 7;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(183, 21);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(33, 13);
            this.label10.TabIndex = 6;
            this.label10.Text = "Serial";
            // 
            // SSDRname
            // 
            this.SSDRname.Location = new System.Drawing.Point(50, 18);
            this.SSDRname.Name = "SSDRname";
            this.SSDRname.ReadOnly = true;
            this.SSDRname.Size = new System.Drawing.Size(113, 20);
            this.SSDRname.TabIndex = 5;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(9, 21);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(35, 13);
            this.label11.TabIndex = 4;
            this.label11.Text = "Name";
            // 
            // SSDRip
            // 
            this.SSDRip.Location = new System.Drawing.Point(222, 44);
            this.SSDRip.Name = "SSDRip";
            this.SSDRip.ReadOnly = true;
            this.SSDRip.Size = new System.Drawing.Size(136, 20);
            this.SSDRip.TabIndex = 3;
            this.SSDRip.TextChanged += new System.EventHandler(this.textBox1_TextChanged_2);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(174, 47);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(42, 13);
            this.label9.TabIndex = 2;
            this.label9.Text = "IP Addr";
            this.label9.Click += new System.EventHandler(this.label9_Click);
            // 
            // SSDRmodel
            // 
            this.SSDRmodel.Location = new System.Drawing.Point(51, 44);
            this.SSDRmodel.Name = "SSDRmodel";
            this.SSDRmodel.ReadOnly = true;
            this.SSDRmodel.Size = new System.Drawing.Size(112, 20);
            this.SSDRmodel.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 47);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(36, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Model";
            // 
            // cbSSDR
            // 
            this.cbSSDR.AutoSize = true;
            this.cbSSDR.Checked = true;
            this.cbSSDR.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbSSDR.Location = new System.Drawing.Point(6, 6);
            this.cbSSDR.Name = "cbSSDR";
            this.cbSSDR.Size = new System.Drawing.Size(65, 17);
            this.cbSSDR.TabIndex = 18;
            this.cbSSDR.Text = "Enabled";
            this.cbSSDR.UseVisualStyleBackColor = true;
            this.cbSSDR.CheckedChanged += new System.EventHandler(this.cbSmart_CheckedChanged);
            // 
            // DDUtilPage
            // 
            this.DDUtilPage.Controls.Add(this.gbRIOX);
            this.DDUtilPage.Controls.Add(this.cbRIOX);
            this.DDUtilPage.Location = new System.Drawing.Point(4, 22);
            this.DDUtilPage.Name = "DDUtilPage";
            this.DDUtilPage.Padding = new System.Windows.Forms.Padding(3);
            this.DDUtilPage.Size = new System.Drawing.Size(423, 168);
            this.DDUtilPage.TabIndex = 1;
            this.DDUtilPage.Text = "RIOX/DDUtil";
            this.DDUtilPage.UseVisualStyleBackColor = true;
            // 
            // gbRIOX
            // 
            this.gbRIOX.Controls.Add(this.btnRIOXTest);
            this.gbRIOX.Controls.Add(this.txtRIOXIP);
            this.gbRIOX.Controls.Add(this.txtRIOXport);
            this.gbRIOX.Controls.Add(this.label5);
            this.gbRIOX.Controls.Add(this.label6);
            this.gbRIOX.Location = new System.Drawing.Point(6, 29);
            this.gbRIOX.Name = "gbRIOX";
            this.gbRIOX.Size = new System.Drawing.Size(378, 133);
            this.gbRIOX.TabIndex = 20;
            this.gbRIOX.TabStop = false;
            // 
            // btnRIOXTest
            // 
            this.btnRIOXTest.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnRIOXTest.Location = new System.Drawing.Point(225, 67);
            this.btnRIOXTest.Name = "btnRIOXTest";
            this.btnRIOXTest.Size = new System.Drawing.Size(75, 23);
            this.btnRIOXTest.TabIndex = 4;
            this.btnRIOXTest.Text = "Test";
            this.btnRIOXTest.UseVisualStyleBackColor = true;
            this.btnRIOXTest.Click += new System.EventHandler(this.btnRIOXTest_Click);
            // 
            // txtRIOXIP
            // 
            this.txtRIOXIP.Location = new System.Drawing.Point(99, 19);
            this.txtRIOXIP.Name = "txtRIOXIP";
            this.txtRIOXIP.Size = new System.Drawing.Size(100, 20);
            this.txtRIOXIP.TabIndex = 1;
            this.txtRIOXIP.Text = "localhost";
            // 
            // txtRIOXport
            // 
            this.txtRIOXport.Location = new System.Drawing.Point(99, 46);
            this.txtRIOXport.Name = "txtRIOXport";
            this.txtRIOXport.Size = new System.Drawing.Size(100, 20);
            this.txtRIOXport.TabIndex = 3;
            this.txtRIOXport.Text = "1234";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(8, 22);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(83, 13);
            this.label5.TabIndex = 0;
            this.label5.Text = "RIOX Server IP:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(62, 48);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(29, 13);
            this.label6.TabIndex = 2;
            this.label6.Text = "Port:";
            // 
            // cbRIOX
            // 
            this.cbRIOX.AutoSize = true;
            this.cbRIOX.Checked = true;
            this.cbRIOX.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbRIOX.Location = new System.Drawing.Point(6, 6);
            this.cbRIOX.Name = "cbRIOX";
            this.cbRIOX.Size = new System.Drawing.Size(65, 17);
            this.cbRIOX.TabIndex = 19;
            this.cbRIOX.Text = "Enabled";
            this.cbRIOX.UseVisualStyleBackColor = true;
            this.cbRIOX.CheckedChanged += new System.EventHandler(this.cbRIOX_CheckedChanged);
            // 
            // N1MMPage
            // 
            this.N1MMPage.Controls.Add(this.cbN1MM);
            this.N1MMPage.Controls.Add(this.gbN1MM);
            this.N1MMPage.Location = new System.Drawing.Point(4, 22);
            this.N1MMPage.Name = "N1MMPage";
            this.N1MMPage.Padding = new System.Windows.Forms.Padding(3);
            this.N1MMPage.Size = new System.Drawing.Size(423, 168);
            this.N1MMPage.TabIndex = 2;
            this.N1MMPage.Text = "N1MM";
            this.N1MMPage.UseVisualStyleBackColor = true;
            // 
            // cbN1MM
            // 
            this.cbN1MM.AutoSize = true;
            this.cbN1MM.Checked = true;
            this.cbN1MM.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbN1MM.Location = new System.Drawing.Point(6, 6);
            this.cbN1MM.Name = "cbN1MM";
            this.cbN1MM.Size = new System.Drawing.Size(65, 17);
            this.cbN1MM.TabIndex = 20;
            this.cbN1MM.Text = "Enabled";
            this.cbN1MM.UseVisualStyleBackColor = true;
            this.cbN1MM.CheckedChanged += new System.EventHandler(this.cbN1MM_CheckedChanged);
            // 
            // gbN1MM
            // 
            this.gbN1MM.Controls.Add(this.label7);
            this.gbN1MM.Controls.Add(this.radioPort);
            this.gbN1MM.Location = new System.Drawing.Point(6, 30);
            this.gbN1MM.Name = "gbN1MM";
            this.gbN1MM.Size = new System.Drawing.Size(376, 132);
            this.gbN1MM.TabIndex = 18;
            this.gbN1MM.TabStop = false;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 27);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(108, 13);
            this.label7.TabIndex = 16;
            this.label7.Text = "Radio Broadcast Port";
            this.label7.Click += new System.EventHandler(this.label7_Click);
            // 
            // radioPort
            // 
            this.radioPort.Location = new System.Drawing.Point(122, 27);
            this.radioPort.Name = "radioPort";
            this.radioPort.Size = new System.Drawing.Size(82, 20);
            this.radioPort.TabIndex = 17;
            this.radioPort.Text = "12060";
            this.radioPort.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // Log4OM
            // 
            this.Log4OM.Controls.Add(this.cbLog4OM);
            this.Log4OM.Controls.Add(this.gbLog4OM);
            this.Log4OM.Location = new System.Drawing.Point(4, 22);
            this.Log4OM.Name = "Log4OM";
            this.Log4OM.Padding = new System.Windows.Forms.Padding(3);
            this.Log4OM.Size = new System.Drawing.Size(423, 168);
            this.Log4OM.TabIndex = 5;
            this.Log4OM.Text = "Log4OM";
            this.Log4OM.UseVisualStyleBackColor = true;
            // 
            // cbLog4OM
            // 
            this.cbLog4OM.AutoSize = true;
            this.cbLog4OM.Checked = true;
            this.cbLog4OM.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbLog4OM.Location = new System.Drawing.Point(6, 6);
            this.cbLog4OM.Name = "cbLog4OM";
            this.cbLog4OM.Size = new System.Drawing.Size(65, 17);
            this.cbLog4OM.TabIndex = 22;
            this.cbLog4OM.Text = "Enabled";
            this.cbLog4OM.UseVisualStyleBackColor = true;
            this.cbLog4OM.CheckedChanged += new System.EventHandler(this.cbLog4OM_CheckedChanged);
            // 
            // gbLog4OM
            // 
            this.gbLog4OM.Controls.Add(this.label21);
            this.gbLog4OM.Controls.Add(this.log4omDBPass);
            this.gbLog4OM.Controls.Add(this.label22);
            this.gbLog4OM.Controls.Add(this.log4omDBUser);
            this.gbLog4OM.Controls.Add(this.label20);
            this.gbLog4OM.Controls.Add(this.log4omDBName);
            this.gbLog4OM.Controls.Add(this.label19);
            this.gbLog4OM.Controls.Add(this.log4omDBServer);
            this.gbLog4OM.Controls.Add(this.label15);
            this.gbLog4OM.Controls.Add(this.log4omService);
            this.gbLog4OM.Location = new System.Drawing.Point(6, 30);
            this.gbLog4OM.Name = "gbLog4OM";
            this.gbLog4OM.Size = new System.Drawing.Size(376, 132);
            this.gbLog4OM.TabIndex = 21;
            this.gbLog4OM.TabStop = false;
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(199, 100);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(46, 13);
            this.label21.TabIndex = 24;
            this.label21.Text = "DB Pwd";
            // 
            // log4omDBPass
            // 
            this.log4omDBPass.Location = new System.Drawing.Point(258, 97);
            this.log4omDBPass.Name = "log4omDBPass";
            this.log4omDBPass.Size = new System.Drawing.Size(103, 20);
            this.log4omDBPass.TabIndex = 25;
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(26, 100);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(47, 13);
            this.label22.TabIndex = 22;
            this.label22.Text = "DB User";
            // 
            // log4omDBUser
            // 
            this.log4omDBUser.Location = new System.Drawing.Point(88, 97);
            this.log4omDBUser.Name = "log4omDBUser";
            this.log4omDBUser.Size = new System.Drawing.Size(95, 20);
            this.log4omDBUser.TabIndex = 23;
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(199, 71);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(53, 13);
            this.label20.TabIndex = 20;
            this.label20.Text = "DB Name";
            // 
            // log4omDBName
            // 
            this.log4omDBName.Location = new System.Drawing.Point(258, 68);
            this.log4omDBName.Name = "log4omDBName";
            this.log4omDBName.Size = new System.Drawing.Size(103, 20);
            this.log4omDBName.TabIndex = 21;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(26, 71);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(56, 13);
            this.label19.TabIndex = 18;
            this.label19.Text = "DB Server";
            // 
            // log4omDBServer
            // 
            this.log4omDBServer.Location = new System.Drawing.Point(88, 68);
            this.log4omDBServer.Name = "log4omDBServer";
            this.log4omDBServer.Size = new System.Drawing.Size(95, 20);
            this.log4omDBServer.TabIndex = 19;
            this.log4omDBServer.Text = "localhost";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(6, 30);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(76, 13);
            this.label15.TabIndex = 16;
            this.label15.Text = "WCF Interface";
            // 
            // log4omService
            // 
            this.log4omService.Location = new System.Drawing.Point(88, 27);
            this.log4omService.Name = "log4omService";
            this.log4omService.Size = new System.Drawing.Size(273, 20);
            this.log4omService.TabIndex = 17;
            this.log4omService.Text = "http://localhost:8080/log4om";
            // 
            // QRZcom
            // 
            this.QRZcom.Controls.Add(this.cbQRZcom);
            this.QRZcom.Controls.Add(this.gbQRZcom);
            this.QRZcom.Location = new System.Drawing.Point(4, 22);
            this.QRZcom.Name = "QRZcom";
            this.QRZcom.Padding = new System.Windows.Forms.Padding(3);
            this.QRZcom.Size = new System.Drawing.Size(423, 168);
            this.QRZcom.TabIndex = 6;
            this.QRZcom.Text = "QRZ.com";
            this.QRZcom.UseVisualStyleBackColor = true;
            // 
            // cbQRZcom
            // 
            this.cbQRZcom.AutoSize = true;
            this.cbQRZcom.Checked = true;
            this.cbQRZcom.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbQRZcom.Location = new System.Drawing.Point(6, 6);
            this.cbQRZcom.Name = "cbQRZcom";
            this.cbQRZcom.Size = new System.Drawing.Size(65, 17);
            this.cbQRZcom.TabIndex = 24;
            this.cbQRZcom.Text = "Enabled";
            this.cbQRZcom.UseVisualStyleBackColor = true;
            this.cbQRZcom.CheckedChanged += new System.EventHandler(this.cbQRZcom_CheckedChanged);
            // 
            // gbQRZcom
            // 
            this.gbQRZcom.Controls.Add(this.QRZPassword);
            this.gbQRZcom.Controls.Add(this.QRZUsername);
            this.gbQRZcom.Controls.Add(this.label18);
            this.gbQRZcom.Controls.Add(this.label17);
            this.gbQRZcom.Controls.Add(this.label16);
            this.gbQRZcom.Controls.Add(this.QRZdburl);
            this.gbQRZcom.Location = new System.Drawing.Point(6, 30);
            this.gbQRZcom.Name = "gbQRZcom";
            this.gbQRZcom.Size = new System.Drawing.Size(376, 132);
            this.gbQRZcom.TabIndex = 23;
            this.gbQRZcom.TabStop = false;
            // 
            // QRZPassword
            // 
            this.QRZPassword.Location = new System.Drawing.Point(88, 82);
            this.QRZPassword.Name = "QRZPassword";
            this.QRZPassword.Size = new System.Drawing.Size(122, 20);
            this.QRZPassword.TabIndex = 21;
            // 
            // QRZUsername
            // 
            this.QRZUsername.Location = new System.Drawing.Point(88, 53);
            this.QRZUsername.Name = "QRZUsername";
            this.QRZUsername.Size = new System.Drawing.Size(122, 20);
            this.QRZUsername.TabIndex = 20;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(29, 83);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(53, 13);
            this.label18.TabIndex = 19;
            this.label18.Text = "Password";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(29, 56);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(55, 13);
            this.label17.TabIndex = 18;
            this.label17.Text = "Username";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(6, 30);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(78, 13);
            this.label16.TabIndex = 16;
            this.label16.Text = "QRZ.com URL";
            // 
            // QRZdburl
            // 
            this.QRZdburl.Location = new System.Drawing.Point(88, 27);
            this.QRZdburl.Name = "QRZdburl";
            this.QRZdburl.Size = new System.Drawing.Size(273, 20);
            this.QRZdburl.TabIndex = 17;
            this.QRZdburl.Text = "http://xmldata.qrz.com/xml/current/";
            // 
            // SaveClose
            // 
            this.SaveClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.SaveClose.Location = new System.Drawing.Point(299, 410);
            this.SaveClose.Name = "SaveClose";
            this.SaveClose.Size = new System.Drawing.Size(101, 23);
            this.SaveClose.TabIndex = 16;
            this.SaveClose.Text = "Save and Close";
            this.SaveClose.UseVisualStyleBackColor = true;
            this.SaveClose.Click += new System.EventHandler(this.SaveClose_Click);
            // 
            // MiniDeluxeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(449, 455);
            this.Controls.Add(this.SaveClose);
            this.Controls.Add(this.Tabs);
            this.Controls.Add(this.btnCheckForUpdate);
            this.Controls.Add(this.btnAbout);
            this.Controls.Add(this.cbLocalOnly);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.btnSave);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MiniDeluxeForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "MiniDeluxe Options";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MiniDeluxeForm_FormClosing);
            this.Load += new System.EventHandler(this.MiniDeluxeForm_Load);
            this.Tabs.ResumeLayout(false);
            this.HRDPage.ResumeLayout(false);
            this.HRDPage.PerformLayout();
            this.PowerSDRPage.ResumeLayout(false);
            this.PowerSDRPage.PerformLayout();
            this.gbPSDR.ResumeLayout(false);
            this.gbPSDR.PerformLayout();
            this.gbPSDR2.ResumeLayout(false);
            this.gbPSDR2.PerformLayout();
            this.SmartSDRPage.ResumeLayout(false);
            this.SmartSDRPage.PerformLayout();
            this.gbSSDR.ResumeLayout(false);
            this.gbSSDR.PerformLayout();
            this.DDUtilPage.ResumeLayout(false);
            this.DDUtilPage.PerformLayout();
            this.gbRIOX.ResumeLayout(false);
            this.gbRIOX.PerformLayout();
            this.N1MMPage.ResumeLayout(false);
            this.N1MMPage.PerformLayout();
            this.gbN1MM.ResumeLayout(false);
            this.gbN1MM.PerformLayout();
            this.Log4OM.ResumeLayout(false);
            this.Log4OM.PerformLayout();
            this.gbLog4OM.ResumeLayout(false);
            this.gbLog4OM.PerformLayout();
            this.QRZcom.ResumeLayout(false);
            this.QRZcom.PerformLayout();
            this.gbQRZcom.ResumeLayout(false);
            this.gbQRZcom.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.CheckBox cbLocalOnly;
        private System.Windows.Forms.Button btnAbout;
        private System.Windows.Forms.Button btnCheckForUpdate;
        private System.Windows.Forms.TabControl Tabs;
        private System.Windows.Forms.TabPage PowerSDRPage;
        private System.Windows.Forms.GroupBox gbPSDR2;
        private System.Windows.Forms.TextBox tbLow;
        private System.Windows.Forms.TextBox tbHigh;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lbPort;
        private System.Windows.Forms.ComboBox cbSerialport;
        private System.Windows.Forms.TabPage DDUtilPage;
        private System.Windows.Forms.Button btnRIOXTest;
        private System.Windows.Forms.TextBox txtRIOXport;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtRIOXIP;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TabPage N1MMPage;
        private System.Windows.Forms.TextBox radioPort;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TabPage SmartSDRPage;
        private System.Windows.Forms.TabPage HRDPage;
        private System.Windows.Forms.TextBox tbPort;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox tbLogPort;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox cbPSDR;
        private System.Windows.Forms.CheckBox cbSSDR;
        private System.Windows.Forms.CheckBox cbRIOX;
        private System.Windows.Forms.GroupBox gbRIOX;
        private System.Windows.Forms.GroupBox gbPSDR;
        private System.Windows.Forms.GroupBox gbSSDR;
        private System.Windows.Forms.CheckBox cbN1MM;
        private System.Windows.Forms.GroupBox gbN1MM;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.TextBox SSDRmodel;
        public System.Windows.Forms.TextBox SSDRip;
        private System.Windows.Forms.Label label9;
        public System.Windows.Forms.TextBox SSDRserial;
        private System.Windows.Forms.Label label10;
        public System.Windows.Forms.TextBox SSDRname;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Button SaveClose;
        public System.Windows.Forms.TextBox SSDRfreq;
        private System.Windows.Forms.Label label12;
        public System.Windows.Forms.TextBox SSDRmode;
        private System.Windows.Forms.Label label13;
        public System.Windows.Forms.TextBox SSDRsmeter;
        private System.Windows.Forms.Label label14;
        public System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.TabPage Log4OM;
        private System.Windows.Forms.CheckBox cbLog4OM;
        private System.Windows.Forms.GroupBox gbLog4OM;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox log4omService;
        private System.Windows.Forms.TabPage QRZcom;
        private System.Windows.Forms.CheckBox cbQRZcom;
        private System.Windows.Forms.GroupBox gbQRZcom;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox QRZdburl;
        private System.Windows.Forms.TextBox QRZPassword;
        private System.Windows.Forms.TextBox QRZUsername;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.TextBox log4omDBServer;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.TextBox log4omDBPass;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.TextBox log4omDBUser;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.TextBox log4omDBName;
    }
}

