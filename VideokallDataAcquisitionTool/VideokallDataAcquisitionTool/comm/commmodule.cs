using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace VideokallSBCDataAcquisionApp.comm
{
    class Commmodule
    {
        UdpClient udpClient;
        public int Portnumber { get; set; } = 11000;
     //   public int SMCPortnumber { get; set; } = 9856;
        public  IPEndPoint SMCEndpoint { get; set; }
        public delegate void NotifyMessage(string msg);
        public delegate void StartTimer( bool b );
        public NotifyMessage ReceivedMessage; 

      public  void initialize()
        {
            udpClient = new UdpClient(Portnumber);
            //IPAddress ipaddress = IPAddress.Parse("192.168.0.33");
            // SMCEndpoint = new IPEndPoint(ipaddress, Portnumber);
        }

        public async void readData()
        {
            IPEndPoint endpoint = new IPEndPoint(IPAddress.Any, 0);
            UdpReceiveResult res = await udpClient.ReceiveAsync();
            SMCEndpoint = res.RemoteEndPoint;
            byte[] buffer = res.Buffer;
           
            string str = Encoding.ASCII.GetString(buffer);

            ReceivedMessage?.Invoke(str); 

        }

        public void SendData(string msg)
        {
            //if (SMCEndpoint != null)
            //{
            //    byte[] buffer = Encoding.Unicode.GetBytes(msg);
            //    udpClient.Send(buffer, buffer.Length, SMCEndpoint);
            //}

            WriteCommFile(msg);
        }

      public  void ReadCommFile()
        {
            string str = "";
            string folder = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
            try
            {
                  str = System.IO.File.ReadAllText(folder + "\\todataacq.bin");
                if(!string.IsNullOrEmpty(str))
                ReceivedMessage?.Invoke(str);

            }catch(Exception ex)
            {
                System.IO.File.WriteAllText("exceptionlog.txt", ex.Message);
            }

            finally
            {
                if (!string.IsNullOrEmpty(str))
                    System.IO.File.WriteAllText(folder + "\\todataacq.bin", "");
            }

        }
        void WriteCommFile(string msg)
        {
            try
            {
                string folder = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
                System.IO.File.WriteAllText(folder + "\\tosmc.bin", msg);

            }
            catch(Exception ex)
            {
                System.IO.File.WriteAllText( "exceptionlog.txt", ex.Message);
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


        public void LocalIPAddress()
        {
            
        }

    }
}
