using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace Alice_server
{
    class SendUDP
    {
       public static void _Send (string IP,int port)
        {
           
            Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Dgram,
ProtocolType.Udp);

            IPAddress broadcast = IPAddress.Parse(IP);//("46.173.208.70");

            byte[] sendbuf = Encoding.ASCII.GetBytes("cclient");
            IPEndPoint ep = new IPEndPoint(broadcast, port);
            for (int i = 0; i < 5; i++)
            {
                Thread.Sleep(3);
                s.SendTo(sendbuf, ep);
               

            }
            Console.WriteLine("Message sent to the broadcast address");
            s.Close();
        }
    }
}
