using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WpfDemo.Bind.Models;

namespace WpfDemo.Bind
{
    /// <summary>
    /// Interaction logic for DataGridBackgroundBind.xaml
    /// </summary>
    public partial class DataGridBackgroundBind : Window
    {
        List<Report> reports;
        public DataGridBackgroundBind()
        {
            InitializeComponent();
            SetData();
            GenerateDataGrid();
        }

        private void GenerateDataGrid()
        {
            DataGrid dataGrid = new DataGrid();
            var _ds = new DataSet("Test");
            DataTable dt= _ds.Tables.Add("月度绩效表");
            //create columns
            //创建列
            dt.Columns.Add("月份");
            foreach (var item in reports[0].ReportDetails)
            {
                dt.Columns.Add(item.EmployeeName);
            }
            //fill data to rows
            //赋值数据
            for(int i=0;i< reports.Count;i++)
            {
                var theRow = dt.NewRow();
                theRow[0] = reports[i].StatisticalDate;
                for (int j = 0; j < reports[i].ReportDetails.Count; j++)
                {
                    theRow[j+1] = reports[i].ReportDetails[j].Data;
                }
                dt.Rows.Add(theRow);
            }
            dataGrid.ItemsSource = dt.AsDataView();
            //将控件添加到Grid
            MyGrid.Children.Add(dataGrid);
        }

        private void SetData()
        {
            reports = new List<Report>();
            reports.Add(new Report()
            {
                StatisticalDate = "2018/01",
                ReportDetails = new List<ReportDetail>()
                {
                    new ReportDetail(){ EmployeeName="李胜",Data=(decimal)1.0},
                    new ReportDetail(){ EmployeeName="杨军",Data=(decimal)1.1},
                    new ReportDetail(){ EmployeeName="李琪",Data=(decimal)0.9},
                    new ReportDetail(){ EmployeeName="刘宇",Data=(decimal)1.2},
                    new ReportDetail(){ EmployeeName="张屹",Data=(decimal)1.5},
                }
            });
            reports.Add(new Report()
            {
                StatisticalDate = "2018/02",
                ReportDetails = new List<ReportDetail>()
                {
                    new ReportDetail(){ EmployeeName="李胜",Data=(decimal)0.9},
                    new ReportDetail(){ EmployeeName="杨军",Data=(decimal)0.8},
                    new ReportDetail(){ EmployeeName="李琪",Data=(decimal)1.3},
                    new ReportDetail(){ EmployeeName="刘宇",Data=(decimal)1.2},
                    new ReportDetail(){ EmployeeName="张屹",Data=(decimal)1.0},
                }
            });
            reports.Add(new Report()
            {
                StatisticalDate = "2018/03",
                ReportDetails = new List<ReportDetail>()
                {
                    new ReportDetail(){ EmployeeName="李胜",Data=(decimal)0.9},
                    new ReportDetail(){ EmployeeName="杨军",Data=(decimal)0.7},
                    new ReportDetail(){ EmployeeName="李琪",Data=(decimal)0.6},
                    new ReportDetail(){ EmployeeName="刘宇",Data=(decimal)1.0},
                    new ReportDetail(){ EmployeeName="张屹",Data=(decimal)1.1},
                }
            });
        }
    }
}
