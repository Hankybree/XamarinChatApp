using Autofac;
using ChatApp.Android.Services;
using ChatApp.Services;
using ChatApp.Setup;

namespace ChatApp.Android.Setup
{
    public class AndroidSetup : AppSetup
    {
        protected override void RegisterDependencies(ContainerBuilder containerBuilder)
        {
            base.RegisterDependencies(containerBuilder);
            // Platform specific config here
            containerBuilder.RegisterType<HelloService>().As<IHelloService>();
        }
    }
}