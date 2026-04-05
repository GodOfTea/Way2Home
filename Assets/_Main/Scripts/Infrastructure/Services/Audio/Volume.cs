namespace Infrastructure.Services.Audio
{
    internal class Volume
    {
        public float Music { get; private set; }
        public float Sound { get; private set; }

        internal Volume(float music, float sound) => SetValues(music, sound);

        public void SetValues(float music, float sound)
        {
            Music = music;
            Sound = sound;
        }
    }
}