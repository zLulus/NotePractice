using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsedCarsPricePrediction.Client
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


        private string name;
        /// <summary>
        /// 汽车名称
        /// </summary>
        public string Name
        {
            set
            {
                name = value;
                OnPropertyChanged(nameof(Name));
            }
            get
            {
                return name;

            }
        }

        private string location;
        /// <summary>
        /// 所在地
        /// </summary>
        public string Location
        {
            set
            {
                location = value;
                OnPropertyChanged(nameof(Location));
            }
            get
            {
                return location;

            }
        }

        private float year;
        /// <summary>
        /// 年份
        /// </summary>
        public float Year
        {
            set
            {
                year = value;
                OnPropertyChanged(nameof(Year));
            }
            get
            {
                return year;

            }
        }

        private float kilometersDriven;
        /// <summary>
        /// 行驶车程
        /// </summary>
        public float KilometersDriven
        {
            set
            {
                kilometersDriven = value;
                OnPropertyChanged(nameof(KilometersDriven));
            }
            get
            {
                return kilometersDriven;

            }
        }

        private string fuelType;
        /// <summary>
        /// 汽油种类
        /// </summary>
        public string FuelType
        {
            set
            {
                fuelType = value;
                OnPropertyChanged(nameof(FuelType));
            }
            get
            {
                return fuelType;

            }
        }

        private string transmission;
        /// <summary>
        /// 手动挡/自动挡
        /// </summary>
        public string Transmission
        {
            set
            {
                transmission = value;
                OnPropertyChanged(nameof(Transmission));
            }
            get
            {
                return transmission;

            }
        }

        private string ownerType;
        /// <summary>
        /// 转手次数
        /// </summary>
        public string OwnerType
        {
            set
            {
                ownerType = value;
                OnPropertyChanged(nameof(OwnerType));
            }
            get
            {
                return ownerType;

            }
        }

        private string engine;
        /// <summary>
        /// 引擎
        /// </summary>
        public string Engine
        {
            set
            {
                engine = value;
                OnPropertyChanged(nameof(Engine));
            }
            get
            {
                return engine;

            }
        }


        private string power;
        /// <summary>
        /// 马力
        /// </summary>
        public string Power
        {
            set
            {
                power = value;
                OnPropertyChanged(nameof(Power));
            }
            get
            {
                return power;

            }
        }

        private float seats;
        /// <summary>
        /// 座位
        /// </summary>
        public float Seats
        {
            set
            {
                seats = value;
                OnPropertyChanged(nameof(Seats));
            }
            get
            {
                return seats;

            }
        }

        private float price;
        /// <summary>
        /// 二手价格
        /// </summary>
        public float Price
        {
            set
            {
                price = value;
                OnPropertyChanged(nameof(Price));
            }
            get
            {
                return price;

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
