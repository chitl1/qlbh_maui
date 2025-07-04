using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using qlbb2.Data.Entities;
using qlbb2.AppService.Services.Interface;
using System.ComponentModel.DataAnnotations;
using qlbb2.Data.Views;

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
            //supplierInfoObj = new SupplierInfo
            //{
            //    SupplierName = SupplierName,
            //    Email = Email,
            //    Phone = Phone,
            //    Address = Address
            //};
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

                TblSupplier supplierInfo = new TblSupplier
                {
                    SupplierId = SupplierId,
                    SupplierName = SupplierName,
                    Phone = Phone,
                    Email = Email,
                    Address = Address
                };
                if (await ValidateAsync(supplierInfo))
                {
                    await _supplierService.UpdateAsync(supplierInDb);
                    await App.Current.MainPage.DisplayAlert("Success", "Supplier updated successfully!", "OK");
                    await Shell.Current.GoToAsync("..", true);
                }

            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Error", $"Failed to update supplier: {ex.Message}", "OK");
            }
        }
        public async Task<bool> ValidateAsync(TblSupplier supplier)
        {
            var context = new ValidationContext(supplier, null, null);
            var results = new List<ValidationResult>();
            bool isValid = Validator.TryValidateObject(supplier, context, results, true);

            if (!isValid)
            {
                // Hiển thị lỗi đầu tiên (hoặc tổng hợp lỗi)
                await App.Current.MainPage.DisplayAlert("Lỗi", string.Join("\n", results.Select(r => r.ErrorMessage)), "OK");
            }
            return isValid;
        }
    }
}
