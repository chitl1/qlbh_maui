using qlbb2.Views;

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
            // Tạo logout item (chưa thêm vào)
            _logoutItem = new ToolbarItem
            {
                Text = "Logout",
                Order = ToolbarItemOrder.Primary,
                Priority = 0
            };
            _logoutItem.Clicked += OnLogoutClicked;

            this.Navigated += AppShell_Navigated;
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
                    await Shell.Current.GoToAsync("//LoginPage");
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
