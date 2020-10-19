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
        private Queue<FvcFlowVolume> FvcFlowVolumeQueue = new Queue<FvcFlowVolume>();
        public delegate void OnFvcComplete(TrialSpiro e);
        public delegate void OnFvcVolumeTime(double v, double t);
        public delegate void OnFvcFlowVolume(float f, float v);
        public OnFvcComplete OnComplete;
        public OnFvcVolumeTime OnVolumeTime;
        public OnFvcFlowVolume OnFlowVolume;
        public Spriometry()
        {
            USB = UsbManager.GetInstance();
        }

        public void StartFVCTest()
        {
            try
            {
                

                SpDevice = USB.GetDeviceConnected();
            }

            catch (Exception ex)
            {
                string s = ex.Message;
               // MessageBox.Show(ex.Message);
                return;
            }


            SpDevice.OnFvcFlowVolume += SpDevice_OnFvcFlowVolume;
            SpDevice.OnFVCVolumeTime += SpDevice_OnFvcVolumeTime;
            SpDevice.OnFvcComplete += SpDevice_OnFvcComplete;
            SpDevice.StartTest(Test.FVC, Turbine.Disposable);
        }

        private void SpDevice_OnFvcComplete(object sender, TrialSpiro e)
        {

           FvcFlowVolume fvcv = new FvcFlowVolume();
            fvcv.TrialType = e.TrialType;
            fvcv.DateAndTime = e.DateAndTime;
            fvcv.CurvePointsCount = e.CurvePoints.Count;
            fvcv.ParametersCount = e.Parameters.Count;
            fvcv.TrialSubType =  e.TrialSubType;
            FvcFlowVolumeQueue.Enqueue(fvcv);
            if (OnComplete != null)
                OnComplete(e);
            // e.TrialType
            //lstCodLast.Items.Add(e.TrialType);
            //  lstCodLast.Items.Add(e.DateAndTime);
            //lstCodLast.Items.Add(e.CurvePoints.Count);
            //lstCodLast.Items.Add(e.Parameters.Count);
            // lstCodLast.Items.Add(e.TrialSubType);
        }

        private void SpDevice_OnFvcVolumeTime(object sender, VolumeAndTimeArgs e)
        {
            FvcFlowVolume fvcv = new FvcFlowVolume();
            fvcv.Time = e.Time;
            fvcv.Volume = e.Volume;
            FvcFlowVolumeQueue.Enqueue(fvcv);
            OnVolumeTime?.Invoke(e.Volume, e.Time);
        }

        private void SpDevice_OnFvcFlowVolume(object sender, FlowAndVolumeArgs e)
        {
            FvcFlowVolume fvcv = new FvcFlowVolume();
            fvcv.Volume = e.Volume;
            fvcv.flow = e.Flow;
            FvcFlowVolumeQueue.Enqueue(fvcv);
            OnFlowVolume?.Invoke(e.Flow, e.Volume);
        }

        public void StopTest()
        {
            SpDevice.StopTest();
        }

       
    }

    
}
