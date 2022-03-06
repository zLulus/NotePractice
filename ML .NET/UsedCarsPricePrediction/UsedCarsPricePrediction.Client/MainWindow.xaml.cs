using System.Windows;
using UsedCarsPricePrediction_Train;

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
    }
}
