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
        //MessagingCenter.Subscribe<AddUserViewModel>(this, "UserAdded", (sender) =>
        //{
        //    _vm.LoadUsers();
        //});
    }
    //protected override void OnDisappearing()
    //{
    //    base.OnDisappearing();
    //    MessagingCenter.Unsubscribe<AddUserViewModel>(this, "UserAdded");
    //}
    protected override void OnAppearing()
    {
        base.OnAppearing();
        _vm.LoadUsers();
    }
}
