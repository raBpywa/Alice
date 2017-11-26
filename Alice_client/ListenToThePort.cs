using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using System.Drawing;
using System.Windows.Forms;

namespace Alice_client
{
    class ListenToThePort
    {
        int countErorr = 0;
        public Bitmap BackGround;
        IPEndPoint ep;
        UdpClient udpClient;
        Viewer viewer;
        public bool isStart = false;
        public bool isViewer = false;

        public delegate void MethodContainer(Bitmap BackGround);

        //Событие OnCount c типом делегата MethodContainer.
        public static event MethodContainer EventConvertToBitmap;

        public ListenToThePort(int Port)
        {
            udpClient = new UdpClient(Port);
            ep = new IPEndPoint(IPAddress.Any,19999);
        }
        
        public void Start()
        {
            isStart = true;
            while (isStart)
            {
                System.Windows.Forms.Application.DoEvents();
                try
                {
                    MemoryStream memoryStream = new MemoryStream();
                    byte[] bytes = udpClient.Receive(ref ep);
                    memoryStream.Write(bytes, 2, bytes.Length - 2);

                    int countMsg = bytes[0] - 1;
                    if (countMsg > 10)
                        throw new Exception("Потеря первого пакета");
                    for (int i = 0; i < countMsg; i++)
                    {
                        byte[] bt = udpClient.Receive(ref ep);
                        memoryStream.Write(bt, 0, bt.Length);
                    }

                    Receive_GetData(memoryStream.ToArray());
                    memoryStream.Close();
                }
                catch(Exception ex)
                {
                  Console.WriteLine(  countErorr++);
                }
            }
        }

        public static void Start(Socket s)
        {
           bool isStart = true;
            while (isStart)
            {
                System.Windows.Forms.Application.DoEvents();
                try
                {
                    MemoryStream memoryStream = new MemoryStream();
                    byte[] bytes = new byte[65500];
                    int o= s.Receive(bytes);

                    if (o != 0)
                    {
                       // break;
                    }
                    memoryStream.Write(bytes, 2, bytes.Length - 2);

                    int countMsg = bytes[0] - 1;
                    if (countMsg > 10)
                        throw new Exception("Потеря первого пакета");
                    for (int i = 0; i < countMsg; i++)
                    {
                        byte[] bt = new byte[65500];

                        o = s.Receive(bt);
                        if (o != 65500)
                        {
                            // break;
                        }
                        memoryStream.Write(bt, 0, bt.Length);
                    }

                    Receive_GetData(memoryStream.ToArray());
                    memoryStream.Close();
                }
                catch (Exception ex)
                {
                  //  Console.WriteLine(countErorr++);
                }
            }
        }


        public void Stop()
        {
            isStart = false;
        }
        
       static private void Receive_GetData(byte[] Date)
        {
            MemoryStream memoryStream = new MemoryStream(Date);
            Bitmap bmp = (Bitmap)System.Drawing.Bitmap.FromStream(memoryStream);
           // bmp.Save("dsadasd.jpg");
            EventConvertToBitmap(bmp);
            Console.WriteLine(Date.Length);
            // Console.WriteLine("Получена картинка");
            //  BackGround = ConvertToBitmap(Date);
        }
        private Bitmap ConvertToBitmap(byte[] bytes)
        {
            MemoryStream memoryStream = new MemoryStream(bytes);
            Bitmap bmp = (Bitmap)System.Drawing.Bitmap.FromStream(memoryStream);
            EventConvertToBitmap(bmp);
            Console.WriteLine("Получена картинка");
            
            return bmp;
        }
    }
}
