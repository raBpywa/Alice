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
        public event MethodContainer EventConvertToBitmap;

        public ListenToThePort(int Port)
        {
            udpClient = new UdpClient(Port);
            ep = new IPEndPoint(IPAddress.Loopback,0);
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

       
        public void Stop()
        {
            isStart = false;
        }
        
        private void Receive_GetData(byte[] Date)
        {
            BackGround = ConvertToBitmap(Date);
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
