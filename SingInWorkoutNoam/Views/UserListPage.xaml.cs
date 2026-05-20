using SingInWorkoutNoam.Service;
using SingInWorkoutNoam.Service.DBService;
using SingInWorkoutNoam.ViewModels;

namespace SingInWorkoutNoam.Views;

public partial class UserListPage : ContentPage
{
	public UserListPage(IAlertService alertService, IAppUserRepository dbService, IAppLogger appLogger)
	{
		InitializeComponent();
		BindingContext = new ViewModels.UsersListViewModel(alertService, dbService, appLogger);

	}
}