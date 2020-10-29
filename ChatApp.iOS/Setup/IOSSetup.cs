using Autofac;
using ChatApp.iOS.Services;
using ChatApp.Services;
using ChatApp.Setup;

namespace ChatApp.iOS.Setup
{
    public class IOSSetup : AppSetup
    {
        protected override void RegisterDependencies(ContainerBuilder containerBuilder)
        {
            base.RegisterDependencies(containerBuilder);
            // Platform specific config here
            containerBuilder.RegisterType<HelloService>().As<IHelloService>();
        }
    }
}