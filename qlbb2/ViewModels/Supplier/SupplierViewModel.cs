using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using qlbb2.AppService.Services;
using qlbb2.AppService.Services.Interface;
using qlbb2.Data.Entities;
using qlbb2.Views;
using System.Collections.ObjectModel;



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
            if (string.IsNullOrWhiteSpace(SearchText))
            {
                LoadSuppliersAsync();
            }

            var filteredSuppliers = await _supplierService.SearchAsync(SearchText);
            Suppliers = new List<TblSupplier>(filteredSuppliers);
        }
        [RelayCommand]
        private async void ClearSearch()
        {
            SearchText = string.Empty;
            LoadSuppliersAsync();
        }

    }
}
