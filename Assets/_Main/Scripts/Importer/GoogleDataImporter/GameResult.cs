using Cathei.BakingSheet;

namespace Editor.GoogleDataImporter
{
    public class GameResult : Sheet<GameResult.Row>
    {
        public class Row : SheetRow
        {
            public string Name { get; private set; }
            public string Indicator { get; private set; }
            public string ImagePath { get; private set; }
            public string Description { get; private set; }
        }
    }
}