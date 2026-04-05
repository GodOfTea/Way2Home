using DG.Tweening;
using UnityEngine;

namespace UI.Event
{
    public abstract class EventWindow : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private float _animationTime;

        public virtual void Show(bool immediate = false)
        {
            DOVirtual.Float(0f, 1f, immediate ? 0f :_animationTime, value => _canvasGroup.alpha = value).
                SetEase(Ease.InCubic);
            _canvasGroup.blocksRaycasts = true;
        }

        public virtual void Hide(bool immediate = false)
        {
            DOVirtual.Float(1f, 0f, immediate ? 0f :_animationTime, value => _canvasGroup.alpha = value).
                SetEase(Ease.OutCubic);
            _canvasGroup.blocksRaycasts = false;
        }
    }
}