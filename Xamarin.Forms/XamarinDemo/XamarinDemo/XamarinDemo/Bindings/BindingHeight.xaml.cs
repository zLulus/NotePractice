using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinDemo.Bindings.Models;

namespace XamarinDemo.Bindings
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class BindingHeight : ContentPage
	{
		public BindingHeight ()
		{
			InitializeComponent ();
            BindingContext = new HeightViewModel()
            {
                CustomHeight =200,
                GridLength=new GridLength(200.00)
            };
        }
	}
}