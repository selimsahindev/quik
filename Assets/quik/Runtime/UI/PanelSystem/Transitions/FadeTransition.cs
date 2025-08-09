using DG.Tweening;
using quik.Runtime.UI.PanelSystem.Interfaces;
using UnityEngine;

namespace quik.Runtime.UI.PanelSystem
{
    [RequireComponent(typeof(CanvasGroup))]
    public class FadeTransition : MonoBehaviour, IPanelTransition
    {
        [SerializeField] private float duration = 0.3f;
        private CanvasGroup _canvasGroup;
    
        private void Awake()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
        }

        public void PlayShow(TweenCallback onComplete = null)
        {
            _canvasGroup.alpha = 0;
            _canvasGroup.DOFade(1, duration).SetUpdate(true).OnComplete(onComplete);
        }

        public void PlayHide(TweenCallback onComplete = null)
        {
            _canvasGroup.DOFade(0, duration).SetUpdate(true).OnComplete(onComplete);
        }
    }
}
