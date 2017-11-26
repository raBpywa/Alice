using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Drawing;
using System.IO;


namespace Alice_client
{
    class SendUDP
    {
        public delegate void MethodContainer(Bitmap BackGround);

        //Событие OnCount c типом делегата MethodContainer.
        public event MethodContainer EventConvertToBitmap;

        Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Dgram,
ProtocolType.Udp);
        string IP;
        int port;
        string text;
        IPAddress broadcast;
        byte[] sendbuf;
        public  SendUDP(string IP,int port,string text)
        {
       this.IP= IP;
            this.port= port;
            this.text= text;
         this.broadcast = IPAddress.Parse(IP);
            this.sendbuf = Encoding.ASCII.GetBytes(text);
            IPEndPoint ep = new IPEndPoint(broadcast, port);
        }
       //("46.173.208.70");
       public void Send(string IP, int port, string text)
        {
            this.IP = IP;
            this.port = port;
            this.text = text;

            byte[] sendbuf = Encoding.ASCII.GetBytes(text);
            IPEndPoint ep = new IPEndPoint(broadcast, port);
            for (int i = 0; i < 5; i++)
            {
                Thread.Sleep(3);
                s.SendTo(sendbuf, ep);
                Console.WriteLine("Message sent to the broadcast address");
                byte[] recdbuf22 = new byte[65500];
                // Console.WriteLine("Message recevied");
                while (true)
                {
                    try
                    {
                        ListenToThePort.Start(s);
                        //s.Receive(recdbuf22);
                        //ConvertToBitmap(recdbuf22, s);
                    }
                    catch(Exception ex)
                    {

                    }

                        //string ttt = Encoding.ASCII.GetString(recdbuf22);
                    // Console.WriteLine(ttt.Replace("\0",""));
                }
            }
       
    }

        private void ConvertToBitmap(byte[] bytes, Socket s)
        {
            int _count = bytes[0] - 1;
            MemoryStream memoryStream = new MemoryStream(bytes);
            memoryStream.Write(bytes, 2, bytes.Length - 2);
            int countMsg = _count;
            if (countMsg > 10)
                throw new Exception("Потеря первого пакета");
            for (int i = 0; i < countMsg; i++)
            {
                s.Receive(bytes);
                memoryStream.Write(bytes, 0, bytes.Length);
            }

            Receive_GetData(memoryStream.ToArray());
            memoryStream.Close();
            //Bitmap bmp = (Bitmap)System.Drawing.Bitmap.FromStream(memoryStream);
         

            //Console.WriteLine("Получена картинка");

           
        }

        private Bitmap Receive_GetData(byte[] v)
        {
            MemoryStream memoryStream = new MemoryStream(v);
            Bitmap bmp = (Bitmap)System.Drawing.Bitmap.FromStream(memoryStream);
            EventConvertToBitmap(bmp);
            //Console.WriteLine("Получена картинка");

            return bmp;
        }

        public void Close()
        {

            s.Close();
        }
    }
}
