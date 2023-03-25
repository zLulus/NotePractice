using Maui.Demo.Pages.Layouts.ViewModels;
using System.Collections.ObjectModel;

namespace Maui.Demo.Pages.Layouts;

public partial class BindableLayoutPage : ContentPage
{
	public BindableLayoutPage()
	{
		InitializeComponent();

		ObservableCollection<User> users = new ObservableCollection<User>();
		users.Add(new User()
		{
			Name = "user 1",
			ImagePath = "cat.png"
		});
		users.Add(new User()
		{
			Name = "user 2",
			ImagePath = "cat.png"
		});
		userList.BindingContext = users;
	}
}