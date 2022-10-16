using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace CodeLibrary.BitmapCopy
{
    public class BitmapCopyTestDemo
    {
        public static void Run()
        {
            Image image = Bitmap.FromFile(@"C:\Users\Luna\Desktop\宋雨琦\New folder\20220606191141_986b0.jpg");
            MemoryStream stream = new MemoryStream();
            // Save image to stream.
            image.Save(stream, ImageFormat.Png);

            Bitmap bitmap1 = (Bitmap)Bitmap.FromStream(stream);

            Bitmap bitmap2 = (Bitmap)Bitmap.FromStream(stream);
            stream.Dispose();
            bitmap1.Dispose();


        }
    }
}
