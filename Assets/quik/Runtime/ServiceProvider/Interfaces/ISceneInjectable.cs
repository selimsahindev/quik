namespace quik.Runtime.ServiceProvider.Interfaces
{
    public interface ISceneInjectable
    {
        void Inject(IServiceProvider provider);
    }
}