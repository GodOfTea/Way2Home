using Cathei.BakingSheet;
using Microsoft.Extensions.Logging;

namespace Editor.GoogleDataImporter
{
    public class SheetContainer : SheetContainerBase
    {
        public SheetContainer(ILogger logger) : base(logger) { }
        
        /* Russian */
        public EventSheet Events { get; private set; }
        public EventSheet SpecialEvents { get; private set; }
        public ResultSheet GameResult { get; private set; }
    }
}