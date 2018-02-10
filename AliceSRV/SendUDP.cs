using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Drawing;
using System.IO;


namespace AliceSRV
{
    static class SendUDP
    {


        public static ServerCon _ConnectionUDP(string IP, int port)
        {
            IPAddress broadcast = IPAddress.Parse(IP);
            IPEndPoint ep = new IPEndPoint(broadcast, port);
            UdpClient Connec = new UdpClient(port);
         
            Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            s.MulticastLoopback = true;
            s.ReceiveTimeout = 5000;
            Send_Messg(Connec, "ping_1", ep);
            byte[] reciv=Whait_Messg(Connec, ep);
            //string you_ip = BaseTool.Convertbtst(reciv);
            ServerCon _new_ser = new ServerCon(Connec, ep);
            return _new_ser;

        }
        /// <summary>
        /// Отправка byte[] на сервер
        /// </summary>
        /// <param name="connect"></param>
        /// <param name="sendbuf"></param>
        /// <param name="ep"></param>
        public static void Send_Messg(UdpClient connect, byte[] sendbuf, IPEndPoint ep)
        {
            connect.Send(sendbuf, sendbuf.Length, ep);
        }

        /// <summary>
        /// Отправка string на сервер
        /// </summary>
        /// <param name="connect"></param>
        /// <param name="command"></param>
        /// <param name="ep"></param>
        public static void Send_Messg(UdpClient connect, string command, IPEndPoint ep)
        {
            byte[] sendbuf = Encoding.ASCII.GetBytes(command);
            connect.Send(sendbuf, sendbuf.Length, ep);
        }
        /// <summary>
        /// Прием данных от сервера
        /// </summary>
        /// <param name="connect"></param>
        /// <param name="ep"></param>
        /// <returns></returns>
        public static byte[] Whait_Messg(UdpClient connect, IPEndPoint ep)
        {
            byte[] bytttt = connect.Receive(ref ep);
            string address = ep.ToString();
            string ppp = Encoding.ASCII.GetString(bytttt);
            Console.Write(ppp);
            Console.WriteLine("...OK");
            return bytttt;
        }

        public static void TEST_ConnectionUDP(string IP, int port)
        {
              
            UdpClient Connec = new UdpClient(19999);
            IPAddress broadcast = IPAddress.Parse(IP);
            byte[] sendbuf = Encoding.ASCII.GetBytes("ping_1");

            IPEndPoint ep = new IPEndPoint(broadcast, port);
            Console.WriteLine("get_connect");
            while (true)
            { 
                Connec.Send(sendbuf, sendbuf.Length, ep);
                Console.WriteLine(Connec.Client.LocalEndPoint.ToString() + "  " + ep.ToString());
                Thread.Sleep(100);
            }


        }

    }
   
}
