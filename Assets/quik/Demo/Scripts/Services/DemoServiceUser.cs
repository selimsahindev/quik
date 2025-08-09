using quik.Runtime.ServiceProvider.Interfaces;
using UnityEngine;
using IServiceProvider = quik.Runtime.ServiceProvider.Interfaces.IServiceProvider;

namespace quik.Demo
{
    public class DemoServiceUser : MonoBehaviour, ISceneInjectable
    {
        private IDemoService _demoService;
        
        public void Inject(IServiceProvider provider)
        {
            _demoService = provider.Resolve<IDemoService>();
        }

        private void Start()
        {
            if (_demoService == null)
            {
                Debug.Log("[DemoServiceUser] DemoService is null");
                return;
            }
            _demoService.SayQuik();
        }
    }
}
