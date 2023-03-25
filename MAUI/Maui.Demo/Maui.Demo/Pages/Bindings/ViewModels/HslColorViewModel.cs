namespace Maui.Demo.Pages.Bindings.ViewModels
{
    public class HslColorViewModel : BaseNotifyPropertyChanged
    {
        float hue;
        public float Hue
        {
            get
            {
                return hue;
            }
            set
            {
                if (hue != value)
                {
                    Color = Color.FromHsla(value, saturation, luminosity);
                }
            }
        }

        float saturation;
        public float Saturation
        {
            get
            {
                return saturation;
            }
            set
            {
                if (saturation != value)
                {
                    Color = Color.FromHsla(hue, value, luminosity);
                }
            }
        }

        float luminosity;
        public float Luminosity
        {
            get
            {
                return luminosity;
            }
            set
            {
                if (luminosity != value)
                {
                    Color = Color.FromHsla(hue, saturation, value);
                }
            }
        }

        Color color;
        public Color Color
        {
            get
            {
                return color;
            }
            set
            {
                if (color != value)
                {
                    color = value;
                    hue = color.GetHue();
                    saturation = color.GetSaturation();
                    luminosity = color.GetLuminosity();
                    Notification(nameof(Hue));
                    Notification(nameof(Saturation));
                    Notification(nameof(Luminosity));
                    Notification(nameof(Color));

                    Name = color.ToString();//NamedColor.GetNearestColorName(color);
                }
            }
        }

        string name;
        public string Name
        {
            get
            {
                return name;
            }
            private set
            {
                if (name != value)
                {
                    name = value;
                    Notification("Name");
                }
            }
        }
    }
}
