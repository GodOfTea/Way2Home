using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using System.Threading;

namespace Infrastructure.Services.Audio
{
    internal class Volumes
    {
        private Dictionary<AudioSource, CancellationTokenSource> _volumeTokenSources = new Dictionary<AudioSource, CancellationTokenSource>();
        private Dictionary<AudioSource, CancellationTokenSource> _pitchTokenSources = new Dictionary<AudioSource, CancellationTokenSource>();
        private Dictionary<string, Volume> _volumes = new Dictionary<string, Volume>();
        private bool _onPause;

        public Volumes(string key) => AddVolume(key, new Volume(1f, 1f));

        public void AddVolume(string key, Volume volume) => _volumes[key] = volume;

        public Volume GetVolume(string key) => _volumes[key];

        public void SetVolume(string key, float musicVolume, float soundVolume) => _volumes[key].SetValues(musicVolume, soundVolume);

        public float ProcessMusicVolume(float startVolume = 1f)
        {
            foreach (var volumeSetting in _volumes)
                startVolume *= volumeSetting.Value.Music;

            return startVolume;
        }
        
        public float ProcessSoundVolume(float startVolume = 1f)
        {
            foreach (var volumeSetting in _volumes)
                startVolume *= volumeSetting.Value.Sound;

            return startVolume;
        }

        public void ChangeVolumeSmooth(float endVolume, float duration, AudioSource source, bool isMusic)
        {
            _onPause = endVolume <= 0;
            
            if (isMusic && !_onPause)
                source.UnPause();
            
            if (_volumeTokenSources.ContainsKey(source) && _volumeTokenSources[source] != null)
            {
                _volumeTokenSources[source].Cancel();
                _volumeTokenSources[source].Dispose();
                _volumeTokenSources[source] = null;
            }

            if (duration > 0f)
            {
                CancellationTokenSource tokenSource = new CancellationTokenSource();
                _volumeTokenSources[source] = tokenSource;
                SmoothVolume(source, endVolume, duration, isMusic, tokenSource.Token);
            }
            else
            {
                if (isMusic && _onPause)
                    source.Pause();

                if (isMusic)
                    source.volume = ProcessMusicVolume(endVolume);
                else
                    source.volume = ProcessSoundVolume(endVolume);
            }
        }

        public void ChangePitchSmooth(float endPitch, float duration, AudioSource source)
        {
            if (_pitchTokenSources.ContainsKey(source) && _pitchTokenSources[source] != null)
            {
                _pitchTokenSources[source].Cancel();
                _pitchTokenSources[source].Dispose();
                _pitchTokenSources[source] = null;
            }

            if (duration > 0f)
            {
                CancellationTokenSource tokenSource = new CancellationTokenSource();
                _pitchTokenSources[source] = tokenSource;
                SmoothPitch(source, endPitch, duration, tokenSource.Token);
            }
            else
                source.pitch = endPitch;
        }

        private async Task SmoothVolume(AudioSource source, float endVolume, float duration, bool isMusic, CancellationToken token)
        {
            var startVolume = source.volume;
            var interpolant = 0f;

            while (interpolant < duration)
            {
                await Task.Delay(100, token);
                interpolant += 0.1f;

                if (isMusic)
                    source.volume = ProcessMusicVolume(Mathf.Lerp(startVolume, endVolume, interpolant / duration));
                else
                    source.volume = ProcessSoundVolume(Mathf.Lerp(startVolume, endVolume, interpolant / duration));
            }

            if (isMusic && _onPause)
                source.Pause();

            if (isMusic)
                source.volume = ProcessMusicVolume(endVolume);
            else
                source.volume = ProcessSoundVolume(endVolume);
        }

        private async Task SmoothPitch(AudioSource source, float endPitch, float duration, CancellationToken token)
        {
            var startPitch = source.pitch;
            var interpolant = 0f;

            while (interpolant < duration)
            {
                await Task.Delay(100, token);
                interpolant += 0.1f;
                source.pitch = Mathf.Lerp(startPitch, endPitch, interpolant / duration);
            }

            source.pitch = endPitch;
        }
    }
}