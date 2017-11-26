using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Alice_client
{
    public partial class Viewer : Form
    {
        public Viewer()
        {
            InitializeComponent();
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
    }
}
