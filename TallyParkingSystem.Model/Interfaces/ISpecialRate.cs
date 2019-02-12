namespace TallyParkingSystem.Model
{
    public interface ISpecialRate
    {
        string Name { get; set; }
        string Type { get; set; }
        decimal TotalPrice { get; set; }
        string EntryPeriodStart { get; set; }
        string EntryPeriodEnd { get; set; }
        string ExitPeriodStart { get; set; }
        string ExitPeriodEnd { get; set; }
        
        
        bool Weekend { get; set; }                
    }
}
