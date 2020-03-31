using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace CodeLibraryForDotNetCore.UseEF
{
    public static class SqlServerUseEFDemo
    {
        private static string connectionString = "";
        public static void Run()
        {
            string tableName = "";
            DataTable dt = new DataTable();
            SqlBulkCopyByDatatable(tableName, dt);
        }

        /// <summary>
        /// sql server批量数据录入
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="dt"></param>
        private static void SqlBulkCopyByDatatable(string tableName, DataTable dt)
        {
            using (var conn = new SqlConnection(connectionString))
            {
                //SqlBulkCopy:大容量加载带有来自其他源的数据的 SQL Server 表
                //https://docs.microsoft.com/zh-cn/dotnet/api/system.data.sqlclient.sqlbulkcopy?view=netframework-4.8&WT.mc_id=DT-MVP-5003010
                //SqlBulkCopyOptions:加载方式
                using (var sqlbulkcopy = new SqlBulkCopy(connectionString, SqlBulkCopyOptions.UseInternalTransaction))
                {
                    try
                    {
                        //超时
                        sqlbulkcopy.BulkCopyTimeout = 600;
                        //数据库目标表名
                        sqlbulkcopy.DestinationTableName = tableName;
                        for (int i = 0; i < dt.Columns.Count; i++)
                        {
                            //列名映射：源列名-目标列名
                            sqlbulkcopy.ColumnMappings.Add(dt.Columns[i].ColumnName, dt.Columns[i].ColumnName);
                        }
                        //数据写入目标表
                        sqlbulkcopy.WriteToServer(dt);
                    }
                    catch (System.Exception ex)
                    {
                        throw ex;
                    }
                }
            }
            
        }
    }
}
