using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideokallSBCDataAcquisionApp.comm
{
   public static class CommCommands
    { 
        public static readonly string DATAACQUISTIONAPPCONNECTION = "<APPC>"; //dataacquistion Connection
        public static readonly string DATAACQSTATUS = "<APPS>"; //dataacquistion Status
        public static readonly string DERMASACOPE = "DER>SH:{0}>H:{1}>W:{2}>X:{3}>Y:{4}>";

        public static readonly string OTOSCOPE = "OTO>SH:{0}>H:{1}>W:{2}>X:{3}>Y:{4}>";

        public static readonly string TAKEPIC = "<PIC>";
        public static readonly string STARTDERMO = "<startdermo>";
        public static readonly string STOPDERMO = "<stopdermo>";
        public static readonly string STARTOTOSCOPE = "<startoto>";
  
        public static readonly string STOPOTOSCOPE = "<stopoto>";

        public static readonly string STPIC = "STPIC>{0}>";
        public static readonly string DERPIC = "DRPIC>{0}>";

        public static readonly string MIROSCOPEPIC = "MRPIC>{0}>";
        public static readonly string MIREXCEPTION = "MREXP>{0}>";
        public static readonly string NotifySAVEDIMAGE = "<imagesaved>";
        public static readonly string SpirometerFVCdata = "SPIROFVC>{0}>";

        public static readonly string SpirometerFVCvtdata = "SPIROFVCvt>{0}>";
        public static readonly string SpirometerVC = "SPIROVC>{0}>";

        public static readonly string StartSpiroFVC = "<startspirofvc>{0}>";

        public static readonly string StartSpiroVC = "<startspirovc>{0}>";
        public static readonly string StopSpiro = "<stopspiro>";

        public static readonly string  Spirostatussuccess = "spirostatussucess> ";
        public static readonly string SpirostatusFailed = "spirostatusfailed>{0}> ";
        public static readonly string StoppedSpiroMeter = "stoppedspirometer>";

        public static readonly string SpiroVCResults = "spirovcresult>{0}>{1}>";

        public static readonly string SpiroFVCResults = "spirofvcresult>{0}>{1}>";

    }
}
