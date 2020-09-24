using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinDemo.PhoneCalls.Interfaces;
using XamarinDemo.PhoneCalls.Models;

namespace XamarinDemo.PhoneCalls
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PhoneCall : ContentPage
	{
		public PhoneCall()
		{
			InitializeComponent ();
            BindingContext = new PhoneCallViewModel()
            {
                PhoneNumber ="",
            };
        }

        public void GiveAPhoneCall(object sender, EventArgs e)
        {
            var data = BindingContext as PhoneCallViewModel;
            if (string.IsNullOrEmpty(data.PhoneNumber))
            {
                DisplayAlert("提示", "请输入手机号码", "确定");
            }
            else
            {
                DependencyService.Get<IPhoneCall>().GiveAPhoneCall(data.PhoneNumber);
            }
        }

    }
}