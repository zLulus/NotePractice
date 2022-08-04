using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeLibrary.BitmapWithMultiTheads
{
    public class BitmapWithMultiTheadsTestDemo
    {
        private static object lockObject = new object();
        public static void Run()
        {
            //Test1();
            Test2();
        }

        private static void Test2()
        {
            var bmp = new Bitmap($"{Directory.GetCurrentDirectory()}\\BitmapWithMultiTheads\\OIP-C.png");
            var t = new Task(() =>
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    object b1 = null;
                    lock (lockObject)
                        b1 = bmp.Clone();
                    if (b1 != null)
                    {
                        var s = (b1 as Bitmap);
                        s.Save(ms, ImageFormat.Png);
                        var bytes1 = ms.GetBuffer();
                        s?.Dispose();
                    }
                }
            });
            var t2 = new Task(() =>
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    object b1 = null;
                    lock (lockObject)
                        b1 = bmp.Clone();
                    if (b1 != null)
                    {
                        var s = (b1 as Bitmap);
                        s.Save(ms, ImageFormat.Png);
                        var bytes1 = ms.GetBuffer();
                        s?.Dispose();
                    }
                }
            });
            t.Start();
            t2.Start();
        }

        private static void Test1()
        {
            var bmp = new Bitmap($"{Directory.GetCurrentDirectory()}\\BitmapWithMultiTheads\\OIP-C.png");
            var t = new Task(() =>
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    lock (lockObject)
                    {
                        bmp.Save(ms, ImageFormat.Png);
                    }
                    var bytes1 = ms.GetBuffer();
                }
            });
            var t2 = new Task(() =>
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    lock (lockObject)
                    {
                        bmp.Save(ms, ImageFormat.Png);
                    }
                    var bytes2 = ms.GetBuffer();
                }
            });
            t.Start();
            t2.Start();
        }
    }
}
