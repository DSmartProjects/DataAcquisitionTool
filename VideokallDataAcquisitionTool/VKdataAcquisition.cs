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
        FilterInfo[] videoCamList;//= new FilterInfo[2] ;
        System.Windows.Forms.Timer Watchdog = null;
        Commmodule CmmModule = null;
        delegate void CallBackDelegate();
        delegate void CallBackDelegatespiro(string txt);
        public  CommwithChartApp commwithChartapp = new CommwithChartApp();
        public VKApp()
        {
            InitializeComponent();
              mainApp = this;
            //StartCamera.Visible = false;
            //// BtnLoadCamera.Visible = true;
            //BTNPIC.Visible = false;
            //BtnStop.Visible = false;

            ShowHideControls(false);
        }

      

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
                    //else if (showhide == 0)
                    //{
                    //    StopCamera();
                    //    this.WindowState = FormWindowState.Minimized;
                    //}
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
                            //int height = Convert.ToInt32(cmd[2].Split(':')[1]);
                            //int width = Convert.ToInt32(cmd[3].Split(':')[1]);
                            //int mainscWidht = Convert.ToInt32(cmd[4].Split(':')[1]);
                            //int mainscheight = Convert.ToInt32(cmd[5].Split(':')[1]);
                            //this.Width = mainscWidht / 2;
                            //this.Height = mainscheight - 30;
                            //this.Left = mainscWidht - mainscWidht / 2;
                            //this.Top = 60;
                            //this.TopMost = true;
                            //videoSourcePlayer.Height = tabPage1.Height;
                            //videoSourcePlayer.Width = tabPage1.Width;
                            //this.Location = new Point(mainscWidht - mainscWidht / 2, 70);
                            //this.WindowState = FormWindowState.Normal;
                            //  StartMicorCamera(1);
                            if (!backgroundWorkerStartCamera.IsBusy)
                                backgroundWorkerStartCamera.RunWorkerAsync(msg);
                            LogMessage(msg);
                        }
                        //else if (showhide == 0)
                        //{

                        //    this.WindowState = FormWindowState.Minimized;
                        //}
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
                  //  commtoMCC.readData();
                    break;
            }
         //   CmmModule.readData();
        }

       
      void  StartST()
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
                // CmmModule.SendData(string.Format(CommCommands.MIREXCEPTION, ex.Message));
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
            LogMessage("*****load camera*****");

            LoadCamera();
            LogMessage("*****end load camera *****");
        }

        void StartMicorCamera(int indx)
        {
            if (indx < 0)
                return;
            LogMessage("*****   start    camera*****");
            try
            {                 
                if (videoCamList!= null && videoCamList.Count() > indx && videoCamList[indx] != null)
                {
                    VideoCaptureDevice videoCaptureSource = new VideoCaptureDevice(videoCamList[indx].MonikerString);
                    videoSourcePlayer.VideoSource = videoCaptureSource;
                    videoSourcePlayer.Start();
                    LogMessage("*****Started camera*****");
                }
                else
                {
                    LogMessage("*****no camera*****");
                    // CmmModule.SendData(string.Format(CommCommands.MIREXCEPTION, videoCamList.Count() == 0 ? "No device Connected" : "Please connect the device"));
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
                //  CmmModule.SendData(string.Format(CommCommands.MIREXCEPTION, ex.ToString()));
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
           new System.IO.StreamWriter("logs.txt", true))
                {
                    file.WriteLine(DateTime.Now.ToString() + ": " + msg);
                }
            }catch(Exception ex)
            {

            }
           
        }
        void StartMicroCamera()
        {
            try
            {
                LogMessage("****start Camera*****");
                int index = CmbCameraList.SelectedIndex;
                VideoCaptureDevice videoCaptureSource = new VideoCaptureDevice(videoCamList[index].MonikerString);
                videoSourcePlayer.VideoSource = videoCaptureSource;
                videoSourcePlayer.Start();
            }catch(Exception ex)
            {
                LogMessage(ex.Message);
               // CmmModule.SendData(string.Format(CommCommands.MIREXCEPTION, ex.Message));
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

                    //   CmmModule.SendData(string.Format(CommCommands.MIROSCOPEPIC, "capturedImage.png"));
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

                //  CmmModule.SendData(string.Format(CommCommands.MIREXCEPTION, ex.Message));
               if( !backgroundWorkerWrite.IsBusy)
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
            //videoSourcePlayer.SignalToStop();
            //videoSourcePlayer.WaitForStop();
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
           // int index = CmbCameraList.SelectedIndex;
          //  VideoCaptureDevice videoCaptureSource = new VideoCaptureDevice(videoDevices[index].MonikerString);
          //  videoSourcePlayer.VideoSource = videoCaptureSource;
            
        }

        private void videoSourcePlayer1_Click(object sender, EventArgs e)
        {
             
        }

        private void VKApp_FormClosing(object sender, FormClosingEventArgs e)
        {
         //  commtoMCC.Reset();
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
                //  CmmModule.SendData("");
                //  CmmModule.readData();
              
                this.WindowState = FormWindowState.Minimized;
                CmmModule.ReadCommFile();
                commtoMCC.ReceivedMessage += ReceivedMessage;

                commtoMCC.initialize();
              
                ///
              //  commsck = new socketComm();
                //commsck.initialize();
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
            //if (this.InvokeRequired)
            //    this.BeginInvoke(new ReadCommFiledelegate(ReadFile));

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
           
            // fill data series
            //for (int i = 0; i < 10; i++)
            //{
            //    testValues[i, 0] = i; // X values
            //    testValues[i, 1] = Math.Sin(i / 18.0 * Math.PI); // Y values
            //}
            // add new data series to the chart
        //    chart1.AddDataSeries("Test", Color.DarkGreen, Chart.SeriesType.Line, 3);
            // set X range to display
        //    chart1.RangeX = new AForge.Range(-12,12);
            // update the chart
         //   chart1.UpdateDataSeries("Test", testValues);
        }

        

        public bool stopSpirometer = false;
        bool testmode = true;
      //  public OnFvcComplete OnComplete;
      //  public OnFvcVolumeTime OnVolumeTimeRec;
     //   public OnFvcFlowVolume OnFlowVolume;
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
                //sp.StopTest();
                label5.Text = "Startfvc";

                sp.OnComplete += OnFVCComplete;
                sp.OnFlowVolume += onVOLFlow;
                sp.OnVolumeTime += OnVolumeTimeRec;

                //      sp.OnvcVolumeTime += onvcVOLFlow;

                if (sp.StartFVCTest())
                    commtoMCC.SendData(CommCommands.Spirostatussuccess);
                else
                {
                    commtoMCC.SendData(string.Format(CommCommands.SpirostatusFailed, "Failed to connect Spirometry"));
                  //  stopSpirometer = true;
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
              //  sp.StopTest();
                label5.Text = "Startvc";
                sp.OnvcVolumeTime += onvcVOLTime;

              if(  sp.StartVCTest())
                    commtoMCC.SendData(CommCommands.Spirostatussuccess);
              else
                {
                    commtoMCC.SendData(string.Format(CommCommands.SpirostatusFailed, "Failed to connect Spirometry"));
                  //  stopSpirometer = false;
                }
                   
                UpdateDatainUI();

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
            //string vol = string.Format("{0:0.00}", v);
            //string time = string.Format("{0:0.00}", t);
            //string s = vol + "," + time;
            //if (!stopSpirometer)
            //{
            //    commwithChartapp.SendMsg(string.Format(CommCommands.SpirometerVC, s));
            //    commtoMCC.SendData(string.Format(CommCommands.SpirometerVC, s));
            //}
           
            string[] arr = new string[2];

            arr[0] = v.ToString();
            arr[1] = t.ToString();
            if ( isDiagnosticMode)
            {
                ListViewItem itm = new ListViewItem(arr);
                listView3.Items.Add(itm);
            }

        }

        private void onVOLFlow(float f, float v )
        {

            //string flow = string.Format("{0:0.00}", f);
            //string vol = string.Format("{0:0.00}", v);
            //string s = flow + "," + vol;
            //if(!stopSpirometer)
            //{
            //    commwithChartapp.SendMsg(string.Format(CommCommands.SpirometerFVCdata, s));
            //    commtoMCC.SendData(string.Format(CommCommands.SpirometerFVCdata, s));

            //}

            string[] arr = new string[2];

            arr[0] = f.ToString();
            arr[1] = v.ToString();
            if (!isDiagnosticMode)
            {
                ListViewItem itm = new ListViewItem(arr);

                listView1.Items.Add(itm);
            }


        }

        private void OnVolumeTimeRec(double v, double t)
        {

            string vol = string.Format("{0:0.00}", v);
            string time = string.Format("{0:0.00}", t);
            string s = vol + "," + time;
            //if(!stopSpirometer)
            //{
            //    commwithChartapp.SendMsg(string.Format(CommCommands.SpirometerFVCvtdata, s));
            //    commtoMCC.SendData(string.Format(CommCommands.SpirometerFVCvtdata, s));
            //}

            string[] arr = new string[2];

            arr[0] = v.ToString();
            arr[1] = t.ToString();
            if ( isDiagnosticMode)
            {
                ListViewItem itm = new ListViewItem(arr);
                listView2.Items.Add(itm);

            }
                //   label2.Text = s;
            }

        private void OnFVCComplete(TrialSpiro e)
        {
            string s = "c";
        }

        void UpdateDatainUI( )
        {
            
        }
        private void backgroundWorkerspirometer_DoWork(object sender, DoWorkEventArgs e)
        {
          
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //sp.OnComplete += OnFVCComplete;
            //  sp.OnFlowVolume += OnVolumeTimeRec;
            //  sp.OnVolumeTime += onVOLFlow;

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
               // tabPage2.Enabled = true;
                StartCamera.Visible = true;
                BtnLoadCamera.Visible = true;
                BTNPIC.Visible = true;
                BtnStop.Visible = true;
                CmbCameraList.Visible = true;
            }
            else
            {
               // tabPage2.Enabled = false;
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
               // tabPage2.Enabled = true;
                isDiagnosticMode = true;
                StartCamera.Visible = true;
                 BtnLoadCamera.Visible = true;
                BTNPIC.Visible = true;
                BtnStop.Visible = true;
                CmbCameraList.Visible = true;
            }
            else
            {
              //  tabPage2.Enabled = false;
                isDiagnosticMode = false;
                StartCamera.Visible = false;
                  BtnLoadCamera.Visible = false;
                BTNPIC.Visible = false;
                BtnStop.Visible = false;
                CmbCameraList.Visible = false;
            }
        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }
    }
}
