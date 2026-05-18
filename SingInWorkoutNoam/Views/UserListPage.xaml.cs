using SingInWorkoutNoam.ViewModels;

namespace SingInWorkoutNoam.Views;

public partial class UserListPage : ContentPage
{
	public UserListPage()
	{
		InitializeComponent();
		BindingContext = new ViewModels.UsersListViewModel();

	}
    protected override void OnAppearing()
    {
        base.OnAppearing();
        // Optionally, you can call a method to load data when the page appears
        if (BindingContext is UsersListViewModel viewModel)
        {

            viewModel.GetAllUsersCommand?.Execute(null);

        }
    }
}