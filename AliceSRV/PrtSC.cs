using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;
using System.Net;

namespace AliceSRV
{
    static class PrtSC
    {
        public static List<byte[]> All_image { get; set; }
        public static void StartPrtSC(Connection server1, IPEndPoint IP)
        {
            int height = Screen.PrimaryScreen.Bounds.Height;
            int width = Screen.PrimaryScreen.Bounds.Width;
            Bitmap printscreen = new Bitmap(width, height);
            Graphics graphics = Graphics.FromImage(printscreen as Image);
            graphics.CopyFromScreen(0, 0, 0, 0, printscreen.Size);
            All_image = BaseTool.CutPart(printscreen);
            for (int i = 0; i < All_image.Count; i++)
            {
                server1.Send_mess(All_image[i], IP);
                server1.Whait_recive();
                Thread.Sleep(10);
            }

        }
        public static Bitmap GetPrtSC()
        {
            int height = Screen.PrimaryScreen.Bounds.Height;
            int width = Screen.PrimaryScreen.Bounds.Width;
            Bitmap printscreen = new Bitmap(width, height);
            Graphics graphics = Graphics.FromImage(printscreen as Image);
            graphics.CopyFromScreen(0, 0, 0, 0, printscreen.Size);
            var ms = new System.IO.MemoryStream();
            printscreen.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);

            return new Bitmap(ms);

        }

        public static void Update(Connection server1, IPEndPoint IP)
        {
            _Get_new_screen(server1, IP);


        }

        public static void _Get_new_screen(Connection server1, IPEndPoint IP)
        {

            List<byte[]> _update = new List<byte[]>();
            Image[] array = BaseTool.CutInToParts(PrtSC.GetPrtSC());
            server1.Send_mess(BaseTool.Convertbtst("update_data"), IP);
            for (byte i = 0; i < array.Length; i++)
            {
                int index = (int)i;
                byte[] bitbyte = BaseTool.ConvertImageToByteArray(array[index]);

                //Thread.Sleep(1);
                byte[] new_byte = new byte[bitbyte.Length + 1];
                Array.Copy(bitbyte, 0, new_byte, 1, new_byte.Length - 1);
                new_byte[0] = i;//добавили координату
                if (!All_image[(int)i].SequenceEqual(new_byte))
                {
                    server1.Send_mess(new_byte, IP);
                    All_image[(int)i] = new_byte;
                    Thread.Sleep(20);
                }
                _update.Add(new_byte);
            }

            server1.Send_mess(BaseTool.Convertbtst("stop_update_data"), IP);

        }

        public static List<byte[]> CutInToParts(Connection server1, IPEndPoint IP)
        {
            All_image = new List<byte[]>();
            //byte[] onliv=server1.Whait_recive_TRY_CATCH(ref IP);
            //server1.Send_mess(BaseTool.Convertbtst("[stop]"), IP);
            // server1.Send_mess(BaseTool.Convertbtst("start_PrtSc"), IP);
            //byte[] imagecomand = BaseTool.Convertbtst("[image_byte]");
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
                Thread.Sleep(50);
                //server1.Send_mess(new_byte, IP);
                

                byte[] rec = server1.Whait_recive(ref IP);
                string BAs = BaseTool.Convertbtst(rec);
                while (BAs!="OKKKK")
                {
                    //server1.Send_mess(new_byte, IP);
                    rec = server1.Whait_recive(ref IP);
                    BAs = BaseTool.Convertbtst(rec);
                }
                //Thread.Sleep(10);

                Console.Write(i);

            }
            
            


            return All_image;
        }


    }
}
