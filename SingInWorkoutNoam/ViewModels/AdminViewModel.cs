using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingInWorkoutNoam.ViewModels
{
    public partial class AdminViewModel : ObservableObject
    {

        public AdminViewModel() { }

        [RelayCommand]
        public async void NavigateToUserS()
        {
            await Shell.Current.GoToAsync("UserListPage");
        }
    }
}
