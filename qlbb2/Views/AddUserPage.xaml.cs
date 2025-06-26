using qlbb2.ViewModels.Users;
namespace qlbb2.Views;

public partial class AddUserPage : ContentPage
{
	private readonly AddUserViewModel _viewModel;
	public AddUserPage(AddUserViewModel vm)
	{
		InitializeComponent();
		_viewModel = vm;
		BindingContext = _viewModel;
    }
}