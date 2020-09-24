using SuperMap.Data;
using SuperMap.Data.Conversion;
using SuperMapWpfDemo.Enums;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;

namespace SuperMapWpfDemo
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

        private void OpenShapeFile_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog()
            {
                Filter = "ShapeFile (*.shp)|*.shp"
            };
            var r = openFileDialog.ShowDialog();
            if (r == true)
                filePathTextBlock.Text = openFileDialog.FileName;
        }

        private void ConvertToGeoJSON_Click(object sender, RoutedEventArgs e)
        {
            ConvertGeometryToFormat(ConvertDataEnum.ToGeoJSON);
        }


        private void ConvertToWKB_Click(object sender, RoutedEventArgs e)
        {
            ConvertGeometryToFormat(ConvertDataEnum.ToWKB);
        }

        private void ConvertToWKT_Click(object sender, RoutedEventArgs e)
        {
            ConvertGeometryToFormat(ConvertDataEnum.ToWKT);
        }

        /// <summary>
        /// 将超图的Geometry格式转换为geojson/wkt/wkb格式
        /// </summary>
        /// <param name="convertDataEnum"></param>
        private void ConvertGeometryToFormat(ConvertDataEnum convertDataEnum)
        {
            var shapeFieldName = filePathTextBlock.Text;
            if (string.IsNullOrEmpty(shapeFieldName))
            {
                throw new Exception($"shp文件为空！");
            }
            Workspace workspace = new Workspace();
            DatasourceConnectionInfo info = new DatasourceConnectionInfo();
            //将shp文件的数据转存为udb临时数据，而不是读取在内存中，防止内存泄漏
            var filePath = $"{Directory.GetCurrentDirectory()}\\{Guid.NewGuid().ToString()}";
            var files = new List<string> { $"{filePath}.udb", $"{filePath}.udd" };
            string targetDatasetName = "targetDatasetName";
            //临时数据源
            DatasourceConnectionInfo tempInfo = new DatasourceConnectionInfo();
            //设置数据源连接的引擎类型
            tempInfo.EngineType = EngineType.UDB;
            tempInfo.Alias = targetDatasetName;

            tempInfo.Server = filePath;
            // 创建/打开数据库数据源
            Datasource tempDatasource = workspace.Datasources.Create(tempInfo);
            Recordset recordset = null, tempRecordset = null;
            if (tempDatasource != null)
            {
                ImportResult result = ImportShpToTemp(shapeFieldName, tempDatasource, targetDatasetName);
                if (result.FailedSettings.Length == 0)
                {
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

                            tempRecordset.MoveFirst();
                            //遍历记录集
                            for (Int32 i = 0; i < tempRecordset.RecordCount; i++)
                            {
                                Geometry geometry = tempRecordset.GetGeometry();
                                if (convertDataEnum == ConvertDataEnum.ToGeoJSON)
                                {
                                    var geoJSONTxt = Toolkit.GeometryToGeoJson(geometry);

                                    var dic = $"{Directory.GetCurrentDirectory()}\\geoJSON";
                                    if (!Directory.Exists(dic))
                                    {
                                        Directory.CreateDirectory(dic);
                                    }
                                    var geoJSONFilePath = $"{dic}\\{i}.txt";
                                    System.IO.File.WriteAllText(geoJSONFilePath, geoJSONTxt);
                                }
                                else if (convertDataEnum == ConvertDataEnum.ToWKB)
                                {
                                    var bytes = Toolkit.GeometryToWKB(geometry);
                                }
                                else if (convertDataEnum == ConvertDataEnum.ToWKT)
                                {
                                    var wktTxt = Toolkit.GeometryToWKT(geometry);

                                    var dic = $"{Directory.GetCurrentDirectory()}\\WKT";
                                    if (!Directory.Exists(dic))
                                    {
                                        Directory.CreateDirectory(dic);
                                    }
                                    var wktFilePath = $"{dic}\\{i}.txt";
                                    System.IO.File.WriteAllText(wktFilePath, wktTxt);
                                }

                                tempRecordset.MoveNext();
                            }
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
                        // 释放工作空间资源
                        info.Dispose();
                        workspace.Dispose();
                        //GC.Collect();
                        foreach (var file in files)
                        {
                            if (File.Exists(file))
                            {
                                File.Delete(file);
                            }
                        }
                    }


                }
                else
                {
                    throw new Exception($"导入{shapeFieldName}失败！");
                }
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
    }
}
