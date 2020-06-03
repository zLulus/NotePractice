using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WpfDemo.Command.Commands;

namespace WpfDemo.DynamicallyGeneratedDataGrid.Bases
{
    public abstract class DataGridWithPageBaseViewModel<T> : PageSettingViewModel where T : class
    {
        private ObservableCollection<T> _fakeSoruce;

        /// <summary>
        /// 显示数据
        /// </summary>
        public ObservableCollection<T> FakeSource
        {
            get
            {
                return _fakeSoruce;
            }
            set
            {
                if (_fakeSoruce != value)
                {
                    _fakeSoruce = value;
                    NotifyPropertyChanged(nameof(FakeSource));
                }
            }
        }

        private ICommand _firstPageCommand;

        public ICommand FirstPageCommand
        {
            get
            {
                return _firstPageCommand;
            }

            set
            {
                _firstPageCommand = value;
            }
        }

        private ICommand _previousPageCommand;

        public ICommand PreviousPageCommand
        {
            get
            {
                return _previousPageCommand;
            }

            set
            {
                _previousPageCommand = value;
            }
        }

        private ICommand _nextPageCommand;

        public ICommand NextPageCommand
        {
            get
            {
                return _nextPageCommand;
            }

            set
            {
                _nextPageCommand = value;
            }
        }

        private ICommand _lastPageCommand;

        public ICommand LastPageCommand
        {
            get
            {
                return _lastPageCommand;
            }

            set
            {
                _lastPageCommand = value;
            }
        }


        public abstract List<T> GetSource(int pageSize, int currentPage);
        public abstract void FirstPageAction();
        public abstract void PreviousPageAction();
        public abstract void NextPageAction();
        public abstract void LastPageAction();

        public DataGridWithPageBaseViewModel(int pageSize, int currentPage)
            : base(pageSize, currentPage)
        {
            //设置各按钮执行方法
            SetCommond(FirstPageAction, PreviousPageAction, NextPageAction, LastPageAction);
        }

        public void SetCommond(Action FirstPageAction, Action PreviousPageAction,
            Action NextPageAction, Action LastPageAction)
        {
            _firstPageCommand = new DelegateCommand(FirstPageAction);

            _previousPageCommand = new DelegateCommand(PreviousPageAction);

            _nextPageCommand = new DelegateCommand(NextPageAction);

            _lastPageCommand = new DelegateCommand(LastPageAction);
        }
    }
}
