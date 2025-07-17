using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Game.UI.MainMenu
{
    public class MainMenuButtonView : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        [SerializeField] private TextMeshProUGUI _textImage;

        private IMainMenuButtonVisualData _visualData;

        public event Action Clicked;

        public void SetVisualData(IMainMenuButtonVisualData visualData) => 
            _visualData = visualData;

        public void OnPointerEnter(PointerEventData eventData)
        {
            _textImage.DOColor(_visualData.SelectedTextColor, _visualData.TransitionTime).SetEase(Ease.Linear);
            _textImage.transform.DOScale(_visualData.SelectedTextSize, _visualData.TransitionTime).SetEase(Ease.Linear);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _textImage.DOColor(_visualData.DefaultTextColor, _visualData.TransitionTime).SetEase(Ease.Linear);
            _textImage.transform.DOScale(_visualData.DefaultTextSize, _visualData.TransitionTime).SetEase(Ease.Linear);
        }

        public void OnPointerClick(PointerEventData eventData) =>
            Clicked?.Invoke();

        public void ChangeSpriteSize()
        {
            return;
            RectTransform rectTransform = _textImage.rectTransform;
            //rectTransform.sizeDelta = new Vector2(_textImage.sprite.rect.width, _textImage.sprite.rect.height);
        }
    }
}