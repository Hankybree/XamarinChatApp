using System.Net.Http;
using System.Net.WebSockets;
using Autofac;
using ChatApp.Models.Authentication;
using ChatApp.Models.Chat;
using ChatApp.Services;
using ChatApp.ViewModels;
using Xamarin.Forms;

namespace ChatApp.Setup
{
    public class AppSetup
    {
        public IContainer CreateContainer()
        {
            ContainerBuilder containerBuilder = new ContainerBuilder();
            
            // Configure builder here
            RegisterDependencies(containerBuilder);

            return containerBuilder.Build();
        }

        protected virtual void RegisterDependencies(ContainerBuilder containerBuilder)
        {
            // Common config here
            containerBuilder.RegisterType<MainPageViewModel>().SingleInstance();
            containerBuilder.RegisterType<SignUpPageViewModel>().SingleInstance();
            containerBuilder.RegisterType<ChatRoomPageViewModel>().SingleInstance();
            containerBuilder.RegisterType<HttpClient>().SingleInstance();
            containerBuilder.RegisterType<Speaker>().SingleInstance();
            containerBuilder.RegisterType<AuthApi>().As<IAuthApi>().SingleInstance();
            containerBuilder.RegisterType<ChatApi>().SingleInstance();
            containerBuilder.RegisterType<ChatClient>().SingleInstance();
            containerBuilder.RegisterType<NavigationService>().As<INavigationService>().SingleInstance();
        }
    }
}