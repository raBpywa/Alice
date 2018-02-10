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
            var outer = Task.Factory.StartNew(() => BaseTools._Cheak_online_Prey(AllPrey));

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
