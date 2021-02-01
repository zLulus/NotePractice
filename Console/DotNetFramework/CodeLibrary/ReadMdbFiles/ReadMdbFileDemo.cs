using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Odbc;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeLibrary.ReadMdbFiles
{
    public class ReadMdbFileDemo
    {
        public static void Run()
        {
            //https://stackoverflow.com/questions/10374808/how-to-connect-to-a-ms-access-file-mdb-using-c
            //OdbcConnection、OleDbConnection均可使用

            var filePath = @"mdb file path";
            var sql = "SELECT * FROM `TableName`";

            if (!File.Exists(filePath))
            {
                throw new Exception("文件：" + filePath + "不存在！");
            }

            UseOleDbConnection(filePath,sql);
            UseOdbcConnection(filePath, sql);

        }

        private static void UseOdbcConnection(string filePath, string sql)
        {
            var myConnectionString = @"Driver={Microsoft Access Driver (*.mdb)};" + $"Dbq={filePath};Uid=Admin;Pwd=;";
            using (OdbcConnection myConnection = new OdbcConnection())
            {
                // 打开OleDb连接
                myConnection.ConnectionString = myConnectionString;
                myConnection.Open();

                // 执行查询
                OdbcDataAdapter pAdapter = new OdbcDataAdapter(sql, myConnection);
                DataTable dataTable = new DataTable();
                pAdapter.Fill(dataTable);
            }
        }

        private static void UseOleDbConnection(string filePath,string sql)
        {
            var myConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;" +
                           $"Data Source={filePath};" +
                           "Persist Security Info=True;" +
                           "Jet OLEDB:Database Password=myPassword;";
            using (OleDbConnection myConnection = new OleDbConnection())
            {
                // 打开OleDb连接
                myConnection.ConnectionString = myConnectionString;
                myConnection.Open();

                // 执行查询
                OleDbCommand cmd = myConnection.CreateCommand();
                cmd.CommandText = sql;
                OleDbDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection); // 完成查询操作后关闭连接

                // 加载数据到DataTable
                DataTable myDataTable = new DataTable();
                myDataTable.Load(reader);
            }
        }
    }
}
