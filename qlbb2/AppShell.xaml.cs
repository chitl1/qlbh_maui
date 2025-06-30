using Microsoft.Maui.Storage;
using qlbb2.Views;
using qlbb2.Helper;

namespace qlbb2
{
    public partial class AppShell : Shell
    {
        private ToolbarItem _logoutItem;
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(UserPage), typeof(UserPage));
            Routing.RegisterRoute(nameof(AddUserPage), typeof(AddUserPage));
            Routing.RegisterRoute(nameof(EditUserPage), typeof(EditUserPage));
            Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));
            // Tạo logout item (chưa thêm vào)
            _logoutItem = new ToolbarItem
            {
                Text = "Logout",
                Order = ToolbarItemOrder.Primary,
                Priority = 0
            };
            _logoutItem.Clicked += OnLogoutClicked;

            this.Navigated += AppShell_Navigated;
            var role = Preferences.Get("UserRole", string.Empty);
            if (role == "User")
            {
                var tabBar = this.Items.OfType<TabBar>().FirstOrDefault();
                if (tabBar != null)
                {
                    var userTab = tabBar.Items.FirstOrDefault(x => x.Title == "UserPage");
                    if (userTab != null)
                    {
                        tabBar.Items.Remove(userTab); // Xoá UserPage khỏi TabBar
                    }
                }
            }
        }
        private void AppShell_Navigated(object sender, ShellNavigatedEventArgs e)
        {
            bool isLoginPage = e.Current.Location.OriginalString.Contains("LoginPage", StringComparison.OrdinalIgnoreCase);

            if (!isLoginPage && !ToolbarItems.Contains(_logoutItem))
            {
                ToolbarItems.Add(_logoutItem); // Thêm nút Logout khi không ở LoginPage
            }
            else if (isLoginPage && ToolbarItems.Contains(_logoutItem))
            {
                ToolbarItems.Remove(_logoutItem); // Xoá nếu đang ở LoginPage
            }
        }

        private async void OnLogoutClicked(object sender, EventArgs e)
        {
            try
            {
                bool confirm = await Shell.Current.DisplayAlert("Xác nhận", "Bạn muốn đăng xuất?", "Đăng xuất", "Huỷ");
                if (confirm)
                {
                    Preferences.Remove("UserRole");
                    Preferences.Remove("UserName");
                    // Resolve LoginViewModel from DI
                    var loginPage = ServiceHelper.GetService<qlbb2.Views.LoginPage>();
                    //var loginVm = ServiceHelper.GetService<qlbb2.ViewModels.Login.LoginViewModel>();
                    Application.Current.MainPage = loginPage;
                }
            }
            catch (Exception ex)
            {
                // Xử lý lỗi nếu cần
                await Shell.Current.DisplayAlert("Lỗi", "Đăng xuất không thành công: " + ex.Message, "OK");
            }
        }
    }
}
