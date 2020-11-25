using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeLibrary.ConnectOracle
{
    public class ConnectOracleDemo
    {
        public static async Task Run()
        {
            try
            {
                //参考资料
                //https://www.cnblogs.com/zouhao/p/9000286.html
                var conn = "";
                OracleConnection con = new OracleConnection(conn);
                con.Open();
                var sql = @"";
                OracleCommand cmd = new OracleCommand(sql, con);
                var count = cmd.ExecuteNonQuery();
            }
            catch(Exception ex)
            {

            }
           
        }
    }
}
