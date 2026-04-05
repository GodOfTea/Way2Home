using System.Collections;
using System.Collections.Generic;
using Infrastructure.Services.Audio;
using UnityEngine;

public class AudioHandler : MonoBehaviour
{
    public static AudioHandler Instance;
    
    [Header("Sounds")]
    [SerializeField] private AudioTrack _tutorialStart;
    
    [Header("Musics")] 
    [SerializeField] private AudioTrack _loseMusic;
    [SerializeField] private AudioTrack _winMusic;
    [SerializeField] private AudioTrack[] _backgroundMusics;
    
    private Dictionary<AudioTrack, AudioClipSettings> _musicClips;

    private AudioTrack _currentBackgroundMusic;
    private bool _isBackgroundMusicPlaying = true;
    
    public AudioTrack TutorialStart
    {
        get
        {
            _currentBackgroundMusic = _tutorialStart;
            return _tutorialStart;
        }
    }

    private void Awake()
    {
        Instance = this;
        CollectClips();
    }

    public void PlayMusic()
    {
        StartCoroutine(MusicLoop());
        _isBackgroundMusicPlaying = true;
    }
    
    public void PlayEndGameMusic(bool isWin)
    {
        StopMusic();
        
        if (isWin) _winMusic.PlayLoop();
        else _loseMusic.PlayLoop();
    }

    public void StopSounds()
    {
        StopMusic();
        
        _loseMusic.StopLoop();
        _winMusic.StopLoop();
    }

    public void StopMusic()
    {
        _isBackgroundMusicPlaying = false;
        _currentBackgroundMusic?.PauseMusic(0.5f);
        StopCoroutine(MusicLoop());
    }

    private void CollectClips()
    {
        _musicClips = new Dictionary<AudioTrack, AudioClipSettings>();
        _backgroundMusics.Shuffle();
        foreach (var clip in _backgroundMusics)
            _musicClips.Add(clip, clip.ClipsSettings[0]);

    }

    private IEnumerator MusicLoop()
    {
        _currentBackgroundMusic = null;
        foreach (var musicClip in _musicClips)
        {
            float time = musicClip.Value.Clip.length;
            _currentBackgroundMusic = musicClip.Key;
            _currentBackgroundMusic.PlayMusic();

            yield return new WaitForSeconds(time - 0.5f);
            _currentBackgroundMusic.PauseMusic(0.5f);
        }
        
        if (_isBackgroundMusicPlaying)
            PlayMusic();
    }
}
