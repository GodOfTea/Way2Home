using System.Linq;
using Data;
using Editor.GoogleDataImporter;
using Game.Economy;
using UnityEngine;

namespace EventComponents
{
    public class Event
    {
        private string _id;
        private Sprite _eventImage;
        private EventOption[] _options;

        public string ID => _id;
        public Sprite EventImage => _eventImage;

        public Event(EventSheet.Reference reference)
        {
            _id = reference.Ref.Id;
            _eventImage = Resources.Load<Sprite>(Paths.EVENT_IMAGES + reference.Ref.ImagePath);

            _options = new EventOption[3];
            for (int i = 0; i < 3; i++)
            {
                var option = reference.Ref.GetOption(i);
                _options[i] = ParseEventOption(option);
            }
        }

        private EventOption ParseEventOption(EventSheet.Elem option)
        {
            IndicatorsKeeper indicatorsKeeper = new IndicatorsKeeper(
                people: option.People, supplies: option.Supplies, risk: option.Risk, days: option.Days);

            Sprite answerImage = Resources.Load<Sprite>(Paths.EVENT_IMAGES + option.ImagePathAnswer);
            return new EventOption(option.OptionKey, option.OptionResultKey, 
                answerImage, indicatorsKeeper);
        }

        public IAnswer[] GetAnswers() => _options;
        public IAnswerResult GetResult(string id) => _options.FirstOrDefault(o => o.AnswerId == id);
    }
}
