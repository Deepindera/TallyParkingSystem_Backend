namespace TallyParkingSystem.Model
{
    public class SpecialRate: ISpecialRate
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public decimal TotalPrice { get; set; }
        public string EntryPeriodStart { get; set; }
        public string EntryPeriodEnd { get; set; }
        public string ExitPeriodStart { get; set; }
        public string ExitPeriodEnd { get; set; }
        public bool Weekend { get; set; }
    }
}
