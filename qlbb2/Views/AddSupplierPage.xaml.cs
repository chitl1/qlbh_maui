using qlbb2.ViewModels.Supplier;

namespace qlbb2.Views;

public partial class AddSupplierPage : ContentPage
{
	private readonly AddSupplierViewModel _vm;
	public AddSupplierPage(AddSupplierViewModel vm)
	{
		InitializeComponent();
        _vm = vm;
		BindingContext = _vm;
    }
}