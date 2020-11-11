using NetTopologySuite.IO;
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

namespace ArcGISWpfDemo
{
    /// <summary>
    /// ConvertDataWindow.xaml 的交互逻辑
    /// </summary>
    public partial class ConvertDataWindow : Window
    {
        public ConvertDataWindow()
        {
            InitializeComponent();
        }

        #region WKTToWKB
        private void ConvertWKTToWKB_Click(object sender, RoutedEventArgs e)
        {
            String WKTText = GetTxtFromFile();
            var wkbBytes = ConvertWKTToWKB(WKTText);
        }
        private static byte[] ConvertWKTToWKB(string wkt)
        {
            WKBWriter writer = new WKBWriter();
            WKTReader reader = new WKTReader();
            var wkb = writer.Write(reader.Read(wkt));
            return wkb;
        }
        #endregion

        #region WKBToWKT
        private void ConvertWKBToWKT_Click(object sender, RoutedEventArgs e)
        {
            //这里需要读取wkb
            byte[] bytes = new byte[1024];
            var wktText = ConvertWKBToWKT(bytes);
        }
        private static string ConvertWKBToWKT(byte[] wkbBytes)
        {
            WKBReader reader = new WKBReader();
            WKTWriter writer = new WKTWriter();
            var wktText = writer.Write(reader.Read(wkbBytes));
            return wktText;
        }
        #endregion

        private void ConvertWKTToGeometry_Click(object sender, RoutedEventArgs e)
        {
            String WKTText = GetTxtFromFile();

            // Create a Well Known Text Reader from NetTopologySuite
            WKTReader reader = new WKTReader();
            // NetTopologySuite passes back a GeoApi IGeometry.  This is a shared interface that can be used by both libraries.
            NetTopologySuite.Geometries.Geometry geom = reader.Read(WKTText);

        }

        private void ConvertGeometryToWKT_Click(object sender, RoutedEventArgs e)
        {
            //WKTWriter writer = new WKTWriter();
            //var wkt = writer.Write(geo);
        }

        private void ConvertGeoJSONToGeometry_Click(object sender, RoutedEventArgs e)
        {
            //引用NetTopologySuite.IO.GeoJSON
            //https://github.com/NetTopologySuite/NetTopologySuite.IO.GeoJSON
            String geoJSONText = GetTxtFromFile();
            GeoJsonReader reader = new GeoJsonReader();
            var geometry = reader.Read<NetTopologySuite.Geometries.Geometry>(geoJSONText);
        }

        private void ConvertWKBToGeometry_Click(object sender, RoutedEventArgs e)
        {
            //这里需要读取wkb
            byte[] wkbBytes = new byte[1024];
            WKBReader reader = new WKBReader();
            NetTopologySuite.Geometries.Geometry geom = reader.Read(wkbBytes);
        }

        private void ConvertGeometryToWKB_Click(object sender, RoutedEventArgs e)
        {

        }


        private void OpenTxtFile_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog()
            {
                Filter = "TXT文档 (*.txt)|*.txt"
            };
            var r = openFileDialog.ShowDialog();
            if (r == true)
                filePathTextBlock.Text = openFileDialog.FileName;
        }
        private string GetTxtFromFile()
        {
            string text;
            using (StreamReader sr = new StreamReader(filePathTextBlock.Text))
            {
                text = sr.ReadToEnd();
            }

            return text;
        }
    }
}
