namespace Game.Economy
{
    public interface IRedZoneValue
    {
        public int RedZoneValue { get; }
        public bool IsLess { get; }
    }
}