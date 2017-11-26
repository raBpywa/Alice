using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Alice_server
{
    class Program
    {
        static void Main(string[] args)
        {
            
            UDPListener _Udp = new UDPListener();
            _Udp.ListenPort += Messag;
            Thread _list = new Thread(_Udp.cheak);
            _list.Start();
            bool _cheak = true;
            while (_cheak)
            {
                System.Windows.Forms.Application.DoEvents();
               try
                {

                    if (_Udp._Cookies == null)
                    {

                       // Console.WriteLine("null");
                    }
                    else
                    {
                        _cheak = false;
                    }

                    Thread.Sleep(10);
                 }
                catch
                {
                    Console.WriteLine("Error");
                }
            }
            

            //Thread.Sleep(200);
            //Console.WriteLine(_Udp._ip.ToString()+":"+ _Udp._port);
            //SendUDP._Send(_Udp._ip.ToString(), _Udp._port);
            ////Console.WriteLine(_Udp._ip.ToString() + ":" + _Udp._port);
            ////SendUDP._Send(_Udp._ip.ToString(), _Udp._port);

            ////SendUDP._Send(_Udp._ip.ToString(), _Udp._port);
            ////Thread.Sleep(200);

         
            //Console.ReadLine();
            ////UDPListener.cheak();




            //Console.WriteLine("Есть");
            //Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Dgram,
            //ProtocolType.Udp);
            //Console.WriteLine(_Udp._ip.ToString() + ":" + _Udp._port);
            //IPAddress broadcast = IPAddress.Parse(_Udp._ip.ToString());

            //byte[] sendbuf = Encoding.ASCII.GetBytes("server");
            //IPEndPoint ep = new IPEndPoint(broadcast, _Udp._port);
            //for (int i = 0; i < 2; i++)
            //{
            //    Thread.Sleep(100);
            //    s.SendTo(sendbuf, ep);
            //}
            //Console.WriteLine("Message sent to the broadcast address");

            ////UDPListener.cheak();
            //Console.ReadLine();
            //AServer _aServer = new AServer(UDPListener._ip.ToString(), UDPListener._port, 1000);

            //Thread start_send = new Thread(_aServer.Start);
            //start_send.Start();
            //string input = "";

            //while (_aServer.isStart)
            //{

            //    System.Windows.Forms.Application.DoEvents();
            //    do
            //    {
            //        input = Console.ReadLine();
            //        if (input == "stop")
            //        {
            //            _aServer.Stop();
            //        }
            //        if (input == "start")
            //        {
            //            _aServer.Start();
            //        }
            //    } while (!String.IsNullOrWhiteSpace(input));
            //}
        }

        public static void Messag(byte[] data)
        {
            Console.WriteLine(Encoding.ASCII.GetString(data, 0, data.Length));
        }

    }
}
