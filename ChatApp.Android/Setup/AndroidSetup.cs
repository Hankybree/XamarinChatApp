using Autofac;
using ChatApp.Setup;

namespace ChatApp.Android.Setup
{
    public class AndroidSetup : AppSetup
    {
        protected override void RegisterDependencies(ContainerBuilder containerBuilder)
        {
            base.RegisterDependencies(containerBuilder);
            // Platform specific config here
        }
    }
}