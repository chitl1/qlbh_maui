using CommunityToolkit.Mvvm.ComponentModel;
using qlbb2.AppService.Services.Interface;
using qlbb2.Data.Entities;



namespace qlbb2.ViewModels.Supplier
{
    public partial class SupplierViewModel : ObservableObject
    {
        private readonly ISupplierService _supplierService;
        [ObservableProperty]
        private List<TblSupplier> _suppliers;
        public SupplierViewModel(ISupplierService supplierService)
        {
            _supplierService = supplierService ?? throw new ArgumentNullException(nameof(supplierService));
            LoadSuppliersAsync();
        }
        private async void LoadSuppliersAsync()
        {
            try
            {
                Suppliers = await _supplierService.GetAllSuppliersAsync();
            }
            catch (Exception ex)
            {
                // Handle exceptions, e.g., log them or show a message to the user
                Console.WriteLine($"Error loading suppliers: {ex.Message}");
            }
        }
    }
}
