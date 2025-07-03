using qlbb2.ViewModels.Supplier;

namespace qlbb2.Views;

public partial class SupplierPage : ContentPage
{
	private readonly SupplierViewModel _vm;
	public SupplierPage(SupplierViewModel vm)
	{
		InitializeComponent();
		_vm = vm;
		BindingContext = _vm;
	}
    protected override void OnAppearing()
    {
        base.OnAppearing();
        _vm.LoadSuppliersAsync();
    }
}