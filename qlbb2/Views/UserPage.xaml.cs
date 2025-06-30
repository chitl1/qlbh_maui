using qlbb2.ViewModels;
using qlbb2.ViewModels.Users;

namespace qlbb2.Views;

[QueryProperty(nameof(RefreshList), "refresh")]
public partial class UserPage : ContentPage
{
	private readonly UserViewModel _vm;
	
	private string _refreshList;
	public string RefreshList
	{
		get => _refreshList;
		set
		{
			_refreshList = value;
			if (value == "true")
			{
				// Force refresh when coming back from edit
				_vm.LoadUsers();
			}
		}
	}
	
	public UserPage(UserViewModel vm)
	{
		InitializeComponent();
		_vm = vm;
        BindingContext = _vm;
    }
    
    protected override void OnAppearing()
    {
        base.OnAppearing();
        _vm.LoadUsers();
    }
    
    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
        _vm.LoadUsers();
    }
}
