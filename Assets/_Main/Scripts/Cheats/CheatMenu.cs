using Enumeration;
using Game.Economy;
using UnityEngine;

namespace Main.Editor
{
    public class CheatMenu : MonoBehaviour
    {
        private int _supplies;
        private int _people;
        private int _risk;
        private int _days;

        private string _eventIdInput = "Event_1";
        private int _moraleInput = 50;

        private bool _isActivated;
        private IndicatorsBank _indicatorsBank;
        private Game.Gameplay _gameplay;

        public void SetIndicatorsBank(IndicatorsBank indicatorsBank)
        {
            _indicatorsBank = indicatorsBank;
        }
        
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.C))
            {
                _isActivated = !_isActivated;
                Debug.LogWarning("Cheat menu is " + (_isActivated ? "activated" : "deactivated"));

                if (_isActivated)
                {
                    _gameplay = FindAnyObjectByType<Game.Gameplay>();
                }
            }
            
            EventsInputs();

            if (!_isActivated)
                return;
            
            if (Input.GetKeyDown(KeyCode.F1))
                _indicatorsBank.UpdateIndicatorValue(IndicatorType.People, 10);
            if (Input.GetKeyDown(KeyCode.F2))
                _indicatorsBank.UpdateIndicatorValue(IndicatorType.People, -10);
            
            if (Input.GetKeyDown(KeyCode.F3))
                _indicatorsBank.UpdateIndicatorValue(IndicatorType.Supplies, 10);
            if (Input.GetKeyDown(KeyCode.F4))
                _indicatorsBank.UpdateIndicatorValue(IndicatorType.Supplies, -10);
            
            if (Input.GetKeyDown(KeyCode.F5))
                _indicatorsBank.UpdateIndicatorValue(IndicatorType.Risk, 5);
            if (Input.GetKeyDown(KeyCode.F6))
                _indicatorsBank.UpdateIndicatorValue(IndicatorType.Risk, -5);
            
            if (Input.GetKeyDown(KeyCode.F7))
                _indicatorsBank.UpdateIndicatorValue(IndicatorType.Days, 1);
            if (Input.GetKeyDown(KeyCode.F8))
                _indicatorsBank.UpdateIndicatorValue(IndicatorType.Days, -1);
        }

        private static void EventsInputs()
        {
            /* Options */
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                //chose first option
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                //chose second option
            }
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                //chose third option
            }

            if (Input.GetKeyDown(KeyCode.F))
            {
                //apply event result
            }
        }

        private void OnGUI()
        {
            if (!_isActivated) return;

            GUILayout.BeginArea(new Rect(10, 10, 450, 600), GUI.skin.box);
            GUILayout.Label("Cheat Menu (C to hide)");

            GUILayout.Space(10);
            GUILayout.Label("Indicators:");
            
            GUILayout.BeginHorizontal();
            GUILayout.Label("People");
            string peopleInput = GUILayout.TextField(_people.ToString(), GUILayout.Width(50));
            if (peopleInput != "-" && int.TryParse(peopleInput, out int p)) _people = p;
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.Label("Supplies");
            string suppliesInput = GUILayout.TextField(_supplies.ToString(), GUILayout.Width(50));
            if (suppliesInput != "-" && int.TryParse(suppliesInput, out int s)) _supplies = s;
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.Label("Risk");
            string riskInput = GUILayout.TextField(_risk.ToString(), GUILayout.Width(50));
            if (riskInput != "-" && int.TryParse(riskInput, out int r)) _risk = r;
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.Label("Days");
            string daysInput = GUILayout.TextField(_days.ToString(), GUILayout.Width(50));
            if (daysInput != "-" && int.TryParse(daysInput, out int d)) _days = d;
            GUILayout.EndHorizontal();

            if (GUILayout.Button("Set Indicators"))
            {
                if (_indicatorsBank != null)
                {
                    _indicatorsBank.SetIndicatorValue(IndicatorType.People, _people);
                    _indicatorsBank.SetIndicatorValue(IndicatorType.Supplies, _supplies);
                    _indicatorsBank.SetIndicatorValue(IndicatorType.Risk, _risk);
                    _indicatorsBank.SetIndicatorValue(IndicatorType.Days, _days);
                    _indicatorsBank.SetIndicators();
                }
            }

            GUILayout.Space(10);
            GUILayout.Label("Morale:");
            string moraleStr = GUILayout.TextField(_moraleInput.ToString(), GUILayout.Width(50));
            if (moraleStr != "-" && int.TryParse(moraleStr, out int m)) _moraleInput = m;
            if (GUILayout.Button("Set Morale"))
            {
                if (_indicatorsBank != null && _indicatorsBank.Moral != null)
                {
                    _indicatorsBank.Moral.SetNewMoralValue(_moraleInput);
                    _indicatorsBank.SetIndicators();
                }
            }

            GUILayout.Space(10);
            GUILayout.Label("Run Event:");
            _eventIdInput = GUILayout.TextField(_eventIdInput);
            if (GUILayout.Button("Run Specific Event"))
            {
                _gameplay.ShowNextEvent(_eventIdInput);
                Debug.Log($"Cheat: Trying to run event {_eventIdInput}");
            }

            GUILayout.EndArea();
        }
    }
}