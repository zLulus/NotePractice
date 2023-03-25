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
            private set
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
            private set
            {
                imagePath = value;
                Notification(nameof(ImagePath));
            }
        }
    }
}
