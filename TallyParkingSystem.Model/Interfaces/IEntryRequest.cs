using System;

namespace TallyParkingSystem.Model
{
    public interface IEntryRequest
    {
        DateTime EntryTime { get; set; }
        DateTime ExitTime { get; set; }
        string RegistrationNo { get; set; }
    }
}
