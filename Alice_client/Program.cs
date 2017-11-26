using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Net;
using System.Net.Sockets;

namespace Alice_client
{
    class Program
    {
        static void Main(string[] args)
        {
            // List<ServerCon> _all_server_connection = new List<ServerCon>();
            // Viewer view = new Viewer();
            // view.Show();

            User _my = new User();

            Connection server1 = new Connection("46.173.208.70",19999);
            if (server1.isConnect)
            {
                string ttt = _my.Authorization();
                server1.send_mess(Convertbtst(_my.Authorization()));
                if (_my._ID_USER != 0)
                {

                    while (true)
                    {
                        server1.send_mess(Convertbtst(Console.ReadLine()));
                    }
                }
                else
                {
                    string _reciv = Convertbtst(server1.Whait_recive());
                    Console.WriteLine(_reciv);
                    _my.UpdateUser(_reciv);
                  
                    while (true)
                    {
                        server1.send_mess(Convertbtst(_my.Authorization()+"--->"+ Console.ReadLine()));
                    }
                }
            }
            else
            {
                Console.WriteLine("Сервер не найден");
                Console.ReadLine();
            }
    }

   


        public static string Convertbtst(byte[] data)
        {
           string out_=Encoding.ASCII.GetString(data, 0, data.Length);
            return out_;
        }

        public static byte[] Convertbtst(string data)
        {
            byte[] bt=Encoding.ASCII.GetBytes(data);
            return bt;
        }
    }


}
