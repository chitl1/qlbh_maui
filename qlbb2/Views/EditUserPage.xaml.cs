using qlbb2.ViewModels.Users;
using qlbb2.Entities;
using System.Text.Json;
using System.Web;

namespace qlbb2.Views;

[QueryProperty(nameof(UserJson), "User")]
public partial class EditUserPage : ContentPage
{
	private readonly EditUserViewModel _vm;
	
	private string _userJson;
	public string UserJson
	{
		get => _userJson;
		set
		{
			_userJson = value;
			OnUserJsonChanged(value);
		}
	}
	
	public EditUserPage(EditUserViewModel vm)
	{
		InitializeComponent();
		_vm = vm;
		BindingContext = _vm;
	}

	private void OnUserJsonChanged(string value)
	{
		if (!string.IsNullOrEmpty(value))
		{
			try
			{
				var decodedUser = HttpUtility.UrlDecode(value);
				var user = JsonSerializer.Deserialize<User>(decodedUser);
				if (user != null)
				{
					_vm.Initialize(user);
				}
			}
			catch (Exception ex)
			{
				App.Current.MainPage.DisplayAlert("Error", $"Failed to load user data: {ex.Message}", "OK");
			}
		}
	}
}