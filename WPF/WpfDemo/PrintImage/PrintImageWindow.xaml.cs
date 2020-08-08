using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfDemo.PrintImage
{
    /// <summary>
    /// PrintImageWindow.xaml 的交互逻辑
    /// </summary>
    public partial class PrintImageWindow : UserControl
    {
        private string outputDic { get { return $"{AppDomain.CurrentDomain.BaseDirectory}\\Temp\\"; } }
        public PrintImageWindow()
        {
            InitializeComponent();
        }

        private void PrintImage(object sender, RoutedEventArgs e)
        {
            //将控件imageBorder的画面输出图片
            var outputPath = GetPicFromControl(imageBorder as FrameworkElement);
            //打印
            Print(outputPath);
            //删除图片
            File.Delete(outputPath);
        }

        private string GetPicFromControl(FrameworkElement element)
        {
            //96为显示器DPI
            double dpiX = 96;
            double dpiY = 96;

            var bitmapRender = new RenderTargetBitmap((int)element.ActualWidth, (int)element.ActualHeight, dpiX, dpiY, PixelFormats.Pbgra32);//位图 宽度  高度   水平DPI  垂直DPI  位图的格式    高度+100保证整个图都能截取
            //控件内容渲染RenderTargetBitmap
            bitmapRender.Render(element);
            BitmapEncoder encoder = new JpegBitmapEncoder();
            var outputPath = $"{outputDic}\\{Guid.NewGuid().ToString()}.jpg";
            //对于一般的图片，只有一帧，动态图片是有多帧的。
            encoder.Frames.Add(BitmapFrame.Create(bitmapRender));//添加图
            if (!Directory.Exists(System.IO.Path.GetDirectoryName(outputPath)))
                Directory.CreateDirectory(System.IO.Path.GetDirectoryName(outputPath));
            using (var file = File.Create(outputPath))//存储文件
                encoder.Save(file);
            return outputPath;
        }

        private void Print(string outputPath)
        {
            try
            {
                this.Cursor = Cursors.Wait;

                //直接打印
                //DirectPrint(outputPath);
                //根据打印设置重新计算图片高宽后打印
                ResizeImageAndPrint(outputPath);
            }
            finally
            {
                this.Cursor = Cursors.AppStarting;
            }
        }

        /// <summary>
        /// 根据打印设置重新计算图片高宽后打印
        /// </summary>
        /// <param name="outputPath"></param>
        private void ResizeImageAndPrint(string outputPath)
        {
            var pdialog = new PrintDialog();
            if (pdialog.ShowDialog() == true)
            {
                //根据纸张大小和纸张方向，读取图片，修改图片大小
                var height = pdialog.PrintableAreaHeight;
                var width = pdialog.PrintableAreaWidth;

                Bitmap bmp = new Bitmap(outputPath);
                var newImage = RezizeImage(bmp, (int)width, (int)height);
                var newPath = $"{outputDic}\\{Guid.NewGuid().ToString()}.png";
                newImage.Save(newPath, ImageFormat.Png);

                var bi = new BitmapImage();
                bi.BeginInit();
                bi.CacheOption = BitmapCacheOption.OnLoad;
                bi.UriSource = new Uri(newPath);
                bi.EndInit();

                var vis = new DrawingVisual();
                using (var dc = vis.RenderOpen())
                {
                    dc.DrawImage(bi, new Rect { Width = bi.Width, Height = bi.Height });
                }

                pdialog.PrintVisual(vis, "My Image");

                File.Delete(newPath);
            }
        }

        /// <summary>
        /// 直接打印
        /// </summary>
        /// <param name="outputPath"></param>
        private static void DirectPrint(string outputPath)
        {
            var bi = new BitmapImage();
            bi.BeginInit();
            bi.CacheOption = BitmapCacheOption.OnLoad;
            //加载图片
            bi.UriSource = new Uri(outputPath);
            bi.EndInit();

            var vis = new DrawingVisual();
            using (var dc = vis.RenderOpen())
            {
                dc.DrawImage(bi, new Rect { Width = bi.Width, Height = bi.Height });
            }

            var pdialog = new PrintDialog();
            if (pdialog.ShowDialog() == true)
            {
                pdialog.PrintVisual(vis, "My Image");
            }
        }

        //https://stackoverflow.com/questions/2319983/resizing-an-image-in-asp-net-without-losing-the-image-quality
        private System.Drawing.Image RezizeImage(System.Drawing.Image img, int maxWidth, int maxHeight)
        {
            //if (img.Height < maxHeight && img.Width < maxWidth) return img;
            using (img)
            {
                Double xRatio = (double)img.Width / maxWidth;
                Double yRatio = (double)img.Height / maxHeight;
                Double ratio = Math.Max(xRatio, yRatio);
                int nnx = (int)Math.Floor(img.Width / ratio);
                int nny = (int)Math.Floor(img.Height / ratio);
                Bitmap cpy = new Bitmap(nnx, nny, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
                using (Graphics gr = Graphics.FromImage(cpy))
                {
                    gr.Clear(System.Drawing.Color.Transparent);

                    // This is said to give best quality when resizing images
                    gr.InterpolationMode = InterpolationMode.HighQualityBicubic;

                    gr.DrawImage(img,
                        new System.Drawing.Rectangle(0, 0, nnx, nny),
                        new System.Drawing.Rectangle(0, 0, img.Width, img.Height),
                        GraphicsUnit.Pixel);
                }
                return cpy;
            }

        }
    }
}
