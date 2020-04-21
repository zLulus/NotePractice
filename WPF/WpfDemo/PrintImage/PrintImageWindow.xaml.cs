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
    /// PrintImageWindow.xaml 的交互逻辑
    /// </summary>
    public partial class PrintImageWindow : Window
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
            finally
            {
                this.Cursor = Cursors.AppStarting;
            }
        }
    }
}
