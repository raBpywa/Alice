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
        public static void Receiver(int localPort)
        {
            Console.WriteLine("Whait connect\n");
            UdpClient udpServer = new UdpClient(localPort);
            List<UserClient> _all_client = new List<UserClient>();
            while (true)
            {
               
                var remoteEP = new IPEndPoint(IPAddress.Any, 19999);
                var data = udpServer.Receive(ref remoteEP);
                Console.WriteLine("Подключен " + remoteEP.Address + ":" + remoteEP.Port + " --> " + BaseTools.Convertbtst(data));
                if (BaseTools.Convertbtst(data)== "[0][][]")
                {
                    _all_client.Add(new UserClient(_all_client.Count));
                    byte[] msg = Encoding.ASCII.GetBytes(_all_client[_all_client.Count-1].Authorization());
                    udpServer.Send(msg, msg.Length, remoteEP);
                }
                else
                {
                    byte[] msg = Encoding.ASCII.GetBytes("...ok");
                    udpServer.Send(msg, msg.Length, remoteEP);
                }
                
               
                      
            }
        }
    }
}
