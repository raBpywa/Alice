using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;

namespace AliceSRV
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

        public static string _GenerateToken()
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var stringChars = new char[8];
            var random = new Random();

            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }

            var finalString = new String(stringChars);
            return finalString;
        }

        public static Image[] CutInToParts(Bitmap image)
        {
            int width = image.Size.Width / 10;
            int height = image.Size.Height / 10;
            var imagearray = new Image[100];
            for(int i=0;i<10;i++)
            {
                for (int j=0;j<10;j++)
                {
                    var index = i * 10 + j;
                    imagearray[index] = new Bitmap(width, height);
                    var graphics = Graphics.FromImage(imagearray[index]);
                    graphics.DrawImage(image, new Rectangle(0, 0, width, height),new Rectangle(i*width,j*height,width,height),GraphicsUnit.Pixel);
                    graphics.Dispose();


                    //imagearray[index].Save(index + ".bmp");
                }
            }
            //Pull(imagearray);  
            return imagearray;
        }

        public static void Pull(Image[] arrray)
        {
            int width = arrray[0].Width*10;
            int height = arrray[0].Height * 10;
            Bitmap bi = new Bitmap(width, height);

            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    var index = i * 10 + j;
                    
                    var graphics = Graphics.FromImage(bi);
                    graphics.DrawImage(arrray[index],new Rectangle(i * arrray[index].Width, j * arrray[index].Height, arrray[index].Width, arrray[index].Height));
                    graphics.Dispose();
                   // bi.Save("b"+index+".bmp");
                }
            }

            bi.Save("bi.bmp");
        }


        public static List<byte[]> CutPart(Bitmap bitmap)
        {
            List<byte[]> alllistbyte = new List<byte[]>();
            MemoryStream stream = new MemoryStream();
            bitmap.Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg);
            byte[] allbytes = stream.ToArray();
            int coordnumber = 0;
            for (int i = 0; i < allbytes.Length; i++)
            {

                byte[] one = new byte[2048];
                if (allbytes.Length - i < one.Length)
                {
                    one = new byte[allbytes.Length - i];
                }
              
                Array.Copy(allbytes, i, one, 0, one.Length);
                byte[] withcoordinatebyte = new byte[one.Length + 4];
                byte[] coord = BitConverter.GetBytes(coordnumber);
                Array.Copy(coord, withcoordinatebyte, coord.Length);
                Array.Copy(one, 0, withcoordinatebyte, coord.Length, one.Length);
                i += one.Length - 1;
                alllistbyte.Add(withcoordinatebyte);
                coordnumber++;
            }
            return alllistbyte;
        }

        public static byte[]  ConvertImageToByteArray(Image bitmap)
        {

           
            MemoryStream stream = new MemoryStream();
            bitmap.Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg);
            byte[] allbytes = stream.ToArray();
            return allbytes; 
        }
    }
}
