using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alice_client
{
    class User
    {
        public static User _My { get; set; }
        public int _ID_USER { get; set; }
        public string _cookies { get; set; }
        public string _Login { get; set; }
        
    public List<Prey> _all_sacrifice { get; set; }

        public User(Connection server1)
        {
            if (!_Open("user.dat"))
            {
                _ID_USER = 0;
                _cookies = null;
                _Login = null;
                _Update_User(server1);
            }
        }

        private void UpdateUserData(string authorizat)
        {
            string[] split = new string[] { "[", "]" };
            string[] mas = authorizat.Split(split, StringSplitOptions.RemoveEmptyEntries);
            _ID_USER = Convert.ToInt32(mas[0]);
            _cookies = mas[1];
            _Login = mas[2];
        }

        public string dataToString()
        {
            return "[" + _ID_USER + "][" + _cookies + "][" + _Login + "]";
        }

        public void _Update_User(Connection server1)
        {
            if (server1.isConnect)
            {
                //string ttt = dataToString();
                server1.Send_mess(BaseTool.Convertbtst(dataToString()));
                if (_ID_USER != 0)
                {

                    while (true)
                    {
                        server1.Send_mess(BaseTool.Convertbtst(Console.ReadLine()));
                    }
                }
                else
                {
                    string _reciv = BaseTool.Convertbtst(server1.Whait_recive());
                    Console.WriteLine(_reciv);
                    UpdateUserData(_reciv);
                    _Save("user.dat");
                   
                }
            }

            else
            {
                Console.WriteLine("Сервер не найден");
                Console.ReadLine();
            }
        }

        public void _Save(string _patch)
        {
            File.WriteAllText(_patch, dataToString());
        }

        public bool _Open(string _patch)
        {
            try
            {
                string _load = File.ReadAllText(_patch);
                if (_load.Length > 0)
                {
                    UpdateUserData(_load);
                    return true;
                }
                else
                    return false;
            }
            catch
            {
                return false;
            }
        }
    }
}

