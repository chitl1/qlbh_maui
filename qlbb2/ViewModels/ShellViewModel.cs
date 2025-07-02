using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.DependencyInjection;
using CommunityToolkit.Mvvm.Input;
using qlbb2.Helper;
using System.Collections.ObjectModel;
using System.Globalization;

namespace qlbb2.ViewModels
{
    public partial class ShellViewModel : ObservableObject
    {
        private readonly LocalizationResourceManager _loc;
        public ObservableCollection<string> Languages { get; } = new()
        {
            "English", "Tiếng Việt"
        };

        [ObservableProperty]
        private string selectedLanguage;
        public ShellViewModel(LocalizationResourceManager loc)
        {
            _loc = loc;
            SelectedLanguage = Languages.First();
        }
        partial void OnSelectedLanguageChanged(string value)
        {
            if (value == "Tiếng Việt")
                _loc.SetCulture(new CultureInfo("vi"));
            else
                _loc.SetCulture(new CultureInfo("en"));
        }
        [RelayCommand]
        private async Task Logout()
        {
            try
            {
                bool confirm = await Shell.Current.DisplayAlert("Xác nhận", "Bạn muốn đăng xuất?", "Đăng xuất", "Huỷ");
                if (confirm)
                {
                    Preferences.Remove("UserRole");
                    Preferences.Remove("UserName");
                    var loginPage = ServiceHelper.GetService<qlbb2.Views.LoginPage>();
                    Application.Current.MainPage = loginPage;
                }
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Lỗi", "Đăng xuất không thành công: " + ex.Message, "OK");
            }
        }
    }
}
