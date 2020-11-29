using AForge.Controls;
using AForge.Video.DirectShow;
using MirDataTypes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using VideokallDataAcquisitionTool.comm;
using VideokallSBCDataAcquisionApp.comm;
using VideokallSBCDataAcquisionApp.Spirometer;

namespace VideokallDataAcquisitionTool
{
    public partial class VKApp : Form
    {
        public static VKApp mainApp;
        FilterInfoCollection videoDevices;
        FilterInfo[] videoCamList; 
        System.Windows.Forms.Timer Watchdog = null;
        Commmodule CmmModule = null;
        delegate void CallBackDelegate();
        delegate void CallBackDelegatespiro(string txt);
        public  CommwithChartApp commwithChartapp = new CommwithChartApp();
        public VKApp()
        {
            InitializeComponent();
              mainApp = this; 
            ShowHideControls(false);
        }
        #region CommMessage
        private void ReceivedMessage(string msg)
        {
            string[] cmd = msg.ToLower().Split('>');
            switch (cmd[0])
            {
                case "<appc":
                    CmmModule.SendData(CommCommands.DATAACQSTATUS);
                    LogMessage(msg);
                    break;
                case "der":
                    LogMessage(msg);
                    //"DER>SH:{0}>H:{1}>W{2}>X:{3}>Y:{4}>";
                    int showhide = Convert.ToInt32(cmd[1].Split(':')[1]);
                    if (showhide == 1)
                    {
                        if (!backgroundWorkerStartCamera.IsBusy)
                            backgroundWorkerStartCamera.RunWorkerAsync(msg);
                    }
                    break;
                case "<pic":
                    LogMessage(msg);
                 //   TakePic();
                    this.BeginInvoke(new ReadCommFiledelegate(TakePic));
                    break;

                case "oto":
                    {
                        //"Oto>SH:{0}>H:{1}>W{2}>X:{3}>Y:{4}>";
                        showhide = Convert.ToInt32(cmd[1].Split(':')[1]);
                        if (showhide == 1)
                        {
                            if (!backgroundWorkerStartCamera.IsBusy)
                                backgroundWorkerStartCamera.RunWorkerAsync(msg);
                            LogMessage(msg);
                        }
                    }
                    break;
                case "<stopdermo":
                case "<stopoto":
                   if(! backgroundWorkerStopCamera.IsBusy)
                    {
                        backgroundWorkerStopCamera.RunWorkerAsync();
                    }
                    LogMessage(msg);
                    break;
                case "otosaveimage":
                     
                    SaveImage(false,cmd[1]);
                    break;
                case "dersaveimage":
                    SaveImage(true, cmd[1]);
                    break;

                case "<startspirofvc":
                    
                    this.Invoke(new CallBackDelegatespiro(StartFVC),msg);
                    commtoMCC.readData();

                    break;

                case "<stopspiro":
                  
                    this.Invoke(new CallBackDelegate(Stopspiro));
                    commtoMCC.readData();
                    break;
                 case "<startspirovc":
                   
                    this.Invoke(new CallBackDelegatespiro(StartVC),msg);
                    commtoMCC.readData();
                    break;
                case "connectiontest":
                  
                    commtoMCC.SendData("connectedmcc");
                    commtoMCC.readData();
                    break;
                case "pod":
                    if(cmd[2].Equals("d"))
                      _serialDevice.DeployPod(cmd[1]);
                    commtoMCC.readData();
                    break;
                case "seatht":
                    _serialDevice.SeatBack(cmd[1]);
                    commtoMCC.readData();
                    break;
                case "seatrec":
                    _serialDevice.Recline(cmd[1]);
                    commtoMCC.readData();
                    break;
                case "hm":
                    _serialDevice.HeightMeasure();
                    commtoMCC.readData();
                    break;
                case "wm":
                    _serialDevice.WeightMeasure();
                    commtoMCC.readData();
                    break;
                case "stl":
                    if (cmd[2].Equals("d"))
                        _serialDevice.DeployPod(cmd[1]);
                    commtoMCC.readData();
                    break;
                case "wt":
                    _serialDevice.TareWeight();
                    commtoMCC.readData();
                    break;
            }
        }
#endregion

        void StartST()
        {
            int height = Convert.ToInt32(cmd[2].Split(':')[1]);
            int width = Convert.ToInt32(cmd[3].Split(':')[1]);
            int mainscWidht = Convert.ToInt32(cmd[4].Split(':')[1]);
            int mainscheight = Convert.ToInt32(cmd[5].Split(':')[1]);
            this.Width = mainscWidht / 2; ;
            this.Height = mainscheight - 30; ;
            this.Left = mainscWidht - mainscWidht / 2; ;
            this.Top = 70;
            this.TopMost = true;

            videoSourcePlayer.Height = tabPage1.Height;
            videoSourcePlayer.Width = tabPage1.Width;
            this.Location = new Point(mainscWidht - mainscWidht / 2, 70);
            this.WindowState = FormWindowState.Normal;

            if (cmd[0].Equals("der"))
                StartMicorCamera(1);
            else if (cmd[0].Equals("oto"))
                StartMicorCamera(0);
        }

        string[] cmd;
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
              cmd = ((string)e.Argument).ToLower().Split('>');
            
            this.Invoke(new ReadCommFiledelegate(StartST));
        }
        #region Microscope
        void LoadCamera()
        {
            try
            { 
                // enumerate video devices
                videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
                 
                CmbCameraList.Items.Clear();
                if (videoDevices.Count == 0)
                    CmmModule.SendData(string.Format(CommCommands.MIREXCEPTION,"No device Connected"));

                int index = 0;
                videoCamList = new FilterInfo[videoDevices.Count];
                // add all devices to combo
                foreach (FilterInfo device in videoDevices)
                {
                    if (device.Name.ToLower().Contains("microscope"))
                    {
                        CmbCameraList.Items.Add(device.Name);
                        CmbCameraList.SelectedIndex = 0;
                        videoCamList[index] = device;
                        index++;
                        LogMessage(device.Name);
                    }
                    else
                    {

                    }
                } 
            }
            catch(Exception ex)
            {
                LogMessage(ex.Message);
                if (!backgroundWorkerWrite.IsBusy)
                {
                    backgroundWorkerWrite.RunWorkerAsync(string.Format(CommCommands.MIREXCEPTION, ex.Message));
                }
            }
        }
        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void BtnLoadCamera_Click(object sender, EventArgs e)
        { 
            LoadCamera(); 
        }

        void StartMicorCamera(int indx)
        {
            if (indx < 0)
                return; 
            try
            {                 
                if (videoCamList!= null && videoCamList.Count() > indx && videoCamList[indx] != null)
                {
                    VideoCaptureDevice videoCaptureSource = new VideoCaptureDevice(videoCamList[indx].MonikerString);
                    videoSourcePlayer.VideoSource = videoCaptureSource;
                    videoSourcePlayer.Start(); 
                }
                else
                {
                    if (!backgroundWorkerWrite.IsBusy)
                    {
                        backgroundWorkerWrite.RunWorkerAsync(string.Format(CommCommands.MIREXCEPTION, videoCamList.Count() == 0 ? "No device Connected" : "Please connect the device"));
                    }
                }
            }
            catch(Exception ex)
            {
                LogMessage("*****exception on start  camera*****");
                LogMessage(ex.ToString());
                LogMessage(ex.Message);
                if (!backgroundWorkerWrite.IsBusy)
                {
                    backgroundWorkerWrite.RunWorkerAsync(string.Format(CommCommands.MIREXCEPTION, ex.ToString()));
                }
            }
        }
        void LogMessage(string msg)
        {
            try
            {
                using (System.IO.StreamWriter file =
                new System.IO.StreamWriter("logs.txt", false))
                {
                    file.WriteLine(DateTime.Now.ToString() + ": " + msg);
                }
            }catch(Exception )
            {

            }
           
        }
        void StartMicroCamera()
        {
            try
            {
                int index = CmbCameraList.SelectedIndex;
                VideoCaptureDevice videoCaptureSource = new VideoCaptureDevice(videoCamList[index].MonikerString);
                videoSourcePlayer.VideoSource = videoCaptureSource;
                videoSourcePlayer.Start();
            }catch(Exception ex)
            {
                LogMessage(ex.Message);
                if (!backgroundWorkerWrite.IsBusy)
                {
                    backgroundWorkerWrite.RunWorkerAsync(string.Format(CommCommands.MIREXCEPTION, ex.Message));
                }
            }
        }
        private void StartCamera_Click(object sender, EventArgs e)
        {
            StartMicorCamera(CmbCameraList.SelectedIndex);
        }

        
        void TakePic()
        {
            try
            {
                LogMessage("TakePic");
                string folder = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
                if (!Directory.Exists(folder + "\\Videokall"))
                {
                    Directory.CreateDirectory(folder + "\\Videokall");
                }
                    try
                    {
                        if (File.Exists(folder + "\\Videokall\\" + "capturedImage.png"))
                        {
                            File.Delete(folder + "\\Videokall\\" + "capturedImage.png");
                        }
                    }
                    catch (Exception ex)
                    {
                        if (!backgroundWorkerWrite.IsBusy)
                        {
                            backgroundWorkerWrite.RunWorkerAsync(string.Format(CommCommands.MIREXCEPTION, ex.Message));
                        }
                    }
                 
               
                if (videoSourcePlayer.IsRunning)
                {
                    Bitmap picture = videoSourcePlayer.GetCurrentVideoFrame();
                    picture.Save(folder + "\\Videokall\\" + "capturedImage.png", ImageFormat.Png);
                    if (!backgroundWorkerWrite.IsBusy)
                    {
                        backgroundWorkerWrite.RunWorkerAsync(string.Format(CommCommands.MIROSCOPEPIC, "capturedImage.png"));
                    }
                    LogMessage("taken pic");

                }
            }
            catch(Exception ex)
            {
                LogMessage("TakePic exception");
                LogMessage(ex.Message);
                if ( !backgroundWorkerWrite.IsBusy)
                {
                    backgroundWorkerWrite.RunWorkerAsync(string.Format(CommCommands.MIREXCEPTION, ex.Message));
                }
            }
        }


        void SaveImage(bool isDermascope , string PatientID)
        {
            try
            {
                string fname = string.Format("{0}_{1}_{2}_{3}.png", "oto", PatientID, DateTime.Now.Month.ToString() +
                     DateTime.Now.Day.ToString() + DateTime.Now.Year.ToString(), DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + DateTime.Now.Millisecond.ToString()); //oto_id_mm_dd_sec_ms

                if (isDermascope)
                    fname = string.Format("{0}_{1}_{2}_{3}.png", "der", PatientID, DateTime.Now.Month.ToString() +
                      DateTime.Now.Day.ToString() + DateTime.Now.Year.ToString(), DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + DateTime.Now.Millisecond.ToString()); //oto_id_mm_dd_sec_ms

                string folder = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
                if (File.Exists(folder + "\\Videokall" + "\\" + "capturedImage.png"))
                {
                    if (!Directory.Exists(folder + "\\Videokall" + "\\SavedImage"))
                    {
                        Directory.CreateDirectory(folder + "\\Videokall" + "\\SavedImage");
                    }

                    File.Move(folder + "\\Videokall" + "\\" + "capturedImage.png", folder + "\\Videokall" + "\\SavedImage\\" + fname);

                }

                if (!backgroundWorkerWrite.IsBusy)
                {
                    backgroundWorkerWrite.RunWorkerAsync(string.Format(CommCommands.NotifySAVEDIMAGE ));
                }

            }
            catch(Exception ex)
            {
                if (!backgroundWorkerWrite.IsBusy)
                {
                    backgroundWorkerWrite.RunWorkerAsync(string.Format(CommCommands.MIREXCEPTION, ex.ToString()));
                }
            }
        }


        private void BTNPIC_Click(object sender, EventArgs e)
        {
            TakePic();
        }

        void Stop()
        {
            videoSourcePlayer.SignalToStop();
            videoSourcePlayer.WaitForStop();
            this.WindowState = FormWindowState.Minimized;
        }
        void StopCamera()
        {
            videoSourcePlayer.BeginInvoke(new ReadCommFiledelegate(Stop));
            LogMessage("StopCamera");

        }
        private void BtnStop_Click(object sender, EventArgs e)
        {
            StopCamera();
        }

        private void CmbCameraList_SelectedIndexChanged(object sender, EventArgs e)
        {
            videoSourcePlayer.SignalToStop();
            videoSourcePlayer.WaitForStop();
        }

        private void videoSourcePlayer1_Click(object sender, EventArgs e)
        {
             
        }

        private void VKApp_FormClosing(object sender, FormClosingEventArgs e)
        {
            videoSourcePlayer.SignalToStop();
            videoSourcePlayer.WaitForStop();
            videoSourcePlayer.Dispose();
            videoDevices.Clear();
            videoDevices = null;
            videoDevices = null;
        }

     //   socketComm commsck;
        private void VKApp_Load(object sender, EventArgs e)
        {
            try
            {
                LoadCamera();

                CmmModule = new Commmodule();
                CmmModule.initialize();

                CmmModule.ReceivedMessage += ReceivedMessage;
                Watchdog = new System.Windows.Forms.Timer();
                Watchdog.Tick += Watchdog_Tick;
                Watchdog.Interval = 400;
                Watchdog.Start();
              
                this.WindowState = FormWindowState.Minimized;
                CmmModule.ReadCommFile();
                commtoMCC.ReceivedMessage += ReceivedMessage;

                commtoMCC.initialize();
                LoadSerialdeviceinfo();
                DisableSimulationMode(false);
            }
            catch(Exception ex)
            {
                LogMessage(ex.Message);
            }
        }

       
      public Commmodulemcc commtoMCC { get; set; } = new Commmodulemcc();
        delegate void ReadCommFiledelegate();

        void ReadFile()
        {
            CmmModule.ReadCommFile();
        }
        private void Watchdog_Tick(object sender, EventArgs e)
        {
            Watchdog.Stop();

            if (!backgroundWorkerReadFile.IsBusy)
                backgroundWorkerReadFile.RunWorkerAsync();

            Watchdog.Start();
        }

        private void backgroundWorkerReadFile_DoWork(object sender, DoWorkEventArgs e)
        {
            CmmModule.ReadCommFile();
        }

        private void backgroundWorkerWrite_DoWork(object sender, DoWorkEventArgs e)
        {
            string msg = (string)e.Argument;
            CmmModule.SendData( msg);
        }

        private void backgroundWorkerStopCamera_DoWork(object sender, DoWorkEventArgs e)
        {
            StopCamera();
        }
#endregion
        #region SPirometry
        Spriometry sp = new Spriometry();
        private void button2_Click(object sender, EventArgs e)
        {
            stopSpirometer = true;
            Stopspiro();
            listView3.Clear();
            listView2.Clear();
            listView1.Clear();
        }

        double[,] testValues = new double[200, 2];
        private void chart1_Click(object sender, EventArgs e)
        {
          
        }

        public bool stopSpirometer = false;
        List<string> readingData = new List<string>();

        void StartFVC(string msg)
        {
            try  
            {
                commwithChartapp.ConnectwithChartApp();
                commwithChartapp.SendMsg(string.Format(CommCommands.StartSpiroFVC, msg));
            }
            catch(Exception ex)
            {
                commtoMCC.SendData(string.Format(CommCommands.SpirostatusFailed, "comm"+ ex.Message));
            }

            try
            {
                stopSpirometer = false;
                label5.Text = "Startfvc";

                sp.OnComplete += OnFVCComplete;
                sp.OnFlowVolume += onVOLFlow;
                sp.OnVolumeTime += OnVolumeTimeRec;
                if (sp.StartFVCTest())
                    commtoMCC.SendData(CommCommands.Spirostatussuccess);
                else
                {
                    commtoMCC.SendData(string.Format(CommCommands.SpirostatusFailed, "Failed to connect Spirometry"));
                }
            }catch(Exception ex)
            {
                commtoMCC.SendData(string.Format(CommCommands.SpirostatusFailed, ex.Message));
            }

            }
        void StartVC(string msg)
        {
            try
            {
                commwithChartapp.ConnectwithChartApp();

                commwithChartapp.SendMsg(string.Format(CommCommands.StartSpiroVC, msg));
            }
            catch (Exception ex)
            {
                commtoMCC.SendData(string.Format(CommCommands.SpirostatusFailed, ex.Message));
            }

            try {
                stopSpirometer = false;
                label5.Text = "Startvc";
                sp.OnvcVolumeTime += onvcVOLTime;

              if(  sp.StartVCTest())
                    commtoMCC.SendData(CommCommands.Spirostatussuccess);
              else
                {
                    commtoMCC.SendData(string.Format(CommCommands.SpirostatusFailed, "Failed to connect Spirometry"));
                }
            } catch(Exception ex) {
                commtoMCC.SendData(string.Format(CommCommands.SpirostatusFailed, ex.Message));
            }
          
        }
        void Stopspiro()
        {
            try {
                label5.Text = "Stop";
                stopSpirometer = true;
                commwithChartapp.SendMsg(string.Format(CommCommands.StopSpiro));
                sp.StopTest();
                commtoMCC.SendData(CommCommands.StoppedSpiroMeter);
                clearspirodata();
            } catch(Exception ex) {
                commtoMCC.SendData(string.Format(CommCommands.SpirostatusFailed, "stop:" + ex.Message));
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            StartFVC("");
        }

        private void onvcVOLTime(double v, double t)
        {
            string[] arr = new string[2];
            arr[0] = v.ToString();
            arr[1] = t.ToString();
            if ( isDiagnosticMode)
            {
                if (listView3.Items.Count > 50)
                {
                    listView3.Items.Clear();
                }
                ListViewItem itm = new ListViewItem(arr);
                listView3.Items.Add(itm);
            }
        }

        private void onVOLFlow(float f, float v )
        {
            string[] arr = new string[2];
            arr[0] = f.ToString();
            arr[1] = v.ToString();
            if (isDiagnosticMode)
            {
                if (listView1.Items.Count > 50)
                    listView1.Items.Clear();
                ListViewItem itm = new ListViewItem(arr);
                listView1.Items.Add(itm);
            }
        }

        private void OnVolumeTimeRec(double v, double t)
        {
            string vol = string.Format("{0:0.00}", v);
            string time = string.Format("{0:0.00}", t);
            string s = vol + "," + time;
            string[] arr = new string[2];
            arr[0] = v.ToString();
            arr[1] = t.ToString();
                if ( isDiagnosticMode)
                {
                 if (listView2.Items.Count > 50)
                    listView2.Items.Clear();
                    ListViewItem itm = new ListViewItem(arr);
                    listView2.Items.Add(itm);
                }
         }

        private void OnFVCComplete(TrialSpiro e)
        { 
        } 
        private void backgroundWorkerspirometer_DoWork(object sender, DoWorkEventArgs e)
        {
          
        }

        private void button4_Click(object sender, EventArgs e)
        { 
            StartVC(""); 
        }

        void clearspirodata()
        {
            listView3.Clear();
            listView2.Clear();
            listView1.Clear();
        }
        private void listView3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            listView3.Clear();
            listView2.Clear();
            listView1.Clear();
        }

        void ShowHideControls(bool showHide)
        {
            if (showHide)
            { 
                StartCamera.Visible = true;
                BtnLoadCamera.Visible = true;
                BTNPIC.Visible = true;
                BtnStop.Visible = true;
                CmbCameraList.Visible = true;
            }
            else
            { 
                StartCamera.Visible = false;
                BtnLoadCamera.Visible = false;
                BTNPIC.Visible = false;
                BtnStop.Visible = false;
                CmbCameraList.Visible = false;
            }
        }

        public   bool isDiagnosticMode = false;
        private void checkBox1_CheckedChanged(object sender, EventArgs e)

        {
            CheckBox ob = (CheckBox)sender;
            if (ob.Checked)
            {
                isDiagnosticMode = true;
                StartCamera.Visible = true;
                BtnLoadCamera.Visible = true;
                BTNPIC.Visible = true;
                BtnStop.Visible = true;
                CmbCameraList.Visible = true;
                tabPage2.Enabled = true;
                ChkSimulation.Enabled = true;
            }
            else
            {
                isDiagnosticMode = false;
                StartCamera.Visible = false;
                BtnLoadCamera.Visible = false;
                BTNPIC.Visible = false;
                BtnStop.Visible = false;
                CmbCameraList.Visible = false;
                ChkSimulation.Enabled = false;
            }
        }

            #endregion
            #region CAS
       private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void TxtValue_TextChanged(object sender, EventArgs e)
        {

        }

        private void BtnPOD_Click(object sender, EventArgs e)
        {
            if (TxtValue.Text.Length > 0)
            {
                _serialDevice.DeployPod(TxtValue.Text.Trim()); 
            }
            else
                ResposeList.Invoke(new CallBackDelegatespiro(UpdateResponseUI), "pleae add id/value");
        }

         
        private void BtnSTLung_Click(object sender, EventArgs e)
        {
            if (TxtValue.Text.Length > 0)
            {
                _serialDevice.DeployStethoscopeLungs(TxtValue.Text.Trim()); 
            }
            else
                ResposeList.Invoke(new CallBackDelegatespiro(UpdateResponseUI), "pleae add id/value");
        }

        private void BtnSeatBack_Click(object sender, EventArgs e)
        {
            if (TxtValue.Text.Length > 0)
            {
                _serialDevice.SeatBack(TxtValue.Text.Trim()); 
            }
            else
                ResposeList.Invoke(new CallBackDelegatespiro(UpdateResponseUI), "pleae add id/value");

        }

        private void BtnSeatRecl_Click(object sender, EventArgs e)
        {
            if (TxtValue.Text.Length > 0)
            {
                _serialDevice.Recline(TxtValue.Text.Trim()); 
            }
            else
                ResposeList.Invoke(new CallBackDelegatespiro(UpdateResponseUI), "pleae add id/value");

        }

        private void BtnHM_Click(object sender, EventArgs e)
        {
            _serialDevice.HeightMeasure( );  
        }

        private void BtnWM_Click(object sender, EventArgs e)
        {
          _serialDevice.WeightMeasure(); 
        }

        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            _serialDevice.Serialports();
            CmbComport.DataSource = _serialDevice.Availableports;
            TxtBaudRate.Text = _serialDevice.BaudRate.ToString();
        }


        SerialDevice _serialDevice = new SerialDevice();
        void LoadSerialdeviceinfo()
        {
            try {
                _serialDevice.DataReceived += SerialPortDataReceived;
                
                _serialDevice.LoadConfig();
                BtnRefresh_Click(null, null);
                _serialDevice.InitializeSerialDevice(); 
            } catch (Exception) { }
           
        }

        void SerialPortDataReceived(string data)
        {
            SendCASMessageToMCC(data);
            ResposeList.Invoke(new CallBackDelegatespiro(UpdateResponseUI), data);
        }

        void SendCASMessageToMCC(string msg)
        {
            string res = string.Format(CommCommands.CASRES, msg);
            commtoMCC.SendData(res);
        }
        void UpdateResponseUI(string msg) {
            try {
                    if (ResposeList.Items.Count > 10)
                        ResposeList.Items.Clear();
                    ResposeList.Items.Add(msg); 

            } catch(Exception )
            {

            }
            
        }
        private void BtnSave_Click(object sender, EventArgs e)
        {
            _serialDevice.SaveConfig();

            _serialDevice.InitializeSerialDevice();
        }

        private void CmbComport_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedport = (string) CmbComport.SelectedItem;
            _serialDevice.SelectePortName = selectedport;
        }

        private void TxtBaudRate_TextAlignChanged(object sender, EventArgs e)
        {
          
        }

        
        private void TxtBaudRate_TextChanged(object sender, EventArgs e)
        {
            try {
               int test = Int32.Parse(TxtBaudRate.Text);
            } catch (Exception) {
                TxtBaudRate.Text = _serialDevice.BaudRate.ToString();
            }
        }

        private void BtnRetract_Click_1(object sender, EventArgs e)
        {
            if (TxtValue.Text.Length > 0)
                _serialDevice.Retract(TxtValue.Text);
            else
                ResposeList.Invoke(new CallBackDelegatespiro(UpdateResponseUI), "pleae add id/value");
        }

        private void BtnRetractPod_Click(object sender, EventArgs e)
        {
            if (TxtValue.Text.Length > 0)
                _serialDevice.RetractPod(TxtValue.Text);
            else
                ResposeList.Invoke(new CallBackDelegatespiro(UpdateResponseUI), "pleae add id/value");
        }

         private void BtnWT_Click(object sender, EventArgs e)
        {
            _serialDevice.TareWeight();
        }
       #endregion CAS
        #region CAS Simulation        
        void DisableSimulationMode( bool enable)
        {
            radSIMPod.Enabled = enable;
            RadSIMHM.Enabled = enable;
            RADSIMWM.Enabled = enable;
            RADSIMSEATBACK.Enabled = enable;
            RADSIMRECLINE.Enabled = enable;
            RADSIMST.Enabled = enable;
            TxtSIMValue.Enabled = enable;
            BTNSIMDACK.Enabled = enable;
            BTNSIMDGOOD.Enabled = enable;
            BTNSIMDBAD.Enabled = enable;
            BTNSIMRACK.Enabled = enable;
            RadSIMWT.Enabled = enable;
            BTNSIMRFAILED.Enabled = enable;
           // ChkSimulation.Enabled = enable;
        }

       
        private void BTNSIMDACK_Click(object sender, EventArgs e)
        {
            if (radSIMPod.Checked)
            {
                string value = string.Format(_serialDevice._ResDeployPodCmd, TxtSIMValue.Text,"D");
                SendCASMessageToMCC(value);
                lblResponse.Text = value;
            }
            else if(RadSIMHM.Checked)
            {
                string value = string.Format(_serialDevice._ResHM, "", "");
                SendCASMessageToMCC(value);
                lblResponse.Text = value;
            }
            else if (RADSIMWM.Checked)
            {
                string value = string.Format(_serialDevice._ResWM, "", "");
                SendCASMessageToMCC(value);
                lblResponse.Text = value;
            }
             
            else if (RADSIMSEATBACK.Checked)
            {
                string value = string.Format(_serialDevice._ResSeatBackCmd, TxtSIMValue.Text,"");
                SendCASMessageToMCC(value);
                lblResponse.Text = value;
            }
            else if (RADSIMRECLINE.Checked)
            {
                string value = string.Format(_serialDevice._ResReclineCmd, TxtSIMValue.Text,"");
                 
                SendCASMessageToMCC(value);
                lblResponse.Text = value;
            }
            else if (RADSIMST.Checked)
            {
                string value = string.Format(_serialDevice._ResStethoscopeLungsCmd, TxtSIMValue.Text,"D");

                SendCASMessageToMCC(value);
                lblResponse.Text = value;
            }
            else if (RadSIMWT.Checked)
            {
                string value = string.Format(_serialDevice._ResWT,"" );

                SendCASMessageToMCC(value);
                lblResponse.Text = value;
            }
            
        }
        private void ChkSimulation_CheckedChanged(object sender, EventArgs e)
        {
            if (ChkSimulation.Checked)
            {
                DisableSimulationMode(true);
            }
            else { DisableSimulationMode(false); }
        }
        #endregion

        private void BTNSIMDGOOD_Click(object sender, EventArgs e)
        {
            if (radSIMPod.Checked)
            {
                string value = string.Format(_serialDevice._ResDeployPodCmd, TxtSIMValue.Text, "DG");
                SendCASMessageToMCC(value);
                lblResponse.Text = value;
            }
            else if (RadSIMHM.Checked)
            {
                string value = string.Format(_serialDevice._ResHM, TxtSIMValue.Text, "G");
                SendCASMessageToMCC(value);
                lblResponse.Text = value;
            }
            else if (RADSIMWM.Checked)
            {
                string value = string.Format(_serialDevice._ResWM, TxtSIMValue.Text, "G");
                SendCASMessageToMCC(value);
                lblResponse.Text = value;
            }

            else if (RADSIMSEATBACK.Checked)
            {
                string value = string.Format(_serialDevice._ResSeatBackCmd, TxtSIMValue.Text, "G");
                SendCASMessageToMCC(value);
                lblResponse.Text = value;
            }
            else if (RADSIMRECLINE.Checked)
            {
                string value = string.Format(_serialDevice._ResReclineCmd, TxtSIMValue.Text, "G");

                SendCASMessageToMCC(value);
                lblResponse.Text = value;
            }
            else if (RADSIMST.Checked)
            {
                string value = string.Format(_serialDevice._ResStethoscopeLungsCmd, TxtSIMValue.Text, "DG");

                SendCASMessageToMCC(value);
                lblResponse.Text = value;
            }
            else if (RadSIMWT.Checked)
            {
                string value = string.Format(_serialDevice._ResWT, "G");

                SendCASMessageToMCC(value);
                lblResponse.Text = value;
            }
        }

        private void RADSIMWM_CheckedChanged(object sender, EventArgs e)
        {
            if(RADSIMWM.Checked)
            lblUnit.Text = "decagrams";
            else
                lblUnit.Text = "";
        }

        private void radWT_Enter(object sender, EventArgs e)
        {

        }

        private void RadSIMHM_CheckedChanged(object sender, EventArgs e)
        {
            if (RadSIMHM.Checked)
                lblUnit.Text = "centimetres";
            else
                lblUnit.Text = "";
        }

        private void BTNSIMDBAD_Click(object sender, EventArgs e)
        {
            if (radSIMPod.Checked)
            {
                string value = string.Format(_serialDevice._ResDeployPodCmd, TxtSIMValue.Text, "DB");
                SendCASMessageToMCC(value);
                lblResponse.Text = value;
            }
            else if (RadSIMHM.Checked)
            {
                string value = string.Format(_serialDevice._ResHM, TxtSIMValue.Text, "B");
                SendCASMessageToMCC(value);
                lblResponse.Text = value;
            }
            else if (RADSIMWM.Checked)
            {
                string value = string.Format(_serialDevice._ResWM, TxtSIMValue.Text, "B");
                SendCASMessageToMCC(value);
                lblResponse.Text = value;
            }

            else if (RADSIMSEATBACK.Checked)
            {
                string value = string.Format(_serialDevice._ResSeatBackCmd, TxtSIMValue.Text, "B");
                SendCASMessageToMCC(value);
                lblResponse.Text = value;
            }
            else if (RADSIMRECLINE.Checked)
            {
                string value = string.Format(_serialDevice._ResReclineCmd, TxtSIMValue.Text, "B");

                SendCASMessageToMCC(value);
                lblResponse.Text = value;
            }
            else if (RADSIMST.Checked)
            {
                string value = string.Format(_serialDevice._ResStethoscopeLungsCmd, TxtSIMValue.Text, "DB");

                SendCASMessageToMCC(value);
                lblResponse.Text = value;
            }
            else if (RadSIMWT.Checked)
            {
                string value = string.Format(_serialDevice._ResWT, "B");

                SendCASMessageToMCC(value);
                lblResponse.Text = value;
            }
        }

        private void BTNSIMRACK_Click(object sender, EventArgs e)
        {
            if (radSIMPod.Checked)
            {
                string value = string.Format(_serialDevice._ResDeployPodCmd, TxtSIMValue.Text, "RG");
                SendCASMessageToMCC(value);
                lblResponse.Text = value;
            }
            else if (RADSIMST.Checked)
            {
                string value = string.Format(_serialDevice._ResStethoscopeLungsCmd, TxtSIMValue.Text, "RG");

                SendCASMessageToMCC(value);
                lblResponse.Text = value;
            }
        }

        private void BTNSIMRFAILED_Click(object sender, EventArgs e)
        {
            if (radSIMPod.Checked)
            {
                string value = string.Format(_serialDevice._ResDeployPodCmd, TxtSIMValue.Text, "RB");
                SendCASMessageToMCC(value);
                lblResponse.Text = value;
            }
            else if (RADSIMST.Checked)
            {
                string value = string.Format(_serialDevice._ResStethoscopeLungsCmd, TxtSIMValue.Text, "RB");

                SendCASMessageToMCC(value);
                lblResponse.Text = value;
            }
        }
    }
}
