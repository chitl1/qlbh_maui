using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using qlbb2.AppService.Services;
using qlbb2.AppService.Services.Interface;
using qlbb2.Data.Entities;
using qlbb2.Views;
using System.Collections.ObjectModel;
using System.Text.Json;
using System.Web;

namespace qlbb2.ViewModels.Supplier
{
    public partial class SupplierViewModel : ObservableObject
    {
        private readonly ISupplierService _supplierService;
        [ObservableProperty]
        private List<TblSupplier> _suppliers;
        [ObservableProperty]
        private string _searchText;
        public SupplierViewModel(ISupplierService supplierService)
        {
            _supplierService = supplierService ?? throw new ArgumentNullException(nameof(supplierService));
            LoadSuppliersAsync();
        }
        public async void LoadSuppliersAsync()
        {
            try
            {
                Suppliers = await _supplierService.GetAllAsync();
            }
            catch (Exception ex)
            {
                // Handle exceptions, e.g., log them or show a message to the user
                Console.WriteLine($"Error loading suppliers: {ex.Message}");
            }
        }
        [RelayCommand]
        private async void AddSupplier()
        {
            // Logic to add a new supplier
            // This could open a new page or display a popup for adding a supplier
            // For example:
            // await Shell.Current.GoToAsync("///AddSupplierPage");
            try
            {
                await Shell.Current.GoToAsync(nameof(AddSupplierPage));
            }
            catch (Exception ex)
            {
                // Handle exceptions, e.g., log them or show a message to the user
                Console.WriteLine($"Error navigating to AddSupplierPage: {ex.Message}");
            }
        }
        [RelayCommand]
        private async void SearchSupplier()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(SearchText))
                {
                    LoadSuppliersAsync();
                }

                var filteredSuppliers = await _supplierService.SearchAsync(SearchText);
                Suppliers = new List<TblSupplier>(filteredSuppliers);

            }
            catch (Exception ex)
            {
                // Handle exceptions, e.g., log them or show a message to the user
                Console.WriteLine($"Error searching suppliers: {ex.Message}");
            }
        }
        [RelayCommand]
        private async void ClearSearch()
        {
            SearchText = string.Empty;
            LoadSuppliersAsync();
        }

        [RelayCommand]
        private async void EditSupplier(TblSupplier supplier)
        {
            try
            {
                if (supplier == null) return;
                var supplierJson = JsonSerializer.Serialize(supplier);
                var encodedSupplier = HttpUtility.UrlEncode(supplierJson);
                await Shell.Current.GoToAsync($"{nameof(EditSupplierPage)}?Supplier={encodedSupplier}");
            }
            catch (Exception ex)
            {
                // Handle exceptions, e.g., log them or show a message to the user
                Console.WriteLine($"Error navigating to EditSupplierPage: {ex.Message}");
            }
        }
        [RelayCommand]
        private async void DeleteSupplier(TblSupplier supplier)
        {
            try
            {
                if (supplier == null) return;
                bool confirm = await App.Current.MainPage.DisplayAlert("Confirm Delete", "Are you sure you want to delete this supplier?", "Yes", "No");
                if (!confirm) return;
                await _supplierService.DeleteAsync(supplier.SupplierId);
                LoadSuppliersAsync();
            }
            catch (Exception ex)
            {
                // Handle exceptions, e.g., log them or show a message to the user
                Console.WriteLine($"Error deleting supplier: {ex.Message}");
            }
        }
    }
}
