using SuperMap.Data;
using SuperMap.Data.Conversion;
using SuperMap.Mapping;
using SuperMap.UI;
using SuperMapWpfDemo.Dtos;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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



        private void InsertShpFile_Click(object sender, RoutedEventArgs e)
        {
            foreach (var file in fileList)
            {
                try
                {
                    String sourceFilePath = $"{baseFilePath}\\{file}.shp";
                    InsertRecordSetToDb(sourceFilePath, targetTableName);
                    //CreatePointDataset(targetTableName)
                }
                catch (Exception ex)
                {

                }
            }
            
        }

        //private void CreatePointDataset(string tableName)
        //{
        //    Workspace workspace = new Workspace();
        //    DatasourceConnectionInfo info = new DatasourceConnectionInfo();
        //    Datasource datasource = GetDbDatasource(workspace, info);
        //    var datasetVector = (DatasetVector)datasource.Datasets[tableName];
        //    if (datasetVector == null)
        //    {
        //        CreateDataset(datasource, DatasetType.Point, tableName);
        //    }
        //    //只取了数据结构，没有取数据
        //    var recordset = datasetVector.GetRecordset(true, SuperMap.Data.CursorType.Dynamic);
        //    recordset.Edit();
        //    recordset.fi
        //}

        private void InsertRecordSetToDb(string shapeFieldName,string tableName)
        {
            Workspace workspace = new Workspace();
            DatasourceConnectionInfo info = new DatasourceConnectionInfo();
            var filePath = $"{Directory.GetCurrentDirectory()}\\{Guid.NewGuid().ToString()}";
            var files = new List<string> { $"{filePath}.udb", $"{filePath}.udd" };

            Datasource datasource = GetDbDatasource(workspace, info);
            if (datasource != null)
            {
                //临时数据源
                DatasourceConnectionInfo tempInfo = new DatasourceConnectionInfo();
                //设置数据源连接的引擎类型
                tempInfo.EngineType = EngineType.UDB;
                tempInfo.Alias = tableName;

                tempInfo.Server = filePath;
                // 创建/打开数据库数据源
                Datasource tempDatasource = workspace.Datasources.Create(tempInfo);
                Recordset recordset = null, tempRecordset = null;
                if (tempDatasource != null)
                {
                    ImportResult result = ImportShpToTemp(shapeFieldName, tempDatasource, tableName);
                    if (result.FailedSettings.Length == 0)
                    {
                        Console.WriteLine($"导入{shapeFieldName}成功！");
                        try
                        {
                            for (int index = 0; index < tempDatasource.Datasets.Count; index++)
                            {
                                DatasetVector tempDatasetVector = (DatasetVector)tempDatasource.Datasets[index];
                                tempRecordset = tempDatasetVector.GetRecordset(false, SuperMap.Data.CursorType.Dynamic);
                                //没有数据
                                if (tempRecordset.RecordCount == 0)
                                {
                                    continue;
                                }
                                var tempFieldInfos = tempDatasetVector.FieldInfos;
                                //注意：数据集是手工录入的，不是超图sdk生成的，所以不能删除数据集
                                //如果更新数据集中的记录，则应该操纵记录集(删除、修改、新增)
                                var datasetVector = (DatasetVector)datasource.Datasets[tableName];
                                if (datasetVector == null)
                                {
                                    CreateDataset(datasource, DatasetType.Point, tableName);
                                    //throw new Exception($"不存在数据集名称为{tableName}的数据集！");
                                }
                                //删去之前的所有记录
                                //datasetVector.GetRecordset(false, SuperMap.Data.CursorType.Dynamic).DeleteAll();
                                //只取了数据结构，没有取数据
                                recordset = datasetVector.GetRecordset(true, SuperMap.Data.CursorType.Dynamic);
                                //设置批量提交
                                // 设置批量更新的限度为5000，注意一定要在开始批量更新前设置MaxRecordCount!
                                recordset.Batch.MaxRecordCount = 500;
                                // 开始批量更新，当添加到设置的MaxRecordCount的下一条记录时，将会将MaxRecordCount条记录自动提交到数据库中。
                                recordset.Batch.Begin();

                                tempRecordset.MoveFirst();
                                //遍历临时记录集
                                for (Int32 i = 0; i < tempRecordset.RecordCount; i++)
                                {
                                    //往mysql新增记录
                                    SuperMap.Data.Geometry geoPoint = tempRecordset.GetGeometry();
                                    recordset.AddNew(geoPoint);
                                    //SeekID:在记录中搜索指定 ID 号的记录，并定位该记录为当前记录。 
                                    recordset.MoveLast();
                                    foreach (SuperMap.Data.FieldInfo fileInfo in tempFieldInfos)
                                    {
                                        if (!fileInfo.IsSystemField && IsHaveField(datasetVector.FieldInfos, fileInfo.Name))
                                        {
                                            recordset.Edit();
                                            recordset.SetFieldValue(fileInfo.Name, tempRecordset.GetFieldValue(fileInfo.Name));
                                            Object valueID = recordset.GetFieldValue(fileInfo.Name);
                                        }
                                    }

                                    //处理业务数据

                                    tempRecordset.MoveNext();

                                    //recordset.Update();
                                }

                                // 使用批量更新的Update，提交没有自动提交的记录
                                recordset.Batch.Update();
                            }

                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }
                        finally
                        {
                            //示例程序BatchAdd说明要释放记录集
                            if (recordset != null)
                            {
                                recordset.Dispose();
                            }
                            if (tempRecordset != null)
                            {
                                tempRecordset.Dispose();
                            }
                        }


                    }
                    else
                    {
                        throw new Exception($"导入{shapeFieldName}失败！");
                    }
                }
                else
                {
                    throw new Exception($"创建临时数据源{filePath}失败！");
                }


            }

            // 释放工作空间资源
            info.Dispose();
            workspace.Dispose();


            foreach (var file in files)
            {
                if (File.Exists(file))
                {
                    File.Delete(file);
                }
            }

            MessageBox.Show("成功!");
        }

        /// <summary>
        /// 创建数据集
		/// Create the dataset
        /// </summary>
        public Boolean CreateDataset(Datasource datasource, DatasetType datasetType, String datasetName)
        {
            Boolean result = false;
            if (datasource == null)
            {
                return result;
            }

            // 首先要判断输入的名字是否可用
            // Judge that whether the input name is usable or not
            if (!datasource.Datasets.IsAvailableDatasetName(datasetName))
            {
                MessageBox.Show($"名称为{datasetName}的数据集已存在");
                return result;
            }

            Datasets datasets = datasource.Datasets;
            DatasetVectorInfo vectorInfo = new DatasetVectorInfo();
            vectorInfo.Name = datasetName;

            try
            {
                // Point等为Vector类型，类型是一样的，可以统一处理
                // Data such as Point,Line,etc can be operated as the same method as they are all vector type
                switch (datasetType)
                {
                    case DatasetType.Point:
                    case DatasetType.Line:
                    case DatasetType.CAD:
                    case DatasetType.Region:
                    case DatasetType.Text:
                    case DatasetType.Tabular:
                        {
                            vectorInfo.Type = datasetType;
                            if (datasets.Create(vectorInfo) != null)
                                result = true;
                        }
                        break;
                    case DatasetType.Grid:
                        {
                            DatasetGridInfo datasetGridInfo = new DatasetGridInfo();
                            datasetGridInfo.Name = datasetName;
                            datasetGridInfo.Height = 200;
                            datasetGridInfo.Width = 200;
                            datasetGridInfo.NoValue = 1.0;
                            datasetGridInfo.PixelFormat = SuperMap.Data.PixelFormat.Single;
                            datasetGridInfo.EncodeType = EncodeType.LZW;

                            if (datasets.Create(datasetGridInfo) != null)
                                result = true;
                        }
                        break;
                    case DatasetType.Image:
                        {
                            DatasetImageInfo datasetImageInfo = new DatasetImageInfo();
                            datasetImageInfo.Name = datasetName;
                            datasetImageInfo.BlockSizeOption = BlockSizeOption.BS_128;
                            datasetImageInfo.Height = 200;
                            datasetImageInfo.Width = 200;
                            //datasetImageInfo.Palette = Colors.MakeRandom(10);
                            datasetImageInfo.EncodeType = EncodeType.None;

                            if (datasets.Create(datasetImageInfo) != null)
                                result = true;
                        }
                        break;

                    default:
                        break;
                }

            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message);
            }

            return result;
        }


        /// <summary>
        /// 连接数据库
        /// </summary>
        /// <returns></returns>
        public Datasource GetDbDatasource(Workspace workspace, DatasourceConnectionInfo info)
        {
            //设置数据源连接的引擎类型
            info.EngineType = EngineType.MySQL;
            //设置数据库连接字符串
            info.Server = server;
            info.Database = database;
            info.User = userName;
            info.Password = password;
            info.Alias = "Test";//不能为空
            // 创建/打开数据库数据源
            Datasource datasource = workspace.Datasources.Open(info);
            if (datasource == null)
            {
                Console.WriteLine("打开数据源失败");
                return null;
            }
            else
            {
                Console.WriteLine("数据源打开成功！");
                return datasource;
            }
        }


        private ImportResult ImportShpToTemp(string filePath, Datasource tempDatasource, string targetTableName)
        {
            ImportSettingSHP importSettingSHP = new ImportSettingSHP();
            importSettingSHP.IsImportEmptyDataset = true;//空数据集的数据源是否导入
            importSettingSHP.IsAttributeIgnored = false;
            importSettingSHP.IsImportAs3D = false;
            //设置当同名数据集存在时导入的模式,如果存在名字冲突，则覆盖(Overwrite)
            importSettingSHP.ImportMode = ImportMode.Overwrite;
            //设置需要导入的数据路径信息
            importSettingSHP.SourceFilePath = filePath;
            //设置需要导入的数据编码类型，因为有中文字段，所以用ASCII编码
            importSettingSHP.SourceFileCharset = Charset.ANSI;
            //设置要导入的目标数据源
            importSettingSHP.TargetDatasource = tempDatasource;
            //设置目标数据集名称
            importSettingSHP.TargetDatasetName = targetTableName;
            importSettingSHP.TargetEncodeType = EncodeType.None;
            DataImport importer = new DataImport();
            importer.ImportSettings.Add(importSettingSHP);
            //数据导入mysql数据库
            ImportResult result = importer.Run();
            return result;
        }

        private bool IsHaveField(FieldInfos fieldInfos, string fieldName)
        {
            foreach (SuperMap.Data.FieldInfo fileInfo in fieldInfos)
            {
                if (fileInfo.Name == fieldName)
                {
                    return true;
                }
            }
            return false;
        }

        private void CreateDataset_Click(object sender, RoutedEventArgs e)
        {
            Workspace workspace = null;
            DatasourceConnectionInfo info = null;
            try
            {
                Toolkit.SetDtNameAsTableName(true);

                var datasetName = "test222";

                workspace = new Workspace();
                info = new DatasourceConnectionInfo();
                Datasource datasource = GetDbDatasource(workspace, info);
                Datasets datasets = datasource.Datasets;
                if (!datasets.IsAvailableDatasetName(datasetName))
                {
                    datasets.Delete(datasetName);
                }
                DatasetVectorInfo vectorInfo = new DatasetVectorInfo();
                vectorInfo.Name = datasetName;
                vectorInfo.Type = DatasetType.Point;
                var result = datasets.Create(vectorInfo);
                if (result != null)
                {
                    MessageBox.Show("添加数据集成功");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (info != null)
                {
                    info.Dispose();
                }
                if (workspace != null)
                {
                    workspace.Dispose();
                }
            }
        }

        private void SetFieldInfo_Click(object sender, RoutedEventArgs e)
        {
            Workspace workspace = null;
            DatasourceConnectionInfo info = null;
            try
            {
                Toolkit.SetDtNameAsTableName(true);

                var datasetName = "";
                var newFieldList = new List<CreateFieldInfo>();
                newFieldList.Add(new CreateFieldInfo { Type = "int", Name = "test1Pro" });
                newFieldList.Add(new CreateFieldInfo { Type = "string", Name = "test2Pro" });

                workspace = new Workspace();
                info = new DatasourceConnectionInfo();
                Datasource datasource = GetDbDatasource(workspace, info);
                var dataset = datasource.Datasets[datasetName];
                if (dataset == null)
                {
                    throw new Exception($"没有名称为{datasetName}的数据集");
                }
                var datasetVector = dataset as DatasetVector;
                var fields = datasetVector.FieldInfos;
                //删除所有非sm系统字段
                foreach (FieldInfo field in fields)
                {
                    if (!field.IsSystemField)
                    {
                        fields.Remove(field.Name);
                    }
                }

                //新增字段
                foreach(var newField in newFieldList)
                {
                    switch (newField.Type)
                    {
                        case "int":
                            fields.Add(new FieldInfo(newField.Name, FieldType.Int32));
                            break;
                        case "string":
                            fields.Add(new FieldInfo(newField.Name, FieldType.Char));
                            break;
                        default:
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (info != null)
                {
                    info.Dispose();
                }
                if (workspace != null)
                {
                    workspace.Dispose();
                }
            }

        }
    }
}
