using System;
using DG.Tweening;
using Game.UI.ScreensComponents.Gameplay;
using Infrastructure.Services.Audio;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Game
{
    [RequireComponent(typeof(Button))]
    public abstract class GameplayMenuButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        private const float DURATION = .25f;
        private const float SCALE_FACTOR = 1.1f;
        
        [SerializeField] private TextMeshProUGUI _buttonTitle;
        [SerializeField] private AudioTrack _clickSound;
        
        private readonly Color _defaultColor = Color.white;
        private readonly Color _selectedColor = Color.red;

        protected GameplayMenuScreen _menuScreen;
        private Button _button;

        private void Awake() =>
            _button = GetComponent<Button>();

        public void Show(GameplayMenuScreen menuScreen)
        {
            if (_menuScreen == null)
                _menuScreen = menuScreen;
            
            _button.onClick.AddListener(OnClicked);
        }

        public void Hide() => 
            _button.onClick.RemoveListener(OnClicked);

        public void OnPointerEnter(PointerEventData eventData)
        {
            _buttonTitle.DOColor(_selectedColor, DURATION).SetEase(Ease.Linear);
            transform.DOScale(Vector3.one * SCALE_FACTOR, DURATION).SetEase(Ease.Linear);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _buttonTitle.DOColor(_defaultColor, DURATION).SetEase(Ease.Linear);
            transform.DOScale(Vector3.one, DURATION).SetEase(Ease.Linear);
        }

        protected virtual void OnClicked()
        {
            _clickSound.PlayOneShot();
        }
    }
}