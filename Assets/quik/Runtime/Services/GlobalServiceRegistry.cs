using quik.Runtime.Pooling;
using quik.Runtime.Pooling.Interfaces;
using quik.Runtime.SaveSystem.Interfaces;
using quik.Runtime.SaveSystem.Services;
using quik.Runtime.Services.Interfaces;
using uniq.Demo;

namespace quik.Runtime.Services
{
    public static class GlobalServiceRegistry
    {
        public static void RegisterAll(IServiceProvider provider)
        {
            // Register your global game services here using:
            // provider.Register<TInterface, TConcrete>()
            
            provider.Register<IDemoService, DemoService>();
            provider.Register<IPoolManager, PoolManager>();
            provider.Register<ISaveService, JsonSaveService>();
        }
    }
}