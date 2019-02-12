using System;
using System.ComponentModel.DataAnnotations;

namespace TallyParkingSystem.Model
{
    public class EntryRequest : IEntryRequest
    {
        [Required]
        [PastDate(ErrorMessage = "Entry Time must be in past")]
        public DateTime EntryTime { get; set; }
        [Required]
        [ExitTimeValidateAttribute("EntryTime")]
        public DateTime ExitTime { get; set; }
        [Required]
        public string RegistrationNo { get; set; }
    }
}
