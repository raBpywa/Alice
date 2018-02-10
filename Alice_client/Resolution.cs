using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alice_client
{
    public static class Resolution
    {
        private static int height_Y = 480;
        private static int weight_x = 640;
        private static int ThreadSpeed = 500;
        private static int TimeSend = 100;
        private static int allpart = 36;
        private static int row_lenght= 6;
        public static int height { get { return height_Y; } }
        public static int weight { get { return weight_x; } }
        public static int part { get { return allpart; } }
        public static int rowlenght { get { return row_lenght; } }
        public static int speed { get { return ThreadSpeed; } }
        public static int timeSend { get { return TimeSend; } }
        public static void SetResolution(string resolu)
        {
            string[] mas = resolu.Split(new char[] { 'x' }, StringSplitOptions.None);
            height_Y = Convert.ToInt32(mas[1]);
            weight_x = Convert.ToInt32(mas[0]);
        }

        public static void SetPart(int part)
        {            
            row_lenght = (int) Math.Sqrt(part);
            allpart = row_lenght * row_lenght;
        }
        public static void SetSpeed(int speed)
        {
            ThreadSpeed = speed;
        }
        public static void SetTimeSendd(int speedtime)
        {
            TimeSend = speedtime;
        }
    }
}
