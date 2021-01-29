namespace VideokallDataAcquisitionTool
{
    partial class VKApp
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.BtnStop = new System.Windows.Forms.Button();
            this.BTNPIC = new System.Windows.Forms.Button();
            this.StartCamera = new System.Windows.Forms.Button();
            this.BtnLoadCamera = new System.Windows.Forms.Button();
            this.CmbCameraList = new System.Windows.Forms.ComboBox();
            this.videoSourcePlayer = new AForge.Controls.VideoSourcePlayer();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.label5 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.listView3 = new System.Windows.Forms.ListView();
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.listView2 = new System.Windows.Forms.ListView();
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.listView1 = new System.Windows.Forms.ListView();
            this.Flow = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.radWT = new System.Windows.Forms.GroupBox();
            this.RadSIMWT = new System.Windows.Forms.RadioButton();
            this.lblResponse = new System.Windows.Forms.Label();
            this.ChkSimulation = new System.Windows.Forms.CheckBox();
            this.label9 = new System.Windows.Forms.Label();
            this.BTNSIMRFAILED = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.BTNSIMRACK = new System.Windows.Forms.Button();
            this.TxtSIMValue = new System.Windows.Forms.TextBox();
            this.BTNSIMDBAD = new System.Windows.Forms.Button();
            this.RADSIMST = new System.Windows.Forms.RadioButton();
            this.BTNSIMDGOOD = new System.Windows.Forms.Button();
            this.RADSIMWM = new System.Windows.Forms.RadioButton();
            this.RadSIMHM = new System.Windows.Forms.RadioButton();
            this.RADSIMRECLINE = new System.Windows.Forms.RadioButton();
            this.RADSIMSEATBACK = new System.Windows.Forms.RadioButton();
            this.radSIMPod = new System.Windows.Forms.RadioButton();
            this.BTNSIMDACK = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.BtnWT = new System.Windows.Forms.Button();
            this.BtnRetractPod = new System.Windows.Forms.Button();
            this.BtnRetract = new System.Windows.Forms.Button();
            this.BtnSave = new System.Windows.Forms.Button();
            this.BtnRefresh = new System.Windows.Forms.Button();
            this.TxtBaudRate = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.BtnWM = new System.Windows.Forms.Button();
            this.BtnHM = new System.Windows.Forms.Button();
            this.BtnSeatRecl = new System.Windows.Forms.Button();
            this.BtnSeatBack = new System.Windows.Forms.Button();
            this.BtnSTLung = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.TxtValue = new System.Windows.Forms.TextBox();
            this.BtnPOD = new System.Windows.Forms.Button();
            this.ResposeList = new System.Windows.Forms.ListBox();
            this.CmbComport = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.backgroundWorkerStartCamera = new System.ComponentModel.BackgroundWorker();
            this.backgroundWorkerReadFile = new System.ComponentModel.BackgroundWorker();
            this.backgroundWorkerWrite = new System.ComponentModel.BackgroundWorker();
            this.backgroundWorkerStopCamera = new System.ComponentModel.BackgroundWorker();
            this.backgroundWorkerspirometer = new System.ComponentModel.BackgroundWorker();
            this.lblUnit = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.radWT.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Location = new System.Drawing.Point(2, 3);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1125, 813);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.checkBox1);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.BtnStop);
            this.tabPage1.Controls.Add(this.BTNPIC);
            this.tabPage1.Controls.Add(this.StartCamera);
            this.tabPage1.Controls.Add(this.BtnLoadCamera);
            this.tabPage1.Controls.Add(this.CmbCameraList);
            this.tabPage1.Controls.Add(this.videoSourcePlayer);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1117, 787);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Microscope";
            this.tabPage1.UseVisualStyleBackColor = true;
            this.tabPage1.Click += new System.EventHandler(this.tabPage1_Click);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(998, 396);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(76, 17);
            this.checkBox1.TabIndex = 7;
            this.checkBox1.Text = "Diagnostic";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(995, 438);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 13);
            this.label1.TabIndex = 6;
            //this.label1.Text = "10-12-2020";
            // 
            // BtnStop
            // 
            this.BtnStop.Location = new System.Drawing.Point(959, 215);
            this.BtnStop.Name = "BtnStop";
            this.BtnStop.Size = new System.Drawing.Size(75, 23);
            this.BtnStop.TabIndex = 5;
            this.BtnStop.Text = "Stop";
            this.BtnStop.UseVisualStyleBackColor = true;
            this.BtnStop.Click += new System.EventHandler(this.BtnStop_Click);
            // 
            // BTNPIC
            // 
            this.BTNPIC.Location = new System.Drawing.Point(955, 151);
            this.BTNPIC.Name = "BTNPIC";
            this.BTNPIC.Size = new System.Drawing.Size(75, 23);
            this.BTNPIC.TabIndex = 4;
            this.BTNPIC.Text = "Pic";
            this.BTNPIC.UseVisualStyleBackColor = true;
            this.BTNPIC.Click += new System.EventHandler(this.BTNPIC_Click);
            // 
            // StartCamera
            // 
            this.StartCamera.Location = new System.Drawing.Point(955, 96);
            this.StartCamera.Name = "StartCamera";
            this.StartCamera.Size = new System.Drawing.Size(75, 23);
            this.StartCamera.TabIndex = 3;
            this.StartCamera.Text = "Start";
            this.StartCamera.UseVisualStyleBackColor = true;
            this.StartCamera.Click += new System.EventHandler(this.StartCamera_Click);
            // 
            // BtnLoadCamera
            // 
            this.BtnLoadCamera.Location = new System.Drawing.Point(955, 44);
            this.BtnLoadCamera.Name = "BtnLoadCamera";
            this.BtnLoadCamera.Size = new System.Drawing.Size(79, 23);
            this.BtnLoadCamera.TabIndex = 2;
            this.BtnLoadCamera.Text = "Load Camera";
            this.BtnLoadCamera.UseMnemonic = false;
            this.BtnLoadCamera.UseVisualStyleBackColor = true;
            this.BtnLoadCamera.Click += new System.EventHandler(this.BtnLoadCamera_Click);
            // 
            // CmbCameraList
            // 
            this.CmbCameraList.FormattingEnabled = true;
            this.CmbCameraList.Location = new System.Drawing.Point(955, 6);
            this.CmbCameraList.Name = "CmbCameraList";
            this.CmbCameraList.Size = new System.Drawing.Size(117, 21);
            this.CmbCameraList.TabIndex = 1;
            this.CmbCameraList.SelectedIndexChanged += new System.EventHandler(this.CmbCameraList_SelectedIndexChanged);
            // 
            // videoSourcePlayer
            // 
            this.videoSourcePlayer.Location = new System.Drawing.Point(0, 0);
            this.videoSourcePlayer.Name = "videoSourcePlayer";
            this.videoSourcePlayer.Size = new System.Drawing.Size(953, 787);
            this.videoSourcePlayer.TabIndex = 0;
            this.videoSourcePlayer.Text = "videoSourcePlayer";
            this.videoSourcePlayer.VideoSource = null;
            this.videoSourcePlayer.Click += new System.EventHandler(this.videoSourcePlayer1_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.label5);
            this.tabPage2.Controls.Add(this.button3);
            this.tabPage2.Controls.Add(this.button4);
            this.tabPage2.Controls.Add(this.listView3);
            this.tabPage2.Controls.Add(this.label3);
            this.tabPage2.Controls.Add(this.label2);
            this.tabPage2.Controls.Add(this.listView2);
            this.tabPage2.Controls.Add(this.listView1);
            this.tabPage2.Controls.Add(this.button2);
            this.tabPage2.Controls.Add(this.button1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Size = new System.Drawing.Size(1117, 787);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Spirometry";
            this.tabPage2.UseVisualStyleBackColor = true;
            this.tabPage2.Click += new System.EventHandler(this.tabPage2_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(638, 12);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(35, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "label5";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(912, 386);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 9;
            this.button3.Text = "Stop VC";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(767, 386);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 23);
            this.button4.TabIndex = 8;
            this.button4.Text = "Start VC";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // listView3
            // 
            this.listView3.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader4,
            this.columnHeader5});
            this.listView3.HideSelection = false;
            this.listView3.Location = new System.Drawing.Point(767, 28);
            this.listView3.Name = "listView3";
            this.listView3.Size = new System.Drawing.Size(256, 329);
            this.listView3.TabIndex = 7;
            this.listView3.UseCompatibleStateImageBehavior = false;
            this.listView3.View = System.Windows.Forms.View.Details;
            this.listView3.SelectedIndexChanged += new System.EventHandler(this.listView3_SelectedIndexChanged);
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Volume";
            this.columnHeader4.Width = 120;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Time";
            this.columnHeader5.Width = 146;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(342, 12);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(92, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Volume Time data";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(32, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(91, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Flow Volume data";
            // 
            // listView2
            // 
            this.listView2.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader2,
            this.columnHeader3});
            this.listView2.HideSelection = false;
            this.listView2.Location = new System.Drawing.Point(345, 28);
            this.listView2.Name = "listView2";
            this.listView2.Size = new System.Drawing.Size(256, 329);
            this.listView2.TabIndex = 4;
            this.listView2.UseCompatibleStateImageBehavior = false;
            this.listView2.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Volume";
            this.columnHeader2.Width = 120;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Time";
            this.columnHeader3.Width = 146;
            // 
            // listView1
            // 
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Flow,
            this.columnHeader1});
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(32, 28);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(297, 329);
            this.listView1.TabIndex = 3;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // Flow
            // 
            this.Flow.Text = "Flow";
            this.Flow.Width = 143;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Volume";
            this.columnHeader1.Width = 172;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(177, 386);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 2;
            this.button2.Text = "Stop FVC";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(32, 386);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "Start FVC";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.radWT);
            this.tabPage3.Controls.Add(this.groupBox1);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(1117, 787);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "CAS";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // radWT
            // 
            this.radWT.Controls.Add(this.lblUnit);
            this.radWT.Controls.Add(this.RadSIMWT);
            this.radWT.Controls.Add(this.lblResponse);
            this.radWT.Controls.Add(this.ChkSimulation);
            this.radWT.Controls.Add(this.label9);
            this.radWT.Controls.Add(this.BTNSIMRFAILED);
            this.radWT.Controls.Add(this.label8);
            this.radWT.Controls.Add(this.BTNSIMRACK);
            this.radWT.Controls.Add(this.TxtSIMValue);
            this.radWT.Controls.Add(this.BTNSIMDBAD);
            this.radWT.Controls.Add(this.RADSIMST);
            this.radWT.Controls.Add(this.BTNSIMDGOOD);
            this.radWT.Controls.Add(this.RADSIMWM);
            this.radWT.Controls.Add(this.RadSIMHM);
            this.radWT.Controls.Add(this.RADSIMRECLINE);
            this.radWT.Controls.Add(this.RADSIMSEATBACK);
            this.radWT.Controls.Add(this.radSIMPod);
            this.radWT.Controls.Add(this.BTNSIMDACK);
            this.radWT.Location = new System.Drawing.Point(479, 21);
            this.radWT.Name = "radWT";
            this.radWT.Size = new System.Drawing.Size(327, 357);
            this.radWT.TabIndex = 1;
            this.radWT.TabStop = false;
            this.radWT.Enter += new System.EventHandler(this.radWT_Enter);
            // 
            // RadSIMWT
            // 
            this.RadSIMWT.AutoSize = true;
            this.RadSIMWT.Location = new System.Drawing.Point(8, 209);
            this.RadSIMWT.Name = "RadSIMWT";
            this.RadSIMWT.Size = new System.Drawing.Size(108, 17);
            this.RadSIMWT.TabIndex = 25;
            this.RadSIMWT.TabStop = true;
            this.RadSIMWT.Text = "Tear Weight(WT)";
            this.RadSIMWT.UseVisualStyleBackColor = true;
            // 
            // lblResponse
            // 
            this.lblResponse.AutoSize = true;
            this.lblResponse.Location = new System.Drawing.Point(190, 51);
            this.lblResponse.Name = "lblResponse";
            this.lblResponse.Size = new System.Drawing.Size(0, 13);
            this.lblResponse.TabIndex = 24;
            // 
            // ChkSimulation
            // 
            this.ChkSimulation.AutoSize = true;
            this.ChkSimulation.Location = new System.Drawing.Point(193, 19);
            this.ChkSimulation.Name = "ChkSimulation";
            this.ChkSimulation.Size = new System.Drawing.Size(15, 14);
            this.ChkSimulation.TabIndex = 22;
            this.ChkSimulation.UseVisualStyleBackColor = true;
            this.ChkSimulation.CheckedChanged += new System.EventHandler(this.ChkSimulation_CheckedChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(22, 19);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(165, 13);
            this.label9.TabIndex = 21;
            this.label9.Text = "CAS Response Simulation mode .";
            // 
            // BTNSIMRFAILED
            // 
            this.BTNSIMRFAILED.Location = new System.Drawing.Point(221, 291);
            this.BTNSIMRFAILED.Name = "BTNSIMRFAILED";
            this.BTNSIMRFAILED.Size = new System.Drawing.Size(95, 23);
            this.BTNSIMRFAILED.TabIndex = 20;
            this.BTNSIMRFAILED.Text = "Retract Failed";
            this.BTNSIMRFAILED.UseVisualStyleBackColor = true;
            this.BTNSIMRFAILED.Click += new System.EventHandler(this.BTNSIMRFAILED_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(12, 48);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(50, 13);
            this.label8.TabIndex = 19;
            this.label8.Text = "ID/Value";
            // 
            // BTNSIMRACK
            // 
            this.BTNSIMRACK.Location = new System.Drawing.Point(7, 291);
            this.BTNSIMRACK.Name = "BTNSIMRACK";
            this.BTNSIMRACK.Size = new System.Drawing.Size(102, 23);
            this.BTNSIMRACK.TabIndex = 9;
            this.BTNSIMRACK.Text = "Retract Ack";
            this.BTNSIMRACK.UseVisualStyleBackColor = true;
            this.BTNSIMRACK.Click += new System.EventHandler(this.BTNSIMRACK_Click);
            // 
            // TxtSIMValue
            // 
            this.TxtSIMValue.Location = new System.Drawing.Point(65, 45);
            this.TxtSIMValue.Name = "TxtSIMValue";
            this.TxtSIMValue.Size = new System.Drawing.Size(57, 20);
            this.TxtSIMValue.TabIndex = 18;
            // 
            // BTNSIMDBAD
            // 
            this.BTNSIMDBAD.Location = new System.Drawing.Point(221, 239);
            this.BTNSIMDBAD.Name = "BTNSIMDBAD";
            this.BTNSIMDBAD.Size = new System.Drawing.Size(99, 23);
            this.BTNSIMDBAD.TabIndex = 8;
            this.BTNSIMDBAD.Text = "Bad Operation";
            this.BTNSIMDBAD.UseVisualStyleBackColor = true;
            this.BTNSIMDBAD.Click += new System.EventHandler(this.BTNSIMDBAD_Click);
            // 
            // RADSIMST
            // 
            this.RADSIMST.AutoSize = true;
            this.RADSIMST.Location = new System.Drawing.Point(7, 188);
            this.RADSIMST.Name = "RADSIMST";
            this.RADSIMST.Size = new System.Drawing.Size(85, 17);
            this.RADSIMST.TabIndex = 7;
            this.RADSIMST.TabStop = true;
            this.RADSIMST.Text = "Stethoscope";
            this.RADSIMST.UseVisualStyleBackColor = true;
            // 
            // BTNSIMDGOOD
            // 
            this.BTNSIMDGOOD.Location = new System.Drawing.Point(113, 239);
            this.BTNSIMDGOOD.Name = "BTNSIMDGOOD";
            this.BTNSIMDGOOD.Size = new System.Drawing.Size(102, 23);
            this.BTNSIMDGOOD.TabIndex = 6;
            this.BTNSIMDGOOD.Text = "Good Operation";
            this.BTNSIMDGOOD.UseVisualStyleBackColor = true;
            this.BTNSIMDGOOD.Click += new System.EventHandler(this.BTNSIMDGOOD_Click);
            // 
            // RADSIMWM
            // 
            this.RADSIMWM.AutoSize = true;
            this.RADSIMWM.Location = new System.Drawing.Point(7, 121);
            this.RADSIMWM.Name = "RADSIMWM";
            this.RADSIMWM.Size = new System.Drawing.Size(129, 17);
            this.RADSIMWM.TabIndex = 5;
            this.RADSIMWM.TabStop = true;
            this.RADSIMWM.Text = "Weight Measure(WM)";
            this.RADSIMWM.UseVisualStyleBackColor = true;
            this.RADSIMWM.CheckedChanged += new System.EventHandler(this.RADSIMWM_CheckedChanged);
            // 
            // RadSIMHM
            // 
            this.RadSIMHM.AutoSize = true;
            this.RadSIMHM.Location = new System.Drawing.Point(6, 96);
            this.RadSIMHM.Name = "RadSIMHM";
            this.RadSIMHM.Size = new System.Drawing.Size(126, 17);
            this.RadSIMHM.TabIndex = 4;
            this.RadSIMHM.TabStop = true;
            this.RadSIMHM.Text = "Height Measure (HM)";
            this.RadSIMHM.UseVisualStyleBackColor = true;
            this.RadSIMHM.CheckedChanged += new System.EventHandler(this.RadSIMHM_CheckedChanged);
            // 
            // RADSIMRECLINE
            // 
            this.RADSIMRECLINE.AutoSize = true;
            this.RADSIMRECLINE.Location = new System.Drawing.Point(7, 167);
            this.RADSIMRECLINE.Name = "RADSIMRECLINE";
            this.RADSIMRECLINE.Size = new System.Drawing.Size(86, 17);
            this.RADSIMRECLINE.TabIndex = 3;
            this.RADSIMRECLINE.TabStop = true;
            this.RADSIMRECLINE.Text = "Seat Recline";
            this.RADSIMRECLINE.UseVisualStyleBackColor = true;
            // 
            // RADSIMSEATBACK
            // 
            this.RADSIMSEATBACK.AutoSize = true;
            this.RADSIMSEATBACK.Location = new System.Drawing.Point(7, 144);
            this.RADSIMSEATBACK.Name = "RADSIMSEATBACK";
            this.RADSIMSEATBACK.Size = new System.Drawing.Size(72, 17);
            this.RADSIMSEATBACK.TabIndex = 2;
            this.RADSIMSEATBACK.TabStop = true;
            this.RADSIMSEATBACK.Text = "SeatBack";
            this.RADSIMSEATBACK.UseVisualStyleBackColor = true;
            // 
            // radSIMPod
            // 
            this.radSIMPod.AutoSize = true;
            this.radSIMPod.Location = new System.Drawing.Point(7, 73);
            this.radSIMPod.Name = "radSIMPod";
            this.radSIMPod.Size = new System.Drawing.Size(99, 17);
            this.radSIMPod.TabIndex = 1;
            this.radSIMPod.TabStop = true;
            this.radSIMPod.Text = "POD Response";
            this.radSIMPod.UseVisualStyleBackColor = true;
            // 
            // BTNSIMDACK
            // 
            this.BTNSIMDACK.Location = new System.Drawing.Point(7, 241);
            this.BTNSIMDACK.Name = "BTNSIMDACK";
            this.BTNSIMDACK.Size = new System.Drawing.Size(99, 23);
            this.BTNSIMDACK.TabIndex = 0;
            this.BTNSIMDACK.Text = "Acknowldgement";
            this.BTNSIMDACK.UseVisualStyleBackColor = true;
            this.BTNSIMDACK.Click += new System.EventHandler(this.BTNSIMDACK_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.BtnWT);
            this.groupBox1.Controls.Add(this.BtnRetractPod);
            this.groupBox1.Controls.Add(this.BtnRetract);
            this.groupBox1.Controls.Add(this.BtnSave);
            this.groupBox1.Controls.Add(this.BtnRefresh);
            this.groupBox1.Controls.Add(this.TxtBaudRate);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.BtnWM);
            this.groupBox1.Controls.Add(this.BtnHM);
            this.groupBox1.Controls.Add(this.BtnSeatRecl);
            this.groupBox1.Controls.Add(this.BtnSeatBack);
            this.groupBox1.Controls.Add(this.BtnSTLung);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.TxtValue);
            this.groupBox1.Controls.Add(this.BtnPOD);
            this.groupBox1.Controls.Add(this.ResposeList);
            this.groupBox1.Controls.Add(this.CmbComport);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Location = new System.Drawing.Point(7, 21);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(389, 357);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // BtnWT
            // 
            this.BtnWT.Location = new System.Drawing.Point(172, 291);
            this.BtnWT.Name = "BtnWT";
            this.BtnWT.Size = new System.Drawing.Size(75, 23);
            this.BtnWT.TabIndex = 18;
            this.BtnWT.Text = "WT";
            this.BtnWT.UseVisualStyleBackColor = true;
            this.BtnWT.Click += new System.EventHandler(this.BtnWT_Click);
            // 
            // BtnRetractPod
            // 
            this.BtnRetractPod.Location = new System.Drawing.Point(234, 260);
            this.BtnRetractPod.Name = "BtnRetractPod";
            this.BtnRetractPod.Size = new System.Drawing.Size(135, 23);
            this.BtnRetractPod.TabIndex = 17;
            this.BtnRetractPod.Text = "Retract POD";
            this.BtnRetractPod.UseVisualStyleBackColor = true;
            this.BtnRetractPod.Click += new System.EventHandler(this.BtnRetractPod_Click);
            // 
            // BtnRetract
            // 
            this.BtnRetract.Location = new System.Drawing.Point(234, 231);
            this.BtnRetract.Name = "BtnRetract";
            this.BtnRetract.Size = new System.Drawing.Size(135, 23);
            this.BtnRetract.TabIndex = 16;
            this.BtnRetract.Text = "Retract Stethoscope Lungs";
            this.BtnRetract.UseVisualStyleBackColor = true;
            this.BtnRetract.Click += new System.EventHandler(this.BtnRetract_Click_1);
            // 
            // BtnSave
            // 
            this.BtnSave.Location = new System.Drawing.Point(295, 98);
            this.BtnSave.Name = "BtnSave";
            this.BtnSave.Size = new System.Drawing.Size(75, 23);
            this.BtnSave.TabIndex = 15;
            this.BtnSave.Text = "Save";
            this.BtnSave.UseVisualStyleBackColor = true;
            this.BtnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // BtnRefresh
            // 
            this.BtnRefresh.Location = new System.Drawing.Point(295, 68);
            this.BtnRefresh.Name = "BtnRefresh";
            this.BtnRefresh.Size = new System.Drawing.Size(75, 23);
            this.BtnRefresh.TabIndex = 14;
            this.BtnRefresh.Text = "Refresh ComPorts";
            this.BtnRefresh.UseVisualStyleBackColor = true;
            this.BtnRefresh.Click += new System.EventHandler(this.BtnRefresh_Click);
            // 
            // TxtBaudRate
            // 
            this.TxtBaudRate.Location = new System.Drawing.Point(295, 27);
            this.TxtBaudRate.Name = "TxtBaudRate";
            this.TxtBaudRate.Size = new System.Drawing.Size(74, 20);
            this.TxtBaudRate.TabIndex = 13;
            this.TxtBaudRate.TextAlignChanged += new System.EventHandler(this.TxtBaudRate_TextAlignChanged);
            this.TxtBaudRate.TextChanged += new System.EventHandler(this.TxtBaudRate_TextChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(231, 27);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(58, 13);
            this.label7.TabIndex = 12;
            this.label7.Text = "Baud Rate";
            // 
            // BtnWM
            // 
            this.BtnWM.Location = new System.Drawing.Point(91, 291);
            this.BtnWM.Name = "BtnWM";
            this.BtnWM.Size = new System.Drawing.Size(75, 23);
            this.BtnWM.TabIndex = 10;
            this.BtnWM.Text = "WM";
            this.BtnWM.UseVisualStyleBackColor = true;
            this.BtnWM.Click += new System.EventHandler(this.BtnWM_Click);
            // 
            // BtnHM
            // 
            this.BtnHM.Location = new System.Drawing.Point(10, 291);
            this.BtnHM.Name = "BtnHM";
            this.BtnHM.Size = new System.Drawing.Size(75, 23);
            this.BtnHM.TabIndex = 9;
            this.BtnHM.Text = "HM";
            this.BtnHM.UseVisualStyleBackColor = true;
            this.BtnHM.Click += new System.EventHandler(this.BtnHM_Click);
            // 
            // BtnSeatRecl
            // 
            this.BtnSeatRecl.Location = new System.Drawing.Point(91, 261);
            this.BtnSeatRecl.Name = "BtnSeatRecl";
            this.BtnSeatRecl.Size = new System.Drawing.Size(88, 23);
            this.BtnSeatRecl.TabIndex = 8;
            this.BtnSeatRecl.Text = "Seat Recline";
            this.BtnSeatRecl.UseVisualStyleBackColor = true;
            this.BtnSeatRecl.Click += new System.EventHandler(this.BtnSeatRecl_Click);
            // 
            // BtnSeatBack
            // 
            this.BtnSeatBack.Location = new System.Drawing.Point(10, 261);
            this.BtnSeatBack.Name = "BtnSeatBack";
            this.BtnSeatBack.Size = new System.Drawing.Size(75, 23);
            this.BtnSeatBack.TabIndex = 7;
            this.BtnSeatBack.Text = "Seat Back";
            this.BtnSeatBack.UseVisualStyleBackColor = true;
            this.BtnSeatBack.Click += new System.EventHandler(this.BtnSeatBack_Click);
            // 
            // BtnSTLung
            // 
            this.BtnSTLung.Location = new System.Drawing.Point(91, 231);
            this.BtnSTLung.Name = "BtnSTLung";
            this.BtnSTLung.Size = new System.Drawing.Size(128, 23);
            this.BtnSTLung.TabIndex = 6;
            this.BtnSTLung.Text = "Stethoscope Lungs";
            this.BtnSTLung.UseVisualStyleBackColor = true;
            this.BtnSTLung.Click += new System.EventHandler(this.BtnSTLung_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 205);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(50, 13);
            this.label6.TabIndex = 5;
            this.label6.Text = "ID/Value";
            // 
            // TxtValue
            // 
            this.TxtValue.Location = new System.Drawing.Point(68, 202);
            this.TxtValue.Name = "TxtValue";
            this.TxtValue.Size = new System.Drawing.Size(57, 20);
            this.TxtValue.TabIndex = 4;
            this.TxtValue.TextChanged += new System.EventHandler(this.TxtValue_TextChanged);
            // 
            // BtnPOD
            // 
            this.BtnPOD.Location = new System.Drawing.Point(10, 231);
            this.BtnPOD.Name = "BtnPOD";
            this.BtnPOD.Size = new System.Drawing.Size(75, 23);
            this.BtnPOD.TabIndex = 3;
            this.BtnPOD.Text = "POD";
            this.BtnPOD.UseVisualStyleBackColor = true;
            this.BtnPOD.Click += new System.EventHandler(this.BtnPOD_Click);
            // 
            // ResposeList
            // 
            this.ResposeList.FormattingEnabled = true;
            this.ResposeList.Location = new System.Drawing.Point(10, 68);
            this.ResposeList.Name = "ResposeList";
            this.ResposeList.Size = new System.Drawing.Size(191, 121);
            this.ResposeList.TabIndex = 2;
            // 
            // CmbComport
            // 
            this.CmbComport.FormattingEnabled = true;
            this.CmbComport.Location = new System.Drawing.Point(80, 20);
            this.CmbComport.Name = "CmbComport";
            this.CmbComport.Size = new System.Drawing.Size(121, 21);
            this.CmbComport.TabIndex = 1;
            this.CmbComport.SelectedIndexChanged += new System.EventHandler(this.CmbComport_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(7, 20);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "COM Port";
            // 
            // backgroundWorkerStartCamera
            // 
            this.backgroundWorkerStartCamera.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            // 
            // backgroundWorkerReadFile
            // 
            this.backgroundWorkerReadFile.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorkerReadFile_DoWork);
            // 
            // backgroundWorkerWrite
            // 
            this.backgroundWorkerWrite.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorkerWrite_DoWork);
            // 
            // backgroundWorkerStopCamera
            // 
            this.backgroundWorkerStopCamera.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorkerStopCamera_DoWork);
            // 
            // backgroundWorkerspirometer
            // 
            this.backgroundWorkerspirometer.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorkerspirometer_DoWork);
            // 
            // lblUnit
            // 
            this.lblUnit.AutoSize = true;
            this.lblUnit.Location = new System.Drawing.Point(129, 48);
            this.lblUnit.Name = "lblUnit";
            this.lblUnit.Size = new System.Drawing.Size(0, 13);
            this.lblUnit.TabIndex = 26;
            // 
            // VKApp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1078, 749);
            this.Controls.Add(this.tabControl1);
            this.Name = "VKApp";
            this.Text = "Videokall DataAcquisitionTool";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.VKApp_FormClosing);
            this.Load += new System.EventHandler(this.VKApp_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.radWT.ResumeLayout(false);
            this.radWT.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private AForge.Controls.VideoSourcePlayer videoSourcePlayer;
        private System.Windows.Forms.ComboBox CmbCameraList;
        private System.Windows.Forms.Button BtnLoadCamera;
        private System.Windows.Forms.Button StartCamera;
        private System.Windows.Forms.Button BTNPIC;
        private System.Windows.Forms.Button BtnStop;
        private System.Windows.Forms.Label label1;
        private System.ComponentModel.BackgroundWorker backgroundWorkerStartCamera;
        private System.ComponentModel.BackgroundWorker backgroundWorkerReadFile;
        private System.ComponentModel.BackgroundWorker backgroundWorkerWrite;
        private System.ComponentModel.BackgroundWorker backgroundWorkerStopCamera;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.ComponentModel.BackgroundWorker backgroundWorkerspirometer;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ListView listView2;
        private System.Windows.Forms.ColumnHeader Flow;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.ListView listView3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox TxtValue;
        private System.Windows.Forms.Button BtnPOD;
        private System.Windows.Forms.ListBox ResposeList;
        private System.Windows.Forms.ComboBox CmbComport;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button BtnWM;
        private System.Windows.Forms.Button BtnHM;
        private System.Windows.Forms.Button BtnSeatRecl;
        private System.Windows.Forms.Button BtnSeatBack;
        private System.Windows.Forms.Button BtnSTLung;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox TxtBaudRate;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button BtnRefresh;
        private System.Windows.Forms.Button BtnSave;
        private System.Windows.Forms.Button BtnRetract;
        private System.Windows.Forms.Button BtnRetractPod;
        private System.Windows.Forms.GroupBox radWT;
        private System.Windows.Forms.CheckBox ChkSimulation;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button BTNSIMRFAILED;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button BTNSIMRACK;
        private System.Windows.Forms.TextBox TxtSIMValue;
        private System.Windows.Forms.Button BTNSIMDBAD;
        private System.Windows.Forms.RadioButton RADSIMST;
        private System.Windows.Forms.Button BTNSIMDGOOD;
        private System.Windows.Forms.RadioButton RADSIMWM;
        private System.Windows.Forms.RadioButton RadSIMHM;
        private System.Windows.Forms.RadioButton RADSIMRECLINE;
        private System.Windows.Forms.RadioButton RADSIMSEATBACK;
        private System.Windows.Forms.RadioButton radSIMPod;
        private System.Windows.Forms.Button BTNSIMDACK;
        private System.Windows.Forms.Label lblResponse;
        private System.Windows.Forms.Button BtnWT;
        private System.Windows.Forms.RadioButton RadSIMWT;
        private System.Windows.Forms.Label lblUnit;
    }
}

