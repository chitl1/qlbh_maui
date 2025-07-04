using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using qlbb2.Data.Entities;
using qlbb2.AppService.Services.Interface;

namespace qlbb2.ViewModels.Supplier
{
    public partial class EditSupplierViewModel : ObservableObject
    {
        private readonly ISupplierService _supplierService;
        [ObservableProperty]
        private int supplierId;
        [ObservableProperty]
        private string supplierName;
        [ObservableProperty]
        private string phone;
        [ObservableProperty]
        private string email;
        [ObservableProperty]
        private string address;

        public EditSupplierViewModel(ISupplierService supplierService)
        {
            _supplierService = supplierService;
        }

        public void Initialize(TblSupplier supplierItem)
        {
            if (supplierItem != null)
            {
                SupplierId = supplierItem.SupplierId;
                SupplierName = supplierItem.SupplierName;
                Email = supplierItem.Email;
                Phone = supplierItem.Phone;
                Address = supplierItem.Address;
            }
        }

        [RelayCommand]
        private async void Save()
        {
            if (string.IsNullOrWhiteSpace(SupplierName) || string.IsNullOrWhiteSpace(Phone) || string.IsNullOrWhiteSpace(Email) || string.IsNullOrWhiteSpace(Address))
            {
                await App.Current.MainPage.DisplayAlert("Error", "All fields are required", "OK");
                return;
            }

            try
            {
                var supplierInDb = await _supplierService.GetByIdAsync(SupplierId); // phải có hàm này
                if (supplierInDb == null)
                {
                    await App.Current.MainPage.DisplayAlert("Error", "Supplier not found", "OK");
                    return;
                }

                supplierInDb.SupplierName = SupplierName;
                supplierInDb.Phone = Phone;
                supplierInDb.Email = Email;
                supplierInDb.Address = Address;

                await _supplierService.UpdateAsync(supplierInDb);
                await App.Current.MainPage.DisplayAlert("Success", "Supplier updated successfully!", "OK");
                await Shell.Current.GoToAsync("..", true);
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Error", $"Failed to update supplier: {ex.Message}", "OK");
            }
        }

    }
}
