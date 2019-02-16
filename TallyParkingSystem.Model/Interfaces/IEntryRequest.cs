using System;

namespace TallyParkingSystem.Model
{
    public interface IEntryRequest
    {
        DateTime EntryTime { get; set; }
        DateTime ExitTime { get; set; }
        DateTime EntryTimeLocal { get; }
        DateTime ExitTimeLocal { get; }
        string RegistrationNo { get; set; }
    }
}
