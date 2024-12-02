namespace _Project.Code.Utils
{
    public static class Constants
    {
        public const string PlayerProgress = "/playerProgress.data";
        public const int DefaultCapacity = 32;
        
        public static class Time
        {
            public const int SecondsInMinute = 60;
            public const float PausedValue = 0f;
            public const float ResumedValue = 1f;
        }
        
        public static class Scene
        {
            public const string Game = "Game";
            public const string Lobby = "Lobby";
        }

        public static class Audio
        {
            public const float MinSliderValue = 0.0001f;
            public const float MaxSliderValue = 1f;
            public const float MuteValue = -80;
            public const float MaxValue = 20f;
            
            public const string Master = "Master";
            public const string Music = "Music";
            public const string SFX = "SFX";
        }
    }
}