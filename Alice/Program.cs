using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Alice_server
{
    class Program
    {
        static void Main(string[] args)
        {

            AServer _aServer = new AServer(1999,100);

            Thread start_send = new Thread(_aServer.Start);
            start_send.Start();
            string input="";
          
            while (_aServer.isStart)
            {
                  
                  System.Windows.Forms.Application.DoEvents();
                do
                {
                    input = Console.ReadLine();
                    if (input=="stop")
                    {
                        _aServer.Stop();
                    }
                    if (input == "start")
                    {
                        _aServer.Start();
                    }
                } while (!String.IsNullOrWhiteSpace(input));
            }
        }
    }
}
