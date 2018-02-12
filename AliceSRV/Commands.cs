using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Threading;

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
                case "stop_watching":
                    Stop_Watching();
                    break;
                case "OK":
                    Start_Watching();
                    break;
                case "for_prey_connect":
                    ForPreyConnect(cmd);
                    break;
                case "OKKKK":
                    Bas();
                    break;
                case "NO":
                    BasMInus();
                    break;
                case "change_resolution":
                    Change_resolution(cmd[1]);
                    break;
                case "change_part":
                    Change_part(cmd[1]);
                    break;
                case "change_speed":
                    change_speed(cmd[1]);
                    break;
                case "change_timesend":
                    change_timesend(cmd[1]);
                    break;
                default:
                    break;
            }
        }


        public static void Mousemove(string[] cmd)
        {

         MouseHook.Mouse_Move(Convert.ToInt32( cmd[1]), Convert.ToInt32(cmd[2]));
        }
        public static void Mouse_down(string[] cmd)
        {

            MouseHook.Mouse_DOWN(cmd[1],Convert.ToInt32(cmd[2]), Convert.ToInt32(cmd[3]));
        }
        public static void Mouse_up(string[] cmd)
        {

            MouseHook.Mouse_UP(cmd[1],Convert.ToInt32(cmd[2]), Convert.ToInt32(cmd[3]));
        }
        public static void Key_Press(string[] cmd)
        {
            SendKeys.SendWait(cmd[1]);
        }
        public static void Stop_Watching()
        {
            WhaitAllData.stopWatch = true;
        }
        public static void Start_Watching()
        {
            WhaitAllData.startWatch = true;
            // SendKeys.SendWait(cmd[1]);
        }
        public static void Bas()
        {
            WhaitAllData.BAsNum++;
            WhaitAllData.BAs = true;
        }
        public static void BasMInus()
        {
            
            WhaitAllData.NoError = true;
        }
        public static void ForPreyConnect(string[] cmd)
        {
            for (int i = 0; i < 2; i++)
            {
                IPAddress ip = IPAddress.Parse(BaseTool._get_ip_or_port(cmd[1], BaseTool.IPorPORT.IP));
                IPEndPoint remote = new IPEndPoint(ip, Convert.ToInt32(BaseTool._get_ip_or_port(cmd[1], BaseTool.IPorPORT.PORT)));
                Program.server1.Send_mess(BaseTool.Convertbtst("[ping]"), remote);
                WhaitAllData.Ip = remote;
                WhaitAllData.ForPreyConnect = true;
                //Thread.Sleep(10);
            }
        }

        public static void Change_resolution(string resolution)
        {
            Resolution.SetResolution(resolution);
        }
        public static void change_speed(string resolution)
        {
            Resolution.SetSpeed(Convert.ToInt32( resolution));
        }
        public static void Change_part(string part)
        {
            WhaitAllData.Updatedata = true;
            Resolution.SetPart(Convert.ToInt32(part));
            Console.WriteLine(Resolution.rowlenght);
            //WhaitAllData.stopWatch = true;
            WhaitAllData.restartWatch = false;
            PrtSC.AllImageUpdatePart(Program.server1, WhaitAllData.Ip);
            WhaitAllData.Updatedata = false;
        }
        public static void change_timesend(string  timesend)
        {
            Resolution.SetTimeSendd(Convert.ToInt32( timesend));
        }
    }
}
