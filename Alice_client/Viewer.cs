using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;

namespace Alice_client
{
    public partial class Viewer : Form
    {
        int index_for_Prey = 0;
        public static List<byte[]> allbyte=new List<byte[]>();
        public Viewer(int index)
        {
            InitializeComponent();

            index_for_Prey = index;
            pictureBox1.SizeMode = PictureBoxSizeMode.AutoSize;
            flowLayoutPanel1.Controls.Add(pictureBox1);
        }


        private void Start(int index)
        {

            
            Command._ConnectPrey(index);
            this.Show();
            IPEndPoint _aliceSRV = null;
            byte[] ping1 = Connection.server1.Whait_recive(ref _aliceSRV);
            byte[] ping2 = Connection.server1.Whait_recive(ref _aliceSRV);
            for (int i = 0; i < 100; i++)
            {
                Application.DoEvents();

              
                    byte[] on = Connection.server1.Whait_recive(ref _aliceSRV);
                    if (on[0].Equals((byte)i))
                    {
                        allbyte.Add(on);
                        Connection.server1.Send_mess(BaseTool.Convertbtst("OK"), _aliceSRV);
                    }
                    else
                    {

                    }
             

            }
            //Start_recive();
            // IPEndPoint _aliceSRV = null;
            //allbyte = new List<byte[]>();
            //for (int i = 0; i < 100; i++)
            //{
            //    byte[] on = Connection.server1.Whait_recive(ref _aliceSRV);
            //    if (on[0].Equals((byte)i))
            //    {
            //        allbyte.Add(on);
            //        Connection.server1.Send_mess(BaseTool.Convertbtst("OK"), _aliceSRV);
            //    }
            //    else
            //    {

            //    }
            //}

            Bitmap _see = BaseTool._Pullimage(BaseTool._GetList(allbyte));
            see(_see);
            var t = Task.Run(() =>
            {
                _update_data();
            });


        }
      


       public static bool  _status = true;
        public static Thread th;
        //public void Start_recive()
        //{
        //    th = new Thread(startThread);
        //    th.Start();

        //}

        public void startThread()
        {
            // var s = Task.Run(() => {
            while (true)
            {

                if (_status)
                {
                    IPEndPoint _aliceSRV = null;
                    Command._CheakCommand(Connection.server1.Whait_recive(ref _aliceSRV), _aliceSRV);
                }
                else
                {
                    Thread.Sleep(100);
                }
            }
            // string test = BaseTool.Convertbtst(Connection.server1.Whait_recive());
            // Console.WriteLine(test);
            // });
        }

        private void _update_data()
        {
            bool isStartUpdate = false;
            while (true)
            {
                byte[] next = Connection.server1.Whait_recive();
                string reciv = BaseTool.Convertbtst(next);

                if (reciv.Equals("stop_update_data"))
                {
                    isStartUpdate = false;

                    Bitmap _see = BaseTool._Pullimage(BaseTool._GetList(allbyte));
                    Invoke(new Action(() => { see(_see); }));


                }

                if (isStartUpdate)
                {
                    allbyte[next[0]] = next;
                }
                if (reciv.Equals("update_data"))
                {
                    isStartUpdate = true;

                }
            }
        }

        public void see(Bitmap bmp)
        {
           
            pictureBox1.Image = bmp;
            pictureBox1.Refresh();
           
        }

        private void Viewer_SizeChanged(object sender, EventArgs e)
        {
            Size new_size = new Size(this.Size.Width - 60, this.Size.Height - 60);
            pictureBox1.Size = new_size;

        }

        private void Viewer_Load(object sender, EventArgs e)
        {
            Start(index_for_Prey);
        }
    }
}
