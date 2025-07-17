using System;
using System.Collections.Generic;
using Enumeration;
using Game.Economy;
using UnityEngine;

namespace EventComponents
{
    [Serializable]
    public class EventOption : IAnswer, IAnswerResult
    {
        [SerializeField] public string _id;
        [SerializeField] public string _resultKey;

        [SerializeField] private Sprite _answerImage;
        
        private IndicatorsKeeper _indicatorsKeeper;

        public string AnswerId => _id;
        public string ResultAnswerId => _resultKey;
        
        public Sprite AnswerImage => _answerImage;
        public IReadOnlyDictionary<IndicatorType, int> IndicatorsResultMap => _indicatorsKeeper.IndicatorsMap;

        public EventOption(string id, string resultKey, Sprite answerImage, IndicatorsKeeper indicatorsKeeper)
        {
            _id = id;
            _resultKey = resultKey;
            _answerImage = answerImage;
            _indicatorsKeeper = indicatorsKeeper;
        }
    }

    public interface IAnswer
    {
        string AnswerId { get; }
    }

    public interface IAnswerResult
    {
        string ResultAnswerId { get; }
        Sprite AnswerImage { get; }
        IReadOnlyDictionary<IndicatorType, int> IndicatorsResultMap { get; }
    }
}