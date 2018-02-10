using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alice_server
{
    static class Authorization
    {
        public static string _cheak(string _command_line)
        {
           
           string[] obj = _command_line.Split(new string[] { "[", "]" }, StringSplitOptions.RemoveEmptyEntries);
           if(obj[0]=="0")
            {
                return "0";
            }

            return "ok";
        }

        
    }
}
