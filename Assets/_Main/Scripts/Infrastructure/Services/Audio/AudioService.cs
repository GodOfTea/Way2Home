using UnityEngine;
using System.Collections.Generic;
using Infrastructure.Services.Settings;

namespace Infrastructure.Services.Audio
{
    public enum ChangeType
    {
        Volume,
        Pitch
    }

    public class AudioService : IAudioService
    {
        private readonly ISettings _settingsService;

        private Sources _sources;
        private Volumes _volumes = new(_gameVolume);
        private AudioClipSettings _currentSoundSettings;
        private Dictionary<AudioTrack, AudioClipSettings> _currentLoopSettings = new Dictionary<AudioTrack, AudioClipSettings>();
        private Dictionary<AudioTrack, AudioClipSettings> _currentMusicSettings = new Dictionary<AudioTrack, AudioClipSettings>();

        public AudioService(ISettings settingsService)
        {
            _settingsService = settingsService;
            _sources = new Sources(_settingsService.SoundsMixerGroup, _settingsService.MusicMixerGroup);
        }

        private const string _gameVolume = "GameVolume";

        public void ExpandPoolTo(int size) => _sources.ExpandPoolTo(size);

        public void AddVolumeSettings(string key, float music, float sound) => _volumes.AddVolume(key, new Volume(music, sound));

        public void SetMusicVolume(float volume)
        {
            _volumes.SetVolume(_gameVolume, volume, _volumes.GetVolume(_gameVolume).Sound);

            foreach (var sourcePair in _sources.MusicSources)
                if (_currentMusicSettings.ContainsKey(sourcePair.Key))
                    sourcePair.Value.volume = _volumes.ProcessMusicVolume(_currentMusicSettings[sourcePair.Key].Volume);
        }

        public void SetSoundVolume(float volume)
        {
            _volumes.SetVolume(_gameVolume, _volumes.GetVolume(_gameVolume).Music, volume);
            _sources.SoundSource.volume = _volumes.ProcessSoundVolume(_currentSoundSettings?.Volume ?? 1f);

            foreach (var sourcePair in _sources.LoopSources)
                if (_currentLoopSettings.ContainsKey(sourcePair.Key))
                    sourcePair.Value.volume = _volumes.ProcessSoundVolume(_currentLoopSettings[sourcePair.Key].Volume);

            foreach (var sourcePair in _sources.OneShotSources)
                if (sourcePair.Value != null)
                    sourcePair.Key.volume = _volumes.ProcessSoundVolume(sourcePair.Value.Volume);
        }

        public void ChangeTrack(AudioTrack track, float value, ChangeType type, float smoothTime)
        {
            ChangeSoundAndOneShot(track, value, type, smoothTime);
            ChangeLoop(track, value, type, smoothTime);
            ChangeMusic(track, value, type, smoothTime);
        }

        public void Play(AudioTrack track)
        {
            _currentSoundSettings = track.GetRandomAudioClipSettings();
            AudioSource source = _sources.SoundSource;
            source.Stop();
            source.clip = _currentSoundSettings.Clip;
            source.volume = _volumes.ProcessSoundVolume(_currentSoundSettings.Volume);
            source.pitch = _currentSoundSettings.Pitch;
            source.Play();
        }

        public void Stop() => _sources.SoundSource.Stop();

        public void Stop(AudioTrack track)
        {
            foreach (AudioClipSettings clipSettings in track.ClipsSettings)
            {
                if (clipSettings == _currentSoundSettings)
                {
                    _sources.SoundSource.Stop();
                    return;
                }
            }
        }

        public void PlayOneShot(AudioTrack track)
        {
            AudioClipSettings clipSettings = track.GetRandomAudioClipSettings();
            AudioSource source = _sources.GetOneShotSoundSource(clipSettings);
            source.clip = clipSettings.Clip;
            source.volume = _volumes.ProcessSoundVolume(clipSettings.Volume);
            source.pitch = clipSettings.Pitch;
            source.Play();
        }

        public void PlayLoop(AudioTrack track)
        {
            AudioClipSettings clipSettings = track.GetRandomAudioClipSettings();
            _currentLoopSettings[track] = clipSettings;
            AudioSource source = _sources.GetLoopSoundSource(track);
            source.clip = clipSettings.Clip;
            source.volume = _volumes.ProcessSoundVolume(clipSettings.Volume);
            source.pitch = clipSettings.Pitch;
            source.loop = true;
            source.Play();
        }

        public void StopLoop(AudioTrack track)
        {
            AudioSource source = _sources.GetLoopSoundSource(track);
            source.Stop();
        }

        public void PlayMusic(AudioTrack track, float smoothTime)
        {
            AudioClipSettings clipSettings = track.GetRandomAudioClipSettings();
            _currentMusicSettings[track] = clipSettings;
            AudioSource source = _sources.GetMusicSource(track);
            source.clip = clipSettings.Clip;
            source.volume = 0;
            source.pitch = clipSettings.Pitch;
            source.loop = true;
            source.Play();
            _volumes.ChangeVolumeSmooth(clipSettings.Volume, smoothTime, source, true);
        }

        public void PauseMusic(AudioTrack track, float smoothTime)
        {
            AudioSource source = _sources.GetMusicSource(track);
            _volumes.ChangeVolumeSmooth(0, smoothTime, source, true);
        }

        public void ContinueMusic(AudioTrack track, float smoothTime)
        {
            if (_currentMusicSettings.ContainsKey(track) == false)
                return;

            AudioSource source = _sources.GetMusicSource(track);
            _volumes.ChangeVolumeSmooth(_currentMusicSettings[track].Volume, smoothTime, source, true);
        }

        private void ChangeSoundAndOneShot(AudioTrack track, float value, ChangeType type, float smoothTime)
        {
            foreach (AudioClipSettings clipSettings in track.ClipsSettings)
            {
                if (clipSettings == _currentSoundSettings && type == ChangeType.Volume)
                    _volumes.ChangeVolumeSmooth(clipSettings.Volume * value, smoothTime, _sources.SoundSource, false);
                else if (clipSettings == _currentSoundSettings)
                    _volumes.ChangePitchSmooth(value, smoothTime, _sources.SoundSource);


                foreach (var sourcePair in _sources.OneShotSources)
                {
                    if (sourcePair.Value == clipSettings && type == ChangeType.Volume)
                        _volumes.ChangeVolumeSmooth(clipSettings.Volume * value, smoothTime, sourcePair.Key, false);
                    else if (sourcePair.Value == clipSettings)
                        _volumes.ChangePitchSmooth(value, smoothTime, sourcePair.Key);

                }
            }
        }

        private void ChangeLoop(AudioTrack track, float value, ChangeType type, float smoothTime)
        {
            if (_currentLoopSettings.ContainsKey(track))
            {
                AudioClipSettings clipSettings = _currentLoopSettings[track];
                AudioSource source = _sources.GetLoopSoundSource(track);

                if (type == ChangeType.Volume)
                    _volumes.ChangeVolumeSmooth(clipSettings.Volume * value, smoothTime, source, false);
                else
                    _volumes.ChangePitchSmooth(value, smoothTime, source);
            }
        }

        private void ChangeMusic(AudioTrack track, float value, ChangeType type, float smoothTime)
        {
            if (_currentMusicSettings.ContainsKey(track))
            {
                AudioClipSettings clipSettings = _currentMusicSettings[track];
                AudioSource source = _sources.GetMusicSource(track);

                if (type == ChangeType.Volume)
                    _volumes.ChangeVolumeSmooth(clipSettings.Volume * value, smoothTime, source, true);
                else
                    _volumes.ChangePitchSmooth(value, smoothTime, source);
            }
        }

        private void Dispose()
        {
            if (_sources != null)
                _sources.Dispose();
        }
    }
}
