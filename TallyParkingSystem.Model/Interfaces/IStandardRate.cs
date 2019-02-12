namespace TallyParkingSystem.Model
{
    public interface IStandardRate
    {
        string Name { get; set; }
        string Type { get; set; }
        int MinimumHours { get; set; }
        int MaximumHours { get; set; }
        decimal TotalPrice { get; set; }
        bool FlatRate { get; set; }
   
    }
}
