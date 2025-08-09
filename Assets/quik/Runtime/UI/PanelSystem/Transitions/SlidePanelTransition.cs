using DG.Tweening;
using quik.Runtime.UI.PanelSystem.Interfaces;
using UnityEngine;

namespace quik.Runtime.UI.PanelSystem
{
    public class SlidePanelTransition : MonoBehaviour, IPanelTransition
    {
        [SerializeField] private Vector3 hiddenPosition = new Vector3(-1000, 0, 0);
        [SerializeField] private Vector3 visiblePosition = Vector3.zero;
        [SerializeField] private float duration = 0.3f;

        private RectTransform _rectTransform;

        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
            _rectTransform.localPosition = hiddenPosition;
        }

        public void PlayShow(TweenCallback onComplete = null)
        {
            _rectTransform.DOLocalMove(visiblePosition, duration).SetUpdate(true).OnComplete(onComplete);
        }

        public void PlayHide(TweenCallback onComplete = null)
        {
            _rectTransform.DOLocalMove(hiddenPosition, duration).SetUpdate(true).OnComplete(onComplete);
        }
    }
}