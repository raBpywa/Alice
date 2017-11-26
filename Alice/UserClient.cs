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
        int _ID_USER { get; set; }
        string _cookies { get; set; }
        string _Login { get; set; }
        int online_number_cl { get; set; }
        public UserClient(int online_number_cl)
        {
            Random _id = new Random();
            _ID_USER = _id.Next(0, 2132123121);
            _cookies = _id.Next(0, 2132123121).ToString();
            _Login= _id.Next(0, 2132123121).ToString();

        }

        public string Authorization()
        {
            return "[" + _ID_USER + "][" + _cookies + "][" + _Login + "]";
        }
    }
}
