using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Event
{
    [RequireComponent(typeof(Button))]
    public class ContinueButton : MonoBehaviour
    {
        private Button _button;

        public event Action Clicked;
        
        private void OnEnable()
        {
            _button ??= GetComponent<Button>();
            
            _button.onClick.AddListener(ShowNextEvent);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(ShowNextEvent);
        }

        private void ShowNextEvent()
        {
            Clicked?.Invoke();
        }
    }
}