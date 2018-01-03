using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.AngularjsStudy
{
    public class AEditResource
    {
        public decimal unitPrice { get; set; }
        /// <summary>
        /// 挂牌数量
        /// </summary>
        public int listingNumber { get; set; }

        public decimal totalPrice { get; set; }
        public DateTime? deliveryDate { get; set; }

        public string address { get; set; }
        public string productRemark { get; set; }
        public string brandName { get; set; }
    }
}
