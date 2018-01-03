using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class TraditionalSQLServerDBManager
    {
        SqlConnection con;
        public TraditionalSQLServerDBManager()
        {
            con = new SqlConnection();
            con.ConnectionString = "Server=;DataBase=;Uid=;pwd=;";
            con.Open();
        }

        public void ExcuteSQL(string sql)
        {
            SqlCommand com = new SqlCommand();
            com.Connection = con;
            com.CommandType = CommandType.Text;
            com.CommandText = sql;
            SqlDataReader dr = com.ExecuteReader();//执行SQL语句
            dr.Close();//关闭执行
            con.Close();//关闭数据库
        }
    }
}
