using qlbb2.ViewModels;
using qlbb2.ViewModels.Users;

namespace qlbb2.Views;

public partial class UserPage : ContentPage
{
	private readonly UserViewModel _vm;
	public UserPage(UserViewModel vm)
	{
		InitializeComponent();
		_vm = vm;
        BindingContext = _vm;
    }
}