using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Game
{
    [RequireComponent(typeof(Button))]
    public class GameplayOpenMenuButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        private readonly float _fillDuration = .1f;
        
        [SerializeField] private Image _filler;
        
        private Button _button;
        
        public event Action OpenMenuPressed;

        public void Show() =>
            _button.onClick.AddListener(OpenMenu);

        public void Hide() =>
            _button.onClick.RemoveListener(OpenMenu);

        public void OnPointerEnter(PointerEventData eventData) =>
            _filler.DOFillAmount(1f, _fillDuration).SetEase(Ease.Linear);

        public void OnPointerExit(PointerEventData eventData) =>
            _filler.DOFillAmount(0f, _fillDuration).SetEase(Ease.Linear);

        private void Awake()
        {
            _button = GetComponent<Button>();
        }

        private void OpenMenu()
        {
            OpenMenuPressed?.Invoke();
        }
    }
}