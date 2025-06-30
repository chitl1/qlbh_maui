using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using qlbb2.Model;
using qlbb2.Services;
using qlbb2.Views;

namespace qlbb2.ViewModels.Login
{
    public partial class LoginViewModel : ObservableObject
    {
        private readonly ILoginService _loginService;
        [ObservableProperty]
        private string _username;
        [ObservableProperty]
        private string _password;
        public LoginViewModel(ILoginService loginService) {
            _loginService = loginService;
        }
        [RelayCommand]
        private async void Login()
        {
            try
            {
                // Simulate a login operation
                if (string.IsNullOrWhiteSpace(Username) || string.IsNullOrWhiteSpace(Password))
                {
                    await App.Current.MainPage.DisplayAlert("Error", "Username and password cannot be empty.", "OK");
                    return;
                }

                // Here you would typically call a service to perform the login
                // For now, we just simulate a successful login
                UserInfo result = await _loginService.LoginAsync(Username, Password);
                if (result == null)
                {
                    await App.Current.MainPage.DisplayAlert("Error", "Invalid username or password.", "OK");
                    return;
                }
                // Lưu role và username vào Preferences
                Preferences.Set("UserRole", result.Role);
                Preferences.Set("UserName", result.UserName);

                await App.Current.MainPage.DisplayAlert("Success", $"Welcome {Username}!", "OK");
                Application.Current.MainPage = new AppShell();

            }
            catch (Exception ex)
            {
                // Handle exceptions, e.g., network issues
                await App.Current.MainPage.DisplayAlert("Error", $"An error occurred: {ex.Message}", "OK");
            }
        }
    }
}
