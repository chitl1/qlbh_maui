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
            //await Shell.Current.GoToAsync(nameof(AddUserPopupPage));
            await Shell.Current.GoToAsync($"//{nameof(AddUserPage)}");
        }
        [RelayCommand]
        private async void ClearSearch()
        {
            SearchText = string.Empty;
            LoadUsers();
        }
    }
}
