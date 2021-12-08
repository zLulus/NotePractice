using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CodeLibrary.BitmapConvert
{
    public static class BitmapConvertDemo
    {
        public static void Run()
        {
            //Test1();
            //Test2();
            //Test3();
            //Test4();
            //Test5();
            Test6();
        }

        private static void Test1()
        {
            System.Drawing.Bitmap bmp = new System.Drawing.Bitmap(@"D:\壁纸\th5.jpg");
            LockBitmap lockBitmap = new LockBitmap(bmp);
            //lockBitmap.LockBits();
            //lockBitmap.UnlockBits();
            bool isOk = true;
            for (int i = 0; i < bmp.Width; i++)
                for (int j = 0; j < bmp.Height; j++)
                {
                    var c1 = bmp.GetPixel(i, j);
                    var c2 = lockBitmap.GetPixel(i, j);
                    if (c1 != c2)
                    {
                        isOk = false;

                    }
                }
            if (isOk)
                Console.WriteLine($"{nameof(LockBitmap)} is ok");
            else
                Console.WriteLine($"{nameof(LockBitmap)} is not ok");
        }

        public static void Test2()
        {
            System.Drawing.Bitmap bmp = new System.Drawing.Bitmap(@"D:\壁纸\th5.jpg");
            Task.Run(() =>
            {
                try
                {
                    while (true)
                    {
                        for (int i = 0; i < bmp.Width; i++)
                            for (int j = 0; j < bmp.Height; j++)
                            {
                                var c1 = bmp.GetPixel(i, j);
                            }
                        Thread.Sleep(100);
                    }
                   
                }
                catch(Exception ex)
                {

                }
            });

            Task.Run(() =>
            {
                try
                {
                    while (true)
                    {
                        for (int i = 0; i < bmp.Width; i++)
                            for (int j = 0; j < bmp.Height; j++)
                            {
                                var c1 = bmp.GetPixel(i, j);
                            }
                        Thread.Sleep(100);
                    }
                       
                }
                catch (Exception ex)
                {

                }
            });
        }

        public static void Test3()
        {
            System.Drawing.Bitmap bmp = new System.Drawing.Bitmap(@"D:\壁纸\th5.jpg");
            LockBitmap lockBitmap = new LockBitmap(bmp);
            Task.Run(() =>
            {
                try
                {
                    while (true)
                    {
                        for (int i = 0; i < bmp.Width; i++)
                            for (int j = 0; j < bmp.Height; j++)
                            {
                                var c1 = lockBitmap.GetPixel(i, j);
                            }
                        Thread.Sleep(100);
                    }
                       
                }
                catch (Exception ex)
                {

                }
            });

            Task.Run(() =>
            {
                try
                {
                    while (true)
                    {
                        for (int i = 0; i < bmp.Width; i++)
                            for (int j = 0; j < bmp.Height; j++)
                            {
                                var c1 = lockBitmap.GetPixel(i, j);
                            }
                        Thread.Sleep(100);
                    }
                        
                }
                catch (Exception ex)
                {

                }
            });
        }

        static System.Drawing.Bitmap staticBitmap;
        public static void Test4()
        {
            staticBitmap= new System.Drawing.Bitmap(@"D:\壁纸\th5.jpg");
            Task.Run(() =>
            {
                try
                {
                    while (true)
                    {
                        for (int i = 0; i < staticBitmap.Width; i++)
                            for (int j = 0; j < staticBitmap.Height; j++)
                            {
                                var c1 = staticBitmap.GetPixel(i, j);
                            }
                        Thread.Sleep(100);
                    }
                        
                }
                catch (Exception ex)
                {

                }
            });

            Task.Run(() =>
            {
                try
                {
                    while (true)
                    {
                        for (int i = 0; i < staticBitmap.Width; i++)
                            for (int j = 0; j < staticBitmap.Height; j++)
                            {
                                var c1 = staticBitmap.GetPixel(i, j);
                            }
                        Thread.Sleep(100);
                    }
                        
                }
                catch (Exception ex)
                {

                }
            });
        }

        public static void Test5()
        {
            System.Drawing.Bitmap bmp1 = new System.Drawing.Bitmap(@"D:\壁纸\th5.jpg");
            System.Drawing.Bitmap bmp2 = new System.Drawing.Bitmap(@"D:\壁纸\th5.jpg");
            Task.Run(() =>
            {
                try
                {
                    while (true)
                    {
                        for (int i = 0; i < bmp1.Width; i++)
                            for (int j = 0; j < bmp1.Height; j++)
                            {
                                var c1 = bmp1.GetPixel(i, j);
                            }
                        Thread.Sleep(100);

                    }
                       
                }
                catch (Exception ex)
                {

                }
            });

            Task.Run(() =>
            {
                try
                {
                    while (true)
                    {
                        for (int i = 0; i < bmp2.Width; i++)
                            for (int j = 0; j < bmp2.Height; j++)
                            {
                                var c1 = bmp2.GetPixel(i, j);
                            }
                        Thread.Sleep(100);

                    }
                        
                }
                catch (Exception ex)
                {

                }
            });
        }

        static readonly System.Drawing.Bitmap readonlyBitmap = new System.Drawing.Bitmap(@"D:\壁纸\th5.jpg");
        public static void Test6()
        {
            Task.Run(() =>
            {
                try
                {
                    while (true)
                    {
                        for (int i = 0; i < readonlyBitmap.Width; i++)
                            for (int j = 0; j < readonlyBitmap.Height; j++)
                            {
                                var c1 = readonlyBitmap.GetPixel(i, j);
                            }
                        Thread.Sleep(100);

                    }

                }
                catch (Exception ex)
                {

                }
            });

            Task.Run(() =>
            {
                try
                {
                    while (true)
                    {
                        for (int i = 0; i < readonlyBitmap.Width; i++)
                            for (int j = 0; j < readonlyBitmap.Height; j++)
                            {
                                var c1 = readonlyBitmap.GetPixel(i, j);
                            }
                        Thread.Sleep(100);

                    }

                }
                catch (Exception ex)
                {

                }
            });
        }
    }
}
