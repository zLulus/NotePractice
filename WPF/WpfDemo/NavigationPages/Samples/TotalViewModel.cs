using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace WpfDemo.NavigationPages
{
    public class TotalViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        //实现INotifyPropertyChanged接口
        internal void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private int departmentId;
        public int DepartmentId
        {
            get
            {
                return departmentId;
            }

            set
            {
                if (departmentId == value)
                    return;

                departmentId = value;
                NotifyPropertyChanged(nameof(DepartmentId));
            }
        }

        private string departmentName;
        public string DepartmentName
        {
            get
            {
                return departmentName;
            }

            set
            {
                if (departmentName == value)
                    return;

                departmentName = value;
                NotifyPropertyChanged(nameof(DepartmentName));
            }
        }

        private int employeeId;
        public int EmployeeId
        {
            get
            {
                return employeeId;
            }

            set
            {
                if (employeeId == value)
                    return;

                employeeId = value;
                NotifyPropertyChanged(nameof(EmployeeId));
            }
        }

        private string employeeName;
        public string EmployeeName
        {
            get
            {
                return employeeName;
            }

            set
            {
                if (employeeName == value)
                    return;

                employeeName = value;
                NotifyPropertyChanged(nameof(EmployeeName));
            }
        }
    }
}
