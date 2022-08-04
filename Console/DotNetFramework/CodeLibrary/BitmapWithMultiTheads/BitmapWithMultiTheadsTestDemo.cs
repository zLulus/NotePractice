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
            //Test2();
            Test3();
        }

        private static void Test3()
        {
            var bmp = new Bitmap($"{Directory.GetCurrentDirectory()}\\BitmapWithMultiTheads\\OIP-C.png");
            var t = new Task(() =>
            {
                Method3(bmp);
            });
            var t2 = new Task(() =>
            {
                Method3(bmp);
            });
            t.Start();
            t2.Start();
        }

        private static void Method3(Bitmap bmp)
        {
            Bitmap b1 = null;
            try
            {
                //lock (lockObject)
                    b1 = bmp.Clone(new Rectangle(0, 0, bmp.Width, bmp.Height), bmp.PixelFormat);
                //using (MemoryStream ms = new MemoryStream())
                //{
                //    b1.Save(ms, ImageFormat.Png);
                //    var bytes1 = ms.GetBuffer();
                //}
            }
            catch(Exception ex)
            {

            }
            finally
            {
                b1?.Dispose();
            }
        }

        private static void Test2()
        {
            var bmp = new Bitmap($"{Directory.GetCurrentDirectory()}\\BitmapWithMultiTheads\\OIP-C.png");
            var t = new Task(() =>
            {
                Method2(bmp);
            });
            var t2 = new Task(() =>
            {
                Method2(bmp);
            });
            t.Start();
            t2.Start();
        }

        private static void Method2(Bitmap bmp)
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
        }

        private static void Test1()
        {
            var bmp = new Bitmap($"{Directory.GetCurrentDirectory()}\\BitmapWithMultiTheads\\OIP-C.png");
            var t = new Task(() =>
            {
                Method1(bmp);
            });
            var t2 = new Task(() =>
            {
                Method1(bmp);
            });
            t.Start();
            t2.Start();
        }

        private static void Method1(Bitmap bmp)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                lock (lockObject)
                {
                    bmp.Save(ms, ImageFormat.Png);
                }
                var bytes2 = ms.GetBuffer();
            }
        }
    }
}
