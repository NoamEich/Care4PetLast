using SingInWorkoutNoam.Helper;
using SingInWorkoutNoam.Models;
using SingInWorkoutNoam.Service;
using SingInWorkoutNoam.ViewModels;
using static System.Net.Mime.MediaTypeNames;

namespace SingInWorkoutNoam.Views;

public partial class SignInPage : ContentPage
{
    private object vm;

    public SignInPage(SignInViewModel vm)
	{
		InitializeComponent();
        vm.Navigation = this.Navigation;
        BindingContext = vm;
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();
        if (BindingContext is SignInViewModel vm)
        {
            vm.OnAppearing();
        }
    }


}