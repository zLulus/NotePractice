using Maui.Demo.Pages.Bindings.ViewModels;
using System.Collections.ObjectModel;

namespace Maui.Demo.Pages.Formats.ViewModels
{
    public class StringFormatPageViewModel : BaseNotifyPropertyChanged
    {
        DateTime displayDateTime;
        public DateTime DisplayDateTime
        {
            get
            {
                return displayDateTime;
            }
            set
            {
                displayDateTime = value;
                Notification(nameof(DisplayDateTime));
            }
        }

        decimal number;
        public decimal Number
        {
            get
            {
                return number;
            }
            set
            {
                number = value;
                Notification(nameof(Number));
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

        decimal score;
        public decimal Score
        {
            get
            {
                return score;
            }
            set
            {
                score = value;
                Notification(nameof(Score));
            }
        }

        public ObservableCollection<ProductViewModel> Products { get; set; }
    }
}
