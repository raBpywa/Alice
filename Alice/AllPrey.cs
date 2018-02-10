using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;


namespace Alice_server
{
    class AllPrey
    {
        public static List<Prey> AllPrey_database { get; set; }
        public XDocument xdoc { get; set; }

        public string _Patch = "preydatabase.xml";
        public AllPrey()
        {
            AllPrey_database = new List<Prey>();
            this.xdoc = XDocument.Load(_Patch);
            foreach (XElement userElement in xdoc.Element("victims").Elements("prey"))
            {
                XElement _Name_sacrifice = userElement.Element("Name_sacrifice");
                XElement Token_sacrifice = userElement.Element("Token_sacrifice");
                XElement IP = userElement.Element("IP");
                XElement Port = userElement.Element("Port");
                
                Prey _one_prey = new Prey(IP.Value, Convert.ToInt32(Port.Value), _Name_sacrifice.Value, Token_sacrifice.Value);
                AllPrey_database.Add(_one_prey);
            }

        }
        public void Save(string str_Name_sacrifice,string str_Token_sacrifice,string str_IP,string str_Port)
        {
            this.xdoc = XDocument.Load("preydatabase.xml");
            XElement vict = xdoc.Element("victims");
            XElement _prey =new XElement("prey");
            XElement _Name_sacrifice = new XElement("Name_sacrifice", str_Name_sacrifice);
            XElement Token_sacrifice = new XElement("Token_sacrifice", str_Token_sacrifice);
            XElement IP = new XElement("IP", str_IP);
            XElement Port = new XElement("Port", str_Port);
            _prey.Add(_Name_sacrifice);
            _prey.Add(Token_sacrifice);
            _prey.Add(IP);
            _prey.Add(Port);
            vict.Add(_prey);

            xdoc.Save("preydatabase.xml");
        }
    }
}
