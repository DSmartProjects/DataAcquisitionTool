using MirDataTypes;
using MirDeviceManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 
using System.IO.MemoryMappedFiles;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Diagnostics;
using VideokallSBCDataAcquisionApp.comm;
using VideokallDataAcquisitionTool;

namespace VideokallSBCDataAcquisionApp.Spirometer
{
   
    class FvcFlowVolume
    {
        public TrialTypeCode TrialType;
        public DateTime DateAndTime;
        public int ParametersCount;
        public int CurvePointsCount;
        public int Parameters;
        public TrialSubTypeCode TrialSubType;
        public double Time;
        public double Volume;
        public float flow;
    }
   
    class Spriometry
    {
        private UsbManager USB;
        private MirDevice SpDevice;
        private MirVirtualDevice myVirtualDevice;
      //  public Queue<FvcFlowVolume> FvcFlowVolumeQueue = new Queue<FvcFlowVolume>();
        public delegate void OnFvcComplete(TrialSpiro e);
        public delegate void OnFvcVolumeTime(double v, double t);
        public delegate void OnFvcFlowVolume(float f, float v);
        public OnFvcComplete OnComplete;
        public OnFvcVolumeTime OnVolumeTime;
        public OnFvcFlowVolume OnFlowVolume;
        public OnFvcVolumeTime OnvcVolumeTime;

        public Spriometry()
        {
           // USB = UsbManager.GetInstance();
        }

        public bool StartVCTest()
        {
            try
            {
                USB = UsbManager.GetInstance();
                SpDevice = USB.GetDeviceConnected();
            }

            catch (Exception ex)
            {
                string s = ex.Message;
                LogMessage(ex.Message);
                  VKApp.mainApp.commtoMCC.SendData(string.Format(CommCommands.SpirostatusFailed,"USB"+ ex.Message));
                return false;
            
            }


            //  SpDevice.OnFvcFlowVolume += SpDevice_OnFvcFlowVolume;
            //  SpDevice.OnFVCVolumeTime += SpDevice_OnFvcVolumeTime;
            // SpDevice.OnFvcComplete += SpDevice_OnFvcComplete;
            try
            {
                SpDevice.OnVcVentilatoryProfile += SpDevice_OnVcVentilatoryProfile;
                SpDevice.OnVcVolumeTime += SpDevice_OnVcVolumeTime;
                SpDevice.OnVcComplete += SpDevice_OnVcComplete;
                SpDevice.StartTest(Test.VC, Turbine.Disposable);
            }
            catch(Exception ex)
            {
                LogMessage(ex.Message);
                VKApp.mainApp.commtoMCC.SendData(string.Format(CommCommands.SpirostatusFailed, "ST" + ex.Message));
            }

            return true;
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
            }
            catch (Exception ex)
            {

            }

        }

        public bool StartFVCTest()
        {
            try
            {
                USB = UsbManager.GetInstance();
                SpDevice = USB.GetDeviceConnected();
            }

            catch (Exception ex)
            {
                string s = ex.Message;
                LogMessage(ex.Message);
                VKApp.mainApp.commtoMCC.SendData(string.Format(CommCommands.SpirostatusFailed, "FVC-USB" + ex.Message));
                return false;
            }

            try
            {
                SpDevice.OnFvcFlowVolume += SpDevice_OnFvcFlowVolume;
                SpDevice.OnFVCVolumeTime += SpDevice_OnFvcVolumeTime;
                SpDevice.OnFvcComplete += SpDevice_OnFvcComplete; 
                SpDevice.StartTest(Test.FVC, Turbine.Disposable);
            }
            catch(Exception ex)
            {
                VKApp.mainApp.commtoMCC.SendData(string.Format(CommCommands.SpirostatusFailed, "FVC-ST" + ex.Message));
                LogMessage(ex.Message);
               // return false;
            }
            return true;
          
        }

        
        private void SpDevice_OnVcComplete(object sender, TrialSpiro res)
        {
            VKApp.mainApp.stopSpirometer = true;
            string Btps = res.Btps.ToString();
            string CreationDate = res.CreationDate.ToString();
            string TempCelsius = res.TempCelsius.ToString();
            string TempFahrenheit = res.TempFahrenheit.ToString();
            string header =  string.Format("{0}!{1}!{2}!{3}!", Btps, CreationDate, TempCelsius, TempFahrenheit);

           
            //string zScoreFormula = res.Parameters.zScoreFormula.tos
            //string UlnValue = res.Parameters.UlnValue;
            //string PredictedValue = res.Parameters.PredictedValue
            //string MeasuredValuePercPred = res.Parameters.MeasuredValuePercPred;
            //string LlnValue = res.Parameters.LlnValue;
            string strparameters = "";
            foreach (var parm in res.Parameters)
            {             
                string Code = parm.Code.ToString();
                string MeasureUnit = parm.MeasureUnit.ToString();
                string MeasuredValue = parm.MeasuredValue.ToString();
                string Name = parm.Name;
                string ParameterType = parm.ParameterType.ToString();
                strparameters+=  string.Format("{0}!{1}!{2}!{3}!@", Code, MeasureUnit, MeasuredValue, Name, ParameterType);
             }

            VKApp.mainApp.commtoMCC.SendData(string.Format(CommCommands.SpiroVCResults, header, strparameters));

         
            Debug.Print(res.CurvePoints.Count().ToString());
        }

        private void SpDevice_OnVcVolumeTime(object sender, VolumeAndTimeArgs e)
        {
            float v = (float)e.Volume;
            float t = (float)e.Time;

            string vol = string.Format("{0:0.00}", v);
            string time = string.Format("{0:0.00}", t);
            string s = vol + "," + time;

            if (!VKApp.mainApp.stopSpirometer)
            {
                VKApp.mainApp.commwithChartapp.SendMsg(string.Format(CommCommands.SpirometerVC, s));
                VKApp.mainApp.commtoMCC.SendData(string.Format(CommCommands.SpirometerVC, s));
            }

            if (VKApp.mainApp.isDiagnosticMode)
                OnvcVolumeTime?.Invoke(e.Volume, e.Time);
        }

        private void SpDevice_OnVcVentilatoryProfile(object sender, EventArgs e)
        {
            Debug.Print("YYY");
        }

        private void SpDevice_OnFvcComplete(object sender, TrialSpiro res)
        {
            VKApp.mainApp.stopSpirometer = true;
            string Btps = res.Btps.ToString();
            string CreationDate = res.CreationDate.ToString();
            string TempCelsius = res.TempCelsius.ToString();
            string TempFahrenheit = res.TempFahrenheit.ToString();
            string header = string.Format("{0}!{1}!{2}!{3}!", Btps, CreationDate, TempCelsius, TempFahrenheit);

            string strparameters = "";
            foreach (var parm in res.Parameters)
            {
                string Code = parm.Code.ToString();
                string MeasureUnit = parm.MeasureUnit.ToString();
                string MeasuredValue = parm.MeasuredValue.ToString();
                string Name = parm.Name;
                string ParameterType = parm.ParameterType.ToString();
                strparameters += string.Format("{0}!{1}!{2}!{3}!@", Code, MeasureUnit, MeasuredValue, Name, ParameterType);
            }

            VKApp.mainApp.commtoMCC.SendData(string.Format(CommCommands.SpiroFVCResults, header, strparameters));

            //FvcFlowVolume fvcv = new FvcFlowVolume();
            //fvcv.TrialType = e.TrialType;
            //fvcv.DateAndTime = e.DateAndTime;
            //fvcv.CurvePointsCount = e.CurvePoints.Count;
            //fvcv.ParametersCount = e.Parameters.Count;
            //fvcv.TrialSubType =  e.TrialSubType;
            //FvcFlowVolumeQueue.Enqueue(fvcv);
            //if (OnComplete != null)
            //    OnComplete(e);
             
        }

        private void SpDevice_OnFvcVolumeTime(object sender, VolumeAndTimeArgs e)
        {
            FvcFlowVolume fvcv = new FvcFlowVolume();
            fvcv.Time = e.Time;
            fvcv.Volume = e.Volume;

            string vol = string.Format("{0:0.00}", fvcv.Volume);
            string time = string.Format("{0:0.00}", fvcv.Time);

            string s = vol + "," + time;
            if (!VKApp.mainApp.stopSpirometer)
            {
                VKApp.mainApp.commwithChartapp.SendMsg(string.Format(CommCommands.SpirometerFVCvtdata, s));
                VKApp.mainApp.commtoMCC.SendData(string.Format(CommCommands.SpirometerFVCvtdata, s));
            }
            //  FvcFlowVolumeQueue.Enqueue(fvcv);
            if (VKApp.mainApp.isDiagnosticMode)
            OnVolumeTime?.Invoke(e.Volume, e.Time);
        }

        private void SpDevice_OnFvcFlowVolume(object sender, FlowAndVolumeArgs e)
        {
            FvcFlowVolume fvcv = new FvcFlowVolume();
            fvcv.Volume = e.Volume;
            fvcv.flow = e.Flow;

            string flow = string.Format("{0:0.00}", fvcv.flow);
            string vol = string.Format("{0:0.00}", fvcv.Volume );
            string s = flow + "," + vol;
            if (!VKApp.mainApp.stopSpirometer)
            {
                VKApp.mainApp.commwithChartapp.SendMsg(string.Format(CommCommands.SpirometerFVCdata, s));
                VKApp.mainApp.commtoMCC.SendData(string.Format(CommCommands.SpirometerFVCdata, s));

            }
            //   FvcFlowVolumeQueue.Enqueue(fvcv);
            if (VKApp.mainApp.isDiagnosticMode)
                OnFlowVolume?.Invoke(e.Flow, e.Volume);
        }

        public void StopTest()
        {if(SpDevice != null)
            SpDevice.StopTest();
            SpDevice = null;
        }

       
    }
    class Params
    {
        public MirDataTypes.ParameterCode Code;
        public double LlnValue;
        public MirDataTypes.MeasureUnitCode MeasureUnit;
        public double MeasuredValue;
        public double MeasuredValuePercPred;
        public MirDataTypes.ParameterType  ParameterType  ;
        public double PredictedValue;
        public double UlnValue;
        public string zScoreFormula;
        public double zScoreValue;
        public string Name;
          
    }


}
