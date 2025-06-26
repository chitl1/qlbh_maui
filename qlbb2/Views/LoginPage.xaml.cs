using qlbb2.ViewModels.Login;

namespace qlbb2.Views;

public partial class LoginPage : ContentPage
{
	private readonly LoginViewModel _viewModel;
	public LoginPage(LoginViewModel vm)
	{
		InitializeComponent();
		_viewModel = vm;
		BindingContext = _viewModel;
	}
}