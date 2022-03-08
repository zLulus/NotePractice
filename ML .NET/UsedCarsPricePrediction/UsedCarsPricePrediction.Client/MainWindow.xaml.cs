using Microsoft.ML;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using UsedCarsPricePrediction_Train;
using static UsedCarsPricePrediction_Train.UsedCarsPricePredictionMLModel;

namespace UsedCarsPricePrediction.Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MainWindowViewModel vm { get; set; }
        public MainWindow()
        {
            InitializeComponent();

            vm = new MainWindowViewModel()
            {
                Name = @"Maruti Wagon R LXI CNG",
                Location = @"Mumbai",
                Year = 2010F,
                KilometersDriven = 72000F,
                FuelType = @"CNG",
                Transmission = @"Manual",
                OwnerType = @"First",
                Engine = @"998 CC",
                Power = @"58.16 bhp",
                Seats = 5F,
            };

            DataContext = vm;
        }

        /// <summary>
        /// 预测
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Prediction_Click(object sender, RoutedEventArgs e)
        {
            //Load sample data
            var sampleData = new UsedCarsPricePredictionMLModel.ModelInput()
            {
                Name = vm.Name,
                Location = vm.Location,
                Year = vm.Year,
                Kilometers_Driven = vm.KilometersDriven,
                Fuel_Type = vm.FuelType,
                Transmission = vm.Transmission,
                Owner_Type = vm.OwnerType,
                Engine = vm.Engine,
                Power = vm.Power,
                Seats = 5F,
            };

            //Load model and predict output
            var result = UsedCarsPricePredictionMLModel.Predict(sampleData);
            vm.Price = result.Score;
        }

        /// <summary>
        /// 训练与评估
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Retrain_Click(object sender, RoutedEventArgs e)
        {
            Task.Run(() =>
            {
                Dispatcher.Invoke(() =>
                {
                    MessageBox.Show($"训练模型和评估为耗时操作，请耐心等待");
                });

                //注意，这里使用txt或者tsv格式的文件
                string trainCsvPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TrainData", "train-data.txt");
                string testCsvPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TrainData", "test-data2.txt");
                //这里保存目录与预测功能读取的模型文件路径不同，预测功能读取的模型文件为可视化生成的模型文件
                string modelDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Model");
                string modelPath = Path.Combine(modelDirectory, "UsedCarsPricePredictionMLModel.zip");

                MLContext mlContext = new MLContext(seed: 0);
                IDataView trainingDataView = mlContext.Data.LoadFromTextFile<ModelInput>(trainCsvPath, hasHeader: true);
                var model = UsedCarsPricePredictionMLModel.RetrainPipeline(mlContext, trainingDataView);
                if (!Directory.Exists(modelDirectory))
                    Directory.CreateDirectory(modelDirectory);

                mlContext.Model.Save(model, trainingDataView.Schema, modelPath);
                Dispatcher.Invoke(() =>
                {
                    MessageBox.Show($"模型保存成功:{modelPath}，开始评估");
                });
                

                ITransformer loadedModel = mlContext.Model.Load(modelPath, out _);

                var testDataView = mlContext.Data.LoadFromTextFile<ModelInput>(testCsvPath, hasHeader: true);
                //https://docs.microsoft.com/zh-cn/dotnet/api/microsoft.ml.regressioncatalog.evaluate?view=ml-dotnet&WT.mc_id=DT-MVP-5003010
                //https://docs.microsoft.com/zh-cn/dotnet/api/microsoft.ml.data.regressionmetrics?view=ml-dotnet&WT.mc_id=DT-MVP-5003010
                var testMetrics = mlContext.Regression.Evaluate(loadedModel.Transform(testDataView), labelColumnName: "Price");

                //获取模型的绝对损失
                vm.MeanAbsoluteError = testMetrics.MeanAbsoluteError;
                //获取模型的平方损失
                vm.MeanSquaredError = testMetrics.MeanSquaredError;
                //获取均方根损失（或 RMS），它是 L2 损失 MeanSquaredError 的平方根
                vm.RootMeanSquaredError = testMetrics.RootMeanSquaredError;
                //获取用户定义的丢失函数的结果
                vm.LossFunction = testMetrics.LossFunction;
                //获取模型的 R 平方值，也称为决定系数。 R-Squared 接近 1 表示模型拟合度更好。
                vm.RSquared = testMetrics.RSquared;
                Dispatcher.Invoke(() =>
                {
                    MessageBox.Show($"评估完成");
                });
            });
        }
    }
}
