using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Alice_client
{
    class Program
    {
        static void Main(string[] args)
        {
            ListenToThePort _listenport = new ListenToThePort(1999);
            //_listenport.Start();
            Thread _thread = new Thread(_listenport.Start);
            _thread.Start();
            Thread.Sleep(1000);
            string input = "";
            Viewer view = new Viewer();
            view.Show();
            _listenport.EventConvertToBitmap += view.see;
            while (true)
            {
                System.Windows.Forms.Application.DoEvents();
                //    view.see(_listenport.BackGround);
                  // Thread.Sleep(1000);
            }

        }
    }
}
