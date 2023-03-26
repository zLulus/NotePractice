using Maui.Demo.Pages.Bindings.ViewModels;

namespace Maui.Demo.Pages.Formats.ViewModels
{
    public class ProductViewModel : BaseNotifyPropertyChanged
    {
        string name;
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
                Notification(nameof(Name));
            }
        }

        decimal price;
        public decimal Price
        {
            get
            {
                return price;
            }
            set
            {
                price = value;
                Notification(nameof(Price));
            }
        }

        int stock;
        public int Stock
        {
            get
            {
                return stock;
            }
            set
            {
                stock = value;
                Notification(nameof(Stock));
            }
        }

        int inStock;
        public int InStock
        {
            get
            {
                return inStock;
            }
            set
            {
                inStock = value;
                Notification(nameof(InStock));
            }
        }
    }
}
