using System;

namespace TallyParkingSystem.Model
{
    public class EntryResponse : IEntryResponse
    {
        public decimal Amount { get; set; }
        public DateTime EntryTime { get; set; }
        public DateTime ExitTime { get; set; }
        public string RateName { get; set; }
        public string RateType { get; set; }
        public string RegistrationNo { get; set; }


        public static EntryResponse FromSpecialRate(ISpecialRate rate, IEntryRequest request)
        {
            if (rate == null)
            {
                return null;
            }

            return new EntryResponse()
            {
                RateName = rate.Name,
                RateType = rate.Type,
                Amount = rate.TotalPrice,
                RegistrationNo = request.RegistrationNo,
                EntryTime = request.EntryTime,
                ExitTime = request.ExitTime
            };
        }

        public static EntryResponse FromStandardRate(IStandardRate rate, IEntryRequest request)
        {
            if (rate == null)
            {
                return null;
            }

            return new EntryResponse()
            {
                RateName = rate.Name,
                RateType = rate.Type,
                Amount = rate.TotalPrice,
                RegistrationNo = request.RegistrationNo,
                EntryTime = request.EntryTime,
                ExitTime = request.ExitTime
            };
        }
    }
}
