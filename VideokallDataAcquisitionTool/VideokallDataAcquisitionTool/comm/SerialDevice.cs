using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideokallDataAcquisitionTool.comm
{

    class SerialDevice
    {
        public readonly string _StethoscopeLungsCmd = "<S{0}D>";
        public readonly string _RectractCmd = "<S{0}R>";
        public readonly string _DeployPodCmd = "<P{0}D>";
        public readonly string _RetractPodCmd = "<P{0}R>";
        public readonly string _SeatBackCmd = "<B{0}>";
        public readonly string _ReclineCmd = "<R{0}>";
        public readonly string _HM = "<HM>";
        public readonly string _WM = "<WM>";
        public readonly string _WT = "<WT>";
        //response format
        public readonly string _ResStethoscopeLungsCmd = "[S{0}{1}]";
        public readonly string _ResDeployPodCmd = "[P{0}{1}]";
        public readonly string _ResSeatBackCmd = "[B{0}{1}]";
        public readonly string _ResReclineCmd = "[R{0}{1}]";
        public readonly string _ResHM = "[HM{0}{1}]";
        public readonly string _ResWM = "[WM{0}{1}]";
        public readonly string _ResWT = "[WT{0}]";

        SerialPort _serialPort;
       public delegate void ResponseReceived(string data);
        public ResponseReceived DataReceived;
        public string[] Availableports { get; set; } 

        public void Serialports()
        {
            Availableports = SerialPort.GetPortNames();
        }
         
        public int BaudRate { get; set; } = 115200;
        public string SelectePortName { get; set; }

        string configFileName = "SerialDeviceConfig.txt";
        string configformat = "Portname:{0}>BaudRate:{1}>";
        public void SaveConfig()
        {
            string config = string.Format(configformat, SelectePortName.Trim(), BaudRate);
            using (System.IO.StreamWriter file =
            new System.IO.StreamWriter(configFileName, false))
            { 
                file.WriteLine(config);
            }
        }

        public void InitializeSerialDevice()
        {
            try {
                _serialPort = new SerialPort();
                _serialPort.BaudRate = BaudRate;
                if (SelectePortName.Length == 0 && Availableports.Count() == 1)
                    SelectePortName = Availableports[0];

                _serialPort.PortName = SelectePortName;
                _serialPort.Encoding = Encoding.ASCII;

                _serialPort.DataReceived += new SerialDataReceivedEventHandler(_serialPort_DataReceived);
                _serialPort.Open();
            } catch (Exception ex) {
                string str = ex.Message; }
            
        }

        public void DeployPod(string cmd)
        {
           string msg = string.Format(_DeployPodCmd, cmd);
            DataReceived?.Invoke("Cmd sent: " + msg);
            WriteData(msg);
        }
        public void RetractPod(string cmd)
        {
            string msg = string.Format(_RetractPodCmd, cmd);
            DataReceived?.Invoke("Cmd sent: " + msg);
            WriteData(msg);
        }

        public void TareWeight()
        {
            DataReceived?.Invoke("Cmd sent: " + _WT);
            WriteData(_WT);
        }
        public void DeployStethoscopeLungs(string cmd)
        {
            string msg = string.Format(_StethoscopeLungsCmd, cmd);
            DataReceived?.Invoke("Cmd sent: " + msg);
            WriteData(msg);
        }

        public void SeatBack(string cmd)
        {
            string msg = string.Format(_SeatBackCmd, cmd);
            DataReceived?.Invoke("Cmd sent: " + msg);
            WriteData(msg);
        }

        public void Recline(string cmd)
        {
            string msg = string.Format(_ReclineCmd, cmd);
            DataReceived?.Invoke("Cmd sent: " + msg);
            WriteData(msg);
        }

        public void HeightMeasure()
        {
            DataReceived?.Invoke("Cmd sent: " + _HM);
            WriteData(_HM);
        }

        public void WeightMeasure()
        {
            DataReceived?.Invoke("Cmd sent: " + _WM);
            WriteData(_WM);
        }
        public void Retract(string cmd)
        {
            
            string msg = string.Format(_RectractCmd, cmd);
            DataReceived?.Invoke("Cmd sent: " + msg);
            WriteData(msg);
           
        }
        public void WriteData(string cmd)
        {
            char[] data = cmd.ToCharArray();
            if(_serialPort.IsOpen)
            _serialPort.Write(data,0,data.Length);
        }
        private void _serialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            string rec = _serialPort.ReadExisting();
            DataReceived?.Invoke(rec);
        }

        public void LoadConfig()
        {
            Serialports();
            if (!File.Exists(configFileName))
                return;
            try {

                string str = System.IO.File.ReadAllText(configFileName);
                string[] congigs = str.Split('>');
                SelectePortName = congigs[0].Split(':')[1];
                BaudRate = Convert.ToInt32(congigs[1].Split(':')[1]);

            } catch(Exception ex) { string s = ex.Message; }
           
        }
    }
}
