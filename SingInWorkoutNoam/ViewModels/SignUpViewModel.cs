using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using SingInWorkoutNoam.Helper;
using SingInWorkoutNoam.Models;
using SingInWorkoutNoam.Service.DBService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SingInWorkoutNoam.ViewModels
{
    public class SignUpViewModel : ViewModelBase
    {
        private IAppUserRepository _dbService;
        private string? _firstName;
        private string? _lastName;
        private string? _email;
        private string? _password;
        private string? _mobile;
        private bool _entryAsPassword;
        private string? _passwordIconCode;
        private bool _isBusy;


        public ICommand? ShowPasswordCommand { get; }
        public ICommand? SignUpCommand { get; }
        public ICommand? ToSignInCommand { get; }



        #region Properties
        public INavigation Navigation { get; set; }
        public string? FirstName
        {
            get => _firstName;
            set
            {
                if (_firstName != value)
                {
                    _firstName = value;
                    OnPropertyChanged();
                    (SignUpCommand as Command).ChangeCanExecute();
                }
            }
        }
        public string? LastName
        {
            get => _lastName;
            set
            {
                if (_lastName != value)
                {
                    _lastName = value;
                    OnPropertyChanged();
                    (SignUpCommand as Command).ChangeCanExecute();
                }
            }
        }
        public string? UserEmail
        {
            get => _email;
            set
            {
                if (_email != value)
                {
                    _email = value;
                    OnPropertyChanged();
                    (SignUpCommand as Command).ChangeCanExecute();
                }
            }
        }
        public string? UserPassword
        {
            get => _password;
            set
            {
                if (_password != value)
                {
                    _password = value;
                    OnPropertyChanged();
                    (SignUpCommand as Command).ChangeCanExecute();
                }
            }
        }
        public string? Mobile
        {
            get => _mobile;
            set
            {
                if (_mobile != value)
                {
                    _mobile = value;
                    OnPropertyChanged();
                    (SignUpCommand as Command).ChangeCanExecute();
                }
            }
        }
        public bool EntryAsPassword
        {
            get => _entryAsPassword;
            set
            {
                if (_entryAsPassword != value)
                {
                    _entryAsPassword = value;
                    OnPropertyChanged();
                    (SignUpCommand as Command).ChangeCanExecute();
                }
            }
        }
        public string? PasswordIconCode
        {
            get => _passwordIconCode;
            set
            {
                if (_passwordIconCode != value)
                {
                    _passwordIconCode = value;
                    OnPropertyChanged();
                    (SignUpCommand as Command).ChangeCanExecute();
                }
            }
        }
        public bool IsBusy
        {
            get { return _isBusy; }
            set
            {
                if (_isBusy != value)
                {
                    _isBusy = value;
                    OnPropertyChanged();
                }
            }
        }
        #endregion

        public SignUpViewModel(IAppUserRepository dbService)
        {
            _dbService = dbService;
            _entryAsPassword = true;
            _passwordIconCode = FontHelper.CLOSED_EYE_ICON;

            ShowPasswordCommand = new Command(TogglePasswordButton);
            SignUpCommand = new Command(SignUpButtonClicked, Validate);
            ToSignInCommand = new Command(NavigateToSignIn);

            //Debug Mode
            FirstName = "Noam";
            LastName = "Eish";
            UserEmail = "noam@gmail.com";
            UserPassword = "123456";
            Mobile = "0534567676";
        }

        
        private void TogglePasswordButton()
        {
            EntryAsPassword = !EntryAsPassword;
            if (EntryAsPassword)
                PasswordIconCode = FontHelper.CLOSED_EYE_ICON;
            else
                PasswordIconCode = FontHelper.OPEN_EYE_ICON;
        }
        private async void SignUpButtonClicked()
        {
            //Register user into DB
            //Save User to Current Usermail
            //Go to Main Page

            if(Validate())
            {
                AppUser user = new AppUser()
                {
                    FirstName = FirstName,
                    LastName = LastName,
                    UserEmail = UserEmail,
                    UserPassword = UserPassword,
                    UserMobile = Mobile
                };

                IsBusy = true;
                try
                {
                    string userID = await _dbService.CreateAsync(user);
                    IsBusy= false;

                    user.Id = userID;
                    (App.Current as App)!.CurrentUser = user;

                    //Navigate to MainPage
                    var mainPage = IPlatformApplication.Current!.Services.GetService<AppShell>();
                    Application.Current!.Windows[0].Page = mainPage;
                }
                catch (Exception ex)
                {
                    IsBusy= false;
                    //Show exception message
                }
            }
            else
            {
                //Show validation error message
            }         
        }

        private void NavigateToSignIn()
        {
            try
            {
                Navigation!.PopAsync();
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        private bool Validate()
        {
            var fnameOK = !string.IsNullOrEmpty(FirstName);
            var lnameOK = !string.IsNullOrEmpty(LastName);
            var emailOK = !string.IsNullOrEmpty(UserEmail);
            var passOK = string.IsNullOrEmpty(UserPassword) ? false :
            UserPassword.Length > 5;
            var mobileOK = string.IsNullOrEmpty(Mobile) ? false :
            Mobile.Length == 10;
            return fnameOK && lnameOK && emailOK && passOK && mobileOK;
        }
    }


}
