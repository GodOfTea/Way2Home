using System;
using System.Collections.Generic;
using System.Linq;
using Data;
using Data.Scripts.Extension;
using Enumeration;
using Game.Cnofigs;
using Game.Economy;
using UnityEngine;

namespace EventComponents
{
    public class EventDispenser
    {
        private readonly EventsConfig _eventsConfig;
        private readonly EventsDatabase _eventsDatabase;
        private readonly IndicatorsBank _indicatorsBank;
        private Dictionary<IndicatorType, IRedZoneValue> _redZonesMap;

        private Dictionary<string, Event> _previousEvents;
        private Dictionary<string, Event> _possibleEvents;
        
        private SpecialEventsHandler _specialEventsHandler;

        private Event _currentEvent;
        private bool _currentEventIsSpecial;

        public Event CurrentEvent => _currentEvent;
        public List<string> PreviousEvents => _previousEvents.Keys.ToList();
        /* Подумать над передачей базы данных, не работает мгновенный перевод для ивентов, сделать табличку в GD? */
        public EventDispenser(EventsConfig eventsConfig, EventsDatabase eventsDatabase, IndicatorsBank indicatorsBank, List<string> previousEvents, string currentEvent)
        {
            _eventsConfig = eventsConfig;
            _eventsDatabase = eventsDatabase;
            _indicatorsBank = indicatorsBank;

            CreateRedZones();
            
            if (previousEvents is not { Count: > 0 })
                CollectEventsMaps();
            else
                CollectEventsMaps(previousEvents);
            
            _specialEventsHandler = new SpecialEventsHandler(_eventsDatabase, _redZonesMap);
            _currentEvent = string.IsNullOrEmpty(currentEvent) ? GetNextRandomEvent() : 
                new Event(_eventsDatabase.MainEvents.FirstOrDefault(o => o.Ref.Id == currentEvent));

            Debug.Log("Events: \n" + 
                      $"Current event: {_currentEvent.ID} \n" +
                      $"Previous events: {_previousEvents.Count} \n" + 
                      $"Possible events: {_possibleEvents.Count}" );
            
            if (_currentEvent == null)
                throw new NullReferenceException();
        }

        private void CreateRedZones()
        {
            _redZonesMap = new Dictionary<IndicatorType, IRedZoneValue>();

            foreach (var indicatorType in _eventsConfig.IndicatorPriority)
            {
                _redZonesMap.Add(indicatorType, _eventsConfig.RedZones.FirstOrDefault(
                    o => o.IndicatorType == indicatorType));
            }
        }

        private void CollectEventsMaps()
        {
            _previousEvents = new Dictionary<string, Event>();
            _possibleEvents = new Dictionary<string, Event>();

            foreach (var reference in _eventsDatabase.MainEvents)
                _possibleEvents.Add(reference.Ref.Id, new Event(reference));
        }

        private void CollectEventsMaps(List<string> previousEvents)
        {
            _previousEvents = new Dictionary<string, Event>();
            _possibleEvents = new Dictionary<string, Event>();

            foreach (var reference in _eventsDatabase.MainEvents)
            {
                if (previousEvents.Contains(reference.Ref.Id))
                    _previousEvents.Add(reference.Ref.Id, new Event(reference));
                else
                    _possibleEvents.Add(reference.Ref.Id, new Event(reference));
            }
        }
        
        public bool HasNextEvent() =>
            _possibleEvents.Count > 0;

        public Event GetNextRandomEvent()
        {
            Event randomEvent = GetNextEvent();

            _currentEvent = randomEvent;
            return _currentEvent;
        }

        private Event GetNextEvent()
        {
            if (_currentEvent != null && _currentEventIsSpecial == false)
            {
                _previousEvents.Add(_currentEvent.ID, _currentEvent);
                _possibleEvents.Remove(_currentEvent.ID);
            }

            Event nextEvent = null;
            _currentEventIsSpecial = false;
            _specialEventsHandler.ReduceTimerOnEvents();
            
            /* Проверка, нужен ли спец. ивент, потому что одно из значений в ред зоне */
            if (TryGetSpecialEvent(out nextEvent))
            {
                _currentEventIsSpecial = true;
                return nextEvent;
            }

            /* В обычном случае мы берем случайный ивент из стандартных */
            nextEvent = DictionaryExtensions.GetRandom(_possibleEvents);
            return nextEvent;
        }

        private bool TryGetSpecialEvent(out Event nextEvent)
        {
            nextEvent = null;
            foreach (var redZone in _redZonesMap)
            {
                /* Берем текущее значение индикатора */
                int value = _indicatorsBank.GetCurrentIndicatorValue(redZone.Key);

                /* Значение должно быть больше или меньше красной зоны */
                if (redZone.Value.IsLess)
                {
                    if (value <= redZone.Value.RedZoneValue)
                        nextEvent = _specialEventsHandler.TryGetEvent(redZone.Key);
                }
                else
                {
                    if (value >= redZone.Value.RedZoneValue)
                        nextEvent = _specialEventsHandler.TryGetEvent(redZone.Key);
                }
                /* Если мы нашли подходящий ивент, можно выходить */
                if (nextEvent != null)
                    return true;
            }
            return false;
        }
    }
}