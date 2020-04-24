using System;
using System.Collections.Generic;
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
    /// PrintImageWithPageSettingWindow.xaml 的交互逻辑
    /// </summary>
    public partial class PrintImageWithPageSettingWindow : Window
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
        public PrintImageWithPageSettingWindow()
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
        
        private string GetPicFromControl(FrameworkElement element)
        {
            //bug:导出的图片有偏移

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
                ctx.DrawRectangle(vb, null, new Rect(new Point(), bounds.Size));
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

                //直接打印
                DirectPrint(outputPath);
            }
            finally
            {
                this.Cursor = Cursors.AppStarting;
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

        private void A4WithHorizontalClick(object sender, RoutedEventArgs e)
        {
            //cm转换
            //https://stackoverflow.com/questions/9504664/wpf-units-and-code-behind
            imageBorder.Height = (double)new LengthConverter().ConvertFrom($"{A4WithVerticalWidth}cm");
            imageBorder.Width = (double)new LengthConverter().ConvertFrom($"{A4WithVerticalHeight}cm");
        }

        private void A4WithVerticalClick(object sender, RoutedEventArgs e)
        {
            imageBorder.Height = (double)new LengthConverter().ConvertFrom($"{A4WithVerticalHeight}cm");
            imageBorder.Width = (double)new LengthConverter().ConvertFrom($"{A4WithVerticalWidth}cm");
        }

        private void A3WithHorizontalClick(object sender, RoutedEventArgs e)
        {
            imageBorder.Height = (double)new LengthConverter().ConvertFrom($"{A3WithVerticalWidth}cm");
            imageBorder.Width = (double)new LengthConverter().ConvertFrom($"{A3WithVerticalHeight}cm");
        }

        private void A3WithVerticalClick(object sender, RoutedEventArgs e)
        {
            imageBorder.Height = (double)new LengthConverter().ConvertFrom($"{A3WithVerticalHeight}cm");
            imageBorder.Width = (double)new LengthConverter().ConvertFrom($"{A3WithVerticalWidth}cm");
        }
    }
}
