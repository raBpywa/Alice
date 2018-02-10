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
            this.KeyPreview = true;
            index_for_Prey = index;
            pictureBox1.SizeMode = PictureBoxSizeMode.AutoSize;
            flowLayoutPanel1.Controls.Add(pictureBox1);
            label1.Text = Resolution.part.ToString();
            label2.Text = Resolution.speed.ToString();
            label3.Text = Resolution.timeSend.ToString();
        }
        IPEndPoint _PreySRV = null;

        private void Start(int index)
        {
            allbyte = new List<byte[]>();
            // comboBox1.SelectedItem = 0;
            ConnectPrey.resolution = comboBox1.GetItemText(comboBox1.Items[0]);
            // MessageBox.Show( comboBox1.SelectedText);

            Command._ConnectPrey(index);
            this.Activate();
            IPEndPoint _aliceSRV = null;

            byte[] ping1 = Connection.server1.Whait_recive(ref _aliceSRV);
            if (ping1.Length < 100)
            {
                string ggg = BaseTool.Convertbtst(ping1);
                Command._CheakCommand(ping1, _aliceSRV);
                byte[] ping2 = Connection.server1.Whait_recive(ref _aliceSRV);
            }

            for (int i = 0; i < Resolution.part; i++)
            {

                Thread.Sleep(10);
                byte[] on = new byte[0];
                if (ping1.Length > 100)
                {
                    on = ping1;
                    ping1 = new byte[0];
                }
                else
                {
                    while (true)
                    {
                        try
                        {
                            on = Connection.server1.Whait_recive(ref _aliceSRV);
                            Console.Write(allbyte.Count);
                            break;
                        }
                        catch
                        {

                            Connection.server1.Send_mess(BaseTool.Convertbtst("NO"), _aliceSRV);
                        }

                    }
                }

                if (on.Length > 1)
                {
                    if (on[0].Equals((byte)allbyte.Count))
                    {
                        allbyte.Add(on);

                        Connection.server1.Send_mess(BaseTool.Convertbtst("OKKKK"), _aliceSRV);
                        _PreySRV = _aliceSRV;
                    }
                    else
                    {
                        Connection.server1.Send_mess(BaseTool.Convertbtst("NO"), _aliceSRV);
                        i = allbyte.Count;
                    }

                    if (on[0] < (byte)allbyte.Count)
                    {
                        Connection.server1.Send_mess(BaseTool.Convertbtst("OKKKK"), _aliceSRV);
                    }
                    //else
                    //Connection.server1.Send_mess(BaseTool.Convertbtst("NO"), _aliceSRV);


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

                //Bitmap _see = BaseTool._Pullimage(BaseTool._GetList(allbyte));
                //see(_see);
            }
            var t = Task.Run(() =>
            {

                _update_dataWithoutSync();


            });


        }

        

        public void _UpdateDAta()
        {
            allbyte = BaseTool.CutInToParts((Bitmap)pictureBox1.Image);
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

       static bool _stop = false;
        private void _update_data()
        {
            bool isStartUpdate = false;
            while (!_stop)
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
                    if (next.Length > 12)//error update_data
                    {
                        Console.WriteLine(next[0]);
                        allbyte[next[0]] = next;
                    }
                }
                if (reciv.Equals("update_data"))
                {
                    isStartUpdate = true;

                }
            }
            _stop = false;
        }


        private void _update_dataWithoutSync()
        {
           
            while (!_stop)
            {
                byte[] next = new byte[0];
                try
                {
                    next = Connection.server1.Whait_recive();
                }
                catch
                {
                    Console.WriteLine("error 1 Нет приема");
                }
                string reciv = BaseTool.Convertbtst(next);

                if (next.Length > 12)//error update_data
                {
                    Console.WriteLine(next[0]);
                    if (allbyte.Count>next[0])
                    {
                        allbyte[next[0]] = next;
                    }
                    
                }
                try
                {
                    Bitmap _see = BaseTool._Pullimage(BaseTool._GetList(allbyte));
                    Invoke(new Action(() => { see(_see); }));
                }
                catch

                {
                    Console.WriteLine("error 213 Bitmap _see = BaseTool._Pullimage(BaseTool._GetList(allbyte))");
                    allbyte = BaseTool.CutInToParts((Bitmap)pictureBox1.Image);
                }
              
               
                  
               
            }
            _stop = false;
        }

        public void see(Bitmap bmp)
        {
           
            pictureBox1.Image = bmp;
            pictureBox1.Refresh();
            flowLayoutPanel1.Refresh();
           
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

      
        int whait_send=0;
        public bool capture = false;
        private void flowLayoutPanel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (capture == true)
            {
                if (whait_send > 3)
                {
                    groupBox1.Text = (e.X )+ " " + (e.Y);
                    int _mouse_X = (e.X);
                    int _mouse_Y = (e.Y);
                    Console.WriteLine(_mouse_X + " " + _mouse_Y);
                    byte[] mousecoord = BaseTool.Convertbtst("[mouse_move][" + _mouse_X + "][" + _mouse_Y + "]");
                    Connection.server1.Send_mess(mousecoord, _PreySRV);
                    whait_send = 0;
                }
                else

                {
                    whait_send++;
                }
            }
        }

        private void picture_MouseDown(object sender, MouseEventArgs e)
        {
            int _mouse_X = (Cursor.Position.X - this.Location.X) - 10;
            int _mouse_Y = (Cursor.Position.Y - this.Location.Y) - 30;

            byte[] mousecoord = BaseTool.Convertbtst("[mouse_down][" + _mouse_X + "][" + _mouse_Y + "]");
                    Connection.server1.Send_mess(mousecoord, _PreySRV);
                    whait_send = 0;
               
        }
        private void picture_MouseUP(object sender, MouseEventArgs e)
        {
            int _mouse_X = (Cursor.Position.X - this.Location.X) - 10;
            int _mouse_Y = (Cursor.Position.Y - this.Location.Y) - 30;
            byte[] mousecoord = BaseTool.Convertbtst("[mouse_up][" + _mouse_X + "][" + _mouse_Y + "]");
            Connection.server1.Send_mess(mousecoord, _PreySRV);
            whait_send = 0;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            capture = true;
        }

        private void Viewer_KeyPress(object sender, KeyPressEventArgs e)
        {
          string   key = e.KeyChar.ToString();
            byte[] mousecoord = BaseTool.Convertbtst("[key_press]["+key+"]");
            Connection.server1.Send_mess(mousecoord, _PreySRV);

        }

        private void Viewer_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            string key = e.KeyChar.ToString();
            byte[] mousecoord = BaseTool.Convertbtst("[key_press][" + key + "]");
            Connection.server1.Send_mess(mousecoord, _PreySRV);
        }

        private void Viewer_KeyDown(object sender, KeyEventArgs e)
        {
            //string key = e.KeyCode.ToString();
            //byte[] mousecoord = BaseTool.Convertbtst("[key_press][" + key + "]");
            //Connection.server1.Send_mess(mousecoord, _PreySRV);
        }

        private void Viewer_FormClosing(object sender, FormClosingEventArgs e)
        {
            for(int i=0;i<10;i++)
            {
               
                byte[] command = BaseTool.Convertbtst("[stop_watching]");
                Connection.server1.Send_mess(command, _PreySRV);
            }
            Alice_client._status = true;
            _stop = true;
            this.Dispose();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ConnectPrey.resolution = comboBox1.GetItemText(comboBox1.SelectedItem);
           byte[] command = BaseTool.Convertbtst("[change_resolution]["+ ConnectPrey.resolution + "]");
            Connection.server1.Send_mess(command, _PreySRV);
        }

           private void trackBar1_MouseUp(object sender, MouseEventArgs e)
        {
            trackBar1.Value = ((int)Math.Round(trackBar1.Value / 10.0)) * 10;
           
            Resolution.SetPart(trackBar1.Value);
            byte[] command = BaseTool.Convertbtst("[change_part][" + Resolution.part + "]");
            Connection.server1.Send_mess(command, _PreySRV);
            _stop = true;
            Start(index_for_Prey);
            label1.Text = Resolution.part.ToString();
        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void trackBar2_ValueChanged(object sender, EventArgs e)
        {
            
            label2.Text = (trackBar2.Value* 100.0/2).ToString();
            Resolution.SetSpeed((int)(trackBar2.Value *100.0 / 2));
            //label2.Text = (trackBar2.Value * 10.0).ToString();
            byte[] command = BaseTool.Convertbtst("[change_speed][" + Resolution.speed + "]");
            Connection.server1.Send_mess(command, _PreySRV);
        }

        private void trackBar3_ValueChanged(object sender, EventArgs e)
        {
            
           // trackBar3.Value = ((int)Math.Round(trackBar3.Value * 100.0 / 2));
            Resolution.SetTimeSendd(((int)(trackBar3.Value)));
            byte[] command = BaseTool.Convertbtst("[change_timesend][" + Resolution.timeSend + "]");
            Connection.server1.Send_mess(command, _PreySRV);
            label3.Text = Resolution.timeSend.ToString();
        }
    }
}
