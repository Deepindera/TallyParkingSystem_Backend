using System;
using System.ComponentModel.DataAnnotations;

namespace TallyParkingSystem.Model
{
    public class EntryRequest : IEntryRequest
    {
        [Required]        
        public DateTime EntryTime { get; set; }

        public DateTime EntryTimeLocal => EntryTime.ToLocalTime();

        [Required]
        [ExitTimeValidateAttribute("EntryTime")]
        public DateTime ExitTime { get; set; }

        public DateTime ExitTimeLocal => ExitTime.ToLocalTime();

        [Required]
        public string RegistrationNo { get; set; }
    }
}
