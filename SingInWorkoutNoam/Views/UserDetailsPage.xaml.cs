using SingInWorkoutNoam.Models;
using SingInWorkoutNoam.ViewModels;


namespace SingInWorkoutNoam.Views;


public partial class UserDetailsPage : ContentPage
{
	public UserDetailsPage()
	{
		InitializeComponent();
		BindingContext = new UserDetailsViewModel1();
	}
    
}