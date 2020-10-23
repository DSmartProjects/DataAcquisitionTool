using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace VideokallDataAcquisitionTool.comm
{
  public  class CommwithChartApp
    {
        UdpClient udpClient;
        IPAddress Ipaddress = null;
        public int Portnumber { get; set; } = 9889;
        IPEndPoint ep;
        public void ConnectwithChartApp()
        {
             if(VKApp.mainApp.commtoMCC.SMCEndpoint !=null)
            {
                if(udpClient == null)
                udpClient = new UdpClient();
                try
                {
                    Ipaddress = VKApp.mainApp.commtoMCC.SMCEndpoint.Address;
                      ep = new IPEndPoint(Ipaddress, Portnumber);
                    udpClient.Connect(ep);
                    CommLogMessage("Connected to chart");

                }
               catch(Exception ex)
                {
                    LogMessage(ex.Message);
                }
            }
        }
        public void SendMsg(string msg)
        {
            if (udpClient != null)
            {
                byte[] buffer = Encoding.Unicode.GetBytes(msg);
                udpClient.Send(buffer, buffer.Length);
            }
        }
        void CommLogMessage(string msg)
        {
            try
            {
                using (System.IO.StreamWriter file =
           new System.IO.StreamWriter("comm.txt", true))
                {
                    file.WriteLine(DateTime.Now.ToString() + ": " + msg);
                }
            }
            catch (Exception ex)
            {

            }

        }
        void LogMessage(string msg)
        {
            try
            {
                using (System.IO.StreamWriter file =
           new System.IO.StreamWriter("exceptionlogs.txt", true))
                {
                    file.WriteLine(DateTime.Now.ToString() + ": " + msg);
                }
            }
            catch (Exception ex)
            {

            }

        }

    }
}
