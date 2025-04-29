using Mobile.ViewModels;
using The49.Maui.BottomSheet;

namespace Mobile.Pages;

public partial class CartSheet : BottomSheet
{
    public CartSheet(CheckoutViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}