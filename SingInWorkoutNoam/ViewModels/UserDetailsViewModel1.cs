using SingInWorkoutNoam.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingInWorkoutNoam.ViewModels
{
    [ QueryProperty ( nameof ( ReceivedUser ), " selectedUser " )]
    public class UserDetailsViewModel1 : ViewModelBase
    {
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

        public UserDetailsViewModel1()
        {
            if (ReceivedUser != null)
            {
                _user = ReceivedUser;
            }
            else
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
    }
}
