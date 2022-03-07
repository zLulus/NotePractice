using Microsoft.ML;
using System;
using System.IO;
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

        private void Retrain_Click(object sender, RoutedEventArgs e)
        {
            //注意，这里使用txt或者tsv格式的文件
            string m_trainCsvPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TrainData", "train-data.txt");
            string m_testCsvPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TrainData", "test-data2.txt");
            //这里保存目录与预测功能读取的模型文件路径不同，预测功能读取的模型文件为可视化生成的模型文件
            string m_modelDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Model");
            string m_modelPath = Path.Combine(m_modelDirectory, "UsedCarsPricePredictionMLModel.zip");

            MLContext mlContext = new MLContext(seed: 0);
            IDataView trainingDataView = mlContext.Data.LoadFromTextFile<ModelInput>(m_trainCsvPath, hasHeader: true);
            var model = UsedCarsPricePredictionMLModel.RetrainPipeline(mlContext, trainingDataView);
            if (!Directory.Exists(m_modelDirectory))
                Directory.CreateDirectory(m_modelDirectory);

            mlContext.Model.Save(model, trainingDataView.Schema, m_modelPath);
            MessageBox.Show($"模型保存成功:{m_modelPath}，开始评估");

            ITransformer loadedModel = mlContext.Model.Load(m_modelPath, out _);

            var testDataView = mlContext.Data.LoadFromTextFile<ModelInput>(m_testCsvPath, hasHeader: true);
            var testMetrics = mlContext.Regression.Evaluate(loadedModel.Transform(testDataView), labelColumnName:"Price");

            vm.MeanAbsoluteError = testMetrics.MeanAbsoluteError;
            vm.MeanSquaredError = testMetrics.MeanSquaredError;
            vm.RootMeanSquaredError = testMetrics.RootMeanSquaredError;
            vm.LossFunction = testMetrics.LossFunction;
            vm.RSquared = testMetrics.RSquared;
            MessageBox.Show($"评估完成");
        }
    }
}
