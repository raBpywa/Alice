using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AliceSRV
{
    static class Commands
    {
        public static void _Start(string[] cmd)
        {
            switch (cmd[0])
            {
                case "mouse_move":
                    Mousemove(cmd);
                    break;
                case "mouse_down":
                    Mouse_down(cmd);
                    break;
                case "mouse_up":
                    Mouse_up(cmd);
                    break;

                case "key_press":
                    Key_Press(cmd);
                    break;
            }
        }


        public static void Mousemove(string[] cmd)
        {

         MouseHook.Mouse_Move(Convert.ToInt32( cmd[1]), Convert.ToInt32(cmd[2]));
        }
        public static void Mouse_down(string[] cmd)
        {

            MouseHook.Mouse_DOWN(Convert.ToInt32(cmd[1]), Convert.ToInt32(cmd[2]));
        }
        public static void Mouse_up(string[] cmd)
        {

            MouseHook.Mouse_UP(Convert.ToInt32(cmd[1]), Convert.ToInt32(cmd[2]));
        }
        public static void Key_Press(string[] cmd)
        {
            SendKeys.SendWait(cmd[1]);
        }
    }
}
