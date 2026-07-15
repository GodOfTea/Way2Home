using Cathei.BakingSheet;
using Microsoft.Extensions.Logging;

namespace Editor.GoogleDataImporter
{
    public class SheetContainer : SheetContainerBase
    {
        public SheetContainer(ILogger logger) : base(logger) { }
        
        public EventSheet Events { get; private set; }
        public EventSheet SpecialEvents { get; private set; }
        public EventsResult EventsResult { get; private set; }
        public EventsResult SpecialEventsResult { get; private set; }
        public GameResult GameResult { get; private set; }
    }
}