using System.Net.Sockets;
using System.Net;
using System.Collections.Generic;


namespace Alice_server
{
    class Create_Upload
    {
        UdpClient udpClient=new UdpClient();
        IPEndPoint pEndPoint;
        IPAddress ip;
        public Create_Upload(string Ip,int Port)
        {
            this.ip = IPAddress.Parse(Ip);
            this.pEndPoint = new IPEndPoint(ip.Address, Port);
            
        }

        public void Send (List<byte[]> lst)
        {
            for (int i = 0; i < lst.Count; i++)
            {
                udpClient.Send(lst[i], lst[i].Length, pEndPoint);
            }
        }



    }
}
