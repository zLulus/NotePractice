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
    /// PrintImageWithPageSettingWindow2.xaml 的交互逻辑
    /// </summary>
    public partial class PrintImageWithPageSettingWindow2 : UserControl
    {
        private string outputDic { get { return $"{AppDomain.CurrentDomain.BaseDirectory}\\Temp\\"; } }
        /// <summary>
        /// A4纸纵向宽度(A4纸横向高度)  单位cm
        /// </summary>
        const double A4WithVerticalWidth = 21;
        /// <summary>
        /// A4纸纵向高度(A4纸横向宽度)  单位cm
        /// </summary>
        const double A4WithVerticalHeight = 29.7;
        /// <summary>
        /// A3纸纵向宽度(A3纸横向高度)  单位cm
        /// </summary>
        const double A3WithVerticalWidth = 29.7;
        /// <summary>
        /// A3纸纵向高度(A3纸横向宽度)  单位cm
        /// </summary>
        const double A3WithVerticalHeight = 42;
        /// <summary>
        /// 显示纸张的缩放比例
        /// </summary>
        double scalingRatio = 1;
        public PrintImageWithPageSettingWindow2()
        {
            InitializeComponent();
        }

        private void PrintImage(object sender, RoutedEventArgs e)
        {
            //将控件imageBorder的画面输出图片
            var outputPath = CaptureScreen(imageBorder as FrameworkElement); //GetPicFromControl(imageBorder as FrameworkElement);
            //打印
            Print(outputPath);
            //删除图片
            File.Delete(outputPath);
        }

        private string CaptureScreen(Visual target)
        {
            //96为显示器DPI
            double dpiX = 96;
            double dpiY = 96;

            if (target == null)
            {
                return null;
            }
            Rect bounds = VisualTreeHelper.GetDescendantBounds(target);
            RenderTargetBitmap rtb = new RenderTargetBitmap((int)(bounds.Width * dpiX / 96.0),
                                                            (int)(bounds.Height * dpiY / 96.0),
                                                            dpiX,
                                                            dpiY,
                                                            PixelFormats.Pbgra32);
            DrawingVisual dv = new DrawingVisual();
            using (DrawingContext ctx = dv.RenderOpen())
            {
                VisualBrush vb = new VisualBrush(target);
                ctx.DrawRectangle(vb, null, new Rect(new System.Windows.Point(), bounds.Size));
            }
            rtb.Render(dv);

            var outputPath = $"{outputDic}\\{Guid.NewGuid().ToString()}.png";
            using (var fileStream = new FileStream(outputPath, FileMode.Create))
            {
                BitmapEncoder encoder = new PngBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(rtb));
                encoder.Save(fileStream);
            }
            return outputPath;
        }

        private void Print(string outputPath)
        {
            try
            {
                this.Cursor = Cursors.Wait;
                
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

        //https://stackoverflow.com/questions/2319983/resizing-an-image-in-asp-net-without-losing-the-image-quality
        //缩放图片高宽
        private System.Drawing.Image RezizeImage(System.Drawing.Image img, int maxWidth, int maxHeight)
        {
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

        private void A4WithHorizontalClick(object sender, RoutedEventArgs e)
        {
            //cm转换
            //https://stackoverflow.com/questions/9504664/wpf-units-and-code-behind
            var heightPx = (double)new LengthConverter().ConvertFrom($"{A4WithVerticalWidth}cm");
            var widthPx = (double)new LengthConverter().ConvertFrom($"{A4WithVerticalHeight}cm");
            Reset( heightPx,  widthPx);

        }

        private void Reset(double heightPx, double widthPx)
        {
            scalingRatio = 1;
            if(myGrid.ActualHeight>0 && myGrid.ActualWidth > 0)
            {
                while (heightPx > myGrid.ActualHeight || widthPx > myGrid.ActualWidth)
                {
                    scalingRatio -= 0.1;
                    heightPx = heightPx * scalingRatio;
                    widthPx = widthPx * scalingRatio;
                    if (scalingRatio <= 0)
                    {
                        break;
                    }
                }
                if (scalingRatio > 0)
                {
                    imageBorder.Height = heightPx;
                    imageBorder.Width = widthPx;
                }
            }
            
        }

        private void A4WithVerticalClick(object sender, RoutedEventArgs e)
        {
            var heightPx = (double)new LengthConverter().ConvertFrom($"{A4WithVerticalHeight}cm");
            var widthPx = (double)new LengthConverter().ConvertFrom($"{A4WithVerticalWidth}cm");
            Reset(heightPx, widthPx);
        }

        private void A3WithHorizontalClick(object sender, RoutedEventArgs e)
        {
            var heightPx = (double)new LengthConverter().ConvertFrom($"{A3WithVerticalWidth}cm");
            var widthPx = (double)new LengthConverter().ConvertFrom($"{A3WithVerticalHeight}cm");
            Reset(heightPx, widthPx);
        }

        private void A3WithVerticalClick(object sender, RoutedEventArgs e)
        {
            var heightPx = (double)new LengthConverter().ConvertFrom($"{A3WithVerticalHeight}cm");
            var widthPx = (double)new LengthConverter().ConvertFrom($"{A3WithVerticalWidth}cm");
            Reset(heightPx, widthPx);
        }

        private void Window_Load(object sender, RoutedEventArgs e)
        {
            //this.Left = 0.0;
            //this.Top = 0.0;
            this.Width = System.Windows.SystemParameters.PrimaryScreenWidth;
            this.Height = System.Windows.SystemParameters.PrimaryScreenHeight;
            A4WithVerticalClick(null, null);
        }
    }
}
