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

namespace Alice_client
{
    public partial class Alice_client : Form
    {
        public static Alice_client one;
        public Alice_client()
        {
            InitializeComponent();
            one = this;
        }

       
        private void button1_Click(object sender, EventArgs e)
        {
          Connection.server1 = new Connection(textBox1.Text, Convert.ToInt32(textBox2.Text));
            User._My = new User(Connection.server1);
            Start_recive();
            textBox1.ReadOnly = true;
            textBox2.ReadOnly = true;
            listView1.View = View.Details;
            listView1.Columns.Add("Name");
            listView1.Columns.Add("Token");
            listView1.Columns.Add("Online");
            listView1.GridLines = true;
            Connection.IpSERVER = Connection.server1.servcon.Server_adress;
            label4.Text = User._My._Login;
            RefreshListview( User._My, Connection.server1);
           
            button2.Enabled = true;
            button3.Enabled = true;
           
        }

        private void RefreshListview(User _my, Connection server1)
        {
             Prey._Get_Prey_List(Connection.server1, User._My);
            // listBox1.DataSource = _my._all_sacrifice;
        
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            AddPrey _add = new AddPrey();
            _add.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {

            _UpdatePrey();
        }
        public static void _UpdatePrey()
        {
            one.listView1.Items.Clear();
           one.RefreshListview(User._My, Connection.server1);
        }

       

        public static void _UpdateListPrey()
        {
            
            foreach (var element in User._My._all_sacrifice)
           one.Invoke(new Action(() => {
               one.listView1.Items.Add(new ListViewItem(new string[] { element.Name_sacrifice, element.Token_sacrifice,element.online.ToString()
               })); })); 
        }
        
        private void button4_Click(object sender, EventArgs e)
        {
            _status = false;
           Viewer _see = new Viewer(listView1.SelectedItems[0].Index);
            _see.Show();
        }

        public static bool _status = true;
        public static Thread th;
        public void Start_recive()
        {
            
                th = new Thread(startThread);
                th.Start();
           
        }

        public void startThread()
        {
           // var s = Task.Run(() => {
                while (true)
                {
                try
                {
                    if (_status)
                    {
                        IPEndPoint _aliceSRV = null;
                        byte[] rec = Connection.server1.Whait_recive(ref _aliceSRV);
                        string str_rec = BaseTool.Convertbtst(rec);
                        Console.WriteLine("<====== " + _aliceSRV.ToString() + "  " + str_rec);
                        Command._CheakCommand(rec, _aliceSRV);
                    }
                   
                }
                catch
                {
                    Console.WriteLine("Ожидание....");
                }
                }
                // string test = BaseTool.Convertbtst(Connection.server1.Whait_recive());
                // Console.WriteLine(test);
           // });
        }
        
        
    }
}
