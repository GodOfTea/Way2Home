using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace Infrastructure.Services.Audio
{
    internal class Sources : IDisposable
    {
        private readonly AudioMixerGroup _sounds;
        private readonly AudioMixerGroup _music;
        
        private GameObject _sourcesHolder;
        private const int _oneShotSourcesCount = 10;
        private AudioTrack _emptyTrack = new AudioTrack();

        public Sources(AudioMixerGroup sounds, AudioMixerGroup music)
        {
            _sounds = sounds;
            _music = music;
            _sourcesHolder = new GameObject("Sources");
            SoundSource = CreateAudioSource();
            
#if UNITY_EDITOR
            _sourcesHolder.AddComponent<AudioListener>();
#endif

            InitializeDictionary(MusicSources, _music);
            InitializeDictionary(LoopSources, _sounds);
            InitializeDictionary(OneShotSources, _sounds);
            UnityEngine.Object.DontDestroyOnLoad(_sourcesHolder);
        }

        public AudioSource SoundSource { get; private set; }
        public Dictionary<AudioTrack, AudioSource> MusicSources { get; private set; } = new Dictionary<AudioTrack, AudioSource>();
        public Dictionary<AudioTrack, AudioSource> LoopSources { get; private set; } = new Dictionary<AudioTrack, AudioSource>();
        public Dictionary<AudioSource, AudioClipSettings> OneShotSources { get; private set; } = new Dictionary<AudioSource, AudioClipSettings>();

        public void ExpandPoolTo(int size)
        {
            int missingCount = size - OneShotSources.Count;

            if (missingCount > 0)
            {
                for (int i = 0; i < missingCount; i++)
                {
                    AudioSource newSource = CreateAudioSource();
                    newSource.outputAudioMixerGroup = _sounds;
                    OneShotSources.Add(newSource, null);
                }
            }
        }

        public AudioSource GetMusicSource(AudioTrack key) => GetOrCreateSource(MusicSources, key, _music);

        public AudioSource GetLoopSoundSource(AudioTrack key) => GetOrCreateSource(LoopSources, key, _sounds);

        public AudioSource GetOneShotSoundSource(AudioClipSettings clipSettings)
        {
            foreach (var sourcePair in OneShotSources)
            {
                if (sourcePair.Key.isPlaying == false)
                {
                    OneShotSources[sourcePair.Key] = clipSettings;
                    return sourcePair.Key;
                }
            }

            AudioSource newSource = CreateAudioSource();
            newSource.outputAudioMixerGroup = _sounds;
            OneShotSources[newSource] = clipSettings;

            return newSource;
        }

        public void Mute()
        {
            SoundSource.mute = true;

            foreach (var sourcePair in OneShotSources)
                sourcePair.Key.mute = true;

            foreach (var sourcePair in LoopSources)
                sourcePair.Value.mute = true;

            foreach (var sourcePair in MusicSources)
                sourcePair.Value.mute = true;
        }

        public void UnMute()
        {
            SoundSource.mute = false;

            foreach (var sourcePair in OneShotSources)
                sourcePair.Key.mute = false;

            foreach (var sourcePair in LoopSources)
                sourcePair.Value.mute = false;

            foreach (var sourcePair in MusicSources)
                sourcePair.Value.mute = false;
        }

        public void Dispose()
        {
            if (_sourcesHolder)
                GameObject.Destroy(_sourcesHolder);
        }

        private void InitializeDictionary(IDictionary dictionary, AudioMixerGroup group)
        {
            AudioSource newSource;

            if (dictionary is Dictionary<AudioTrack, AudioSource>)
            {
                newSource = CreateAudioSource();
                newSource.outputAudioMixerGroup = group;
                dictionary.Add(_emptyTrack, newSource);
            }
            else if (dictionary is Dictionary<AudioSource, AudioClipSettings>)
            {
                for (int i = 0; i < _oneShotSourcesCount; i++)
                {
                    newSource = CreateAudioSource();
                    newSource.outputAudioMixerGroup = group;
                    dictionary.Add(newSource, null);
                }
            }
        }

        private AudioSource GetOrCreateSource(Dictionary<AudioTrack, AudioSource> dictionary, AudioTrack key, AudioMixerGroup group)
        {
            AudioSource source = null;

            if (dictionary.ContainsKey(key))
            {
                source = dictionary[key];
            }
            else if (dictionary.ContainsKey(_emptyTrack))
            {
                source = dictionary[_emptyTrack];
                dictionary.Remove(_emptyTrack);
                dictionary.Add(key, source);
            }
            else
            {
                source = CreateAudioSource();
                dictionary.Add(key, source);
            }

            source.outputAudioMixerGroup = group;
            return source;
        }

        private AudioSource CreateAudioSource()
        {
            AudioSource newSource = _sourcesHolder.AddComponent<AudioSource>();
            newSource.playOnAwake = false;

            if (SoundSource)
                newSource.mute = SoundSource.mute;

            return newSource;
        }
    }
}
