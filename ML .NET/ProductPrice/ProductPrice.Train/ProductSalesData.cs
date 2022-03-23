using Microsoft.ML.Data;

namespace ProductPrice.Train
{
    /// <summary>
    /// 输入数据类
    /// </summary>
    public class ProductSalesData
    {
        //指定应加载数据集中的哪些列（按列索引）
        [LoadColumn(0)]
        public string Month;

        [LoadColumn(1)]
        public float numSales;
    }

    /// <summary>
    /// 预测数据类
    /// </summary>
    public class ProductSalesPrediction
    {
        //对于异常情况检测，预测包括指示是否存在异常、原始分数和 p 值的警报。 P 值越接近 0，出现异常的可能性就越大。
        //vector to hold alert,score,p-value values
        [VectorType(3)]
        public double[] Prediction { get; set; }
    }
}