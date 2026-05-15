using System;
using System.Linq;
using Data;
using Editor.GoogleDataImporter;
using UnityEngine;

namespace EventComponents
{
    [Serializable]
    public class Event
    {
        [SerializeField] private string _id;
        [SerializeField] private Sprite _eventImage;
        [SerializeField] private EventOption[] _options;

        public string ID => _id;
        public Sprite EventImage => _eventImage;

        public Event(EventSheet.Row eventData, EventsResult.Row[] resultData)
        {
            _id = eventData.Id;
            _eventImage = Resources.Load<Sprite>(Paths.EVENT_IMAGES + eventData.ImagePath);

            _options = new EventOption[3];
            for (int i = 0; i < 3; i++)
            {
                EventSheet.Elem option = eventData.GetOption(i);
                _options[i] = new EventOption(option.OptionKey, resultData[i]);
            }
        }

        public IAnswer[] GetAnswers() => _options;
        public EventOptionResult GetResult(string answerId, int moralStatus)
        {
            var option = _options.FirstOrDefault(o => o.AnswerId == answerId);
            var result = option.GetResultByMoral(moralStatus);
            return result;
        }
    }
}
