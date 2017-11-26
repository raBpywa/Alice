using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Drawing;

namespace Alice_server
{
    static class BaseTools
    {
        public static List<byte[]> CutMsg(byte[] bt)
        {
            int Lenght = bt.Length;
            byte[] temp;
            List<byte[]> msg = new List<byte[]>();
            MemoryStream memoryStream = new MemoryStream();
            // Записываем в первые 2 байта количество пакетов
            memoryStream.Write(BitConverter.GetBytes((short)((Lenght /65500) + 1)), 0, 2);
            // Далее записываем первый пакет
            memoryStream.Write(bt, 0, bt.Length);
            memoryStream.Position = 0;
            // Пока все пакеты не разделили - делим КЭП
            Console.WriteLine(bt.Length);
            while (Lenght > 0)
            {
                temp = new byte[65500];
                memoryStream.Read(temp, 0, 65500);
                msg.Add(temp);
                Lenght -= 65500;
            }
            return msg;
        }

        public static string Convertbtst(byte[] data)
        {
            string out_ = Encoding.ASCII.GetString(data, 0, data.Length);
            return out_;
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
