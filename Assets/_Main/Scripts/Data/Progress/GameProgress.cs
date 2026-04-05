using System;

namespace Data.Progress
{
    [Serializable]
    public class GameProgress
    {
        public string MainMenuScene;
        
        public SettingsData SettingsData;
        public GameplayData GameplayData;

        public GameProgress(string mainMenuScene)
        {
            SettingsData = new SettingsData();
            GameplayData = new GameplayData();
            MainMenuScene = mainMenuScene;
        }
    }
}