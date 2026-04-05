using System.Collections.Generic;
using Data;
using Enumeration;
using Game.Economy;

namespace EventComponents
{
    public class SpecialEventsHandler
    {
        private Dictionary<IndicatorType, SpecialEventGroup> _groups;

        public SpecialEventsHandler(EventsDatabase eventsDatabase, Dictionary<IndicatorType, IRedZoneValue> redZonesMap)
        {
            _groups = new Dictionary<IndicatorType, SpecialEventGroup>();
            
            foreach (var data in eventsDatabase.SpecialEvents)
            {
                _groups.TryAdd(data.EventIndicator, new SpecialEventGroup());
                
                _groups[data.EventIndicator].AddEvent(
                    new SpecialEvent(data.Event, data.EventReloadTime));
            }

            foreach (var redZone in redZonesMap)
            {
                if (_groups.ContainsKey(redZone.Key))
                    _groups[redZone.Key].AddData(redZone.Value.RedZoneValue, redZone.Value.IsLess);
            }
        }
        
        public Event TryGetEvent(IndicatorType indicatorType)
        {
            Event question = null;

            if (_groups.ContainsKey(indicatorType))
            {
                question = _groups[indicatorType].GetEvent();
            }

            return question;
        }

        public void ReduceTimerOnEvents()
        {
            foreach (var group in _groups)
                group.Value.ReduceReloadTime();
        }
        
        private class SpecialEventGroup
        {
            private List<SpecialEvent> _specialEvents;

            private int _triggerValue;
            private int _reloadTime;
            private bool _isLess;

            public SpecialEventGroup()
            {
                _specialEvents = new List<SpecialEvent>();
                
                _reloadTime = 0;
            }

            public void AddData(int triggerValue, bool isLess)
            {
                _triggerValue = triggerValue;
                _isLess = isLess;
            }

            public void AddEvent(SpecialEvent newEvent)
            {
                _specialEvents.Add(newEvent);
            }

            public SpecialEvent GetEvent()
            {
                SpecialEvent eventToUse = null;
                
                if (_reloadTime > 0)
                    return eventToUse;

                foreach (var specialEvent in _specialEvents)
                {
                    if (specialEvent.CanUse == false)
                        continue;
                    
                    eventToUse = specialEvent;
                    eventToUse.SetAsUse();
                    _reloadTime = eventToUse.EventsToReload;
                    break;
                }

                /* Раскомментировать, если нужен повтор спец событий */
                // if (eventToUse == null)
                // {
                //     foreach (var specialEvent in _specialEvents)
                //         specialEvent.Reload();
                //
                //     eventToUse = _specialEvents[0];
                // }
                //
                // eventToUse.SetAsUse();
                // _reloadTime = eventToUse.EventsToReload;
                return eventToUse;

            }

            public void ReduceReloadTime()
            {
                if (_reloadTime > 0)
                    _reloadTime -= 1;
            }
        }
    }
}