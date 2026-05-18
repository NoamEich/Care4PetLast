using Newtonsoft.Json;
using SingInWorkoutNoam.Helper;
using SingInWorkoutNoam.Models;
using SingInWorkoutNoam.Service;
using SingInWorkoutNoam.Service.DBService;
using SingInWorkoutNoam.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SingInWorkoutNoam.ViewModels
{
    public class SignInViewModel : ViewModelBase
    {
        private Page _signupPage;
        private IAppUserRepository _dbService;
        private string _userName;
        private string _password;
        private bool _entryAsPassword = true;
        private bool _signInMessageVisible = false;
        private string _loginMassage;
        private string _passIcon = FontHelper.OPEN_EYE_ICON;
        private readonly INavigation _navigation;
        private bool _isRememberMeChecked;
        private bool _isBusy;

        public INavigation Navigation { get; set; }
        public ICommand ShowPasswordCommand { get; }
        public ICommand SignInCommand { get; }
        public ICommand NavigateToSignUpCommand { get; }

        

        public SignInViewModel(SignUpPage signUpPage, IAppUserRepository dbService)
        {
            _signupPage = signUpPage;
            _dbService = dbService;
            _entryAsPassword = true;
            _signInMessageVisible = false;
            //_navigation = navigation;

            //_passwordIconCode = FontHelper.CLOSED_EYE_ICON;
            //_loginMessage = String.Empty;


            ShowPasswordCommand = new Command(TogglePasswordButton);
            SignInCommand = new Command(SignInButtonClick);
            //NavigateToSignUpCommand = new Command(async () => await _navigation!.PushAsync(_signupPage));
            NavigateToSignUpCommand = new Command(ToSignUpPage);

            //Debug Mode
            UserName = "noam@gmail.com";
            UserPassword = "123456";
        }

        public bool IsBusy
        {
            get { return _isBusy; }
            set 
            { 
                if( _isBusy!=value )
                {
                    _isBusy = value;
                    OnPropertyChanged();
                }
            }
        }
        public bool RememberMeChecked
        {
            get { return _isRememberMeChecked; }
            set
            {
                if (_isRememberMeChecked != value)
                {
                    _isRememberMeChecked = value;
                    OnPropertyChanged();
                    //OnPropertyChanged(nameof(IsSignInButtonEnabled));
                }

            }
        }
        public bool EntryAsPassword
        {
            get { return _entryAsPassword; }
            set
            {
                if (_entryAsPassword != value)
                {
                    _entryAsPassword = value;
                    OnPropertyChanged();
                }

            }
        }
        public string UserName
        {
            get { return _userName; }
            set
            {
                if (_userName != value)
                {
                    _userName = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(IsSignInButtonEnabled));
                }
            }
        }
        public string UserPassword
        {
            get { return _password; }
            set
            {
                if (_password != value)
                {
                    _password = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(IsSignInButtonEnabled));
                }
            }

        }
        public bool IsSignInButtonEnabled
        {

            get => !(string.IsNullOrEmpty(UserName) || string.IsNullOrEmpty(UserPassword));

        }

        public bool SignInMessageVisible
        {
            get { return _signInMessageVisible; }
            set
            {
                if (_signInMessageVisible != value)
                {
                    _signInMessageVisible = value;
                    OnPropertyChanged();
                }

            }
        }

        public string LoginMessage
        {
            get { return _loginMassage; }
            set
            {
                if ( _loginMassage != value)
                    
                {
                    _loginMassage = value;
                    OnPropertyChanged();
                }

            }
        }

        public string PassIcon
        {
            get { return _passIcon; }
            set
            {
                if (_passIcon != value)

                {
                    _passIcon = value;
                    OnPropertyChanged();
                }

            }
        }

        private void TogglePasswordButton()
        {
            EntryAsPassword = !EntryAsPassword;

            if (EntryAsPassword)
                PassIcon= FontHelper.CLOSED_EYE_ICON;
            else
                PassIcon= FontHelper.OPEN_EYE_ICON;
        }

        private async void SignInButtonClick()
        {
            SignInMessageVisible = true;
            IsBusy = true; //Show Progress Bar

            try
            {
                var user = await _dbService.SignInAsync(UserName, UserPassword);

                //Set Admin this user
                //await _dbService.SetToAdmin((user as AppUser).Id);

                //IF Remeber me checked
                if(RememberMeChecked)
                {
                    await SecureStorage.Default.SetAsync("current_user_object", user.Id);
                }

                IsBusy = false;

                (App.Current as App)!.CurrentUser = user as AppUser;

                //Navigate to MainPage
                var mainPage = IPlatformApplication.Current!.Services.GetService<AppShell>();
                Application.Current!.Windows[0].Page = mainPage;
            }
            catch (Exception ex)
            {
                IsBusy = false;
                LoginMessage = ex.Message;
            }
        }
        private async void ToSignUpPage()
        {
            await Navigation.PushAsync(_signupPage);
        }

        internal async void OnAppearing()
        {         
            //Check if user's Id exist in storage
            string? token = await SecureStorage.Default.GetAsync("current_user_object");
            if (!string.IsNullOrEmpty(token))
            {
                try
                {
                    IsBusy = true;
                    var user = await _dbService.GetUserByIdAsync(token);

                    (App.Current as App)!.CurrentUser = user as AppUser;;
                    var mainpage = IPlatformApplication.Current!.Services.GetService<AppShell>();
                    Application.Current!.Windows[0].Page = mainpage;

                }
                catch (Exception ex)
                {
                    IsBusy=false; 
                    ShowErrorMessage(ex.Message);
                }
            }
        }

        private void ShowErrorMessage(string message)
        {
            SignInMessageVisible = true;
            LoginMessage = message;
        }
    }
}
