using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Alice_client
{
    public partial class AddPrey : Form
    {
        public AddPrey()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            label3.Text = "";
            Prey.AddNewPrey(textBox1.Text, textBox2.Text, User._My, Connection.server1);
            

            var s = Task.Run(() => {
                while (true)
                {
                    Thread.Sleep(100);
                    if (_refresh())
                    {
                       
                        Invoke(new Action(() => {
                            Alice_client._UpdatePrey();
                            this.Close(); }));
                        break;
                    }
                }
                // string test = BaseTool.Convertbtst(Connection.server1.Whait_recive());
                // Console.WriteLine(test);
            });

        }

       
        public bool _refresh()
        {
            Invoke(new Action(() => { label3.Text = Command.response; }));
                
            if (Command.response == "OK")
                return true;
            else return false;
        }

     
    }
}
