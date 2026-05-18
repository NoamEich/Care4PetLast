using SingInWorkoutNoam.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingInWorkoutNoam.Service
{
    public class DBSQLiteService : IDBService
    {
        public void AddUser(AppUser user)
        {
            throw new NotImplementedException();
        }

        public AppUser GetUserByIdAsync(string uEmail)
        {
            throw new NotImplementedException();
        }

        public bool IsExist(string uEmail, string uPass)
        {
            throw new NotImplementedException();
        }

        public void RemoveUser(AppUser user)
        {
            throw new NotImplementedException();
        }

        public void UpdateUser(AppUser user)
        {
            throw new NotImplementedException();
        }
    }
}
