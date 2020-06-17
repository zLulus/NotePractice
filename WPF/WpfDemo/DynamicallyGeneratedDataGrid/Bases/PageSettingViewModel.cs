using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace WpfDemo.DynamicallyGeneratedDataGrid.Bases
{
    public abstract class PageSettingViewModel : INotifyPropertyChanged
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

        private int _pageSize;

        public int PageSize
        {
            get
            {
                return _pageSize;
            }
            set
            {
                if (_pageSize != value)
                {
                    _pageSize = value;
                    NotifyPropertyChanged(nameof(PageSize));
                }
            }
        }

        private int _currentPage;

        public int CurrentPage
        {
            get
            {
                return _currentPage;
            }

            set
            {
                if (_currentPage != value)
                {
                    _currentPage = value;
                    NotifyPropertyChanged(nameof(CurrentPage));
                }
            }
        }

        private int _totalPage;

        public int TotalPage
        {
            get
            {
                return _totalPage;
            }

            set
            {
                if (_totalPage != value)
                {
                    _totalPage = value;
                    NotifyPropertyChanged(nameof(TotalPage));
                }
            }
        }

        private int _totalCount;
        public int TotalCount
        {
            get
            {
                return _totalCount;
            }

            set
            {
                if (_totalCount != value)
                {
                    _totalCount = value;
                    NotifyPropertyChanged(nameof(TotalCount));
                    //修改总页数
                    TotalPage = _totalCount / PageSize;
                    if (_totalCount % PageSize != 0)
                    {
                        TotalPage = TotalPage + 1;
                    }
                    if (TotalPage == 0)
                        TotalPage = 1;
                }
            }
        }

        public PageSettingViewModel(int pageSize, int currentPage)
        {
            _currentPage = currentPage;

            _pageSize = pageSize;
        }
    }
}
