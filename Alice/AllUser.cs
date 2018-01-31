using System;
using System.Collections.Generic;
using System.Linq;

using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;


namespace Alice_server
{


    class AllUser
    {
        public List<UserClient> AllUser_database { get; set; }
        public XDocument xdoc { get; set; }
        public AllUser()
        {
            AllUser_database = new List<UserClient>();
            this.xdoc = XDocument.Load("userdatabase.xml");
            foreach (XElement userElement in xdoc.Element("users").Elements("user"))
            {
                XAttribute idAttribute = userElement.Attribute("_ID_USER");
                XElement _cookiesElement = userElement.Element("_cookies");
                XElement LoginElement = userElement.Element("_Login");
                XElement preyElement = userElement.Element("_list_Prey");

                List<Prey> _all_sacrifice= _Get_Prey(preyElement);

                UserClient _new_client = new UserClient(LoginElement.Value, _cookiesElement.Value, Convert.ToInt32(idAttribute.Value), _all_sacrifice);
                AllUser_database.Add(_new_client);
            }
        }

        public int _new_user()
        {
            UserClient _new_client = new UserClient("Client_"+AllUser_database.Count.ToString(), "Cookies" + AllUser_database.Count, AllUser_database.Count);
            XElement user = new XElement("user");
            XAttribute attrib = new XAttribute("_ID_USER", (this.AllUser_database.Count + 1));
            XElement login = new XElement("_Login", "Client_" +( this.AllUser_database.Count + 1));
            XElement cookies = new XElement("_cookies", "Cookies" + (this.AllUser_database.Count + 1));
            user.Add(attrib);
            user.Add(login);
            user.Add(cookies);
            XElement _list_Prey = new XElement("_list_Prey");
            XElement _Prey = new XElement("_Prey");
            XElement _IP_Last = new XElement("IP_Last", 0);
            XElement _Port = new XElement("Port",0 );
            XElement _Name_Prey = new XElement("Name_Prey", 0);
            XElement _Token_Prey = new XElement("Token_Prey", 0);
            _Prey.Add(_IP_Last);
            _Prey.Add(_Port);
            _Prey.Add(_Name_Prey);
            _Prey.Add(_Token_Prey);
            _list_Prey.Add(_Prey);
            user.Add(_list_Prey);
            XDocument ff = new XDocument();
            xdoc.Element("users").Add(user);
            
            xdoc.Save("userdatabase.xml");
            AllUser_database.Add(_new_client);
            return this.AllUser_database.Count;
        }

        public List<Prey> _Get_Prey(XElement preyElement)
        {
            List<Prey> _allsacrifice = new List<Prey>();
           foreach (XElement _one in preyElement.Elements("_Prey"))
            {
                Prey _prey = new Prey(_one.Element("IP_Last").Value,
                     Convert.ToInt32(_one.Element("Port").Value),
                                      _one.Element("Name_Prey").Value,
                                     _one.Element("Token_Prey").Value);
                if (_prey.ip.Address.ToString() == "0.0.0.0" & _prey.online == false & _prey.Name_sacrifice == "0" & _prey.Token_sacrifice == "0") 
                {

                }
                else
                {
                    _allsacrifice.Add(_prey);
                }
            }

           return _allsacrifice;
        }


        public void _SavePrey(Prey _one_prey,UserClient _user)
        {
            var result = xdoc.Element("users").Elements("user").Single(x => x.Attribute("_ID_USER").Value == _user._ID_USER.ToString());

           
            XElement _Prey = new XElement("_Prey");
            XElement _IP_Last = new XElement("IP_Last",_one_prey.ip.Address);
            XElement _Port = new XElement("Port", _one_prey.ip.Port);
            XElement _Name_Prey = new XElement("Name_Prey", _one_prey.Name_sacrifice);
            XElement _Token_Prey = new XElement("Token_Prey", _one_prey.Token_sacrifice);
            _Prey.Add(_IP_Last);
            _Prey.Add(_Port);
            _Prey.Add(_Name_Prey);
            _Prey.Add(_Token_Prey);

            result.Element("_list_Prey").Add(_Prey);
            XDocument ff = new XDocument();
            //xdoc.Element("users").Add(user);

            xdoc.Save("userdatabase.xml");
        }

        

    }
}
