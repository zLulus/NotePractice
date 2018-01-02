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
	public partial class BindingFirstName2 : ContentPage
	{
		public BindingFirstName2 ()
		{
			InitializeComponent ();
            BindingContext = new DetailsViewModel();
        }
	}
}