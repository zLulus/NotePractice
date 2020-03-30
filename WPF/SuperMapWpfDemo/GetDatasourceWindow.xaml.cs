using SuperMap.Data;
using SuperMapWpfDemo.Common;
using System;
using System.Collections.Generic;
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

namespace SuperMapWpfDemo
{
    /// <summary>
    /// Interaction logic for GetDatasourceWindow.xaml
    /// </summary>
    public partial class GetDatasourceWindow : Window
    {
        string filePath = @"D:\MicroDesktop\Temp\ExportShape.shp";
        string targetDatasetName = "sdfsd";
        Workspace workspace;
        Datasource memDatasource;
        public GetDatasourceWindow()
        {
            InitializeComponent();
            Initialize();
        }

        private void Initialize()
        {
            workspace = new Workspace();
            //Memory数据源
            DatasourceConnectionInfo memInfo = new DatasourceConnectionInfo();
            //设置数据源连接的引擎类型
            memInfo.EngineType = EngineType.Memory;
            memInfo.Alias = "fdgdfgd";
            memInfo.Server = "tyjyutjyu";
            // 创建/打开数据库数据源
            memDatasource = workspace.Datasources.Create(memInfo);
            var r= SuperMapSdkHelper.ImportShpToDatasource(filePath, memDatasource, targetDatasetName);
            if (r.FailedSettings.Count() > 0)
            {
                MessageBox.Show("打开shp文件失败");
            }
        }

        /// <summary>
        /// 获得srid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GetSrid_Click(object sender, RoutedEventArgs e)
        {
            if (memDatasource != null)
            {
                var dataset = memDatasource.Datasets[targetDatasetName];
                if (dataset != null)
                {
                    PrjCoordSys crtPrjSys = dataset.PrjCoordSys;
                    //https://www.cnblogs.com/arxive/p/5082761.html
                    //EPSGCode=srid
                    var srid = crtPrjSys.EPSGCode;
                    MessageBox.Show($"srid:{srid}");
                }
            }
        }

        /// <summary>
        /// Datasource查询targetDatasetName-FieldInfo
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GetFieldInfo_Click(object sender, RoutedEventArgs e)
        {
            //http://ask.supermap.com/2761
            if (memDatasource != null)
            {
                var dataset = memDatasource.Datasets[targetDatasetName];
                if (dataset != null)
                {
                    //限矢量数据集(DatasetVector)使用
                    var datasetVector = dataset as DatasetVector;
                    List<FieldInfo> fs = new List<FieldInfo>();
                    foreach (FieldInfo f in datasetVector.FieldInfos)
                    {
                        fs.Add(f);
                    }
                }
            }
        }

        /// <summary>
        /// Datasource查询targetDatasetName-datasetVector.Type  点线面...
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GetType_Click(object sender, RoutedEventArgs e)
        {
            //数据集类型包括纯属性表数据集、点数据集、线数据集、面数据集、文本数据集、CAD数据集、路由数据集等矢量数据集（DatasetVector），栅格数据集（DatasetGrid），影像数据集（DatasetImage），以及网络数据集（DatasetNetwork）。
            if (memDatasource != null)
            {
                var dataset = memDatasource.Datasets[targetDatasetName];
                if (dataset != null)
                {
                    //这里可以是DatasetVolume/DatasetTopology/DatasetImage...
                    var datasetVector = dataset as DatasetVector;
                    switch (datasetVector.Type)
                    {
                        case DatasetType.Point:
                            break;
                        case DatasetType.Line:
                            break;
                        case DatasetType.Region:
                            break;
                            //...
                        default:
                            break;
                    }
                }
            }
        }
    }
}
