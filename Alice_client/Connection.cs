using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Net;

namespace Alice_client
{
    class Connection
    {
        int count_try = 0;

       public static Connection server1 { get; set; }

        public static IPEndPoint   lastIp { get; set; }

        public bool isConnect=false;
        public ServerCon servcon { get; }
       public Connection(string IP,int Port)
        {
            while (servcon == null & count_try < 5)
            {
                try
                {
                    count_try++;
                    servcon = StartTheRepeat(IP, Port);
                    isConnect = true;
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    isConnect = false;
                }

            }
        }
        private ServerCon StartTheRepeat(string IP, int Port)
        {
            Console.WriteLine("Подключение к серверу, попытка " + count_try);

            ServerCon servcon = SendUDP._ConnectionUDP(IP, Port);
            return servcon;
        }

        public byte[]  Whait_recive()
        {
            IPEndPoint iprec = servcon.Server_adress;
            byte[] sd = servcon.server_conect.Receive(ref iprec);
            lastIp = iprec;
            return sd;
        }

        public byte[] Whait_recive(ref IPEndPoint iprec)
        {
             byte[] sd = servcon.server_conect.Receive(ref iprec);
            return sd;
        }
        public byte[] Whait_recive_TRY_CATCH(ref IPEndPoint iprec)
        {
            try
            {
                byte[] sd = servcon.server_conect.Receive(ref iprec);
                return sd;
            }
            catch
            {
                return new byte[] { 0 };
            }
        }

        public void Send_mess(byte[] data)
        {
            servcon.server_conect.Send(data, data.Length, servcon.Server_adress);
        }

        public void Send_mess(byte[] data, IPEndPoint remoteIP)
        {
            servcon.server_conect.Send(data, data.Length, remoteIP);
        }

        public void Send_mess(string strdata, IPEndPoint remoteIP)
        {
            byte[] data = BaseTool.Convertbtst(strdata);
            servcon.server_conect.Send(data, data.Length, remoteIP);
        }

    }
}
