using Autofac;
using ChatApp.Services;
using ChatApp.ViewModels;

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
        }
    }
}