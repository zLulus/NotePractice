using SuperMap.Data;
using SuperMap.Data.Conversion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMapWpfDemo.Common
{
    internal class SuperMapSdkHelper
    {
        internal static ImportResult ImportShpToDatasource(string filePath, Datasource datasource,string targetTableName)
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
            return result;
        }
    }
}
