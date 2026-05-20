using SingInWorkoutNoam.ViewModels;

namespace SingInWorkoutNoam.Views;

public partial class AddNewTask : ContentPage
{
	public AddNewTask()
	{
		InitializeComponent();
        BindingContext = new AddNewTaskViewModel();
    }
}