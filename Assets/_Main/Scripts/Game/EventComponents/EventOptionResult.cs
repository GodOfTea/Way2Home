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
        [SerializeField] private IndicatorsKeeper _indicatorsKeeper;

        public string ID => _id;
        public int MoraleStatus => _moraleStatus;
        public Sprite ResultImage => _resultImage;
        public int MoraleChange => _moraleChange;
        public IndicatorsKeeper IndicatorsKeeper => _indicatorsKeeper;

        public EventOptionResult(string id, EventsResult.Elem resultData)
        {
            _id = resultData.OptionEndingKey;
            _moraleStatus = resultData.MoraleStatus;
            _resultImage = Resources.Load<Sprite>(Paths.EVENT_IMAGES + resultData.ImagePathAnswer);
            _moraleChange = resultData.Morale;
            FillIndicators(resultData);
        }

        private void FillIndicators(EventsResult.Elem resultData)
        {
            _indicatorsKeeper = new IndicatorsKeeper(
                people: resultData.People, 
                supplies: resultData.Supplies, 
                risk: resultData.Risk, 
                days: resultData.Days);
        }
    }
}