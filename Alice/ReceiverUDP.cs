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
    static class ReceiverUDP
    {
        public static UdpClient udpServer;
        public static void Receiver(int localPort, AllUser AllPlayer,AllPrey AllPrey)
        {
            Console.WriteLine("Whait connect\n");
            udpServer = new UdpClient(localPort);
            byte[] ggg = BaseTools.Convertbtst("sdad");
            // IPAddress ip = IPAddress.Parse("192.168.100.10");    //только если сервер за NAT
            // IPEndPoint ep = new IPEndPoint(ip, 19999);           //только если сервер за NAT
            // udpServer.Send(ggg, ggg.Length, ep);                  //только если сервер за NAT
            while (true)
            {
                var remoteEP = new IPEndPoint(IPAddress.Any, 19999);

                var data = udpServer.Receive(ref remoteEP);
                Console.WriteLine("Подключен " + remoteEP.Address + ":" + remoteEP.Port + " --> " + BaseTools.Convertbtst(data));
                Command._start_command(data, remoteEP,AllPlayer,AllPrey);
            }
        }
        
        
    }
}
