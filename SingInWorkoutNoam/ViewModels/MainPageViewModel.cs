using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingInWorkoutNoam.ViewModels
{
    public class MainPageViewModel: ViewModelBase
    {
        private string _welcomeText;

        public string WelcomeText
        {
            get => _welcomeText;
            set
            {
                if (_welcomeText != value)
                {
                    _welcomeText = value;
                    OnPropertyChanged();
                }
            }

        }
        public MainPageViewModel() 
        {
            WelcomeText = (App.Current as App)?.CurrentUser != null ?
                $"Welcome, {(App.Current as App)!.CurrentUser!.UserEmail}!" :
                "Error, CurrentUser is NULL!";
        }
    }
}
