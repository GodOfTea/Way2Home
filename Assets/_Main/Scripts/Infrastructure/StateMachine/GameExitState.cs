using Infrastructure.Services.SaveLoad;
using UnityEngine;

namespace Infrastructure.Services.State
{
    public class GameExitState : IState
    {
        private ISaveLoadService _saveLoad;
        
        public GameExitState(ISaveLoadService saveLoad)
        {
            _saveLoad = saveLoad;
        }

        public void Enter()
        {
            _saveLoad.SaveProgress();
            
#if UNITY_STANDALONE
            Application.Quit();
#endif
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
        }

        public void Exit()
        {
            
        }
    }
}