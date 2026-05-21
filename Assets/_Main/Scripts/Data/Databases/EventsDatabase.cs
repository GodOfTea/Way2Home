using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cathei.BakingSheet.Unity;
using Editor.GoogleDataImporter;
using UnityEngine;
using Event = EventComponents.Event;

namespace Data
{
    [CreateAssetMenu(fileName = "EventsDatabase", menuName = "Databases/EventsDatabase", order = 0)]
    public class EventsDatabase : ScriptableObject
    {
        private readonly string _importContainerPath = "Import/_Container";

        [SerializeField] private Event[] _mainEvents;

        // TODO: Преобразовать SE в обычный Event
        [SerializeField] private SpecialEventData[] _specialEvents;

        private SheetContainer _container;
        public Event[] MainEvents => _mainEvents;
        public SpecialEventData[] SpecialEvents => _specialEvents;

        [ContextMenu("Load References From Sheet")]
        public async Task LoadReferencesFromSheet()
        {
            var sheetContainerSO = Resources.Load<SheetContainerScriptableObject>(_importContainerPath);
            var importer = new ScriptableObjectSheetImporter(sheetContainerSO);
            _container = new SheetContainer(UnityLogger.Default);

            await _container.Bake(importer);
            ConvertReferencesToEvents();
            ConvertSpecialEventReferencesToEvents();

            UnityEditor.EditorUtility.SetDirty(this);
            Debug.Log($"Loaded {_container.Events.Count} events from sheet for EventsDatabase.");
        }

        [ContextMenu("Convert References To Events")]
        public void ConvertReferencesToEvents()
        {
            if (_container == null) return;

            Dictionary<string, EventsResult.Row> resultsDict =
                _container.EventsResult.ToDictionary(r => r.Id, r => r);
            int count = _container.Events.Count;
            _mainEvents = new Event[count];

            for (int i = 0; i < count; i++)
            {
                EventsResult.Row[] resultRefs = new EventsResult.Row[3];
                for (int j = 0; j < resultRefs.Length; j++)
                {
                    var optionResultKey = _container.Events[i].GetOption(j).OptionResultKey.Id;

                    if (resultsDict.TryGetValue(optionResultKey, out var resultRef))
                        resultRefs[j] = resultRef;
                    else
                        throw new System.Exception($"Event result with key '{optionResultKey}' not found in _results!");
                }

                _mainEvents[i] = new Event(_container.Events[i], resultRefs);
                Debug.Log("Converted event reference with ID: " + _mainEvents[i].ID);
            }

#if UNITY_EDITOR
            UnityEditor.EditorUtility.SetDirty(this);
#endif
        }

        [ContextMenu("Convert References To Special Events")]
        public void ConvertSpecialEventReferencesToEvents()
        {
            Dictionary<string, EventsResult.Row> specialResultsDict =
                _container.SpecialEventsResult.ToDictionary(r => r.Id, r => r);
            int count = _container.SpecialEvents.Count;
            for (int i = 0; i < count; i++)
            {
                EventsResult.Row[] resultRefs = new EventsResult.Row[3];
                for (int j = 0; j < resultRefs.Length; j++)
                {
                    var optionResultKey = _container.SpecialEvents[i].GetOption(j).OptionResultKey.Id;

                    if (specialResultsDict.TryGetValue(optionResultKey, out var resultRef))
                        resultRefs[j] = resultRef;
                    else
                        throw new System.Exception(
                            $"Special event result with key '{optionResultKey}' not found in _results!");
                }
                _specialEvents[i].SetEvent(new Event(_container.SpecialEvents[i], resultRefs));
                Debug.Log("Converted s.event reference with ID: " + _specialEvents[i].Event.ID);
            }

#if UNITY_EDITOR
            UnityEditor.EditorUtility.SetDirty(this);
#endif
        }
    }
}