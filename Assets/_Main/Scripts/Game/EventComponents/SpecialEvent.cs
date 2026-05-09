using Editor.GoogleDataImporter;

namespace EventComponents
{
    public class SpecialEvent : Event
    {
        private int _eventsToReload;
        
        private bool _canUse;

        public int EventsToReload => _eventsToReload;
        public bool CanUse => _canUse;

        public SpecialEvent(EventSheet.Row eventData, EventsResult.Row[] resultData, int eventsToReload) : 
            base(eventData, resultData)
        {
            _eventsToReload = eventsToReload;
            _canUse = true;
        }

        public void SetAsUse()
        {
            _canUse = false;
        }

        public void Reload()
        {
            _canUse = true;
        }
    }
}