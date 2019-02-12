using System;

namespace TallyParkingSystem.Model
{
    public interface IEntryResponse 
    {
        decimal Amount { get; set; }
        DateTime EntryTime { get; set; }
        DateTime ExitTime { get; set; }
        string RateName { get; set; }
        string RateType { get; set; }     
        string RegistrationNo { get; set; }
    }
}
