using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfDemo.HistoricalDataManage.Tools
{
    public class SqliteTool
    {
        //数据库连接
        SQLiteConnection m_dbConnection;

        /// <summary>
        /// 创建一个空的数据库
        /// </summary>
        /// <param name="fileName"></param>
        public void CreateNewDatabase(string dicPath,string dbName)
        {
            var dbPath = $"{dicPath}\\{dbName}.sqlite";
            if (!File.Exists(dbPath))
            {
                SQLiteConnection.CreateFile(dbPath);
            }
            
        }

        /// <summary>
        /// 创建一个连接到指定数据库
        /// </summary>
        public void ConnectToDatabase(string dicPath, string dbName)
        {
            m_dbConnection = new SQLiteConnection($"Data Source={dicPath}\\{dbName}.sqlite;Version=3;");
            m_dbConnection.Open();
        }

        public int ExecuteNonQuery(string sql)
        {
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            return command.ExecuteNonQuery();
        }

        public int ExecuteQueryCount(string sql)
        {
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();
            int count = 0;
            while (reader.Read())
            {
                count++;
            }
            return count;
        }

        /// <summary>
        /// 使用sql查询语句
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <returns></returns>
        public List<T> Query<T>(string sql) where T : class, new()
        {
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();
            List<T> result = new List<T>();
            while (reader.Read())
            {
                T item = new T();
                Type type = typeof(T);
                var properties = type.GetProperties();
                foreach(var property in properties)
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
        
    }
}
