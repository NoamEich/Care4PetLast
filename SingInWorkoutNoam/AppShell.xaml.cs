using SingInWorkoutNoam.ViewModels;
using SingInWorkoutNoam.Views;

namespace SingInWorkoutNoam
{
    public partial class AppShell : Shell
    {
        public AppShell(AppShellViewModel vm)
        {
            InitializeComponent();
            BindingContext = vm;

            Routing.RegisterRoute(nameof(SignInPage), typeof(SignInPage));
            Routing.RegisterRoute(nameof(MainPage1), typeof(MainPage1));
            Routing.RegisterRoute(nameof(AdminView), typeof(AdminView));
            Routing.RegisterRoute(nameof(UserDetailsPage), typeof(UserDetailsPage));
        }
    }
}
