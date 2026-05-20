using SingInWorkoutNoam.Models;
using SingInWorkoutNoam.Service.DBService;
using SingInWorkoutNoam.ViewModels;


namespace SingInWorkoutNoam.Views;


public partial class UserDetailsPage : ContentPage
{
	public UserDetailsPage(IAppUserRepository dbService)
	{
		InitializeComponent();
		BindingContext = new UserDetailsViewModel1(dbService);
	}

    protected override void OnAppearing()
    {
        base.OnAppearing();
        (BindingContext as UsersListViewModel)!.OnAppearing();
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        (BindingContext as UsersListViewModel)!.OnDisappearing();
    }

}