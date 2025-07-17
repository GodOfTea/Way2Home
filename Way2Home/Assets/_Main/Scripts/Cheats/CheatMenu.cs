using Enumeration;
using Game.Economy;
using UnityEngine;

namespace Main.Editor
{
    public class CheatMenu : MonoBehaviour
    {
        [SerializeField] private int _supplies;
        [SerializeField] private int _people;
        [SerializeField] private int _risk;
        [SerializeField] private int _days;

        private bool _isActivated;
        private IndicatorsBank _indicatorsBank;

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
    }
}