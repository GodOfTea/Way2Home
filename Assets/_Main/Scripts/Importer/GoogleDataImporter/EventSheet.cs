using System;
using Cathei.BakingSheet;

namespace Editor.GoogleDataImporter
{
    [Serializable]
    public class EventSheet : Sheet<EventSheet.Row>
    {
        public class Row : SheetRowArray<Elem>
        {
            public string Name { get; private set; }
            public string ImagePath { get; private set; }
            
            public Elem GetOption(int number)
            {
                return this[number];
            }
        }

        public class Elem : SheetRowElem
        {
            public string OptionKey { get; private set; }
            // public string ImagePathAnswer { get; private set; }
            //public string OptionResultKey { get; private set; }
            public EventsResult.Reference OptionResultKey { get; private set; }

            // public int People { get; private set; }
            // public int Supplies { get; private set; }
            // public int Risk { get; private set; }
            // public int Days { get; private set; }
        }
    }
}