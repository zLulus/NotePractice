using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DbManager<T> where T : class
    {
        private static DbManager<T> instance;
        private static object _lock = new object();
        private SqlConnection connection;
        public static DbManager<T> Instance
        {
            get
            {
                lock (_lock)
                {
                    if (instance == null)
                    {
                        instance = new DbManager<T>();
                    }
                }
                return instance;
            }
        }

        public DbManager()
        {
            connection = new SqlConnection("Server=115.28.102.108;DataBase=test2;Uid=sa;pwd=Woshizenglu9501;");
            connection.Open();
        }

        public IEnumerable<T> QueryBySQL(string sql)
        {
            return connection.Query<T>(sql);
        }

        public bool ExecuteOne(string sql)
        {
            if (connection.Execute(sql) != 0)
                return true;
            return false;
        }
    }
}
