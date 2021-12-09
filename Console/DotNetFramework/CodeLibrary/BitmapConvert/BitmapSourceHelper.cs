using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace CodeLibrary.BitmapConvert
{
    [StructLayout(LayoutKind.Sequential)]
    public struct PixelColor
    {
        public byte Blue;
        public byte Green;
        public byte Red;
        public byte Alpha;
    }
    public static class BitmapSourceHelper
    {
        public static Color GetColor(PixelColor[,] pixelColors, int x, int y)
        {
            return Color.FromArgb(pixelColors[x, y].Alpha, pixelColors[x, y].Red, pixelColors[x, y].Green, pixelColors[x, y].Blue);
        }

        public static void PutPixels(WriteableBitmap bitmap, PixelColor[,] pixels, int x, int y)
        {
            int width = pixels.GetLength(0);
            int height = pixels.GetLength(1);
            bitmap.WritePixels(new Int32Rect(0, 0, width, height), pixels, width * 4, x, y);
        }

        public static BitmapImage ImageBytesToImageSource(byte[] buffer)
        {
            return ImageBytesToImageSource(new System.IO.MemoryStream(buffer));
        }

        public static BitmapImage ImageBytesToImageSource(System.IO.MemoryStream ms)
        {
            BitmapImage bitmapImage = new BitmapImage();
            bitmapImage.BeginInit();
            bitmapImage.StreamSource = ms;
            bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
            bitmapImage.EndInit();
            ms.Dispose();

            return bitmapImage;
        }



        [System.Runtime.InteropServices.DllImport("gdi32")]
        static extern int DeleteObject(IntPtr o);
        //Bitmap转BitmapSource
        public static BitmapSource BitmapToBitmapSource(System.Drawing.Bitmap bitMap)
        {
            //System.Drawing.Bitmap newBitMap = CaptureBitmapFormBitMap(bitMap, new System.Drawing.Rectangle(0, 0, bitMap.Width, bitMap.Height));
            IntPtr ip = bitMap.GetHbitmap();
            BitmapSource bitmapSource = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(
                ip, IntPtr.Zero, Int32Rect.Empty,
                 System.Windows.Media.Imaging.BitmapSizeOptions.FromWidthAndHeight(bitMap.Width, bitMap.Height));
            DeleteObject(ip);
            return bitmapSource;
        }

#if UNSAFE
  public unsafe static void CopyPixels(this BitmapSource source, PixelColor[,] pixels, int stride, int offset)
  {
    fixed(PixelColor* buffer = &pixels[0, 0])
      source.CopyPixels(
        new Int32Rect(0, 0, source.PixelWidth, source.PixelHeight),
        (IntPtr)(buffer + offset),
        pixels.GetLength(0) * pixels.GetLength(1) * sizeof(PixelColor),
        stride);
  }
#else
        public static void CopyPixels(this BitmapSource source, PixelColor[,] pixels, int stride, int offset)
        {
            var height = source.PixelHeight;
            var width = source.PixelWidth;
            var pixelBytes = new byte[height * width * 4];
            source.CopyPixels(pixelBytes, stride, 0);
            int y0 = offset / width;
            int x0 = offset - width * y0;
            for (int y = 0; y < height; y++)
                for (int x = 0; x < width; x++)
                    pixels[x + x0, y + y0] = new PixelColor
                    {
                        Blue = pixelBytes[(y * width + x) * 4 + 0],
                        Green = pixelBytes[(y * width + x) * 4 + 1],
                        Red = pixelBytes[(y * width + x) * 4 + 2],
                        Alpha = pixelBytes[(y * width + x) * 4 + 3],
                    };
        }
#endif
    }
}
