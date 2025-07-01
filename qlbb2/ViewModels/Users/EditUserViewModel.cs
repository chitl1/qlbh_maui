using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using qlbb2.Data.Entities;
using qlbb2.AppService.Services.Interface;
using System.Collections.ObjectModel;

namespace qlbb2.ViewModels.Users
{
    public partial class EditUserViewModel : ObservableObject
    {
        private readonly IUserService _userService;
        
        [ObservableProperty]
        private int userId;
        
        [ObservableProperty]
        private string userName;
        
        [ObservableProperty]
        private string password;
        
        [ObservableProperty]
        private string selectedRole;
        
        [ObservableProperty]
        private ObservableCollection<string> roles = new ObservableCollection<string>
        {
            "Admin",
            "User",
        };

        public EditUserViewModel(IUserService userService)
        {
            _userService = userService;
        }

        // Method to receive navigation parameters
        public void Initialize(User user)
        {
            if (user != null)
            {
                UserId = user.UserId;
                UserName = user.UserName;
                Password = user.Password;
                SelectedRole = user.Role;
            }
        }

        [RelayCommand]
        private async void UpdateUser()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(UserName) || string.IsNullOrWhiteSpace(Password) || string.IsNullOrWhiteSpace(SelectedRole))
                {
                    await App.Current.MainPage.DisplayAlert("Error", "All fields are required", "OK");
                    return;
                }

                var updatedUser = new User
                {
                    UserId = UserId,
                    UserName = UserName,
                    Password = Password,
                    Role = SelectedRole,
                };

                await _userService.UpdatePersonAsync(updatedUser);
                await App.Current.MainPage.DisplayAlert("Success", "User updated successfully", "OK");
                
                // Navigate back to UserPage with refresh parameter
                await Shell.Current.GoToAsync("///UserPage?refresh=true");
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }
        }

        [RelayCommand]
        private async void Cancel()
        {
            try
            {
                await Shell.Current.GoToAsync("..");
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }
        }
    }
}
