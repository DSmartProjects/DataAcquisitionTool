using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace VideokallDataAcquisitionTool.comm
{

    // State object for receiving data from remote device.  
    public class socketComm
    {
        UdpClient udpClient;
        Thread thserver;
        public int Portnumber { get; set; } = 8889;
        //   public int SMCPortnumber { get; set; } = 9856;
        public IPEndPoint SMCEndpoint { get; set; }
        public delegate void NotifyMessage(string msg);
        public delegate void StartTimer(bool b);
        public NotifyMessage ReceivedMessage;

        public void initialize()
        {
            udpClient = new UdpClient(Portnumber);
               IPAddress ipaddress = IPAddress.Parse("192.168.0.17");
            SMCEndpoint = new IPEndPoint(ipaddress, Portnumber);
            ThreadStart ths = new ThreadStart(createListener);
            thserver = new Thread(ths);
            thserver.Start();
        }

        public async void readData()
        {
             udpClient.Connect(SMCEndpoint);
            byte[] buffer = Encoding.Unicode.GetBytes("stesttss");
            udpClient.Send(buffer, buffer.Length);

          //  IPEndPoint endpoint = new IPEndPoint(IPAddress.Any, 0);
            UdpReceiveResult res = await udpClient.ReceiveAsync();
            SMCEndpoint = res.RemoteEndPoint;
          //  byte[]
                buffer = res.Buffer;

            string str = Encoding.ASCII.GetString(buffer);
            SendData(str);
            ReceivedMessage?.Invoke(str);

        }

        public void SendData(string msg)
        {
            if (SMCEndpoint != null)
            {
                byte[] buffer = Encoding.Unicode.GetBytes(msg);
                udpClient.Send(buffer, buffer.Length, SMCEndpoint);
            }


        }

        /////////////
        ///
        int servPort = 8888;
        Socket client;
        // ManualResetEvent instances signal completion.  
        private static ManualResetEvent connectDone =
            new ManualResetEvent(false);
        private static ManualResetEvent sendDone =
            new ManualResetEvent(false);
        private static ManualResetEvent receiveDone =
            new ManualResetEvent(false);
        public void createListener()
        {
            IPAddress ipAddress = IPAddress.Parse("192.168.0.17");
            IPEndPoint localEP = new IPEndPoint(ipAddress, 8888);

             
            IPEndPoint remoteEP = new IPEndPoint(ipAddress, servPort);

            // Create a TCP/IP socket.  
              client = new Socket(ipAddress.AddressFamily,
                SocketType.Stream, ProtocolType.Tcp);

            // Connect to the remote endpoint.  
            client.BeginConnect(remoteEP,
                new AsyncCallback(ConnectCallback), client);
            connectDone.WaitOne();

            // Send test data to the remote device.  
            Send(client, "This is a test<EOF>");
            sendDone.WaitOne();

            // Receive the response from the remote device.  
            Receive(client);
            receiveDone.WaitOne();

            //// Create an instance of the TcpListener class.
            //TcpListener tcpListener = null;
            //IPAddress ipAddress =  IPAddress.Parse("192.168.0.17"); ;
            //string output = "";

            //try
            //{
            //    // Set the listener on the local IP address and specify the port.
            //    // 
            //    tcpListener = new TcpListener(ipAddress, servPort);
            //    tcpListener.Start();
            //    output = "Waiting for a connection...";
            //}
            //catch (Exception e)
            //{
            //   // output = "Error: " + e.ToString();
            //   // MessageBox.Show(output);
            //}
            //while (true)
            //{
            //    // Always use a Sleep call in a while(true) loop 
            //    // to avoid locking up your CPU.
            //    Thread.Sleep(10);
            //    // Create socket
            //    //Socket socket = tcpListener.AcceptSocket(); 
            //    TcpClient tcpClient = tcpListener.AcceptTcpClient();
            //    // Read the data stream from the client. 
            //    byte[] bytes = new byte[256];
            //    NetworkStream stream = tcpClient.GetStream();
            //    stream.Read(bytes, 0, bytes.Length);
            //    string s = bytes.ToString();
            //   // SocketHelper helper = new SocketHelper();
            //    //helper.processMsg(tcpClient, stream, bytes);
            //}
        }
        private static void ConnectCallback(IAsyncResult ar)
        {
            try
            {
                // Retrieve the socket from the state object.  
                Socket client = (Socket)ar.AsyncState;

                // Complete the connection.  
                client.EndConnect(ar);

                Console.WriteLine("Socket connected to {0}",
                    client.RemoteEndPoint.ToString());

                // Signal that the connection has been made.  
                connectDone.Set();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        // State object for receiving data from remote device.  
        public class StateObject
        {
            // Client socket.  
            public Socket workSocket = null;
            // Size of receive buffer.  
            public const int BufferSize = 256;
            // Receive buffer.  
            public byte[] buffer = new byte[BufferSize];
            // Received data string.  
            public StringBuilder sb = new StringBuilder();
        }
        private static void Receive(Socket client)
        {
            try
            {
                // Create the state object.  
                StateObject state = new StateObject();
                state.workSocket = client;

                // Begin receiving the data from the remote device.  
                client.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
                    new AsyncCallback(ReceiveCallback), state);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        private static void ReceiveCallback(IAsyncResult ar)
        {
            try
            {
                // Retrieve the state object and the client socket
                // from the asynchronous state object.  
                StateObject state = (StateObject)ar.AsyncState;
                Socket client = state.workSocket;

                // Read data from the remote device.  
                int bytesRead = client.EndReceive(ar);

                if (bytesRead > 0)
                {
                    // There might be more data, so store the data received so far.  
                    state.sb.Append(Encoding.ASCII.GetString(state.buffer, 0, bytesRead));

                    // Get the rest of the data.  
                    client.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
                        new AsyncCallback(ReceiveCallback), state);
                }
                else
                {
                    // All the data has arrived; put it in response.  
                    if (state.sb.Length > 1)
                    {
                        //response = state.sb.ToString();
                    }
                    // Signal that all bytes have been received.  
                    receiveDone.Set();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        private static void Send(Socket client, String data)
        {
            // Convert the string data to byte data using ASCII encoding.  
            byte[] byteData = Encoding.ASCII.GetBytes(data);

            // Begin sending the data to the remote device.  
            client.BeginSend(byteData, 0, byteData.Length, 0,
                new AsyncCallback(SendCallback), client);
        }

        private static void SendCallback(IAsyncResult ar)
        {
            try
            {
                // Retrieve the socket from the state object.  
                Socket client = (Socket)ar.AsyncState;

                // Complete sending the data to the remote device.  
                int bytesSent = client.EndSend(ar);
                Console.WriteLine("Sent {0} bytes to server.", bytesSent);

                // Signal that all bytes have been sent.  
                sendDone.Set();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        
        ///////////////
    }
}
