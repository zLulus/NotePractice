using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace CodeLibrary.BitmapCopy
{
    public class BitmapCopyTestDemo
    {
        public static void Run()
        {
            Image image = Bitmap.FromFile(@"file path");
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
