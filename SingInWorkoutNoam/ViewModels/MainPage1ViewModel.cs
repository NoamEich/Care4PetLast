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
    public partial class MainPage1ViewModel : ObservableObject
    {
        public MainPage1ViewModel()
        {
        }

        [RelayCommand]
        public async Task GoToAddPet()
        {
            await Shell.Current.GoToAsync(nameof(Views.AddPetPage));
        }
        [RelayCommand]
        public async Task GoToAddTask()
        {
            await Shell.Current.GoToAsync(nameof(AddNewTask));
        }
    }
}
