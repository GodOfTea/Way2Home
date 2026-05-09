using Cathei.BakingSheet;

namespace Editor.GoogleDataImporter
{
    public class EventsResult : Sheet<EventsResult.Row>
    {
        public class Row : SheetRowArray<Elem>
        {
            public Elem GetOption(int number)
            {
                return this[number];
            }
        }

        public class Elem : SheetRowElem
        {
            public int MoraleStatus { get; private set; }
            public string ImagePathAnswer { get; private set; }
            public string OptionEndingKey { get; private set; }
            
            public int Morale { get; private set; }
            public int People { get; private set; }
            public int Supplies { get; private set; }
            public int Risk { get; private set; }
            public int Days { get; private set; }
        }
    }
}