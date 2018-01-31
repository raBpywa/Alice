using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;
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

        public static Bitmap[] CutInToParts(Bitmap image)
        {
            //image.Save("final", ImageFormat.Png);
            int width = image.Size.Width / Resolution.rowlenght;
            int height = image.Size.Height / Resolution.rowlenght;
            var imagearray = new Bitmap[Resolution.part];
            for(int i=0;i < Resolution.rowlenght; i++)
            {
                for (int j=0;j < Resolution.rowlenght; j++)
                {
                    var index = i * Resolution.rowlenght + j;
                    imagearray[index] = new Bitmap(width, height);
                    //imagearray[index].Save("image", ImageFormat.Png);
                    try
                    {
                        var graphics = Graphics.FromImage(imagearray[index]);
                        graphics.DrawImage(image, new Rectangle(0, 0, width, height), new Rectangle(i * width, j * height, width, height), GraphicsUnit.Pixel);
                        graphics.Dispose();
                    }
                    catch
                    {
                        imagearray = new Bitmap[Resolution.part];
                        i = 0;j = 0;
                    }
                    //imagearray[index].Save("image2Jpeg", ImageFormat.Jpeg);
                    //imagearray[index].Save("image2PNG", ImageFormat.Png);
                    ////imagearray[index] = imagearray[index].Clone(new Rectangle(0, 0, width, height), PixelFormat.Format8bppIndexed);
                    //imagearray[index].Save("image3", ImageFormat.Png);
                    //imagearray[index].Save(index + ".bmp");
                }
            }
            //Pull(imagearray);  
            return imagearray;
        }

        public static void Pull(Image[] arrray)
        {
            int width = arrray[0].Width * Resolution.rowlenght;
            int height = arrray[0].Height * Resolution.rowlenght;
            Bitmap bi = new Bitmap(width, height);

            for (int i = 0; i < Resolution.rowlenght; i++)
            {
                for (int j = 0; j < Resolution.rowlenght; j++)
                {
                    var index = i * Resolution.rowlenght + j;
                    
                    var graphics = Graphics.FromImage(bi);
                    graphics.DrawImage(arrray[index],new Rectangle(i * arrray[index].Width, j * arrray[index].Height, arrray[index].Width, arrray[index].Height));
                    graphics.Dispose();
                   // bi.Save("b"+index+".bmp");
                }
            }

          //  bi.Save("bi.bmp");
        }


        public static List<byte[]> CutPart(Bitmap bitmap)
        {
            List<byte[]> alllistbyte = new List<byte[]>();
            MemoryStream stream = new MemoryStream();
            bitmap.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
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
            bitmap.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
            byte[] allbytes = stream.ToArray();
            return allbytes; 
        }
        public static Bitmap CropImage(Bitmap source, Rectangle section)
        {
            // An empty bitmap which will hold the cropped image
            Bitmap bmp = new Bitmap(section.Width - section.X, section.Height - section.Y);

            Graphics g = Graphics.FromImage(bmp);

            // Draw the given area (section) of the source image
            // at location 0,0 on the empty bitmap (bmp)
            g.DrawImage(source, 0,0, section, GraphicsUnit.Pixel);

            return bmp;
        }

        public static Bitmap ResizeImage(Bitmap imgToResize, Size size)
        {
            try
            {
                Bitmap b = new Bitmap(size.Width, size.Height);
                using (Graphics g = Graphics.FromImage((Image)b))
                {
                    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                    g.DrawImage(imgToResize, 0, 0, size.Width, size.Height);
                }
                return b;
            }
            catch
            {
                Console.WriteLine("Bitmap could not be resized");
                return imgToResize;
            }
        }
    }
}
