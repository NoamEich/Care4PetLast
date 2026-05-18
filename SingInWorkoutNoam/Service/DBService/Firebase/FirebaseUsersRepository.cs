using Firebase.Database;
using Firebase.Database.Query;
using SingInWorkoutNoam.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingInWorkoutNoam.Service.DBService.Firebase
{
    public class FirebaseUsersRepository : FirebaseRealtimeService, IAppUserRepository
    {
        private IAuthService _authService;
        private IAppLogger _appLogger;

        public FirebaseUsersRepository(IAuthService authService, IAppLogger appLogger)
        {
            _authService = authService;
            _appLogger = appLogger;
        }

        public async Task<string> CreateAsync(AppUser appUser)
        {
            try
            {
                string userId = await _authService.CreateAuth(appUser.UserEmail!, appUser.UserPassword!);

                //Add ID to the user object and save it to the DB
                appUser.Id = userId;
                await RegisterAppUser(appUser);
                _appLogger.LogDebug($"FirebaseUsersRepository {appUser.UserEmail} SignUp successfully");
                return userId;
            }
            catch (Exception ex)
            {
                _appLogger.LogDebug($"FirebaseUsersRepository SignIn failed: {ex.Message}");
                if (!ex.Message.Contains("RealTimeDB"))
                    throw new Exception(ex.Message);

                throw new Exception("SignUp new user failed!");
            }
        }

        public async Task RegisterAppUser(AppUser appUser)
        {
            try
            {
                await _firebaseClient!
               .Child("users")
               .Child(appUser.Id)
               .PutAsync(new AppUser()
               {
                   Id = appUser.Id,
                   FirstName = appUser.FirstName,
                   LastName = appUser.LastName,
                   UserEmail = appUser.UserEmail,
                   UserPassword = appUser.UserPassword,
                   UserMobile = appUser.UserMobile
                   //RegDate = appUser.RegDate,
                   //UBDate = appUser.UBDate,
                   //IsAdmin = appUser.IsAdmin
               });
            }
            catch (Exception ex)
            {
                _appLogger.LogDebug($"RealTimeDB SignUp failed: {ex.Message}");
                throw new Exception("RealTimeDB add new user failed");
            }
        }

        public Task DeleteAsync(AppUser appUser)
        {
            throw new NotImplementedException();
        }

        public List<AppUser> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<AppUser> GetUserByIdAsync(string userId)
        {
            string errorMessage = string.Empty;
            try
            {
                var user = await _firebaseClient!
                    .Child("users")
                    .Child(userId) //using Firebase.Database.Query;
                    .OnceSingleAsync<AppUser>();

                return user;
            }
            catch (FirebaseException ex)
            {
                if (ex.Message.Contains("401") || ex.Message.Contains("Permission denied"))
                {
                    errorMessage = "GetUserByIdAsync failed: Permissions denied!";
                }
                else if (ex.Message.Contains("404"))
                {
                    errorMessage = "GetUserByIdAsync failed: Wrong db path!";
                }
                else
                {
                    errorMessage = "GetUserByIdAsync failed: Unknown exception!";
                }

                _appLogger.LogDebug($"FirebaseUsersRepository {errorMessage}");
                throw new Exception(errorMessage);
            }
            catch (Exception ex)
            {
                throw new Exception($"FirebaseUsersRepository GetUserByIdAsync failed! {ex.Message}");
            }
        }

        public async Task SetToAdmin(string userId)
        {
            try
            {
                await _firebaseClient!
                .Child("users")
                .Child(userId)
                .PatchAsync(new { IsAdmin = true }); // שולח רק את השדה הזה

                _appLogger.LogDebug("User admin status updated successfully.");
            }
            catch (Exception ex)
            {
                _appLogger.LogDebug($"Error updating field: {ex.Message}");
                throw new Exception("SetToAdmin failed!");
            }
        }

        public async Task<AppUser> SignInAsync(string userEmail, string userPassword)
        {
            try
            {
                //1 SignIn to Firebase Authentication and get the user ID
                string userId = await _authService.SignIn(userEmail, userPassword);

                //2 Get the user data from RealTimeDB using the user ID
                AppUser appUser = await GetUserByIdAsync(userId);

                _appLogger.LogDebug($"FirebaseUsersRepository {userEmail} SignIn successfully");
                return appUser;
            }
            catch (Exception ex)
            {
                _appLogger.LogDebug($"FirebaseUsersRepository SignIn failed: {ex.Message}");
                if (!ex.Message.Contains("Incorrect email or password"))
                    throw new Exception("SignIn failed!");

                throw new Exception(ex.Message);
            }
        }

        public async Task UpdateAsync(AppUser appUser)
        {
            try
            {
                await _firebaseClient!
                .Child("users")
                .Child(appUser.Id)
                .PatchAsync(new
                {
                    FirstName = appUser.FirstName,
                    LastName = appUser.LastName,
                    UserMobile = appUser.UserMobile
                });

                _appLogger.LogDebug($"Update user {appUser.UserEmail} detailes successfully.");
            }
            catch (Exception ex)
            {
                _appLogger.LogDebug($"Error updating user details: {ex.Message}");
                throw new Exception("Update failed!");
            }
        }

    }
}
