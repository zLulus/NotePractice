using Microsoft.ML;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductPrice.Train
{
    /// <summary>
    /// https://docs.microsoft.com/zh-cn/dotnet/machine-learning/tutorials/sales-anomaly-detection?WT.mc_id=DT-MVP-5003010
    /// </summary>
    public class ProductPriceMLModel
    {
        //数据所在位置
        static string _dataPath = Path.Combine(Environment.CurrentDirectory, "TrainData", "product-sales.csv");
        //assign the Number of records in dataset file to constant variable
        //共36条数据
        static int _docsize = 36;
        public static void Train()
        {
            MLContext mlContext = new MLContext();
            //加载数据
            IDataView dataView = mlContext.Data.LoadFromTextFile<ProductSalesData>(path: _dataPath, hasHeader: true, separatorChar: ',');

            DetectIidSpike(mlContext, dataView);

            DetectIidChangePoint(mlContext, dataView);
        }


        /// <summary>
        /// 峰值检测
        /// </summary>
        /// <param name="mlContext"></param>
        /// <param name="dataView"></param>
        private static void DetectIidSpike(MLContext mlContext, IDataView dataView)
        {
            Console.WriteLine("峰值检测");
            //引用Microsoft.ML.TimeSeries
            //https://docs.microsoft.com/zh-cn/dotnet/api/microsoft.ml.transforms.timeseries.iidspikeestimator?view=ml-dotnet&WT.mc_id=DT-MVP-5003010
            //outputColumnName:输出
            //inputColumnName:输入
            //confidence 和 pvalueHistoryLength 参数会影响检测峰值的方式。
            //confidence:确定模型对峰值的敏感度。 置信度越低，算法检测到“较小”峰值的可能性就大。
            //pvalueHistoryLength:参数定义滑动窗口中数据点的数量。 此参数的值通常是占整个数据集的百分比。 pvalueHistoryLength 越低，模型忘记之前的较大峰值的速度就越快。
            //调用DetectIidSpike方法检测峰值
            var iidSpikeEstimator = mlContext.Transforms.DetectIidSpike(
                outputColumnName: nameof(ProductSalesPrediction.Prediction), 
                inputColumnName: nameof(ProductSalesData.numSales),
                confidence: 95, 
                pvalueHistoryLength: _docsize / 4);

            ITransformer iidSpikeTransform = iidSpikeEstimator.Fit(CreateEmptyDataView(mlContext));

            //使用 Transform() 方法转换数据
            IDataView transformedData = iidSpikeTransform.Transform(dataView);
            //使用 CreateEnumerable() 方法和以下代码将 transformedData 转换为强类型 IEnumerable
            var predictions = mlContext.Data.CreateEnumerable<ProductSalesPrediction>(transformedData, reuseRowObject: false);
            
            Console.WriteLine("Alert\tScore\tP-Value");
            foreach (var p in predictions)
            {
                var results = $"{p.Prediction[0]}\t{p.Prediction[1]:f2}\t{p.Prediction[2]:F2}";

                if (p.Prediction[0] == 1)
                {
                    results += " <-- Spike detected";
                }

                Console.WriteLine(results);
            }
            Console.WriteLine("");
        }

        /// <summary>
        /// 更改点检测
        /// </summary>
        /// <param name="mlContext"></param>
        /// <param name="dataView"></param>
        private static void DetectIidChangePoint(MLContext mlContext, IDataView dataView)
        {
            Console.WriteLine("更改点检测");
            //https://docs.microsoft.com/zh-cn/dotnet/api/microsoft.ml.transforms.timeseries.iidchangepointestimator?view=ml-dotnet&WT.mc_id=DT-MVP-5003010
            var iidChangePointEstimator = mlContext.Transforms.DetectIidChangePoint(outputColumnName: nameof(ProductSalesPrediction.Prediction), inputColumnName: nameof(ProductSalesData.numSales), confidence: 95, changeHistoryLength: _docsize / 4);
            var iidChangePointTransform = iidChangePointEstimator.Fit(CreateEmptyDataView(mlContext));
            IDataView transformedData = iidChangePointTransform.Transform(dataView);
            var predictions = mlContext.Data.CreateEnumerable<ProductSalesPrediction>(transformedData, reuseRowObject: false);
            Console.WriteLine("Alert\tScore\tP-Value\tMartingale value");
            foreach (var p in predictions)
            {
                var results = $"{p.Prediction[0]}\t{p.Prediction[1]:f2}\t{p.Prediction[2]:F2}\t{p.Prediction[3]:F2}";

                if (p.Prediction[0] == 1)
                {
                    results += " <-- alert is on, predicted changepoint";
                }
                Console.WriteLine(results);
            }
            Console.WriteLine("");
        }

        static IDataView CreateEmptyDataView(MLContext mlContext)
        {
            //生成一个空数据视图对象，该对象具有正确架构，可用作 IEstimator.Fit() 方法的输入
            // Create empty DataView. We just need the schema to call Fit() for the time series transforms
            IEnumerable<ProductSalesData> enumerableData = new List<ProductSalesData>();
            return mlContext.Data.LoadFromEnumerable(enumerableData);
        }
    }
}
