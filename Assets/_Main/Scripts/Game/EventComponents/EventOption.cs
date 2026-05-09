using System;
using System.Linq;
using Editor.GoogleDataImporter;
using UnityEngine;

namespace EventComponents
{
    [Serializable]
    public class EventOption : IAnswer
    {
        [SerializeField] public string _id;
        [SerializeField] private EventOptionResult[] _results;
        
        public string AnswerId => _id;

        public EventOption(string id, EventsResult.Row resultRef)
        {
            _id = id;
            _results = new EventOptionResult[3];
            for (int i = 0; i < _results.Length; i++)
            {
                _results[i] = new EventOptionResult(resultRef.Id, resultRef.GetOption(i));
            }
        }
        
        public EventOptionResult GetResultByMoral(int moraleStatus) =>
            _results.FirstOrDefault(o => o.MoraleStatus == moraleStatus);
    }

    public interface IAnswer
    {
        string AnswerId { get; }
    }
}