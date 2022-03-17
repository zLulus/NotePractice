using OrderStatusPrediction_Train;
using System.Windows;

namespace OrderStatusPrediction.Client
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
                Order_no = "404-3964908-7850720",
                Order_date = "Tue, 19 Oct, 2021, 6:05 pm IST",
                Buyer = "Minam",
                Ship_city = "PASIGHAT,",
                Ship_state = "ARUNACHAL PRADESH",
                Sku = "DN-0WDX-VYOT",
                Description = "Women's Set of 5 Multicolor Pure Leather Single Lipstick Cases with Mirror, Handy and Compact Handcrafted Shantiniketan Block Printed Jewelry Boxes",
                Quantity = 1,
                Item_total = (float)449.00,
                Shipping_fee = (float)60.18,
                Cod = "Not Cash On Delivery"
            };
            DataContext = vm;
        }

        private void Prediction_Click(object sender, RoutedEventArgs e)
        {
            var sampleData = new OrderStatusPredictionMLModel.ModelInput()
            {
                Order_no = vm.Order_no,
                Order_date = vm.Order_date,
                Buyer = vm.Buyer,
                Ship_city = vm.Ship_city,
                Ship_state = vm.Ship_state,
                Sku = vm.Sku,
                Description = vm.Description,
                Quantity = vm.Quantity,
                Item_total = vm.Item_total,
                Shipping_fee = vm.Shipping_fee,
                Cod = vm.Cod
            };

            //Load model and predict output
            var result = OrderStatusPredictionMLModel.Predict(sampleData);
            vm.PredictionResult = $"{result.Prediction}({result.Score[0]})";
        }
    }
}
