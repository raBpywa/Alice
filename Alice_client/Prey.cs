using System;
using System.Collections.Generic;
using System.Net;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alice_client
{
    class Prey
    {
        public IPEndPoint ip { get; set; }
        public string Name_sacrifice { get; set; }
        public string Token_sacrifice { get; set; }
        
        public bool online { get; set; }

        public Prey(string IP_sacrifice, int IP_Port, string Name_sacrifice, string Token_sacrifice)
        {
            IPAddress ip_adr =IPAddress.Parse( IP_sacrifice);
            this.ip = new IPEndPoint(ip_adr, IP_Port);
            this.Name_sacrifice = Name_sacrifice;
            this.Token_sacrifice = Token_sacrifice;
        }
        public Prey( string Name_sacrifice, string Token_sacrifice, string online, string IP_sacrifice, int IP_Port)
        {
            IPAddress ip_adr = IPAddress.Parse(IP_sacrifice);
            this.ip = new IPEndPoint(ip_adr, IP_Port);
            this.Name_sacrifice = Name_sacrifice;
            this.Token_sacrifice = Token_sacrifice;
            this.online =Convert.ToBoolean( online);
        }
        public static void _Get_Prey_List(Connection server1, User _my)
        {
            string _comm = _my.dataToString() + "[get_all_sacrifice]";
            Console.WriteLine("====> " + _comm);
            server1.Send_mess(BaseTool.Convertbtst(_comm), Connection.IpSERVER);
            //byte[] anyrec = server1.Whait_recive();
        }

        public static List<Prey> _ConvertByteArr(string[] mas)
        {
            List<Prey> _sacrifice = new List<Prey>();
            string pl = "";
            pl=String.Join("", mas);
            if (pl != "00False0.0.0.00")
            {
                
                for (int i = 0; i < mas.Length; i += 5)
                {
                    Prey _one = new Prey(mas[i], mas[i + 1], mas[i + 2], mas[i + 3], Convert.ToInt32(mas[i + 4]));
                    _sacrifice.Add(_one);
                }
                return _sacrifice;
            }
            else return _sacrifice;
        }

        private static void _AppendPrey(string Name_sacrifice, string Token_sacrifice, User _my, Connection server1)
        {
            
            string _comm = "[append_prey][" + Name_sacrifice
                                   + "][" + Token_sacrifice
                                   + "][" + _my._ID_USER
                                   + "][" + _my._Login
                                   + "][" + _my._cookies;

            server1.Send_mess(BaseTool.Convertbtst(_comm));
            Console.WriteLine("Attention");
           
        }

        public static void AddNewPrey (string Name,string token,User _my, Connection server1)
        {
            Console.WriteLine("Введите имя удаленного копьютера");
            string Name_sacrifice = Name;
            Console.WriteLine("Введите Токен");
            string Token_sacrifice = token;
            Prey._AppendPrey(Name_sacrifice, Token_sacrifice, _my, server1);     //сопоставить Prey с User

        }


        public override string ToString() { return Name_sacrifice+" "+Token_sacrifice; }


    }
}
