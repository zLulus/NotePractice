using SuperMap.Data;
using SuperMap.Data.Conversion;
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
        private DataImport m_dataImport;
        private Datasource m_desDatasource;

        public MainWindow()
        {
            InitializeComponent();
            Initialize();
        }

        private void Initialize()
        {
            m_dataImport = new DataImport();
        }

        private void ReadShpFile_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //https://www.supermap.com/EN/online/Deskpro%206.0/SDMain/html/R_Dataset_Import.htm
                //https://www.supermap.com/EN/online/Deskpro%206.0/SDTechTheme/ExpressHtml/ImEx_ArcGIS_Shape.htm
                Workspace workspace =new Workspace();
                DatasourceConnectionInfo info=new DatasourceConnectionInfo();
                //https://blog.csdn.net/tane_e/article/details/89393493
                String sourceFilePath = @".shp";
                var targetTableName = "";
                
                Datasource datasource = GetDatasource(workspace, info);
                if (datasource != null)
                {
                    //创建临时数据集
                    Datasets datasets = datasource.Datasets;
                    bool flag = datasets.Delete(targetTableName);
                    String tableName = datasets.GetAvailableDatasetName(targetTableName);

                    // 设置矢量数据集的信息
                    DatasetVectorInfo datasetVectorInfo = GetDatasetVector(datasets, tableName);
                    datasetVectorInfo.Dispose();// 释放资源

                    //导入到指定数据源的数据集中
                    //实际数据表名与取名不同，但使用超图sdk可以用“自己的命名”取出数据(smregister表查看数据集对应的实体表)
                    ImportShapeDataToDb(sourceFilePath, datasource, tableName);

                    //todo 查询临时表名
                    String sql = $"select SMTABLENAME from SMREGISTER where SMDATASETNAME = '{targetTableName}'";
                    String temp_name = "";
                }

                // 释放工作空间资源
                info.Dispose();
                workspace.Dispose();

            }
            catch (Exception ex)
            {

            }
            
        }


        /// <summary>
        /// 导入shape数据
        /// </summary>
        /// <param name="shapeFilePath">元数据shape文件位置</param>
        /// <param name="datasource">目标数据源</param>
        /// <param name="datasetName">目标数据dataSet名称</param>
        private static void ImportShapeDataToTarget(string shapeFilePath, Datasource datasource, string datasetName)
        {
            ImportSettingSHP importSettingSHP = new ImportSettingSHP();
            importSettingSHP.IsAttributeIgnored = false;
            //设置当同名数据集存在时导入的模式,如果存在名字冲突，则覆盖(Overwrite)
            importSettingSHP.ImportMode = ImportMode.Overwrite;
            //设置需要导入的数据路径信息
            importSettingSHP.SourceFilePath = shapeFilePath;
            //设置需要导入的数据编码类型，因为有中文字段，所以用ASCII编码
            importSettingSHP.SourceFileCharset = Charset.ANSI;
            //设置要导入的目标数据源
            importSettingSHP.TargetDatasource = datasource;
            //设置目标数据集名称
            importSettingSHP.TargetDatasetName = datasetName;
            DataImport importer = new DataImport();
            importer.ImportSettings.Add(importSettingSHP);
            //数据导入mysql数据库
            ImportResult result = importer.Run();
            if (result.FailedSettings.Length == 0)
            {
                Console.WriteLine("导入成功！");
            }
            else
            {
                Console.WriteLine("导入失败！");
            }
            importer.Dispose();
        }

        /// <summary>
        /// 新增/修改shape空间数据
        /// </summary>
        /// <param name="tableName">目标表名</param>
        /// <param name="shapeFieldName">新的shape数据</param>
        /// <param name="oidFieldName">不需要的字段</param>
        /// <param name="oid">不需要的字段</param>
        /// <param name="g">不需要的字段</param>
        public void UpdateShape(string tableName, string shapeFieldName)
        {
            DataImport m_dataImport = new DataImport();
            Datasource m_desDatasource;

            //https://blog.csdn.net/tane_e/article/details/89393493
            //https://www.supermap.com/EN/online/Deskpro%206.0/SDMain/html/R_Dataset_Import.htm
            //https://www.supermap.com/EN/online/Deskpro%206.0/SDTechTheme/ExpressHtml/ImEx_ArcGIS_Shape.htm
            Workspace workspace = new Workspace();
            DatasourceConnectionInfo info = new DatasourceConnectionInfo();

            Datasource datasource = GetDbDatasource(workspace, info);
            if (datasource != null)
            {
                var tempName = $"A{Guid.NewGuid().ToString()}";
                var filePath = $"{AppDomain.CurrentDomain.BaseDirectory}\\tempFiles\\{tempName}";
                DatasourceConnectionInfo fileConnectionInfo = new DatasourceConnectionInfo();
                //创建本地数据集
                var fileDatasource = GetFileDatasource(workspace, filePath);
                if (fileDatasource != null)
                {
                    //导入shape数据
                    ImportShapeDataToTarget(shapeFieldName, fileDatasource, tempName);
                }

                //拷贝本地数据集到mysql目标数据表
                datasource.CopyDataset(fileDatasource.Datasets[0], tempName, EncodeType.None);

                if (File.Exists(filePath))
                {
                    File.Delete($"{filePath}.udb");
                    File.Delete($"{filePath}.udd");
                }

                //                    //导入到本地文件
                //                    //实际数据表名与取名不同，但使用超图sdk可以用“自己的命名”取出数据(smregister表查看数据集对应的实体表)
                //                    //符合超图规范
                //                    var tempName =$"A{Guid.NewGuid().ToString()}";
                //                tempName= tempName.Replace("-", "_");

                //                ImportShapeDataToDb(shapeFieldName, datasource, tempName);

                //                //读取本地文件的数据，手动存入指定表tableName
                //                //todo
                //                //fileDatasource.Datasets
                //                //}
                //                //读取本地数据库，手动存入指定表tableName
                //                var r = QueryOne($"select SmTableName from smregister where SmDatasetName='{tempName}'");
                //                var realTableName = "";
                //                if (r != null)
                //                {
                //                    realTableName = r.ToString();
                //                    //读取超图临时表数据
                //                    var dt = GetDataTable(realTableName);
                //                    //读取业务表表结构
                //                    var targetDt = GetDataTable(tableName, true);
                //                    //依次赋值
                //                    foreach (DataRow row in dt.Rows)
                //                    {
                //                        targetDt.ImportRow(row);
                //                    }
                //                    //录入到指定业务表
                ////                    INSERT INTO jcsj_lq(SmID, SmKey, SmSdriW, SmSdriN, SmSdriE, SmSdriS, SmGranule, SmGeometry, SmUserID, SmLibTileID, SmArea, SmPerimeter)
                ////SELECT SmID, SmKey, SmSdriW, SmSdriN, SmSdriE, SmSdriS, SmGranule, SmGeometry, SmUserID, SmLibTileID, SmArea, SmPerimeter FROM smdtv_134

                //                    //删表删记录
                //                    //ExecuteNonQuery($"DROP TABLE {realTableName} ");
                //                    //ExecuteNonQuery($"delete from smregister where SmDatasetName='{tempName}' ");
                //                }



                //                //删去本地文件
                //                //File.Delete(filePath);

            }

            // 释放工作空间资源
            info.Dispose();
            workspace.Dispose();
        }

        /// <summary>
        /// 获得文件型数据源
        /// </summary>
        /// <param name="workspace"></param>
        /// <param name="info"></param>
        /// <param name="path"></param>
        /// <param name="isOpenOrCreate"></param>
        /// <returns></returns>
        private Datasource GetFileDatasource(Workspace workspace, string path)
        {
            DatasourceConnectionInfo info = new DatasourceConnectionInfo();
            //设置数据源连接的引擎类型
            info.EngineType = EngineType.UDB;
            //设置文件位置
            info.Server = path;
            // 创建/打开数据库数据源
            Datasource datasource;
            //if (operationType == DatasourceOperationTypeEnum.Create)
            //{
                datasource = workspace.Datasources.Create(info);
            //}
            //else
            //{
                datasource = workspace.Datasources.Open(info);
            //}
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


        /// <summary>
        /// 连接数据库
        /// </summary>
        /// <returns></returns>
        private Datasource GetDbDatasource(Workspace workspace, DatasourceConnectionInfo info)
        {
            //设置数据源连接的引擎类型
            info.EngineType = EngineType.MySQL;
            //设置数据库连接字符串
            info.Server = "";
            info.Database = "";
            //todo
            info.User = "";
            info.Password = "";
            info.Alias = "Test";//不能为空
            // 创建/打开数据库数据源
            Datasource datasource;
            //if (operationType == DatasourceOperationTypeEnum.Create)
            //{
            //    datasource = workspace.Datasources.Create(info);
            //}
            //else
            //{
            datasource = workspace.Datasources.Open(info);
            //}
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


        /// <summary>
        /// 连接数据库
        /// </summary>
        /// <returns></returns>
        private static Datasource GetDatasource(Workspace workspace, DatasourceConnectionInfo info)
        {
            //设置数据源连接的引擎类型
            info.EngineType = EngineType.MySQL;
            //设置数据库连接字符串
            info.Server = "";
            info.Database = "";
            info.User = "";
            info.Password = "";
            info.Alias = "Test";//不能为空
                                // 打开数据库数据源
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

        private static void ImportShapeDataToDb(string path, Datasource datasource, string name)
        {
            ImportSettingSHP importSettingSHP = new ImportSettingSHP();
            importSettingSHP.IsAttributeIgnored = false;
            //设置当同名数据集存在时导入的模式,如果存在名字冲突，则覆盖(Overwrite)
            importSettingSHP.ImportMode = ImportMode.Overwrite;
            //设置需要导入的数据路径信息
            importSettingSHP.SourceFilePath = path;
            //设置需要导入的数据编码类型，因为有中文字段，所以用ASCII编码
            importSettingSHP.SourceFileCharset = Charset.ANSI;
            //设置要导入的目标数据源
            importSettingSHP.TargetDatasource = datasource;
            //设置目标数据集名称
            importSettingSHP.TargetDatasetName = name;
            DataImport importer = new DataImport();
            importer.ImportSettings.Add(importSettingSHP);
            //数据导入mysql数据库
            ImportResult result = importer.Run();
            if (result.FailedSettings.Length == 0)
            {
                Console.WriteLine("导入成功！");
            }
            else
            {
                Console.WriteLine("导入失败！");
            }
            importer.Dispose();
        }

        private static DatasetVectorInfo GetDatasetVector(Datasets datasets, string name)
        {
            DatasetVectorInfo datasetVectorInfo = new DatasetVectorInfo();
            datasetVectorInfo.Type = DatasetType.Region;
            datasetVectorInfo.EncodeType = EncodeType.None;
            datasetVectorInfo.IsFileCache = true;
            datasetVectorInfo.Name = name;
            Console.WriteLine("临时数据集的信息为：" + datasetVectorInfo.ToString());
            DatasetVector dv_temp = datasets.Create(datasetVectorInfo);
            //业务逻辑
            dv_temp.Close();
            return datasetVectorInfo;
        }
    }
}
