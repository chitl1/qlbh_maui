using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using qlbb2.AppService.Services;
using qlbb2.AppService.Services.Interface;
using qlbb2.Data.Entities;


namespace qlbb2.ViewModels.Supplier
{
    public partial class AddSupplierViewModel : ObservableObject
    {
        private readonly ISupplierService _supplierService;
        [ObservableProperty]
        private string supplierName;

        [ObservableProperty]
        private string phone;

        [ObservableProperty]
        private string email;

        [ObservableProperty]
        private string address;

        public AddSupplierViewModel(ISupplierService supplierService)
        {
            _supplierService = supplierService;
        }

        [RelayCommand]
        private async void AddSupplier()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(SupplierName) || string.IsNullOrWhiteSpace(Phone) || string.IsNullOrWhiteSpace(Email) || string.IsNullOrWhiteSpace(Address))
                {
                    await App.Current.MainPage.DisplayAlert("Error", "All fields are required", "OK");
                    return;
                }

                // Here you would typically call a service to add the supplier
                // For example:
                var supplier = new TblSupplier
                {
                    SupplierName = SupplierName,
                    Phone = Phone,
                    Email = Email,
                    Address = Address,
                };
                await _supplierService.AddAsync(supplier);

                // Clear fields after adding
                SupplierName = string.Empty;
                Phone = string.Empty;
                Email = string.Empty;
                Address = string.Empty;

                // Optionally notify the main page or close the popup
                await App.Current.MainPage.DisplayAlert("Success", "Supplier added successfully", "OK");
                await Shell.Current.GoToAsync("///SupplierPage");
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Error", $"Failed to add supplier: {ex.Message}", "OK");
            }
        }

        [RelayCommand]
        private async void Cancel()
        {
            // Logic to close the popup or reset fields
            try
            {
                SupplierName = string.Empty;
                Phone = string.Empty;
                Email = string.Empty;
                Address = string.Empty;
                await Shell.Current.GoToAsync("///SupplierPage");
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Error", $"Failed to cancel: {ex.Message}", "OK");
            }
        }
    }
}
