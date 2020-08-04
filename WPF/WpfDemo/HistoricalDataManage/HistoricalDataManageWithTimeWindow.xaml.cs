using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WpfDemo.HistoricalDataManage.Models;
using WpfDemo.HistoricalDataManage.Tools;

namespace WpfDemo.HistoricalDataManage
{
    /// <summary>
    /// HistoricalDataManageWithTimeWindow.xaml 的交互逻辑
    /// </summary>
    public partial class HistoricalDataManageWithTimeWindow : UserControl
    {
        SqliteTool sqliteTool;
        string tableName = "StudentScore";
        string filePath { get { return $"{System.Environment.CurrentDirectory}\\HistoricalDataManage\\"; } }

        public HistoricalDataManageWithTimeWindow()
        {
            InitializeComponent();
        }

        #region 录入数据
        /// <summary>
        /// 录入数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void InsertData_Click(object sender, RoutedEventArgs e)
        {
            sqliteTool = new SqliteTool();
            var insertDataTime = DateTime.Parse(insertDataTimeDatePicker.Text);
            var insertDataCount = int.Parse(insertDataCountTextBox.Text);
            //按年份分库
            var dbName = $"HistoricalDataManageDb_{insertDataTime.ToString("yyyy")}";
            sqliteTool.CreateNewDatabase(filePath, dbName);
            sqliteTool.ConnectToDatabase(filePath, dbName);
            //按天分表
            var suffix = GetSuffix(insertDataTime);
            CreateTable(suffix);
            InsertData(insertDataCount, suffix, insertDataTime);
        }

        //在指定数据库中创建一个table
        private void CreateTable(string suffix)
        {
            //这里可以写成反射参数建立表的亚子
            string sql = $"create table {tableName}_{suffix} (Name varchar(20), Score int,CreateTime varchar(50))";
            sqliteTool.ExecuteNonQuery(sql);
        }

        //插入一些数据
        private void InsertData(int count, string suffix, DateTime createTime)
        {
            for (int i = 0; i < count; i++)
            {
                //用SqlParameter
                string sql = $"insert into {tableName}_{suffix} (Name, Score,CreateTime) values ('Me{i}', {i},'{createTime.ToString()}')";
                sqliteTool.ExecuteNonQuery(sql);
            }
        }

        #endregion

        #region 查询数据
        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void QueryData_Click(object sender, RoutedEventArgs e)
        {
            List<StudentScore> result = new List<StudentScore>();
            var queryDataStartTime= DateTime.Parse(queryDataStartTimeDatePicker.Text);
            var queryDataEndTime = DateTime.Parse(queryDataEndTimeDatePicker.Text);
            //分库分表查询，再合并
            //这里没有考虑时分秒维度的数据查询
            var queryDate = new DateTime(queryDataStartTime.Year, queryDataStartTime.Month, queryDataStartTime.Day);
            while(queryDate<= queryDataEndTime)
            {
                var suffix = GetSuffix(queryDate);
                var tempTableName = $"{tableName}_{suffix}";
                sqliteTool = new SqliteTool();
                var dbName = $"HistoricalDataManageDb_{queryDate.ToString("yyyy")}";
                //不必每次都重新连接，可以优化
                sqliteTool.ConnectToDatabase(filePath, dbName);
                //查询数据表是否存在
                var tableCount = sqliteTool.ExecuteQueryCount($"SELECT name FROM sqlite_master WHERE type='table' AND name='{tempTableName}';");
                if (tableCount > 0)
                {
                    //该用SqlParameter请不要吝惜，使用sql时需要时刻考虑sql注入的危险  ————我只是懒
                    var scores = sqliteTool.Query<StudentScore>($"select Name,Score,CreateTime from {tempTableName} order by Name asc");
                    result.AddRange(scores);
                }

                queryDate= queryDate.AddDays(1);
            }
            DataContext = result;

        }

        #endregion

        private string GetSuffix(DateTime time)
        {
            return time.ToString("MMdd");
        }
    }
}
