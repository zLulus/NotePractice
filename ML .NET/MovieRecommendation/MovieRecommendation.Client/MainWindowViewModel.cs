using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieRecommendation.Client
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

        private float userId;
        /// <summary>
        /// 用户id
        /// </summary>
        public float UserId
        {
            set
            {
                userId = value;
                OnPropertyChanged(nameof(UserId));
            }
            get
            {
                return userId;
            }
        }

        private float movieId;
        /// <summary>
        /// 电影id
        /// </summary>
        public float MovieId
        {
            set
            {
                movieId = value;
                OnPropertyChanged(nameof(MovieId));
            }
            get
            {
                return movieId;
            }
        }

        private float timestamp;
        /// <summary>
        /// 时间戳
        /// </summary>
        public float Timestamp
        {
            set
            {
                timestamp = value;
                OnPropertyChanged(nameof(Timestamp));
            }
            get
            {
                return timestamp;
            }
        }


        private string predictionResult;
        /// <summary>
        /// 预测结果
        /// </summary>
        public string PredictionResult
        {
            set
            {
                predictionResult = value;
                OnPropertyChanged(nameof(PredictionResult));
            }
            get
            {
                return predictionResult;
            }
        }

        #region 评估结果
        private double meanAbsoluteError;
        public double MeanAbsoluteError
        {
            set
            {
                meanAbsoluteError = value;
                OnPropertyChanged(nameof(MeanAbsoluteError));
            }
            get
            {
                return meanAbsoluteError;

            }
        }

        private double meanSquaredError;
        public double MeanSquaredError
        {
            set
            {
                meanSquaredError = value;
                OnPropertyChanged(nameof(MeanSquaredError));
            }
            get
            {
                return meanSquaredError;

            }
        }

        private double rootMeanSquaredError;
        public double RootMeanSquaredError
        {
            set
            {
                rootMeanSquaredError = value;
                OnPropertyChanged(nameof(RootMeanSquaredError));
            }
            get
            {
                return rootMeanSquaredError;

            }
        }

        private double lossFunction;
        public double LossFunction
        {
            set
            {
                lossFunction = value;
                OnPropertyChanged(nameof(LossFunction));
            }
            get
            {
                return lossFunction;

            }
        }

        private double rSquared;
        public double RSquared
        {
            set
            {
                rSquared = value;
                OnPropertyChanged(nameof(RSquared));
            }
            get
            {
                return rSquared;

            }
        }
        #endregion
    }
}
