using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Alice_client
{
    class PartsImage
    {
       public int number { get; set; }
       public Bitmap _real_image { get; set; }


        public PartsImage(int number, byte[] _real_byte_image)
        {
            this.number = number;
            this._real_image =BaseTool._ConvertListToArray(_real_byte_image);
        }
    }
}
