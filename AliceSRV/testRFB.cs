using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Drawing;
namespace AliceSRV
{
    public static class testRFB
    {
        public static void Start(int i)
        {
            Size sz = PrtSC.GetSizeScreen();
            //Thread.Sleep(10000);
            Bitmap bm1=PixelGrabb.CreateScreenCapture(0, 0, sz.Width, sz.Height);
            bm1.Save("1.bmp");
            //Thread.Sleep(1);
            Bitmap bm2 = PixelGrabb.CreateScreenCapture(0, 0, sz.Width, sz.Height);
            bm2.Save("2.bmp");
            Rectangle rc=new Rectangle();

            rc= PixelGrabb.GetChangeArea(bm1, bm2);
            if (rc.Width != -1)
            {
                Bitmap nwbtmp = BaseTool.CropImage(bm2, rc);
                nwbtmp.Save("change.bmp");
            }
        }

    }
}
