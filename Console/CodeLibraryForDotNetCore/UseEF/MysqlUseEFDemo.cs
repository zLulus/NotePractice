using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace CodeLibraryForDotNetCore.UseEF
{
    public static class MysqlUseEFDemo
    {
        private static string connectionString = "connectionString";
        private static MySqlConnection connection;

        public static void Run()
        {
            connection = new MySqlConnection(connectionString);
            connection.Open();

            string tableName = "tableName";
            //创建数据
            DataTable dt = new DataTable();
            string ID = "ID";
            string col2 = "col2";
            dt.Columns.Add(ID);
            dt.Columns.Add(col2);
            DataRow dr = dt.NewRow();
            dr[ID] = Guid.NewGuid().ToString();
            dr[col2] = "123";
            dt.Rows.Add(dr);
            //录入数据方法1
            InsertDataTable(tableName, dt);
            //录入数据方法2
            SqlBulkCopyByDatatable(tableName, dt);
            //录入数据方法3
            MySqlExcuteBatch("", tableName);

            connection.Close();
        }

        /// <summary>
        /// 一条条进行数据录入
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        private static int InsertDataTable(string tableName, DataTable dt)
        {
            //循环列，循环每行，执行insert sql语句
            string colStr = "";
            List<string> col = new List<string>();
            foreach (DataColumn column in dt.Columns)
            {
                col.Add(column.ColumnName);
                colStr += $"`{column.ColumnName}`,";
            }
            colStr = colStr.TrimEnd(',');
            var totalCount = 0;
            foreach (DataRow dr in dt.Rows)
            {
                string val = "";
                foreach (var columnName in col)
                {
                    val += $"\"{dr[columnName]}\",";
                }
                val = val.TrimEnd(',');
                //这里可以使用MySqlParameter
                string sql = $"insert into `{tableName}`({colStr}) values({val})";
                totalCount+= ExecuteNonQuery(sql, null);
            }

            return totalCount;
        }

        /// <summary>
        /// 批量数据录入
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="dt"></param>
        public static void SqlBulkCopyByDatatable(string tableName, DataTable dt)
        {
            try
            {
                //循环列，循环每行，执行insert sql语句
                //一次提交500条记录
                var maxSingleCount = 500;
                //本次录入的数据条数
                var singleCount = 0;
                var totalCount = 0;
                //总共需要录入的数据条数
                var recordCount = dt.Rows.Count;
                string colStr = "";
                List<string> col = new List<string>();
                foreach (DataColumn column in dt.Columns)
                {
                    col.Add(column.ColumnName);
                    colStr += $"`{column.ColumnName}`,";
                }
                colStr = colStr.TrimEnd(',');
                var valueStr = "";
                foreach (DataRow dr in dt.Rows)
                {
                    string val = "";
                    foreach (var columnName in col)
                    {
                        val += $"\"{dr[columnName]}\",";
                    }
                    val = val.TrimEnd(',');
                    //https://blog.csdn.net/atgc/article/details/2039672
                    valueStr += $"({val}),";
                    singleCount++;
                    totalCount++;
                    //满足maxSingleCount条数据，或者到达最后一条数据，则录入
                    if (singleCount >= maxSingleCount || totalCount >= recordCount)
                    {
                        singleCount = 0;
                        valueStr = valueStr.TrimEnd(',');
                        //insert into table(columns) values(value1),(value2),.....(valuen);
                        string sql = $"insert into `{tableName}`({colStr}) values{valueStr}";
                        ExecuteNonQuery(sql, null);
                        valueStr = "";
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private static int ExecuteNonQuery(string sql, IEnumerable<MySqlParameter> parameters = null)
        {
            int n = 0;
            using (var cmd = new MySqlCommand(sql, connection))
            {
                //事务
                //https://www.cnblogs.com/lhyqzx/p/6440959.html
                var sqlTransaction = connection.BeginTransaction();
                cmd.Transaction = sqlTransaction;
                if (parameters != null)
                {
                    foreach(var p in parameters)
                    {
                        cmd.Parameters.Add(p);
                    }
                }
                try
                {
                    n = cmd.ExecuteNonQuery();
                    sqlTransaction.Commit();
                }
                catch(Exception ex)
                {
                    sqlTransaction.Rollback();
                    throw ex;
                }
                
            }
            return n;
        }

        /// <summary>
        /// 通过csv文件导入数据
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="tableName"></param>
        public static void MySqlExcuteBatch( string filePath, string tableName)
        {
            MySqlBulkLoader bulk = new MySqlBulkLoader(connection)
            {
                FieldTerminator = ",",//这个地方字段间的间隔方式，为逗号
                FieldQuotationCharacter = '"',
                EscapeCharacter = '"',
                LineTerminator = "\r\n",//每行
                FileName = filePath,//文件地址
                NumberOfLinesToSkip = 0,
                TableName = tableName,
            };
            bulk.Load();
        }
    }
}
