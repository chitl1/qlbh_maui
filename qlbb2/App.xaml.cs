using qlbb2.Views;
using qlbb2.ViewModels.Login;
using qlbb2.Helper;
using System.Globalization;

namespace qlbb2
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            // Đặt ngôn ngữ mặc định là tiếng Anh
            LocalizationResourceManager.Instance.SetCulture(new CultureInfo("en"));

            // Kiểm tra trạng thái đăng nhập
            var role = Preferences.Get("UserRole", string.Empty);
            if (string.IsNullOrEmpty(role))
            {
                // Resolve LoginViewModel from DI
                //var loginVm = ServiceHelper.GetService<LoginViewModel>();
                var loginPage = ServiceHelper.GetService<LoginPage>();
                MainPage = loginPage;
            }
            else
            {
                // Resolve AppShell from DI to satisfy constructor dependency
                MainPage = ServiceHelper.GetService<AppShell>();
            }
        }
    }

    //public static class ServiceHelper
    //{
    //    public static IServiceProvider ServiceProvider { get; set; }
    //    public static T GetService<T>() => ServiceProvider.GetService<T>();
    //}
}
