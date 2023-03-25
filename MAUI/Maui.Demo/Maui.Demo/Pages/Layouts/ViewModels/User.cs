using Maui.Demo.Pages.Bindings.ViewModels;

namespace Maui.Demo.Pages.Layouts.ViewModels
{
    public class User : BaseNotifyPropertyChanged
    {
        private string name;
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

        private string imagePath;
        public string ImagePath
        {
            get
            {
                return imagePath;
            }
            set
            {
                imagePath = value;
                Notification(nameof(ImagePath));
            }
        }
    }
}
