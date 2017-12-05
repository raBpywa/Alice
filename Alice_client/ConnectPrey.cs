using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Net;


namespace Alice_client
{
    static class ConnectPrey
    {
        public static void Connect(User _my, Connection server1, int num_sacrifice)
        {
            string comand = "[need_prey][" +
                _my._all_sacrifice[num_sacrifice].Name_sacrifice + "][" +
                _my._all_sacrifice[num_sacrifice].Token_sacrifice + "]";
               server1.Send_mess(BaseTool.Convertbtst(comand));
            }
    
        
    }
}
