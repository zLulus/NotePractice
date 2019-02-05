using Android.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinDemo.Parameters.Models;

namespace XamarinDemo.Parameters
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SetParameter : ContentPage
	{
		public SetParameter ()
		{
			InitializeComponent ();
            BindingContext = new SetParameterViewModel()
            {
                Data1 ="Test"
            };
        }

        public void GoToGetParameter(object sender, EventArgs e)
        {
            Intent intent = new Intent();
            var data = BindingContext as SetParameterViewModel;
            intent.PutExtra("Data1", data.Data1);
            
            Navigation.PushModalAsync(new GetParameter());
        }

        public void GoToGetParameter2(object sender, EventArgs e)
        {
            var data = BindingContext as SetParameterViewModel;
            Navigation.PushModalAsync(new GetParameter(data.Data1));
        }

    }
}