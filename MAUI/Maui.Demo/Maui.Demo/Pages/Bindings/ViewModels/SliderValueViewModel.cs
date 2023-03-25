namespace Maui.Demo.Pages.Bindings.ViewModels
{
    public class SliderValueViewModel : BaseNotifyPropertyChanged
    {
        private double sliderValue;

        public double SliderValue
        {
            get => sliderValue;
            set
            {
                sliderValue = value;
                Notification(nameof(SliderValue));
            }
        }
    }
}
