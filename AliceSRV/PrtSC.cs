using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using System.Threading;
using System.Net;
using System.IO;

namespace AliceSRV
{
    static class PrtSC
    {
        public static List<byte[]> All_image { get; set; }
        //public static void StartPrtSC(Connection server1, IPEndPoint IP)
        //{
        //    int height =Screen.PrimaryScreen.Bounds.Height;
        //    int width = Screen.PrimaryScreen.Bounds.Width;
        //    Bitmap printscreen = new Bitmap(width, height);
        //    Graphics graphics = Graphics.FromImage(printscreen as Image);
        //    graphics.CopyFromScreen(0, 0, 0, 0, printscreen.Size);
        //    All_image = BaseTool.CutPart(printscreen);
        //    for (int i = 0; i < All_image.Count; i++)
        //    {
        //        server1.Send_mess(All_image[i], IP);
        //        server1.Whait_recive();
        //        Thread.Sleep(10);
        //    }

        //}
        public static Bitmap GetPrtSC()
        {
            int height = Screen.PrimaryScreen.Bounds.Height;
            int width = Screen.PrimaryScreen.Bounds.Width;

            Bitmap printscreen = new Bitmap(width, height);
            Graphics graphics = Graphics.FromImage(printscreen as Image);

            graphics.CopyFromScreen(0, 0, 0, 0, printscreen.Size);

            if (height != Resolution.height & width != Resolution.weight)
            {
                printscreen = BaseTool.ResizeImage(printscreen, new Size(Resolution.weight, Resolution.height));
            }

            Bitmap printscreenTiff = printscreen.Clone(new Rectangle(0, 0, Resolution.weight, Resolution.height), PixelFormat.Format8bppIndexed);
            //BitmapPixel8.SaveGIFWithNewColorTable(printscreen, "1s.jpg", 1024, false);
            //printscreenTiff.Save("rrrrrr.png",ImageFormat.Png);
            //printscreenTiff.Save("12.png", ImageFormat.Png);
            //printscreen.Save("13.Gif", ImageFormat.Gif);
            //printscreen.Save("14.Png", ImageFormat.Png);
            //printscreen.Save("15.Bmp", ImageFormat.Bmp);

            var ms = new System.IO.MemoryStream();
            printscreenTiff.Save(ms, System.Drawing.Imaging.ImageFormat.Png);

            return new Bitmap(ms);

        }
        private static Object thisLock = new Object();
        public static void Update(Connection server1, IPEndPoint IP)
        {

            SendWithOUtSync(server1, IP);


        }

        public static void _Get_new_screen(Connection server1, IPEndPoint IP)
        {
            bool first = true;
            //  List<byte[]> _update = new List<byte[]>();
            Bitmap[] array = BaseTool.CutInToParts(PrtSC.GetPrtSC());

            for (byte i = 0; i < array.Length; i++)
            {
                int index = (int)i;
                byte[] bitbyte = BaseTool.ConvertImageToByteArray(array[index]);
                //File.WriteAllBytes("bitbyte.png", bitbyte);

                //Thread.Sleep(1);
                byte[] new_byte = new byte[bitbyte.Length + 1];
                Array.Copy(bitbyte, 0, new_byte, 1, new_byte.Length - 1);
                new_byte[0] = i;//добавили координату
                while (WhaitAllData.Updatedata)
                {
                    Thread.Sleep(1);
                }
                if (All_image.Count != array.Length)
                {
                    array = BaseTool.CutInToParts(PrtSC.GetPrtSC());
                    i = 0;
                    continue;
                }
                if (!All_image[(int)i].SequenceEqual(new_byte))
                {
                    if (first)
                    {
                        server1.Send_mess(BaseTool.Convertbtst("update_data"), IP);
                        first = false;
                    }
                    //Console.WriteLine(i);
                    server1.Send_mess(new_byte, IP);
                    //Console.WriteLine(new_byte.Length);
                    //File.WriteAllBytes("test.bin", new_byte);
                    All_image[(int)i] = new_byte;
                    Thread.Sleep(100);
                }
                //  _update.Add(new_byte);
            }
            if (!first)
            {
                server1.Send_mess(BaseTool.Convertbtst("stop_update_data"), IP);
            }
        }

        public static void SendWithOUtSync(Connection server1, IPEndPoint IP)
        {
            bool first = true;
            //  List<byte[]> _update = new List<byte[]>();
            Bitmap[] array = BaseTool.CutInToParts(PrtSC.GetPrtSC());

            for (byte i = 0; i < array.Length; i++)
            {
                int index = (int)i;
                byte[] bitbyte = BaseTool.ConvertImageToByteArray(array[index]);
                //File.WriteAllBytes("bitbyte.png", bitbyte);

                //Thread.Sleep(1);
                byte[] new_byte = new byte[bitbyte.Length + 1];
                Array.Copy(bitbyte, 0, new_byte, 1, new_byte.Length - 1);
                new_byte[0] = i;//добавили координату
                while (WhaitAllData.Updatedata)
                {
                    Thread.Sleep(1);
                }
                if (All_image.Count != array.Length)
                {
                    array = BaseTool.CutInToParts(PrtSC.GetPrtSC());
                    i = 0;
                    continue;
                }
                if (!All_image[(int)i].SequenceEqual(new_byte))
                {
                    // Console.WriteLine(i);
                    server1.Send_mess(new_byte, IP);
                    //Console.WriteLine(new_byte.Length);
                    //File.WriteAllBytes("test.bin", new_byte);
                    All_image[(int)i] = new_byte;
                    Thread.Sleep(Resolution.timeSend);
                }
                //  _update.Add(new_byte);
            }

        }

        public static List<byte[]> CutInToParts(Connection server1, IPEndPoint IP)
        {
            WhaitAllData.BAsNum = 1;
            All_image = new List<byte[]>();
            Image[] array = BaseTool.CutInToParts(PrtSC.GetPrtSC());
            for (byte i = 0; i < array.Length; i++)
            {
                int index = (int)i;
                byte[] bitbyte = BaseTool.ConvertImageToByteArray(array[index]);
                byte[] new_byte = new byte[bitbyte.Length + 1];

                Array.Copy(bitbyte, 0, new_byte, 1, new_byte.Length - 1);
                new_byte[0] = i;//добавили координату
                All_image.Add(new_byte);
                //byte[] withimagecomand = imagecomand.Concat(new_byte).ToArray();
                server1.Send_mess(new_byte, IP);
                Thread.Sleep(100);

                while (!WhaitAllData.BAs)
                {
                    while (i >= WhaitAllData.BAsNum)
                    {

                        server1.Send_mess(new_byte, IP);
                        Console.WriteLine("REPEAT SEND " + i);
                        //byte[] rec = server1.Whait_recive(ref IP);
                        //string  BAs = BaseTool.Convertbtst(rec);
                        Thread.Sleep(100);
                        WhaitAllData.BAsNum++;
                    }

                    if (WhaitAllData.NoError)
                    {
                        WhaitAllData.BAsNum--;
                        i--;
                        WhaitAllData.BAs = true;
                        WhaitAllData.NoError = false;
                    }
                    Thread.Sleep(10);
                    Console.WriteLine(i + " > " + WhaitAllData.BAsNum);
                }

                WhaitAllData.BAs = false;
                Console.Write(i + " to---> " + IP);

            }




            return All_image;
        }


        public static List<byte[]> AllImageUpdatePart(Connection server1, IPEndPoint IP)
        {

            All_image = new List<byte[]>();
            Image[] array = BaseTool.CutInToParts(PrtSC.GetPrtSC());
            for (byte i = 0; i < array.Length; i++)
            {
                int index = (int)i;
                byte[] bitbyte = BaseTool.ConvertImageToByteArray(array[index]);
                byte[] new_byte = new byte[bitbyte.Length + 1];

                Array.Copy(bitbyte, 0, new_byte, 1, new_byte.Length - 1);
                new_byte[0] = i;//добавили координату
                All_image.Add(new_byte);
                //byte[] withimagecomand = imagecomand.Concat(new_byte).ToArray();
                server1.Send_mess(new_byte, IP);
                Thread.Sleep(10);
                Console.Write(i);

            }

            return All_image;
        }
        public static Size GetSizeScreen()
        {
            return new Size(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);

        }

    }


}
