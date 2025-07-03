using Microsoft.Maui.Storage;
using qlbb2.Views;
using qlbb2.Helper;
using System.Globalization;
using qlbb2.ViewModels;

namespace qlbb2
{
    public partial class AppShell : Shell
    {
        private readonly ShellViewModel _vm;
        public AppShell(ShellViewModel vm)
        {
            _vm = vm;
            BindingContext = _vm;
            InitializeComponent();
            Routing.RegisterRoute(nameof(UserPage), typeof(UserPage));
            Routing.RegisterRoute(nameof(AddUserPage), typeof(AddUserPage));
            Routing.RegisterRoute(nameof(EditUserPage), typeof(EditUserPage));
            Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));
            Routing.RegisterRoute(nameof(SupplierPage), typeof(SupplierPage));
            Routing.RegisterRoute(nameof(AddSupplierPage), typeof(AddSupplierPage));
        }
    }
}
