using System.Collections.Generic;
using Data;
using Enumeration;
using Game.Economy;
using UnityEngine;

namespace Game.Cnofigs
{
    [CreateAssetMenu(fileName = "EventsConfig", menuName = "Configs/EventsConfig")]
    public class EventsConfig : ScriptableObject
    {
        [Header("Events")]
        [SerializeField] private EventsDatabase _eventsDatabase;
        [SerializeField] private ResultsDatabase _resultsDatabase;
        
        [Header("Indicators")]
        [SerializeField] private IndicatorType[] _indicatorPriority;

        [Space]
        [SerializeField] private IndicatorRedZone[] _redZones;

        public IReadOnlyList<IndicatorType> IndicatorPriority => _indicatorPriority;
        public IReadOnlyList<IndicatorRedZone> RedZones => _redZones;
        
        public (EventsDatabase, ResultsDatabase) GetDatabases() =>
            (_eventsDatabase, _resultsDatabase);
    }
}