using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatAndDogClassification.Client
{
    internal class MainWindowViewModel : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        private string srcImagePath;
        public string SrcImagePath
        {
            set
            {
                srcImagePath = value;
                OnPropertyChanged(nameof(SrcImagePath));
            }
            get
            {
                return srcImagePath;

            }
        }

        private string classificationResult;
        public string ClassificationResult
        {
            set
            {
                classificationResult = value;
                OnPropertyChanged(nameof(ClassificationResult));
            }
            get
            {
                return classificationResult;

            }
        }

        public float[] Score { get; set; }
    }
}
