using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alice_client
{
    class User
    {
       public int _ID_USER { get; set; }
        public string _cookies { get; set; }
        public string _Login { get; set; }
        public User()
        {
          
            _ID_USER= 0;
            _cookies = null;
            _Login = null;
        }

        public void UpdateUser(string authorizat)
        {
            string[] split = new string[] { "[", "]" };
            string[] mas = authorizat.Split(split, StringSplitOptions.RemoveEmptyEntries);
            _ID_USER =Convert.ToInt32( mas[0]);
            _cookies = mas[1];
            _Login = mas[2];
        }

            public string Authorization()
        {
            return "["+_ID_USER+"]["+ _cookies+"][" +_Login+"]";
        }

    }
}
