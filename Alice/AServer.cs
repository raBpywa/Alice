using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace Alice_server
{
    class AServer
    {
        public int delay = 0;
        public bool isStart = true;
        int width, height;
        GetSC _screen;
        Create_Upload _connection;
        public AServer(int Port,int delay)
        {
            this.width = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width;
            this.height = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height;
            this._screen = new GetSC(width, height);
            this._connection = new Create_Upload("127.0.0.1", Port);
            this.delay = delay;
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
                _connection.Send(parts);
                Thread.Sleep(delay);
            }
        }
        public void Stop()
        {
            isStart = false;
        }
    }
}
