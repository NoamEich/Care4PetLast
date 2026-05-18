using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using SingInWorkoutNoam.Service;
using SingInWorkoutNoam.Service.DBService;
using SingInWorkoutNoam.Service.DBService.Firebase;
using SingInWorkoutNoam.ViewModels;

namespace SingInWorkoutNoam
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    fonts.AddFont("MaterialIcons-Regular.ttf", "MaterialIcons");
                });
            #region Dependency Injection for Views, ViewModels and Services
            builder.RegisterViews().RegisterViewModels().RegisterServices();
            #endregion

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
        public static MauiAppBuilder RegisterViews(this MauiAppBuilder builder)
        {

            builder.Services.AddTransient<Views.SignInPage>();
            builder.Services.AddTransient<Views.SignUpPage>();

            return builder;
        }

        public static MauiAppBuilder RegisterViewModels(this MauiAppBuilder builder)
        {
            builder.Services.AddTransient<SingInWorkoutNoam.ViewModels.SignInViewModel>();
            builder.Services.AddTransient<SingInWorkoutNoam.ViewModels.SignUpViewModel>();
            builder.Services.AddSingleton<AppShellViewModel>();
            return builder;

        }

        public static MauiAppBuilder RegisterServices(this MauiAppBuilder builder)
        {
            builder.Services.AddSingleton<IDBService, DBMokup>();
            builder.Services.AddSingleton<AppShell>();
            builder.Services.AddSingleton<IAppLogger, LogService>();
            builder.Services.AddSingleton<IAuthService, FirebaseAuthService>();
            builder.Services.AddSingleton<IAppUserRepository, FirebaseUsersRepository>();
            return builder;
        }

    }
}
