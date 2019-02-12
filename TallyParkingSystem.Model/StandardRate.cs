namespace TallyParkingSystem.Model
{
    public class StandardRate : IStandardRate
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public int MinimumHours { get; set; }
        public int MaximumHours { get; set; }
        public decimal TotalPrice { get; set; }
        public bool FlatRate { get; set; }
    }
}
