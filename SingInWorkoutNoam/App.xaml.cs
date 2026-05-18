using SingInWorkoutNoam.Models;
using SingInWorkoutNoam.Views;

namespace SingInWorkoutNoam
{
    public partial class App : Application
    {
        private Page _page;
        public AppUser? CurrentUser { get; set; }
        public bool IsDebugMode { get; set; } = true;
        public App(SignInPage page)
        {
            _page = page;
            InitializeComponent();
            Routing.RegisterRoute(nameof(UserListPage), typeof(UserListPage));
            Routing.RegisterRoute(nameof(UserDetailsPage), typeof(UserDetailsPage));
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            //Version InClass1 with Kostya
            return new Window(new NavigationPage(_page));
            //return new Window(new UserListPage());

            /*if ((App.Current as App)!.IsDebugMode)
            {
                AppUser testUser = new AppUser()
                {
                    UserEmail = "Noam",
                    UserPassword = "1234567890",
                    IsAdmin = true
                };
                (App.Current as App)!.CurrentUser = testUser;
                return new Window(new AppShell());
                //return new Window(new UserListPage());

            }*/
            //return new Window(new NavigationPage(new SignInPage()));

            //return new Window(new NavigationPage(new SignInPage()));

            //return new Window(new AppShell());
            //return new Window(new SignInPage());
            //return new Window(new SignUpPage());


        }
    }
}