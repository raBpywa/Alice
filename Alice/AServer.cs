using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Net;
using System.Net.Sockets;

namespace Alice_server
{
    class AServer
    {
        public int delay = 0;
        public bool isStart = true;
        int width, height;
        GetSC _screen;
        //Create_Upload _connection;
        UdpClient listener;
        IPEndPoint groupEP;
        public AServer(UdpClient listener, IPEndPoint groupEP, int delay)
        {
            this.width = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width;
            this.height = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height;
            this._screen = new GetSC(width, height);
            this.listener = listener;
           // this._connection = new Create_Upload(IP, Port);
            this.delay = delay;
            this.groupEP = groupEP;
        }


        public void Start()
        {
            isStart = true;
            string input = "";
            
            while (isStart)
            {
                System.Windows.Forms.Application.DoEvents();
                _screen.Update();
                var parts = BaseTools.CutMsg(_screen.bytes);
                Send(parts);
               // _connection.Send(parts);
                Thread.Sleep(delay);
            }
        }
        public void Send(List<byte[]> lst)
        {
            for (int i = 0; i < lst.Count; i++)
            {
               // Console.WriteLine("SEND = > " +lst[i].Length +"   |  "+ lst[i][0].ToString() + "   |  " + lst[i][1].ToString()+"   |  " + lst[i][2].ToString());
                listener.Send(lst[i], lst[i].Length, groupEP);
            }
        }
        public void Stop()
        {
            isStart = false;
        }
    }
}
