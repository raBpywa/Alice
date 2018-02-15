using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Net;

namespace AliceSRV
{
    public class Connection
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
            WhaitAllData.Ip = iprec;
            Console.WriteLine(BaseTool.Convertbtst(sd));
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
        public void Send_mess(byte[] data,IPEndPoint remoteIP)
        {
            servcon.server_conect.Send(data, data.Length, remoteIP);
        }
        public void _SendPreyOnline(Prey my)
        {
            var s = Task.Run(() => { _Start_Task(my); });

        }

        private void _Start_Task(Prey my)
        {
            byte[] _onl = BaseTool.Convertbtst("[prey_online][" + my.Name_sacrifice + "][" + my.Token_sacrifice + "]");
            while (true)
            {
                servcon.server_conect.Send(_onl, _onl.Length, servcon.Server_adress);
                Thread.Sleep(20000);
            }
        }

        public static IPEndPoint _WhaitAutorization(Connection server1)
        {
            IPEndPoint iprem = new IPEndPoint(IPAddress.Parse("0.0.0.0"), 0);
            while (true)
            {
                Thread.Sleep(10);
                byte[] bt = server1.Whait_recive(ref iprem);
                
                string reciv = BaseTool.Convertbtst(bt);
                string[] mas = BaseTool._GetArrSplit(bt);
                if (mas[0] == "for_prey_connect")
                {
                    IPAddress ip = IPAddress.Parse(BaseTool._get_ip_or_port(mas[1], BaseTool.IPorPORT.IP));
                    IPEndPoint remote = new IPEndPoint(ip, Convert.ToInt32(BaseTool._get_ip_or_port(mas[1], BaseTool.IPorPORT.PORT)));
                    for (int i = 0; i < 2; i++)
                    {
                        server1.Send_mess(BaseTool.Convertbtst("[ping]"), remote);
                        Thread.Sleep(10);
                    }
                 
                    
                }
                if (mas[0] == "OK")
                {
                    string strok = string.Join(" ", mas);
                    Console.WriteLine(strok);
                    break;
                }
                 //server1.Send_mess("ping",)   
            }

            return iprem;
        }
    }
}
