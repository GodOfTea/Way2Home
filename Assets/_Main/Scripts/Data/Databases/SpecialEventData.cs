using System;
using Cathei.BakingSheet;
using Editor.GoogleDataImporter;
using Enumeration;
using UnityEngine;

namespace Data
{
    [Serializable]
    public class SpecialEventData
    {
        [SerializeField] private string _name;
        [SerializeField] private IndicatorType _eventIndicator;
        [SerializeField] private int _eventReloadTime;
        [SerializeField] private EventSheet.Reference _event;

        public IndicatorType EventIndicator => _eventIndicator;
        public int EventReloadTime => _eventReloadTime;
        public Sheet<string, EventSheet.Row>.Reference Event => _event;
    }
}