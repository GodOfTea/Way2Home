namespace Infrastructure.Services.Audio
{
    public interface IAudioService : IService
    {           
        void ExpandPoolTo(int size);
        void AddVolumeSettings(string key, float music, float sound);
        void SetMusicVolume(float volume);
        void SetSoundVolume(float volume);
        void ChangeTrack(AudioTrack track, float value, ChangeType type, float smoothTime);
        void Play(AudioTrack track);
        void Stop();
        void Stop(AudioTrack track);
        void PlayOneShot(AudioTrack track);
        void PlayLoop(AudioTrack track);
        void StopLoop(AudioTrack track);
        void PlayMusic(AudioTrack track, float smoothTime);
        void PauseMusic(AudioTrack track, float smoothTime);
        void ContinueMusic(AudioTrack track, float smoothTime);
    }
}