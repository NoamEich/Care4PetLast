using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingInWorkoutNoam.Service.DBService
{
    //Interface usefull for Dependency Injection
    public interface IDbInstance
    {
        string Info();
    }
}
