using System.Drawing;
using System.IO;


namespace Alice_server
{
    class GetSC
    {
        int width;
        int height;
        public byte[] bytes;
        Bitmap BackGround;
        public  GetSC(int width, int height)
        {
            this.BackGround = new Bitmap(width, height);
            this.width = width;
            this.height = height;
            GetPrtSc(width, height);
        }
        public void Update()
        {
            GetPrtSc(width, height);
        }

        private void GetPrtSc(int width, int height)
        {
            Graphics graphics = Graphics.FromImage(BackGround);
            graphics.CopyFromScreen(0, 0, 0, 0, new Size(width, height));
            this.bytes = ConvertToByte(BackGround);
        }
        private byte[] ConvertToByte(Bitmap bmp)
        {
            MemoryStream memoryStream = new MemoryStream();
            // Конвертируем в массив байтов с сжатием Jpeg
            bmp.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Jpeg);
            return memoryStream.ToArray();
        }

    }
}
