using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using SingInWorkoutNoam.Helper;
using SingInWorkoutNoam.ViewModels;
using System.ComponentModel.DataAnnotations;
using System.Windows.Input;

namespace SingInWorkoutNoam.Views;

public partial class SignUpPage : ContentPage
{
    public SignUpPage(SignUpViewModel vm)
	{
        InitializeComponent();
        BindingContext = vm;
        vm.Navigation = this.Navigation;

    }
    
   
}