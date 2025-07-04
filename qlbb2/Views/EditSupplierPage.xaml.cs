using qlbb2.Data.Entities;
using System.Text.Json;
using System.Web;
using qlbb2.ViewModels.Supplier;

namespace qlbb2.Views;

[QueryProperty(nameof(Supplier), "Supplier")]
public partial class EditSupplierPage : ContentPage
{
    private readonly EditSupplierViewModel _vm;
    private string _supplier;
    public string Supplier
    {
        get => _supplier;
        set
        {
            _supplier = value;
            OnSupplierChanged(value);
        }
    }
    public EditSupplierPage(EditSupplierViewModel vm)
    {
        InitializeComponent();
        _vm = vm;
        BindingContext = _vm;
    }
    private void OnSupplierChanged(string value)
    {
        if (!string.IsNullOrEmpty(value))
        {
            try
            {
                var decodedSupplier = HttpUtility.UrlDecode(value);
                var supplier = JsonSerializer.Deserialize<TblSupplier>(decodedSupplier);
                if (supplier != null)
                {
                    _vm.Initialize(supplier);
                }
            }
            catch (Exception ex)
            {
                App.Current.MainPage.DisplayAlert("Error", $"Failed to load supplier data: {ex.Message}", "OK");
            }
        }
    }
}