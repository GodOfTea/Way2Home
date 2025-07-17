using GodlikeShortcuts.Shortcuts;
using UnityEditor;
using UnityEngine;

namespace GodlikeShortcuts.Editor
{
    public class GodlikeShortcutsSettings : EditorWindow
    {
        public const string _roundToDecimal = "Round to 0.5";
        private const string _roundToOne = "Round to 1.0";
        private const string _roundToFive = "Round to 5.0";
        private const string _roundToTen = "Round to 10.0";

        public string[] RoundOptions = new string[]
        {
            _roundToDecimal, _roundToOne, _roundToFive, _roundToTen
        };
        
        [MenuItem("GodlikeShortcuts/Settings")]
        private static void ShowWindow()
        {
            var window = GetWindow<GodlikeShortcutsSettings>();
            window.titleContent = new GUIContent("GodlikeShortcuts");
            window.Show();
        }

        private void OnGUI()
        {
            GUILayout.Label("Settings", EditorStyles.boldLabel);

            TransformRound.RoundIndex = EditorGUILayout.Popup("Round Factor", TransformRound.RoundIndex, RoundOptions);
        }
    }
}