using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.UI.ScreensComponents
{
    public class Screens : MonoBehaviour
    {
        [SerializeField] private ScreenBase[] _screens;
        
        private void Awake()
        {
            _screens = GetComponentsInChildren<ScreenBase>(true);
            
            InitializeScreens();
            HideAllImmediate();
        }
        
        public T Get<T>() where T : ScreenBase
        {
            foreach (var screen in _screens)
            {
                if (screen.GetType() == typeof(T))
                {
                    return (T)screen;
                }
            }

            throw new Exception("Can't find screen");
        }
        
        public bool TryGet<T>(out T screen) where T : ScreenBase
        {
            screen = Get<T>();
            return screen != null;
        }
        
        public IEnumerable<ScreenBase> All => _screens;

        public void HideAllImmediate()
        {
            foreach (var screen in _screens)
            {
                screen.HideImmediate();
            }
        }

        private void InitializeScreens()
        {
            foreach (var screen in _screens)
            {
                screen.Initialize();
            }
        }
    }
}
