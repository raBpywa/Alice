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

        private static IPAddress remoteIPAddress;
        private static int remotePort;
        private static int localPort = 19999;
        static void Main(string[] args)
        {

            AllUser AllPlayer = new AllUser();
            AllPrey AllPrey = new AllPrey();
            ReceiverUDP.Receiver(localPort, AllPlayer, AllPrey);
        }



    }
}

