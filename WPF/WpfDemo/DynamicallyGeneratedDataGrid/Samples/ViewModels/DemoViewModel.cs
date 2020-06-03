using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfDemo.DynamicallyGeneratedDataGrid.Bases;

namespace WpfDemo.DynamicallyGeneratedDataGrid.Samples
{
    public class DemoViewModel : DataGridWithPageBaseViewModel<FakeDatabase>
    {
        private List<FakeDatabase> _source;

        public DemoViewModel(int pageSize, int currentPage) : base(pageSize, currentPage)
        {
            FakeDatabase fake = new FakeDatabase();
            _source = fake.GenerateFakeSource();
            FakeSource = new ObservableCollection<FakeDatabase>();

            //设置数据总数
            TotalCount = _source.Count;
            FirstPageAction();
        }

        /// <summary>
        /// 模拟读取数据库
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="currentPage"></param>
        /// <returns></returns>
        public override List<FakeDatabase> GetSource(int pageSize, int currentPage)
        {
            return _source.Skip((CurrentPage - 1) * PageSize).Take(PageSize).ToList();
        }

        public override void FirstPageAction()
        {
            CurrentPage = 1;

            var result = GetSource(PageSize, CurrentPage);

            FakeSource.Clear();

            FakeSource.AddRange(result);
        }

        public override void PreviousPageAction()
        {
            if (CurrentPage == 1)
            {
                return;
            }

            List<FakeDatabase> result = new List<FakeDatabase>();

            CurrentPage--;
            result = GetSource(PageSize, CurrentPage);

            FakeSource.Clear();

            FakeSource.AddRange(result);

        }

        public override void NextPageAction()
        {
            if (CurrentPage == TotalPage)
            {
                return;
            }

            List<FakeDatabase> result = new List<FakeDatabase>();

            CurrentPage++;
            result = GetSource(PageSize, CurrentPage);
            
            FakeSource.Clear();

            FakeSource.AddRange(result);

        }

        public override void LastPageAction()
        {
            CurrentPage = TotalPage;

            var result = GetSource(PageSize, CurrentPage);

            FakeSource.Clear();

            FakeSource.AddRange(result);
        }
    }
}
