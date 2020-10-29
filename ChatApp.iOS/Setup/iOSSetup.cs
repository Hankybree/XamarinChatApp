using Autofac;
using ChatApp.Setup;

namespace ChatApp.iOS.Setup
{
    public class iOSSetup : AppSetup
    {
        protected override void RegisterDependencies(ContainerBuilder containerBuilder)
        {
            base.RegisterDependencies(containerBuilder);
            // Platform specific config here
        }
    }
}