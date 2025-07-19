using quik.Runtime.Services.Interfaces;
using quik.Runtime.Signals.Gameplay;
using quik.Runtime.Signals.Interfaces;
using UnityEngine;
using IServiceProvider = quik.Runtime.Services.Interfaces.IServiceProvider;

namespace quik.Demo
{
    public class AchievementTester : MonoBehaviour, IInjectable
    {
        private ISignalBus _signalBus;

        public void Inject(IServiceProvider provider)
        {
            Debug.Log("[AchievementTester] inject called");
            _signalBus = provider.Resolve<ISignalBus>();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                FireEnemyKilledSignal();
            }
        }

        private void FireEnemyKilledSignal()
        {
            _signalBus.Fire(new EnemyKilledSignal());
            Debug.Log($"[{nameof(AchievementTester)}] {nameof(EnemyKilledSignal)} is fired.");
        }
    }
}
