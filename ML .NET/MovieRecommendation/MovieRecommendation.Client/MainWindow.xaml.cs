using Microsoft.ML;
using MovieRecommendation_Train;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static MovieRecommendation_Train.MovieRecommendationMLModel;
using Path = System.IO.Path;

namespace MovieRecommendation.Client
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

            vm=new MainWindowViewModel() 
            { 
                UserId=1,
                MovieId=1,
                Timestamp= 964982224
            };
            DataContext = vm;
        }

        private void Retrain_Click(object sender, RoutedEventArgs e)
        {
            Task.Run(() =>
            {
                Dispatcher.Invoke(() =>
                {
                    MessageBox.Show($"训练模型和评估为耗时操作，请耐心等待");
                });

                //注意，这里使用txt或者tsv格式的文件
                string trainCsvPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TrainData", "recommendation-ratings-train.txt");
                string testCsvPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TrainData", "recommendation-ratings-test.txt");
                //这里保存目录与预测功能读取的模型文件路径不同，预测功能读取的模型文件为可视化生成的模型文件
                string modelDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Model");
                string modelPath = Path.Combine(modelDirectory, "MovieRecommendationMLModel.zip");

                MLContext mlContext = new MLContext(seed: 0);
                IDataView trainingDataView = mlContext.Data.LoadFromTextFile<ModelInput>(trainCsvPath, hasHeader: true);
                var model = MovieRecommendationMLModel.RetrainPipeline(mlContext, trainingDataView);
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
                var testMetrics = mlContext.Regression.Evaluate(loadedModel.Transform(testDataView), labelColumnName: "rating");

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

        private void Prediction_Click(object sender, RoutedEventArgs e)
        {
            //Load sample data
            var sampleData = new MovieRecommendationMLModel.ModelInput()
            {
                UserId=vm.UserId,
                MovieId=vm.MovieId,
                Timestamp=vm.Timestamp
            };

            //Load model and predict output
            var result = MovieRecommendationMLModel.Predict(sampleData);
            var isRecommendation = result.Score > 3.5 ? "推荐" : "不推荐";
            vm.PredictionResult = $"{result.Score}({isRecommendation})";

        }
    }
}
