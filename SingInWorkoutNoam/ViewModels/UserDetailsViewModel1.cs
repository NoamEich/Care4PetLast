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
    [ QueryProperty ( nameof ( ReceivedUser ), " selectedUser " )]
    public class UserDetailsViewModel1 : ViewModelBase
    {
        private IAppUserRepository _dbService;
        private string _firstName;
        private string _lastName;
        private string _email;
        private string _mobile;

        private AppUser _user;
        private AppUser _receivedUser;
        public AppUser ReceivedUser
        {
            get => _receivedUser;
            set
            {
                if (_receivedUser != value)
                {
                    _receivedUser = value;
                    OnPropertyChanged(nameof(ReceivedUser));

                    // Load user details based on the received user
                    LoadUserDetails(_receivedUser);
                }
            }
        }
        public string FirstName
        {
            get => _firstName;
            set
            {
                if (_firstName != value)
                {
                    _firstName = value;
                    OnPropertyChanged(nameof(FirstName));
                }
            }
        }
        public string LastName
        {
            get => _lastName;
            set
            {
                if (_lastName != value)
                {
                    _lastName = value;
                    OnPropertyChanged(nameof(LastName));
                }
            }
        }
        public string Email
        {
            get => _email;
            set
            {
                if (_email != value)
                {
                    _email = value;
                    OnPropertyChanged(nameof(Email));
                }
            }
        }
        public string Mobile
        {
            get => _mobile;
            set
            {
                if (_mobile != value)
                {
                    _mobile = value;
                    OnPropertyChanged(nameof(Mobile));
                }
            }
        }

        public ICommand UpdateUserCommand { get; }

        public UserDetailsViewModel1(IAppUserRepository dbService)
        {
            _dbService = dbService;  //Firebase user repo methods
            UpdateUserCommand = new Command(UpdateUser);

            if (ReceivedUser != null) //Arrived from admin page
            {
                _user = ReceivedUser;
            }
            else  //Arrived from Current User
            {
                _user = (App.Current as App)!.CurrentUser;
            }
            LoadUserDetails(_user);
        }
        private void LoadUserDetails(AppUser user)
        {
            FirstName = _user?.FirstName ?? string.Empty;
            LastName = _user?.LastName ?? string.Empty;
            Email = _user?.UserEmail ?? string.Empty;
            Mobile = _user?.UserMobile ?? string.Empty;
        }
        private async void UpdateUser()
        {
            _user.FirstName = FirstName;
            _user.LastName = LastName;
            _user.UserMobile = Mobile;

            try
            {
                IsBusy = true;
                await _dbService.UpdateAsync(_user);


                IsBusy = false;
                //Show ntification: Update detailse succeded!
                if (ReceivedUser != null)
                {
                    //return to users list
                }
                else
                {
                    (App.Current as App)!.CurrentUser = _user;
                }

            }
            catch (Exception ex)
            {
                IsBusy=false;
                //Show error message 
            }
        }
    }
}
