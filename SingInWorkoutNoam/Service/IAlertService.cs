using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingInWorkoutNoam.Service
{
    public interface IAlertService
    {
        Task ShowAlertAsync(string title, string message, string cancel);
    }
}
