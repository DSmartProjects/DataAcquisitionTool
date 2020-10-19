using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace VideokallSBCDataAcquisionApp.comm
{
   public class Commmodulemcc
    {
        UdpClient udpClient;
        public int Portnumber { get; set; } = 9854;
     //   public int SMCPortnumber { get; set; } = 9856;
        public  IPEndPoint SMCEndpoint { get; set; }
        public delegate void NotifyMessage(string msg);
        public delegate void StartTimer( bool b );
        public NotifyMessage ReceivedMessage; 

      public  void initialize()
        {
           
            udpClient = new UdpClient(Portnumber);
            //   IPAddress ipaddress = IPAddress.Parse("192.168.0.17");
            //  SMCEndpoint = new IPEndPoint(ipaddress, SMCPortnumber);
            IPEndPoint endpoint = new IPEndPoint(IPAddress.Any, 0);
            readData();
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

        public async void readData()
        {
            try {
                UdpReceiveResult res = await udpClient.ReceiveAsync();
                SMCEndpoint = res.RemoteEndPoint;
                byte[] buffer = res.Buffer;

                string str = Encoding.ASCII.GetString(buffer);

                ReceivedMessage?.Invoke(str);

                LogMessage(str);

            } catch (Exception ex) { LogMessage(ex.Message); } 
        }

        public void SendData(string msg)
        {
            try {

                if (SMCEndpoint != null)
                {
                    byte[] buffer = Encoding.Unicode.GetBytes(msg);
                    udpClient.Send(buffer, buffer.Length, SMCEndpoint);
                }


            } catch (Exception ex )
            {
                LogMessage(ex.Message);
            }
           
        }



        public void SendByteData(byte[] buffer)
        {
            if (SMCEndpoint != null)
            {
              //  byte[] buffer = Encoding.Unicode.GetBytes(msg);
                udpClient.Send(buffer, buffer.Length, SMCEndpoint);
            }
        }


        public void Reset()
        {
            udpClient.Close();
            udpClient.Dispose();

        }

    }
}
