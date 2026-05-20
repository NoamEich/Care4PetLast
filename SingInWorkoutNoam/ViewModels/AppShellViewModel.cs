using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SingInWorkoutNoam.Helper;
using SingInWorkoutNoam.Service;
using SingInWorkoutNoam.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SingInWorkoutNoam.ViewModels
{
    public class AppShellViewModel: ViewModelBase
    {
        private Page _signInPage;
        private string _logoutIcon;
        private string _adminIcon;
        private string _accountIcon;
        private string _homeIcon;

        
        public bool IsAdmin { get => (App.Current as App)?.CurrentUser?.IsAdmin ?? false; }

        public string LogoutIcon
        {
            get=> _logoutIcon;
            set
            {
                if(_logoutIcon != value)
                {
                    _logoutIcon = value;
                    OnPropertyChanged(nameof(LogoutIcon));
                }
            }
        }
        public string AdminIcon
        {
            get => _adminIcon;
            set
            {
                if (_adminIcon != value)
                {
                    _adminIcon = value;
                    OnPropertyChanged(nameof(AdminIcon));
                }
            }
        }
        public string AccountIcon
        {
            get => _accountIcon;
            set
            {
                if (_accountIcon != value)
                {
                    _accountIcon = value;
                    OnPropertyChanged(nameof(AccountIcon));
                }

            }
        }
        public string HomeIcon
        {
            get => _homeIcon;
            set
            {
                if (HomeIcon != value)
                {
                    _homeIcon = value;
                    OnPropertyChanged(nameof(HomeIcon));
                }
            }
        }

        

        public ICommand LogoutCommand { get; }
        public ICommand UserPageCommand { get; }
        public ICommand AdminCommand { get; }
        public ICommand MainPage { get; }

        public AppShellViewModel(SignInPage signInPage)
        {
            _signInPage=signInPage;
            _logoutIcon = FontHelper.LOGOUT_ICON;
            LogoutCommand = new Command(Logout);
            AdminCommand = new Command(async () => { await Shell.Current.GoToAsync("AdminView"); });
            UserPageCommand = new Command(async () => { await Shell.Current.GoToAsync("UserDetailsPage"); });
            MainPage = new Command(async () => { await Shell.Current.GoToAsync("MainPage1"); });
        }    

        private void Logout()
        {
            (App.Current as App)!.CurrentUser = null;

            //Remove user from secured storage
            SecureStorage.Default.Remove("current_user_object");

            //OnPropertyChanged(nameof(IsAdmin));
            Application.Current.Windows[0].Page = new NavigationPage(_signInPage);
        }

    }
}
