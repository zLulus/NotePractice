using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinDemo.Bindings.Models;

namespace XamarinDemo.PopUps
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PopUpsDemo : ContentPage
	{
        //参考资料：https://docs.microsoft.com/en-us/xamarin/xamarin-forms/app-fundamentals/navigation/pop-ups
        public PopUpsDemo()
		{
			InitializeComponent ();
        }

        public void DisplayAlertClicked(object sender, EventArgs e)
        {
            DisplayAlert("Alert", "You have been alerted", "OK");
        }

        public async void OnAlertYesNoClicked(object sender, EventArgs e)
        {
            var answer = await DisplayAlert("Question?", "Would you like to play a game", "Yes", "No");
            await DisplayAlert("Answer", answer ? "Yes" : "No", "OK");
        }

        async void OnActionSheetSimpleClicked(object sender, EventArgs e)
        {
            var action = await DisplayActionSheet("ActionSheet: Send to?", "Cancel", null, "Email", "Twitter", "Facebook");
            await DisplayAlert("Answer", action, "OK");
        }

        async void OnActionSheetCancelDeleteClicked(object sender, EventArgs e)
        {
            var action = await DisplayActionSheet("ActionSheet: SavePhoto?", "Cancel", "Delete", "Photo Roll", "Email");
            await DisplayAlert("Answer", action, "OK");
        }
    }
}