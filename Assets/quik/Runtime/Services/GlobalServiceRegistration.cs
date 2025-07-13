using quik.Runtime.Services.Interfaces;

namespace quik.Runtime.Services
{
    public static class GlobalServiceRegistration
    {
        public static void RegisterAll(IServiceProvider provider)
        {
            // Register your global game services here using:
            // provider.Register<T>(T service)
        }
    }
}