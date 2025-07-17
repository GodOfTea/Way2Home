using Infrastructure.Services.Audio;
using UnityEngine;

namespace Game.UI.MainMenu.Settings
{
    public abstract class SettingsField : MonoBehaviour
    {
        [SerializeField] private SwitchButton _previous;
        [SerializeField] private SwitchButton _next;

        [SerializeField] private AudioTrack _clickSound;

        private void OnEnable()
        {
            _previous.Pressed += PreviousOnPressed; 
            _next.Pressed += NextOnPressed;
        }

        private void OnDisable()
        {
            _previous.Pressed -= PreviousOnPressed; 
            _next.Pressed -= NextOnPressed;
        }

        protected virtual void PreviousOnPressed()
        {
            _clickSound.PlayOneShot();
        }

        protected virtual void NextOnPressed()
        {
            _clickSound.PlayOneShot();
        }
    }
}