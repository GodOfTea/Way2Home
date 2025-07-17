using UnityEngine;
using Random = UnityEngine.Random;
using System.Collections.Generic;

namespace Infrastructure.Services.Audio
{
    public class AudioTrack : MonoBehaviour
    {
        [SerializeField] private List<AudioClipSettings> _clipSettings =
            new List<AudioClipSettings>() { new AudioClipSettings() };

        private IAudioService _audioService;
        private int _lastIndex;
        private int _newIndex;

        public List<AudioClipSettings> ClipsSettings => _clipSettings;

        private void Start()
        {
            InitAudioService();
        }
        

        public void ChangeVolume(float volume, float smoothTime = 0f)
        {
            volume = Mathf.Clamp01(volume);
            _audioService.ChangeTrack(this, volume, ChangeType.Volume, smoothTime);
        }

        /// <summary>
        /// Though in Unity pitch can be from -3 to 3, in Luna negative pitch is not supported.
        /// </summary>
        public void ChangePitch(float pitch, float smoothTime = 0f)
        {
            pitch = Mathf.Clamp(pitch, -3f, 3f);
            _audioService.ChangeTrack(this, pitch, ChangeType.Pitch, smoothTime);

            if (pitch < 0)
                Debug.LogWarning("Negative pitch is not supported in Luna.");
        }

        /// <summary>
        /// Plays a random clip from the ClipSettings list. If AudioTrack.Play() is already playing then it will be restarted.
        /// </summary>
        public void Play()
        {
            if (_audioService == null)
                InitAudioService();
            
            _audioService.Play(this);
        }

        /// <summary>
        /// Stops current clip that is being played by AudioTrack.Play().
        /// </summary>
        public void Stop()
        {
            if (_audioService == null)
                InitAudioService();
            
            _audioService.Stop(this);
        }

        /// <summary>
        /// Plays a random clip from the ClipSettings list. Does not cancel clips that are already playing.
        /// </summary>
        public void PlayOneShot()
        {
            if (_audioService == null)
                InitAudioService();
            
            _audioService.PlayOneShot(this);
        }

        public void PlayLoop()
        {
            if (_audioService == null)
                InitAudioService();
            
            _audioService.PlayLoop(this);
        }

        public void StopLoop()
        {
            if (_audioService == null)
                InitAudioService();
            
            _audioService.StopLoop(this);
        }

        public void PlayMusic(float smoothTime = 0f)
        {
            if (_audioService == null)
                InitAudioService();
            
            _audioService.PlayMusic(this, smoothTime);
        }

        public void PauseMusic(float smoothTime = 0f)
        {
            if (_audioService == null)
                InitAudioService();
            
            _audioService.PauseMusic(this, smoothTime);
        }

        public void ContinueMusic(float smoothTime = 0f)
        {
            if (_audioService == null)
                InitAudioService();
            
            _audioService.ContinueMusic(this, smoothTime);
        }

        internal AudioClipSettings GetRandomAudioClipSettings()
        {
            return ClipsSettings.Count <= 1 ? ClipsSettings[0] : ClipsSettings[GetRandomNotRepeatIndex()];
        }

        private int GetRandomNotRepeatIndex()
        {
            while (_lastIndex == _newIndex)
                _newIndex = Random.Range(0, ClipsSettings.Count);

            _lastIndex = _newIndex;
            
            return _lastIndex;
        }
        
        private void InitAudioService() => _audioService = AllServices.Container.Single<IAudioService>();
    }
}
