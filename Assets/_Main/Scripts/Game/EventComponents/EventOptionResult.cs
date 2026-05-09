using System;
using Data;
using Editor.GoogleDataImporter;
using Enumeration;
using Game.Economy;
using UnityEngine;

namespace EventComponents
{
    [Serializable]
    public class EventOptionResult
    {
        [SerializeField] public string _id;
        [SerializeField] private int _moraleStatus;
        [SerializeField] private Sprite _resultImage;
        [SerializeField] private int _moraleChange;

        [Space]
        [SerializeField, TextArea] private string _indicatorsValue;

        private EventsResult.Elem _resultData;
        private IndicatorsKeeper _indicatorsKeeper;

        public string ID => _id;
        public int MoraleStatus => _moraleStatus;
        public Sprite ResultImage => _resultImage;
        public int MoraleChange => _moraleChange;
        public IndicatorsKeeper IndicatorsKeeper => _indicatorsKeeper;

        /* TODO: Это место не работает. Null Ref, возможно инициализация на старте нужна, увы */
        public EventOptionResult(string id, EventsResult.Elem resultData)
        {
            _resultData = resultData;
            _id = resultData.OptionEndingKey;
            _moraleStatus = resultData.MoraleStatus;
            _resultImage = Resources.Load<Sprite>(Paths.EVENT_IMAGES + resultData.ImagePathAnswer);
            _moraleChange = resultData.Morale;
            
            _indicatorsValue = " People: " + resultData.People + 
                               "\n Supplies: " + resultData.Supplies + 
                               "\n Risk: " + resultData.Risk + 
                               "\n Days: " + resultData.Days;
        }

        public void FillIndicators()
        {
            _indicatorsKeeper = new IndicatorsKeeper(
                people: _resultData.People, 
                supplies: _resultData.Supplies, 
                risk: _resultData.Risk, 
                days: _resultData.Days);
        }
    }
}