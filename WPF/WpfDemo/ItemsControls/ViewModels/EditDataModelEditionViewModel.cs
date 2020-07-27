using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfDemo.ItemsControls.ViewModels
{
    public class EditDataModelEditionViewModel: BaseNotifyPropertyChanged
    {
        public bool IsCanModify { get; set; }
        public long DataModelId { get; set; }
        public long EditionId { get; set; }
        public string MainTableChineseName { get; set; }
        public string TableChineseName { get; set; }
        public Visibility RowVisibility { get; set; }
        private bool oneToOneIsChecked;
        public bool OneToOneIsChecked
        {
            get
            {
                return oneToOneIsChecked;
            }
            set
            {
                if (oneToOneIsChecked != value)
                {
                    oneToOneIsChecked = value;
                    NotifyPropertyChanged(nameof(OneToOneIsChecked));
                }
            }
        }
        private bool manyToOneIsChecked;
        public bool ManyToOneIsChecked
        {
            get
            {
                return manyToOneIsChecked;
            }
            set
            {
                if (manyToOneIsChecked != value)
                {
                    manyToOneIsChecked = value;
                    NotifyPropertyChanged(nameof(ManyToOneIsChecked));
                }
            }
        }
        public string GroupName { get { return $"GroupName_{EditionId}"; } }
    }
}
