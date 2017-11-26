using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Alice_client
{
    public class UDPListener
    {
        private const int listenPort = 19999;
        public IPAddress _ip;
        public int _port;
        public string _Cookies;
        byte[] bytes;
        UdpClient listener;
        IPEndPoint groupEP;
        public delegate void StartListen(byte[] bytes);

        //Событие OnCount c типом делегата MethodContainer.
        public event StartListen ListenPort;
        private void StartListener()
        {
            bool done = false;

            listener = new UdpClient(listenPort);
            groupEP = new IPEndPoint(IPAddress.Any, listenPort);

            try
            {
                while (!done)
                {

                    Console.WriteLine("Waiting for broadcast");
                    bytes = listener.Receive(ref groupEP);
                    ListenPort(bytes);
                    _ip = groupEP.Address;
                    _port = groupEP.Port;
                    _Cookies = Encoding.ASCII.GetString(bytes, 0, bytes.Length);
                    Console.WriteLine("Received broadcast from {0} :\n {1}\n",
                        groupEP.ToString(),
                        _Cookies);


                    //if (_Cookies=="1qa2ws3ed")
                    //{
                    //    done = true;
                    //}
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            finally
            {
                listener.Close();
            }
        }



        public void cheak()
        {
            StartListener();

            
        }
    }
}
