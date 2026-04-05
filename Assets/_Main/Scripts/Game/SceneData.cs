using Game.UI.ScreensComponents;
using UnityEngine;

namespace Game
{
    public class SceneData : MonoBehaviour
    {
        [SerializeField] private EventView _eventView;
        [SerializeField] private Screens _screens;

        public EventView EventView => _eventView;
        public Screens Screens => _screens;
    }
}