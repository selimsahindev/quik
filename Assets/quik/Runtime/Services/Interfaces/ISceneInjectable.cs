namespace quik.Runtime.Services.Interfaces
{
    public interface ISceneInjectable
    {
        void Inject(IServiceProvider provider);
    }
}