using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Alice_client
{
    class Program
    {
        static void Main(string[] args)
        {

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Alice_client());

           


            //Connection server1 = new Connection("46.173.208.70",19999);//("195.128.124.171", 19999);//("192.168.100.3", 19999);
            //User _my = new User(server1);
            //_my._all_sacrifice = Prey._Get_Prey_List(server1,_my);
            //if (_my._all_sacrifice.Count==0)
            //{
            //    Prey.AddNewPrey(_my, server1) ; //сопоставить Prey с User
            //}
          

            //ConnectPrey.Connect(_my, server1,2);
            //IPEndPoint _aliceSRV = null ;
            //byte[] mess= server1.Whait_recive(ref _aliceSRV);
            //Console.WriteLine(BaseTool.Convertbtst(mess));
            ////byte[] _resp = BaseTool.Convertbtst("OK");
            //server1.Send_mess("[OK]["+ _my._Login+ "][connect]", _aliceSRV);

            
            //while (true)
            //{
            //    string uuu = BaseTool.Convertbtst(server1.Whait_recive());
            //    Console.WriteLine(uuu);
            //    server1.Send_mess(BaseTool.Convertbtst("OK"), _aliceSRV);

            //    if (uuu == "start_PrtSc")
            //    {
            //        break;
            //    }
                
            //}
            //List<byte[]> allbyte = new List<byte[]>();
            //while (true)
            //{
            //    allbyte.Add(server1.Whait_recive());
            //    server1.Send_mess(BaseTool.Convertbtst("OK"), _aliceSRV);
            //    if (allbyte.Count==100)
            //    {

            //        break;
            //    }
            //}

            //BaseTool._Pullimage(BaseTool._GetList(allbyte));


            //    IPEndPoint iprec = null;
           

            //    while (true)
            //    {
            //        server1.Send_mess(BaseTool.Convertbtst(_my.dataToString() + "--->" + Console.ReadLine()));
            //    }

           
        }





    }


}
