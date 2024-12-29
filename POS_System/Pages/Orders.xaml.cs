using POS_System.ViewModels;

namespace POS_System.Pages;

public partial class Orders : ContentPage
{
	public Orders(OrderViewModel orderViewModel)
	{
		BindingContext = orderViewModel;
		InitializeComponent();
	}
}