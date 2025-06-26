using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using qlbb2.Entities;
using qlbb2.Services;
using System.Collections.ObjectModel;
using qlbb2.Views;

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
        public async void LoadUsers()
        {
            var userList = await _userService.GetDisplayUserAsync();
            Users = new ObservableCollection<User>(userList);
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
        private async void DeleteUser(User user)
        {
            if (user == null) return;

            bool confirm = await App.Current.MainPage.DisplayAlert("Confirm", $"Are you sure you want to delete user {user.UserName}?", "Yes", "No");
            if (confirm)
            {
                //try
                //{
                //    await _userService.DeleteUserAsync(user.Id);
                //    Users.Remove(user);
                //    await App.Current.MainPage.DisplayAlert("Success", "User deleted successfully.", "OK");
                //}
                //catch (Exception ex)
                //{
                //    await App.Current.MainPage.DisplayAlert("Error", $"An error occurred while deleting the user: {ex.Message}", "OK");
                //}
            }
        }
        [RelayCommand]
        private async void EditUser(User user)
        {
            //if (user == null) return;

            //try
            //{
            //    await Shell.Current.GoToAsync($"{nameof(EditUserPage)}?userId={user.Id}");
            //}
            //catch (Exception ex)
            //{
            //    await App.Current.MainPage.DisplayAlert("Error", $"An error occurred while editing the user: {ex.Message}", "OK");
            //}
        }
    }
}
