using SingInWorkoutNoam.Models;
using SingInWorkoutNoam.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingInWorkoutNoam.ViewModels
{
    public class UsersListViewModel : ViewModelBase
    {
        #region Fields
        private string? _searchText; //Text entered in the search bar
        private List<AppUser> _allUsers;
        private bool _isBusy;
        DBMokup _dB;
        #endregion

        #region Properties
        public string? SearchText
        {
            get => _searchText;
            set
            {
                if (_searchText != value)
                {
                    _searchText = value;
                    OnPropertyChanged();
                    //Update the command state when SearchText change
                    ClearFilterCommand?.ChangeCanExecute();
                }
            }
        }

        public bool IsBusy
        {
            get => _isBusy;
            set
            {
                if (_isBusy != value)
                {
                    _isBusy = value;
                    OnPropertyChanged();
                }
            }
        }

        public ObservableCollection<AppUser> AllUsers { get; set; }
        #endregion

        #region Commands
        public Command? SearchCommand { get; }
        public Command? ClearFilterCommand { get; }
        public Command? ViewAccountPage { get; }

        // פעולה של מחיקת משתמש מתוך הרשימה
        // בעת הפעלת הפעולה, חייבים להעביר גם פרמטר (user)
        public Command<AppUser>? DeleteUserCommand { get; }

        public Command<AppUser>? ViewAccountPageCommand { get; }

        public Command? GetAllUsersCommand
        {
            get
            {
                return new Command(() =>
                {
                    IsBusy = true; // Set IsBusy to true to indicate loading state
                    // This command can be used to fetch all users from the database
                    // For example, it could be bound to a button to refresh the user list
                    _allUsers = _dB.Users;
                    AllUsers.Clear(); // Clear the existing collection
                    foreach (var user in _allUsers)
                    {
                        AllUsers.Add(user); // Add each user to the ObservableCollection
                    }
                    IsBusy = false; // Set IsBusy to false after loading is complete
                });
            }
        }
        #endregion

        // Constructor
        public UsersListViewModel()
        {
            _dB = new DBMokup();
            AllUsers = new();

            //Initialize properties and commands here
            SearchCommand = new Command(OnSearch);
            ClearFilterCommand = new Command(ClearFilter, () => !string.IsNullOrEmpty(SearchText));
            DeleteUserCommand = new Command<AppUser>(DeleteUser); // Initialize with parameter
            ViewAccountPageCommand = new Command<AppUser>(GoToAccountPage);
        }

        #region Private Methods

        private void OnSearch()
        {
            // Implement search functionality
            if (!string.IsNullOrEmpty(SearchText))
            {
                var filteredUsers = _allUsers.Where(u =>
                    u.FirstName?.Contains(SearchText, StringComparison.OrdinalIgnoreCase) == true ||
                    u.LastName?.Contains(SearchText, StringComparison.OrdinalIgnoreCase) == true ||
                    u.UserEmail?.Contains(SearchText, StringComparison.OrdinalIgnoreCase) == true
                ).ToList();

                AllUsers.Clear();
                foreach (var user in filteredUsers)
                {
                    AllUsers.Add(user);
                }
            }
            else
            {
                // If search text is empty, show all users
                GetAllUsersCommand?.Execute(null);
            }
        }

        private void ClearFilter()
        {
            // Clear search text and reload all users
            SearchText = string.Empty;
            GetAllUsersCommand?.Execute(null);
        }

        private bool IsSearchTextEmpty()
        {
            return string.IsNullOrEmpty(SearchText);
        }

        // פעולה של מחיקת משתמש מתוך הרשימה
        // קבלת user כפרמטר
        private void DeleteUser(AppUser user)
        {
            if (user != null)
            {
                // Remove the user from the ObservableCollection
                AllUsers.Remove(user);

                // Optionally, also remove from the database
                // _db.Users.Remove(user);
            }
        }

        // פעולה לצפייה בדף חשבון המשתמש
/*        private void ViewAccountPage(AppUser user)
        {
            if (user != null)
            {
                // Navigate to account page with the selected user
                // You can implement navigation here
                // For example:
                // await Shell.Current.GoToAsync($"accountpage?userid={user.Id}");
                // Or use your navigation service
            }
        }*/

        private async void GoToAccountPage(AppUser user)
        {
            if (user != null)
            {
                // Navigate to the account page for the selected user
                //await Shell.Current.GoToAsync (@$"UserDetailsPage?UserId={user.Id}");
                Dictionary<string, object> param = new Dictionary<string, object>();
                param.Add(" selectedUser ", user);
                await Shell.Current.GoToAsync(" UserDetailsPage ", param);
            }
            else
            {
                // Handle the case where user is null, if necessary
            }
        }

        #endregion
    }
}