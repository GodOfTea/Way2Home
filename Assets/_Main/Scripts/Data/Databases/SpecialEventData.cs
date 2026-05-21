using System;
using Enumeration;
using UnityEngine;
using Event = EventComponents.Event;

namespace Data
{
    [Serializable]
    public class SpecialEventData
    {
        [SerializeField] private string _name;
        [SerializeField] private IndicatorType _eventIndicator;
        [SerializeField] private int _eventReloadTime;
        [SerializeField] private Event _event;

        public IndicatorType EventIndicator => _eventIndicator;
        public int EventReloadTime => _eventReloadTime;
        public Event Event => _event;
        
        public void SetEvent(Event e)
        {
            _event = e;
        }
    }
}