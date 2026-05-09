using Data;
using Editor.GoogleDataImporter;
using Enumeration;
using UnityEngine;

namespace Game.ResultComponents
{
    public class Result
    {
        private string _id;
        private string _name;
        private Sprite _resultImage;
        private IndicatorType _resultType;
        private string _description;

        public string ID => _id;
        public string Description => _description;
        public Sprite ResultImage => _resultImage;
        public IndicatorType ResultType => _resultType;

        public Result(GameResult.Reference reference)
        {
            _id = reference.Ref.Id;
            _name = reference.Ref.Name;
            _description = reference.Ref.Description;
            _resultImage = GetResultImage(reference.Ref.ImagePath);
            _resultType = GetIndicatorType(reference.Ref.Indicator);
        }

        private Sprite GetResultImage(string imagePath) => 
            Resources.Load<Sprite>(Paths.RESULT_IMAGES + imagePath);

        private IndicatorType GetIndicatorType(string type) =>
            type switch
            {
                "People" => IndicatorType.People,
                "Supplies" => IndicatorType.Supplies,
                "Risk" => IndicatorType.Risk,
                "Days" => IndicatorType.Days,
                _ => IndicatorType.People
            };
    }
}