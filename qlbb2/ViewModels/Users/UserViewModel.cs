using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using qlbb2.Data.Entities;
using qlbb2.AppService.Services.Interface;
using System.Collections.ObjectModel;
using qlbb2.Views;
using System.Text.Json;
using System.Web;


namespace qlbb2.ViewModels.Users
{
    public partial class UserViewModel : ObservableObject
    {
        private readonly IUserService _userService;
        [ObservableProperty]
        private ObservableCollection<User> users = new();
        [ObservableProperty]
        private string searchText;
        [ObservableProperty]
        private string role;
        
        public UserViewModel(IUserService userService)
        {
            _userService = userService;
            LoadUsers();
        }
        [RelayCommand]
        private async Task RefreshUsers()
        {
            await LoadUsersAsync();
        }
        
        public async Task LoadUsersAsync()
        {
            try
            {
                System.Diagnostics.Debug.WriteLine("Loading users...");
                var userList = await _userService.GetDisplayUserAsync();
                System.Diagnostics.Debug.WriteLine($"Retrieved {userList.Count} users from service");
                
                Users = new ObservableCollection<User>(userList);
                System.Diagnostics.Debug.WriteLine($"Updated ObservableCollection with {Users.Count} users");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error loading users: {ex.Message}");
                await App.Current.MainPage.DisplayAlert("Error", $"Failed to load users: {ex.Message}", "OK");
            }
        }
        
        public async void LoadUsers()
        {
            await LoadUsersAsync();
        }

        //[RelayCommand]
        //private async void OnSearchTextChanged(string searchText)
        //{
        //    if (string.IsNullOrWhiteSpace(searchText))
        //    {
        //        LoadUsers();
        //    }
        //    else
        //    {
        //        SearchUsers();
        //    }
        //}
        [RelayCommand]
        private async void SearchUsers()
        {
            if (string.IsNullOrWhiteSpace(SearchText))
            {
                LoadUsers();
                return;
            }

            var filteredUsers = await _userService.SearchUsersAsync(SearchText);
            Users = new ObservableCollection<User>(filteredUsers);
        }
        [RelayCommand]
        private async void AddUser()
        {
            try
            {
                await Shell.Current.GoToAsync(nameof(AddUserPage));
                //await Shell.Current.GoToAsync($"//{nameof(AddUserPage)}");
            }
            catch (Exception ex)
            {
                // Handle exceptions, e.g., navigation issues
                await App.Current.MainPage.DisplayAlert("Error", $"An error occurred: {ex.Message}", "OK");
            
            }
            //await Shell.Current.GoToAsync(nameof(AddUserPopupPage));

        }
        [RelayCommand]
        private async void ClearSearch()
        {
            SearchText = string.Empty;
            LoadUsers();
        }
        [RelayCommand]
        private async Task DeleteUser(User user)
        {
            try
            {
                if (user == null) return;
                bool confirm = await App.Current.MainPage.DisplayAlert("Xác nhận", $"Bạn có chắc muốn xóa user {user.UserName}?", "Xóa", "Hủy");
                if (!confirm) return;
                await _userService.DeletePersonAsync(user.UserId);
                LoadUsers();
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Error", $"An error occurred while deleting the user: {ex.Message}", "OK");
            }
        }
        [RelayCommand]
        private async Task EditUser(User user)
        {
            try
            {
                if (user == null) return;
                var userJson = JsonSerializer.Serialize(user);
                var encodedUser = HttpUtility.UrlEncode(userJson);
                await Shell.Current.GoToAsync($"{nameof(EditUserPage)}?User={encodedUser}");
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Error", $"An error occurred while editing the user: {ex.Message}", "OK");
            }
        }
    }
}
