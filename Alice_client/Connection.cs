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
        public bool isConnect=false;
        ServerCon servcon { get; }
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
                catch
                {
                    Console.WriteLine("Нет соединения с сервером");
                    isConnect = false;
                }

            }
        }

        

        private ServerCon StartTheRepeat(string IP, int Port)
        {
            Console.WriteLine("Подключение к серверу, попытка "+ count_try);

            ServerCon servcon = SendUDP._ConnectionUDP(IP, Port);
            return servcon;
        }

        public byte[]  Whait_recive()
        {
            IPEndPoint iprec = servcon.Server_adress;
            byte[] sd = servcon.server_conect.Receive(ref iprec);
            return sd;
        }

      
        public void send_mess(byte[] data)
        {
            servcon.server_conect.Send(data, data.Length, servcon.Server_adress);
        }

    }
}
