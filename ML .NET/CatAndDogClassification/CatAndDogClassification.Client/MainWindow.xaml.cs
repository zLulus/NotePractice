using CatAndDogClassification_Train;
using Microsoft.Win32;
using System.Threading.Tasks;
using System.Windows;

namespace CatAndDogClassification.Client
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

            vm = new MainWindowViewModel();
            DataContext = vm;
        }

        private void CatAndDogClassification_Click(object sender, RoutedEventArgs e)
        {
            Task.Run(() =>
            {
                var output = CatAndDogMLModel.Predict(new CatAndDogMLModel.ModelInput()
                {
                    ImageSource = vm.SrcImagePath
                });
                Dispatcher.Invoke(() =>
                {
                    MessageBox.Show($"分类结果为:{output.Prediction}");
                });
                vm.ClassificationResult = output.Prediction;
                vm.Score = output.Score;
            });
        }

        private void SelectSrcImagePath_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog file = new OpenFileDialog();
            file.Filter = "jpg/png/jpeg/gif文件|*.jpg;*.png;*.jpeg;*.gif";
            var isOk = file.ShowDialog();
            if (isOk.HasValue && isOk.Value)
            {
                vm.SrcImagePath = file.FileName;
            }
        }
    }
}
