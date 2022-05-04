namespace Utils
{
    public static class SharedValues
    {
        public static float Volume { get; private set; } = 100;

        public static void SetVolume(float volume)
        {
            Volume = volume;
        }
    }
}
