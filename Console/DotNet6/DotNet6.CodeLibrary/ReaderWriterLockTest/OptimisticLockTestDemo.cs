using Microsoft.Data.Sqlite;

namespace DotNet6.CodeLibrary.ReaderWriterLockTest
{
    public class MyResource
    {
        public long Id { get; set; }
        public long version { get; set; }
        public long resource { get; set; }
    }

    /// <summary>
    /// 乐观锁实现
    /// </summary>
    public class OptimisticLockTestDemo
    {
        static string dbConnectionString;
        
        public static void Run()
        {
            dbConnectionString = $"Data Source=..\\..\\..\\..\\DotNet6.CodeLibrary\\ReaderWriterLockTest\\testdb.db";

            for (int i = 0; i < 100; i++)
            {
                Task.Run(() =>
                {
                    using(DbConnection connection = new DbConnection(dbConnectionString))
                    {
                        var resource = connection.Query<MyResource>("select Id,Version,Resource from myresource where Id=1").FirstOrDefault();

                        //模拟进行了一些耗时业务处理
                        Thread.Sleep(100);

                        var oldVersion = resource.version;
                        if (connection.ExecuteNonQuery($"update myresource set Resource={++resource.resource},Version={++resource.version} where Id={resource.Id} and Version={oldVersion}") == 1)
                        {
                            Console.WriteLine($"修改数据成功：当前数据为{resource.resource}，当前版本号为{resource.version}");
                        }
                        else
                        {
                            Console.WriteLine("版本号变更，不能修改数据");
                        }
                    }

                });
            }
        }

    }

    internal class DbConnection : System.IDisposable
    {
        private string dbConnectionString;
        private SqliteConnection dbConnection;

        internal DbConnection(string dbConnectionString)
        {
            dbConnection=new SqliteConnection(dbConnectionString);
            dbConnection.Open();
        }
        /// <summary>
        /// 使用sql查询语句
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <returns></returns>
        internal List<T> Query<T>(string sql) where T : class, new()
        {
            SqliteCommand command = new SqliteCommand(sql, dbConnection);
            SqliteDataReader reader = command.ExecuteReader();
            List<T> result = new List<T>();
            while (reader.Read())
            {
                T item = new T();
                Type type = typeof(T);
                var properties = type.GetProperties();
                foreach (var property in properties)
                {
                    if (property.PropertyType != typeof(DateTime))
                    {
                        type.GetProperty(property.Name).SetValue(item, reader[property.Name]);
                    }
                    else
                    {
                        DateTime dateTime = DateTime.Parse(reader[property.Name].ToString());
                        type.GetProperty(property.Name).SetValue(item, dateTime);
                    }
                }
                result.Add(item);
            }
            return result;
        }

        /// <summary>
        /// 执行，返回影响行数
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        internal int ExecuteNonQuery(string sql)
        {
            SqliteCommand command = new SqliteCommand(sql, dbConnection);
            return command.ExecuteNonQuery();
        }

        public void Dispose()
        {
            dbConnection?.Dispose();
        }
    }
}
