using qlbb2.ViewModels;
using qlbb2.ViewModels.Users;

namespace qlbb2.Views;

[QueryProperty(nameof(RefreshList), "refresh")]
public partial class UserPage : ContentPage
{
	private readonly UserViewModel _vm;
	private System.Timers.Timer _searchTimer;
	
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
        System.Diagnostics.Debug.WriteLine("UserPage constructor - BindingContext set");
        
        // Initialize search timer for debounced search
        _searchTimer = new System.Timers.Timer(500); // 500ms delay
        _searchTimer.Elapsed += async (sender, e) =>
        {
            await MainThread.InvokeOnMainThreadAsync(() =>
            {
                _vm.SearchUsers();
            });
        };
        _searchTimer.AutoReset = false;
    }
    
    private void OnSearchTextChanged(object sender, TextChangedEventArgs e)
    {
        // Reset timer to implement debounced search
        _searchTimer.Stop();
        _searchTimer.Start();
    }
    
    protected override void OnAppearing()
    {
        base.OnAppearing();
        System.Diagnostics.Debug.WriteLine("UserPage OnAppearing - Loading users");
        _vm.LoadUsers();
    }
    
    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
        System.Diagnostics.Debug.WriteLine("UserPage OnNavigatedTo - Loading users");
        _vm.LoadUsers();
    }
    
    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        _searchTimer?.Stop();
        _searchTimer?.Dispose();
    }
}
