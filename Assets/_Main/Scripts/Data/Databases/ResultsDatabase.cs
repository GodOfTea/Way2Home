using Editor.GoogleDataImporter;
using Enumeration;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "Results Database", menuName = "Databases/Results Database", order = 0)]
    public class ResultsDatabase : ScriptableObject
    {
        [SerializeField] private ResultSheet.Reference[] _results;

        public ResultSheet.Reference[] Results => _results;
        
        public ResultSheet.Reference GetResult(IndicatorType type)
        {
            foreach (var result in _results)
            {
                if (result.Ref.Indicator == type.ToString())
                    return result;
            }

            throw new System.ArgumentException($"Result for indicator {type} not found");
        }
    }
}