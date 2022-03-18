using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieSentiment.Client
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

        private string review;
        /// <summary>
        /// 评论
        /// </summary>
        public string Review
        {
            set
            {
                review = value;
                OnPropertyChanged(nameof(Review));
            }
            get
            {
                return review;
            }
        }

        private string predictionSentiment;
        /// <summary>
        /// 预测结果
        /// </summary>
        public string PredictionSentiment
        {
            set
            {
                predictionSentiment = value;
                OnPropertyChanged(nameof(PredictionSentiment));
            }
            get
            {
                return predictionSentiment;
            }
        }
    }
}
