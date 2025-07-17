using Editor.GoogleDataImporter;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "EventsDatabase", menuName = "Databases/EventsDatabase", order = 0)]
    public class EventsDatabase : ScriptableObject
    {
        [SerializeField] private EventSheet.Reference[] _mainEvents;
        [SerializeField] private SpecialEventData[] _specialEvents;
        
        public EventSheet.Reference[] MainEvents => _mainEvents;
        public SpecialEventData[] SpecialEvents => _specialEvents;
    }
}