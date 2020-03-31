using SuperMap.Data;
using SuperMap.Data.Conversion;
using SuperMap.Mapping;
using SuperMap.UI;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SuperMapWpfDemo
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        string baseFilePath = @"";
        List<string> fileList = new List<string>()
        {
            "",
        };
        
        string targetTableName = "";
        string server = "";
        string database = "";
        string userName = "";
        string password = "";
        string driver = "";

        public MainWindow()
        {
            InitializeComponent();
            Initialize();
        }

        private void Initialize()
        {
            
        }

        /// <summary>
        /// 录入shp数据到业务表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveShpFile_Click(object sender, RoutedEventArgs e)
        {
            foreach(var file in fileList)
            {
                try
                {
                    String sourceFilePath = $"{baseFilePath}\\{file}.shp";
                    //CreateDb();
                    //ImportShpDirectly(sourceFilePath);
                    ImportShpByMemory(sourceFilePath);
                }
                catch (Exception ex)
                {

                }
            }
            
            
        }

        private void CreateDb()
        {
            Workspace workspace = new Workspace();
            DatasourceConnectionInfo info = new DatasourceConnectionInfo();

            //mysql数据源
            //设置数据源连接的引擎类型
            info.EngineType = EngineType.MySQL;
            //设置数据库连接字符串
            info.Server = server;
            info.Database = database;
            info.User = userName;
            info.Password = password;
            info.Driver = driver;
            info.IsAutoConnect = true;
            info.Alias = "MySQL";//不能为空
                                 // 打开数据库数据源
                                 //超图sdk不能直接连接空数据库，需要使用Create方法新建数据库，才有超图“系统表”
            Datasource datasource = workspace.Datasources.Create(info);
        }

        private void ImportShpByMemory(string filePath)
        {
            //https://blog.csdn.net/tane_e/article/details/89393493


            //https://www.supermap.com/EN/online/Deskpro%206.0/SDMain/html/R_Dataset_Import.htm
            //https://www.supermap.com/EN/online/Deskpro%206.0/SDTechTheme/ExpressHtml/ImEx_ArcGIS_Shape.htm

            Workspace workspace = new Workspace();
            DatasourceConnectionInfo info = new DatasourceConnectionInfo();

            //mysql数据源
            //设置数据源连接的引擎类型
            info.EngineType = EngineType.MySQL;
            //设置数据库连接字符串
            info.Server = server;
            info.Database = database;
            info.User = userName;
            info.Password = password;
            info.Driver = driver;
            info.IsAutoConnect = true;
            info.Alias = "MySQL";//不能为空
                                 // 打开数据库数据源
                                 //超图sdk不能直接连接空数据库，需要使用Create方法新建数据库，才有超图“系统表”
            Datasource datasource = workspace.Datasources.Open(info);
            ////udb数据源
            //DatasourceConnectionInfo udbInfo = new DatasourceConnectionInfo();
            ////设置数据源连接的引擎类型
            //udbInfo.EngineType = EngineType.UDB;
            ////设置文件位置
            //udbInfo.Server = @"D:\MicroDesktop\Temp\test";
            //// 创建/打开数据库数据源
            //Datasource udbDatasource = workspace.Datasources.Create(udbInfo);
            //Datasource udbDatasource = workspace.Datasources.Open(udbInfo);

            //Memory数据源
            DatasourceConnectionInfo memInfo = new DatasourceConnectionInfo();
            //设置数据源连接的引擎类型
            memInfo.EngineType = EngineType.Memory;
            memInfo.Alias = "fdgdfgd";
            memInfo.Server = "tyjyutjyu";
            // 创建/打开数据库数据源
            Datasource memDatasource = workspace.Datasources.Create(memInfo);

            //svc-矢量数据
            //DatasourceConnectionInfo scvInfo = new DatasourceConnectionInfo();
            ////设置数据源连接的引擎类型
            //scvInfo.EngineType = EngineType.VectorFile;
            ////设置文件位置
            //scvInfo.Server = @"D:\MicroDesktop\Temp\test2";
            //// 创建/打开数据库数据源
            //Datasource scvDatasource = workspace.Datasources.Create(scvInfo);

            if (datasource != null)
            {
                ImportResult result = ImportShpToMemory(filePath, memDatasource);
                if (result.FailedSettings.Length == 0)
                {
                    Console.WriteLine($"导入{filePath}成功！");
                    //for (int i = 0; i < memDatasource.Datasets.Count; i++)
                    //{
                    //    DatasetVector datasetVector = (DatasetVector)memDatasource.Datasets[i];
                    //    Dataset newDataset = datasource.CopyDataset(datasetVector, datasetVector.Name, EncodeType.None);
                    //}


                    DatasetVector datasetVector = (DatasetVector)memDatasource.Datasets[0];
                    //datasource.Datasets.CreateFromTemplate(datasetVector.Name, datasetVector);
                    //var t1=datasource.Datasets.CreateFromTemplate(datasetVector.Name, memDatasource.Datasets[0]);
                    //var t2= datasource.CopyDataset(datasetVector, datasetVector.Name, EncodeType.None);
                    //datasource.Flush(datasetVector.Name);

                    var re = datasetVector.GetRecordset(false, SuperMap.Data.CursorType.Dynamic);
                    //re.AddNew(

                    var v3 = datasource.Datasets.CreateAndAppendWithSmid(targetTableName, re);
                    var v4 = datasource.Datasets.CreateFromTemplate(targetTableName, memDatasource.Datasets[0]);
                    var v5 = datasource.CopyDataset(datasetVector, targetTableName, datasetVector.EncodeType);
                    //datasource.Datasets.Create(datasetVector);
                    var dataset = datasource.Datasets[targetTableName];
                    var ve = dataset as DatasetVector;
                    var record = ve?.GetRecordset(false, SuperMap.Data.CursorType.Dynamic);
                    //record.AddNew(...);
                    //var v2= datasource.RecordsetToDataset(re, targetTableName);

                    datasource.Refresh();
                    //String name = datasource.Datasets.GetAvailableDatasetName(targetTableName);
                    // 设置矢量数据集的信息
                    //DatasetVectorInfo datasetVectorInfo = new DatasetVectorInfo();
                    //datasetVectorInfo.Type = DatasetType.Line;
                    //datasetVectorInfo.IsFileCache = true;
                    //datasetVectorInfo.Name = name;
                    //datasetVectorInfo.Bounds = new Rectangle2D(new Point2D(0, 0), new Point2D(10, 10));
                    //Console.WriteLine("矢量数据集的信息为：" + datasetVectorInfo.ToString());

                    //// 创建矢量数据集
                    //datasource.Datasets.Create(datasetVectorInfo);
                    //datasource.Flush(name);

                    //var d2= datasource.CopyDatasetWithSmID(udbDatasource.Datasets[0], targetTableName, EncodeType.None);
                    //var d = datasource.CopyDataset(udbDatasource.Datasets[0], targetTableName, EncodeType.None);
                }
                else
                {
                    Console.WriteLine($"导入{filePath}失败！");
                }
            }


            // 释放工作空间资源
            info.Dispose();
            workspace.Dispose();
        }

        private ImportResult ImportShpToMemory(string filePath, Datasource memDatasource)
        {
            ImportSettingSHP importSettingSHP = new ImportSettingSHP();
            importSettingSHP.IsAttributeIgnored = false;
            importSettingSHP.IsImportAs3D = false;
            //设置当同名数据集存在时导入的模式,如果存在名字冲突，则覆盖(Overwrite)
            importSettingSHP.ImportMode = ImportMode.Overwrite;
            //设置需要导入的数据路径信息
            importSettingSHP.SourceFilePath = filePath;
            //设置需要导入的数据编码类型，因为有中文字段，所以用ASCII编码
            importSettingSHP.SourceFileCharset = Charset.ANSI;
            //设置要导入的目标数据源
            //importSettingSHP.TargetDatasource = udbDatasource;
            //importSettingSHP.TargetDatasource = memDatasource;
            importSettingSHP.TargetDatasource = memDatasource;
            //importSettingSHP.TargetDatasource = scvDatasource;
            //importSettingSHP.TargetDatasourceConnectionInfo = info;
            //设置目标数据集名称
            importSettingSHP.TargetDatasetName = targetTableName;
            importSettingSHP.TargetEncodeType = EncodeType.None;
            DataImport importer = new DataImport();
            importer.ImportSettings.Add(importSettingSHP);
            //数据导入mysql数据库
            ImportResult result = importer.Run();
            return result;
        }

        private void ImportShpDirectly(string filePath)
        {
            Workspace workspace = new Workspace();
            DatasourceConnectionInfo info = new DatasourceConnectionInfo();

            //mysql数据源
            //设置数据源连接的引擎类型
            info.EngineType = EngineType.MySQL;
            //设置数据库连接字符串
            info.Server = server;
            info.Database = database;
            info.User = userName;
            info.Password = password;
            info.Driver = driver;
            info.IsAutoConnect = true;
            info.Alias = "MySQL";//不能为空
                                 // 打开数据库数据源
                                 //超图sdk不能直接连接空数据库，需要使用Create方法新建数据库，才有超图“系统表”
            Datasource datasource = workspace.Datasources.Open(info);
            
            if (datasource != null)
            {
                ImportSettingSHP importSettingSHP = new ImportSettingSHP();
                importSettingSHP.IsAttributeIgnored = false;
                importSettingSHP.IsImportAs3D = false;
                //设置当同名数据集存在时导入的模式,如果存在名字冲突，则覆盖(Overwrite)
                importSettingSHP.ImportMode = ImportMode.Overwrite;
                //设置需要导入的数据路径信息
                importSettingSHP.SourceFilePath = filePath;
                //设置需要导入的数据编码类型，因为有中文字段，所以用ASCII编码
                importSettingSHP.SourceFileCharset = Charset.ANSI;
                //设置要导入的目标数据源
                importSettingSHP.TargetDatasource = datasource;
                //设置目标数据集名称
                importSettingSHP.TargetDatasetName = targetTableName;
                importSettingSHP.TargetEncodeType = EncodeType.None;
                DataImport importer = new DataImport();
                importer.ImportSettings.Add(importSettingSHP);
                //数据导入mysql数据库
                ImportResult result = importer.Run();
                if (result.FailedSettings.Length == 0)
                {
                    Console.WriteLine($"导入{filePath}成功！");
                }
                else
                {
                    Console.WriteLine($"导入{filePath}失败！");
                }
            }


            // 释放工作空间资源
            info.Dispose();
            workspace.Dispose();
        }

        private void UpdateShpFile_Click(object sender, RoutedEventArgs e)
        {
            string tableName = "pointtesttable";
            Dictionary<string, string> updateData = new Dictionary<string, string>();
            updateData.Add("numberPro", "123.32");
            updateData.Add("stringPro", "vervre");
            int smId = 1;

            UpdateTable(tableName, updateData, smId);
        }

        private void UpdateTable(string tableName, Dictionary<string, string> updateData, int smId)
        {
            foreach (var dic in updateData)
            {
                var setSql = "";
                setSql += $"{dic.Key}='{dic.Value}' ";
                //这里最好使用sqlparameter拼接sql
                string sql = $"update {tableName} set {setSql} where SMID={smId}";

                //执行sql
            }
        }

        private Workspace showMapWorkspace;
        private MapControl showMapControl;

        private void ReadShpFileAndShow_Click(object sender, RoutedEventArgs e)
        {
            LoadMapFromDatasource();
            //LoadMapFromWorkspace();
        }
        private void LoadMapFromDatasource()
        {
            showMapWorkspace = new Workspace();

            //Memory数据源
            DatasourceConnectionInfo memInfo = new DatasourceConnectionInfo();
            //设置数据源连接的引擎类型
            memInfo.EngineType = EngineType.Memory;
            memInfo.Alias = "fdgdfgd";
            memInfo.Server = "tyjyutjyu";
            Datasource memDatasource = showMapWorkspace.Datasources.Create(memInfo);

            String sourceFilePath = $"{baseFilePath}\\{fileList[0]}.shp";
            var r= ImportShpToMemory(sourceFilePath, memDatasource);
            showMapControl = new MapControl();
            showMapControl.Action = SuperMap.UI.Action.Pan;
            //必须设置
            showMapControl.Map.Workspace = showMapWorkspace;
            //添加数据集到地图控件
            showMapControl.Map.Layers.Add(memDatasource.Datasets[0], true);

            hostMapControl.Child = showMapControl;
        }

        private void LoadMapFromWorkspace()
        {
            showMapWorkspace = new Workspace();
            showMapWorkspace.Open(new WorkspaceConnectionInfo(@"...\\.smwu"));

            showMapControl = new MapControl();

            showMapControl.Action = SuperMap.UI.Action.Pan;
            //必须设置
            showMapControl.Map.Workspace = showMapWorkspace;
            showMapControl.Map.Open(showMapWorkspace.Maps[0]);

            hostMapControl.Child = showMapControl;
        }
    }
}
