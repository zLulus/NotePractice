using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderStatusPrediction.Client
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

        private string order_no;
        /// <summary>
        /// 唯一的亚马逊订单号
        /// </summary>
        public string Order_no
        {
            set
            {
                order_no = value;
                OnPropertyChanged(nameof(Order_no));
            }
            get
            {
                return order_no;
            }
        }

        private string order_date;
        /// <summary>
        /// 订单下单时间
        /// </summary>
        public string Order_date
        {
            set
            {
                order_date = value;
                OnPropertyChanged(nameof(Order_date));
            }
            get
            {
                return order_date;
            }
        }

        private string buyer;
        /// <summary>
        /// 买家姓名
        /// </summary>
        public string Buyer
        {
            set
            {
                buyer = value;
                OnPropertyChanged(nameof(Buyer));
            }
            get
            {
                return buyer;
            }
        }

        private string ship_city;
        /// <summary>
        /// 收货城市
        /// </summary>
        public string Ship_city
        {
            set
            {
                ship_city = value;
                OnPropertyChanged(nameof(Ship_city));
            }
            get
            {
                return ship_city;
            }
        }

        private string ship_state;
        /// <summary>
        /// 收货联邦/州
        /// </summary>
        public string Ship_state
        {
            set
            {
                ship_state = value;
                OnPropertyChanged(nameof(Ship_state));
            }
            get
            {
                return ship_state;
            }
        }

        private string sku;
        /// <summary>
        /// 产品的唯一标识符
        /// </summary>
        public string Sku
        {
            set
            {
                sku = value;
                OnPropertyChanged(nameof(Sku));
            }
            get
            {
                return sku;
            }
        }

        private string description;
        /// <summary>
        /// 产品描述
        /// </summary>
        public string Description
        {
            set
            {
                description = value;
                OnPropertyChanged(nameof(Description));
            }
            get
            {
                return description;
            }
        }

        private float quantity;
        /// <summary>
        /// 购买数量
        /// </summary>
        public float Quantity
        {
            set
            {
                quantity = value;
                OnPropertyChanged(nameof(Quantity));
            }
            get
            {
                return quantity;
            }
        }

        private float item_total;
        /// <summary>
        /// 订单总金额
        /// </summary>
        public float Item_total
        {
            set
            {
                item_total = value;
                OnPropertyChanged(nameof(Item_total));
            }
            get
            {
                return item_total;
            }
        }

        private float shipping_fee;
        /// <summary>
        /// 卖家承担的邮费
        /// </summary>
        public float Shipping_fee
        {
            set
            {
                shipping_fee = value;
                OnPropertyChanged(nameof(Shipping_fee));
            }
            get
            {
                return shipping_fee;
            }
        }

        private string cod;
        /// <summary>
        /// 付款方式：货到付款与否
        /// </summary>
        public string Cod
        {
            set
            {
                cod = value;
                OnPropertyChanged(nameof(Cod));
            }
            get
            {
                return cod;
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
    }
}
