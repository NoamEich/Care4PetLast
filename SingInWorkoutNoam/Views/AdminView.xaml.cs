using SingInWorkoutNoam.ViewModels;

namespace SingInWorkoutNoam.Views;

public partial class AdminView : ContentPage
{
	public AdminView(AdminViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}