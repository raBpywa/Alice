using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Drawing;
using System.IO;
namespace Alice_client
{
   static class BaseTool
    {
         public enum  IPorPORT
        {
            IP,
            PORT
        }
        public static string Convertbtst(byte[] data)
        {
            string out_ = Encoding.ASCII.GetString(data, 0, data.Length);
            return out_;
        }

        public static byte[] Convertbtst(string data)
        {
            byte[] bt = Encoding.ASCII.GetBytes(data);
            return bt;
        }
        public static string _get_ip_or_port(string stroka, IPorPORT ipport)
        {
            string[] spl = new string[] { ":" };
            string[] mas = stroka.Split(spl, StringSplitOptions.None);

            return mas[(int)ipport];
        }

        public static string[] _GetArrSplit(string input)
        {
            string[] mas = input.Split(new char[] { '[', ']' }, StringSplitOptions.RemoveEmptyEntries);
            return mas;
        }
        public static string[] _GetArrSplit(byte[] bt)
        {
            string data = BaseTool.Convertbtst(bt);
            string[] mas = data.Split(new char[] { '[', ']' }, StringSplitOptions.RemoveEmptyEntries);
            return mas;
        }

        public static IPEndPoint  _GetArrSplit(string strmas,int indexIP)
        {
            string data =BaseTool._GetArrSplit( strmas)[indexIP];
            IPAddress ipadress = IPAddress.Parse(_get_ip_or_port(data, IPorPORT.IP));
            IPEndPoint _ip = new IPEndPoint(ipadress,Convert.ToInt32( _get_ip_or_port(data, IPorPORT.PORT)));
            
            return _ip;
        }

        public static Bitmap _Pullimage(List<PartsImage> ForPullImage)
        {
            Bitmap test = ForPullImage[0]._real_image;
            int width = test.Width * 10;
            int height = test.Height * 10;
            Bitmap bi = new Bitmap(width, height);

            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    var index = i * 10 + j;

                    var graphics = Graphics.FromImage(bi);
                    graphics.DrawImage(ForPullImage[index]._real_image, new Rectangle(
                        i * ForPullImage[index]._real_image.Width, 
                        j * ForPullImage[index]._real_image.Height,
                        ForPullImage[index]._real_image.Width,
                        ForPullImage[index]._real_image.Height));
                    graphics.Dispose();
                    // bi.Save("b"+index+".bmp");
                }
            }

            // bi.Save("bi.bmp");
            return bi;
        }

        public static List<PartsImage> _GetList (List<byte[]> ForBigImage)
        {
            List<PartsImage> imageListAndCoord = new List<PartsImage>();
            int i = 0;
            foreach (var byt in ForBigImage)
            {
                PartsImage one = new PartsImage(byt[0], byt);

                imageListAndCoord.Add(one);

            }

            return imageListAndCoord;

        }
        public static Bitmap _ConvertListToArray(byte[] one_image)
        {
            byte[] realimage = new byte[one_image.Length - 1];
            Array.Copy(one_image, 1, realimage, 0, realimage.Length);
            Bitmap bmp;
            using (var ms = new MemoryStream(realimage))
            {
                bmp = new Bitmap(ms);
            }
            return bmp;
        }

        public static string[] _DelFirstElement(string[] mas)
        {
            string[] new_mas = new string[mas.Length - 1];
            Array.Copy(mas, 1, new_mas, 0, new_mas.Length);
            return new_mas;
        }

    }
}
