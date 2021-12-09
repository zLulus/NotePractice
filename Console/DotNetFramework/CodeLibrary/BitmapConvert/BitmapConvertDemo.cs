using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;
using static CodeLibrary.BitmapConvert.BitmapSourceHelper;

namespace CodeLibrary.BitmapConvert
{
    public static class BitmapConvertDemo
    {
        private static string file = @"D:\壁纸\th5.jpg";
        public static void Run()
        {
            //Test1();
            //Test2();
            //Test3();
            //Test4();
            //Test5Ok();
            //Test6();
            //Test7Ok();
            Test8();
        }

        private static void Test1()
        {
            System.Drawing.Bitmap bmp = new System.Drawing.Bitmap(file);
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
            System.Drawing.Bitmap bmp = new System.Drawing.Bitmap(file);
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
            System.Drawing.Bitmap bmp = new System.Drawing.Bitmap(file);
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
            staticBitmap= new System.Drawing.Bitmap(file);
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

        public static void Test5Ok()
        {
            System.Drawing.Bitmap bmp1 = new System.Drawing.Bitmap(file);
            System.Drawing.Bitmap bmp2 = new System.Drawing.Bitmap(file);
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

        static readonly System.Drawing.Bitmap readonlyBitmap = new System.Drawing.Bitmap(file);
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

        public static void Test7Ok()
        {
            System.Drawing.Bitmap bmp1 = new System.Drawing.Bitmap(file);
            Task.Run(() =>
            {
                try
                {
                    var cpbmp1 = (Bitmap)bmp1.Clone();
                    while (true)
                    {
                        for (int i = 0; i < cpbmp1.Width; i++)
                            for (int j = 0; j < cpbmp1.Height; j++)
                            {
                                var c1 = cpbmp1.GetPixel(i, j);
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
                    var cpbmp2 = (Bitmap)bmp1.Clone();
                    while (true)
                    {
                        for (int i = 0; i < cpbmp2.Width; i++)
                            for (int j = 0; j < cpbmp2.Height; j++)
                            {
                                var c1 = cpbmp2.GetPixel(i, j);
                            }
                        Thread.Sleep(100);

                    }
                }
                catch (Exception ex)
                {

                }
            });
        }



        private static void Test8()
        {
            //https://stackoverflow.com/questions/1176910/finding-specific-pixel-colors-of-a-bitmapimage
            System.Drawing.Bitmap bmp1 = new System.Drawing.Bitmap(file);
            //MemoryStream ms = new MemoryStream();
            //bmp1.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);

            //var bitmapSource =  BitmapSourceHelper.ImageBytesToImageSource(ms);
            //ms.Close();

            //var pixelColors= BitmapSourceHelper.GetPixels(bitmapSource);
            var bitmapSource = BitmapSourceHelper.BitmapToBitmapSource(bmp1);
            PixelColor[,] pixelColors = new PixelColor[1920, 1080];
            BitmapSourceHelper.CopyPixels(bitmapSource, pixelColors, bitmapSource.PixelWidth * 4, 0);
            
            Task.Run(() =>
            {
                try
                {
                    while (true)
                    {

                        for (int i = 0; i < bmp1.Width; i++)
                            for (int j = 0; j < bmp1.Height; j++)
                            {
                                var c1 = BitmapSourceHelper.GetColor(pixelColors,i, j);
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
                        for (int i = 0; i < bmp1.Width; i++)
                            for (int j = 0; j < bmp1.Height; j++)
                            {
                                var c1 = BitmapSourceHelper.GetColor(pixelColors, i, j);
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
