using SingInWorkoutNoam.ViewModels;

namespace SingInWorkoutNoam.Views;

public partial class MainPage1 : ContentPage
{
	public MainPage1()
	{
		InitializeComponent();
        BindingContext = new MainPage1ViewModel();
    }
}