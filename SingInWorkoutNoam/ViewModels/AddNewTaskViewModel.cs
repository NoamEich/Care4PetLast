using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SingInWorkoutNoam.Views;
namespace SingInWorkoutNoam.ViewModels
{

    public partial class AddNewTaskViewModel : ObservableObject
    {
        [RelayCommand]
        public async Task Cancel()
        {
            await Shell.Current.GoToAsync($"///{nameof(MainPage1)}");
        }
    }
}
