using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using qlbb2.ViewModels.Login;
using qlbb2.ViewModels.Users;
using qlbb2.Views;
using qlbb2.Helper;
using qlbb2.ViewModels.Supplier;
using qlbb2.AppService.Services.Interface;
using qlbb2.AppService.Services;
using qlbb2.Infrastructure.Repositories.Interface;
using qlbb2.Infrastructure.Repositories;
using qlbb2.Infrastructure;
using qlbb2.ViewModels;


namespace qlbb2
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
    		builder.Logging.AddDebug();
#endif
            //var folder = FileSystem.Current.AppDataDirectory;
            var folderPath = @"D:\Hoc\maui\database\qlbb2";
            var dbPath = Path.Combine(folderPath, "app_qlbb2.db");
            var connectionString = $"Data Source={dbPath}";

            // Register the DbContext with the connection string
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlite(connectionString));

            builder.Services.AddTransient<IUserRepository, UserRepository>();
            builder.Services.AddTransient<IUserService, UserService>();
            builder.Services.AddTransient<ILoginRepository, LoginRepository>();
            builder.Services.AddTransient<ILoginService, LoginService>();
            builder.Services.AddTransient<ISupplierRepository, SupplierRepository>();
            builder.Services.AddTransient<ISupplierService, SupplierService>();

            // Register ViewModels and Views
            builder.Services.AddTransient<ShellViewModel>();
            builder.Services.AddTransient<UserViewModel>();
            builder.Services.AddTransient<AddUserViewModel>();
            builder.Services.AddTransient<LoginViewModel>();
            builder.Services.AddTransient<EditUserViewModel>();
            builder.Services.AddTransient<SupplierViewModel>();
            builder.Services.AddTransient<AddSupplierViewModel>();
            builder.Services.AddTransient<EditSupplierViewModel>();
            builder.Services.AddTransient<EditSupplierPage>();
            builder.Services.AddTransient<MainPage>();
            builder.Services.AddTransient<AddSupplierPage>();
            builder.Services.AddTransient<UserPage>();
            builder.Services.AddTransient<AddUserPage>();
            builder.Services.AddTransient<LoginPage>();
            builder.Services.AddTransient<EditUserPage>();
            builder.Services.AddTransient<SupplierPage>();
            builder.Services.AddTransient<AppShell>();

            builder.Services.AddSingleton(LocalizationResourceManager.Instance);
            var app = builder.Build();
            ServiceHelper.ServiceProvider = app.Services;
            return app;
        }
    }
}
