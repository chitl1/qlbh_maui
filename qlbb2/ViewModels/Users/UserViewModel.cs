﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using qlbb2.Data.Entities;
using qlbb2.AppService.Services.Interface;
using System.Collections.ObjectModel;
using qlbb2.Views;
using System.Text.Json;
using System.Web;
using qlbb2.Helper;
using System.Globalization;


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
        
        [ObservableProperty]
        private bool isLoading;
        
        public UserViewModel(IUserService userService)
        {
            _userService = userService;
            LoadUsers();
        }
        
        [RelayCommand]
        public async Task LoadUsersAsync()
        {
            try
            {
                IsLoading = true;
                System.Diagnostics.Debug.WriteLine("Loading users...");
                var userList = await _userService.GetDisplayUserAsync();
                System.Diagnostics.Debug.WriteLine($"Retrieved {userList.Count} users from service");
                
                // Clear and add items to ensure UI updates
                Users.Clear();
                foreach (var user in userList)
                {
                    Users.Add(user);
                }
                
                System.Diagnostics.Debug.WriteLine($"Updated ObservableCollection with {Users.Count} users");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error loading users: {ex.Message}");
                await App.Current.MainPage.DisplayAlert("Error", $"Failed to load users: {ex.Message}", "OK");
            }
            finally
            {
                IsLoading = false;
            }
        }
        
        public async void LoadUsers()
        {
            await LoadUsersAsync();
        }
        
        // Public method for debounced search
        public async void SearchUsers()
        {
            await SearchUsersAsync();
        }
        
        [RelayCommand]
        private async Task SearchUsersAsync()
        {
            if (string.IsNullOrWhiteSpace(SearchText))
            {
                LoadUsers();
                return;
            }

            try
            {
                IsLoading = true;
                System.Diagnostics.Debug.WriteLine($"Searching for: {SearchText}");
                var filteredUsers = await _userService.SearchUsersAsync(SearchText);
                
                // Clear and add items to ensure UI updates
                Users.Clear();
                foreach (var user in filteredUsers)
                {
                    Users.Add(user);
                }
                
                System.Diagnostics.Debug.WriteLine($"Found {filteredUsers.Count} users matching '{SearchText}'");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error searching users: {ex.Message}");
                await App.Current.MainPage.DisplayAlert("Error", $"Failed to search users: {ex.Message}", "OK");
            }
            finally
            {
                IsLoading = false;
            }
        }
        
        [RelayCommand]
        private async void AddUser()
        {
            try
            {
                await Shell.Current.GoToAsync(nameof(AddUserPage));
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Error", $"An error occurred: {ex.Message}", "OK");
            }
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
