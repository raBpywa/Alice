﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;


namespace Alice_client
{
    class ServerCon
    {
      public UdpClient server_conect { get; }
      public  IPEndPoint Server_adress { get; }

        public ServerCon(UdpClient server_conect, IPEndPoint Server_adress)
        {
            this.server_conect = server_conect;
            this.server_conect.Client.ReceiveTimeout =2000;
            this.Server_adress = Server_adress;
        }
        

    }
}
