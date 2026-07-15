using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;


namespace UI.Event
{
    public class MoraleBar : MonoBehaviour
    {
        [SerializeField] private Image _bar;

        public void UpdateValue(float moralModifier, float speed)
        {
            _bar.DOKill();
            _bar.DOFillAmount(moralModifier, speed).SetEase(Ease.OutSine);
        }

        public void ChangeBar(Sprite barSprite)
        {
            _bar.sprite = barSprite;
        }
    }
}