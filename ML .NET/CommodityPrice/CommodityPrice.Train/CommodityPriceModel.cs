using Microsoft.ML.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommodityPrice.Train
{
    /// <summary>
    /// 输入数据类
    /// </summary>
    public class CommodityPriceModel
    {
        //指定应加载数据集中的哪些列（按列索引）
        [LoadColumn(0)]
        public string date;

        [LoadColumn(1)]
        public float open;
    }

    /// <summary>
    /// 预测数据类
    /// </summary>
    public class CommodityPricePrediction
    {
        //对于异常情况检测，预测包括指示是否存在异常、原始分数和 p 值的警报。 P 值越接近 0，出现异常的可能性就越大。
        //vector to hold alert,score,p-value values
        [VectorType(3)]
        public double[] Prediction { get; set; }
    }
}
