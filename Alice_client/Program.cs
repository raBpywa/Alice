using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Net;
using System.Net.Sockets;

namespace Alice_client
{
    class Program
    {
        static void Main(string[] args)
        {
            Viewer view = new Viewer();
            view.Show();
            ListenToThePort.EventConvertToBitmap += view.see;
            //UDPListener _Udp = new UDPListener();
            //_Udp.ListenPort += Messag;
            //Thread _list = new Thread(_Udp.cheak);
            //_list.Start();
            //Thread.Sleep(200);
            SendUDP _sec = new SendUDP("46.173.208.70", 19999,  "1q2w3e4");
            _sec.EventConvertToBitmap += view.see;
            _sec.Send("46.173.208.70", 19999, "1q2w3e4");
          

            while (true)
            {
                System.Windows.Forms.Application.DoEvents();
            }
            Console.ReadLine();
            //ListenToThePort _listenport = new ListenToThePort(19999);
            ////_listenport.Start();
            //Thread _thread = new Thread(_listenport.Start);
            //_thread.Start();
            //Thread.Sleep(1000);
            //string input = "";
           
            //while (true)
            //{
            //    System.Windows.Forms.Application.DoEvents();
            //    //    view.see(_listenport.BackGround);
            //      // Thread.Sleep(1000);
            //}

        }

   


        public static void Messag(byte[] data)
        {
            Console.WriteLine(Encoding.ASCII.GetString(data, 0, data.Length));
        }
    }
}
