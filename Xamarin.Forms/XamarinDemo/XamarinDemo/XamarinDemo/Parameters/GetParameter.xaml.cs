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
	public partial class GetParameter : ContentPage
	{
		public GetParameter ()
		{
			InitializeComponent ();
            Intent intent = new Intent("ParameterTest");
            string Data1 = intent.GetStringExtra("Data1");
            BindingContext = new SetParameterViewModel()
            {
                Data1 = Data1
            };
        }

        public GetParameter(string data1)
        {
            InitializeComponent();
            BindingContext = new SetParameterViewModel()
            {
                Data1 = data1
            };
        }
    }
}