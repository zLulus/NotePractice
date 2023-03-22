namespace Maui.Demo.Pages.Bindings.ViewModels
{
    public class HslColorViewModel : BaseNotifyPropertyChanged
    {
        Color color;
        string name;
        float hue;
        float saturation;
        float luminosity;

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
                    Notification("Hue");
                    Notification("Saturation");
                    Notification("Luminosity");
                    Notification("Color");

                    Name = color.ToString();//NamedColor.GetNearestColorName(color);
                }
            }
        }

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
