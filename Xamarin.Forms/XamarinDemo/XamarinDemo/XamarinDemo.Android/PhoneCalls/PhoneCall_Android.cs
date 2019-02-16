using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Forms;
using XamarinDemo.Droid.PhoneCalls;
using XamarinDemo.PhoneCalls.Interfaces;

[assembly:Dependency(typeof(PhoneCall_Android))]
namespace XamarinDemo.Droid.PhoneCalls
{
    public class PhoneCall_Android : IPhoneCall
    {
        public void GiveAPhoneCall(string phoneNumber)
        {
            //注意开启打电话的相关权限
            Android.Net.Uri.Parse
        }
    }
}