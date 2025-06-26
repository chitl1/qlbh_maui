using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using qlbb2.Entities;
using qlbb2.Services;
using qlbb2.Views;
using System.Collections.ObjectModel;

namespace qlbb2.ViewModels.Users
{
    public partial class AddUserViewModel : ObservableObject
    {
        private readonly IUserService _userService;
        [ObservableProperty]
        private string userName;
        [ObservableProperty]
        private string password;
        //[ObservableProperty]
        //private string role;
        [ObservableProperty]
        private ObservableCollection<string> roles = new ObservableCollection<string>
        {
            "Admin",
            "User",
        };
        [ObservableProperty]
        private string selectedRole;
        public AddUserViewModel(IUserService userService)
        {
            _userService = userService;
        }
        [RelayCommand]
        private async void AddUser()
        {
            if (string.IsNullOrWhiteSpace(UserName) || string.IsNullOrWhiteSpace(Password) || string.IsNullOrWhiteSpace(SelectedRole))
            {
                await App.Current.MainPage.DisplayAlert("Error", "All fields are required", "OK");
                return;
            }

            // Here you would typically call a service to add the user
            var user = new User
            {
                UserName = UserName,
                Password = Password,
                Role = SelectedRole,
            };
            await _userService.AddPersonAsync(user);

            // Clear fields after adding
            UserName = string.Empty;
            Password = string.Empty;
            SelectedRole = string.Empty;

            // Optionally notify the main page or close the popup
            await App.Current.MainPage.DisplayAlert("Success", "User added successfully", "OK");
            await Shell.Current.GoToAsync("///UserPage");


        }
        [RelayCommand]
        private async void Cancel()
        {
            // Logic to close the popup or reset fields
            try
            {
                UserName = string.Empty;
                Password = string.Empty;
                SelectedRole = string.Empty;
                //await Shell.Current.GoToAsync(nameof(UserPage));
                await Shell.Current.GoToAsync("///UserPage");
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }

        }

    }
}
