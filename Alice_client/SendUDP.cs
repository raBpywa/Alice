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


namespace Alice_client
{
    static class  SendUDP
    {
        
        public static ServerCon _ConnectionUDP(string IP,int port)
        {
        
            UdpClient Connec = new UdpClient();
            Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Dgram,ProtocolType.Udp);
            s.ReceiveTimeout =5000;
            IPAddress broadcast = IPAddress.Parse(IP);
            byte[] sendbuf = Encoding.ASCII.GetBytes("ping");
            IPEndPoint ep = new IPEndPoint(broadcast, port);
            s.SendTo(sendbuf, ep);
            Connec.Client = s;
            IPEndPoint remm;
            
            byte[] bytttt = Connec.Receive(ref ep);
                string ppp = Encoding.ASCII.GetString(bytttt);
                Console.Write(ppp);
                Console.WriteLine("...OK");
           
            ServerCon _new_ser = new ServerCon(Connec, ep);
            //Console.ReadLine();
            return _new_ser;
        }

    }

       
   
}
