using quik.Runtime.Services.Interfaces;
using uniq.Demo;
using UnityEngine;
using IServiceProvider = quik.Runtime.Services.Interfaces.IServiceProvider;

public class DemoServiceUser : MonoBehaviour, IInjectable
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
            Debug.Log("service is null");
            return;
        }
        _demoService.SayQuik();
    }
}
