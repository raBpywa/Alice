using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Net;
using System.Windows.Forms;

namespace AliceSRV
{
    public partial class AliceSRV : Form
    {
        private System.Windows.Forms.NotifyIcon notifyIcon1;

        public static Connection server1;
        static bool start_watch = false;
        public AliceSRV()
        {
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon();
            notifyIcon1.Icon = new Icon("favicon.ico");

            notifyIcon1.Text = "AliceSRV (Управление Desktop персонального компьютера)";
            notifyIcon1.DoubleClick += new System.EventHandler(this.notifyIcon1_Click);


            InitializeComponent();

        }
       

        private void button1_Click(object sender, EventArgs e)
        {
        
            var s = Task.Run(() =>
            {
                Start();
            });
          
        }

        public void Start()
        {
            WhaitAllData.Updatedata = false;
            //byte[] t = new byte[2];
            //t = BitConverter.GetBytes((int)259);
            //int j=0;
            //while (true)
            //{
            //    testRFB.Start(j++);
            //}
            Prey _My;
            server1 = new Connection(textBox1.Text,Convert.ToInt32( textBox2.Text));//("195.128.124.171", 19999);
            if (server1.isConnect)
            {
                _My = new Prey(server1);
                Invoke(new Action(() => {
                    textBox3.Text = _My.Name_sacrifice;
                    textBox4.Text = _My.Token_sacrifice;
                    textBox3.Refresh();
                    textBox4.Refresh();
                }));
                
                Console.WriteLine(_My.Name_sacrifice + " " + _My.Token_sacrifice);
                server1._SendPreyOnline(_My);
                Console.WriteLine("Ожидаем команды");
                // IPEndPoint ipclient = Connection._WhaitAutorization(server1);
                // PrtSC.StartPrtSC(server1, ipclient);
                Start_recive();

                while (true)
                {

                    while (!WhaitAllData.ForPreyConnect)
                    {
                        //string mess=BaseTool.Convertbtst(  server1.Whait_recive());
                        //  if(BaseTool._GetArrSplit( mess)[0]=="OK")
                        //  {
                        //      break;
                        //  }
                        Thread.Sleep(1);
                    }

                    while (!WhaitAllData.startWatch)
                    {
                        //string mess=BaseTool.Convertbtst(  server1.Whait_recive());
                        //  if(BaseTool._GetArrSplit( mess)[0]=="OK")
                        //  {
                        //      break;
                        //  }
                        Thread.Sleep(1);
                    }


                    IPEndPoint ipclient = WhaitAllData.Ip;
                    PrtSC.CutInToParts(server1, ipclient);
                    WhaitAllData.restartWatch = false;


                    while (!WhaitAllData.stopWatch)
                    {

                        //WhaitAllData.Updatedata = true;
                        PrtSC.Update(server1, ipclient);

                        //WhaitAllData.Updatedata = false; 
                        Thread.Sleep(Resolution.speed);

                    }

                    WhaitAllData.stopWatch = false;
                    WhaitAllData.startWatch = false;
                    WhaitAllData.ForPreyConnect = false;
                }
            }
            //ожидаем ответа от клиента

            else
            {
                Console.WriteLine("Сервер недоступен!");
                Console.ReadLine();
            }
        }

        public static void Start_recive()
        {
            var s = Task.Run(() =>
            {
                while (true)
                {
                    byte[] resp = server1.Whait_recive();
                    Commands._Start(BaseTool._GetArrSplit(BaseTool.Convertbtst(resp)));
                }
            });
        }

        private void AliceSRV_Deactivate(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.ShowInTaskbar = false;
                notifyIcon1.Visible = true;
            }
        }

        private void notifyIcon1_Click(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.WindowState = FormWindowState.Normal;
                this.ShowInTaskbar = true;
                notifyIcon1.Visible = false;
            }
        }
    }
}
