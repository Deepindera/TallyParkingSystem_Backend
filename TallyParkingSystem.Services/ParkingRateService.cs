using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using TallyParkingSystem.Model;
using TallyParkingSystem.Services.Properties;
using TallyParkingSystem.Utilities;

namespace TallyParkingSystem.Services
{
    public static class ParkingRateService
    {
        public static IEntryResponse CalculateParkingFee(IEntryRequest request)
        {
            return IsWeekendRateApplicable(request) ? CalculateWeekendParkingFee(request) : CalculateWeekdayParkingFee(request);
        }

        private static bool IsWeekendRateApplicable(IEntryRequest request)
        {
            return Constants.WeekendDays.Contains(request.EntryTime.DayOfWeek.ToString()) && Constants.WeekendDays.Contains(request.ExitTime.DayOfWeek.ToString());
        }

        private static IEntryResponse CalculateWeekendParkingFee(IEntryRequest request)
        {
            var weekendRate = JsonConvert.DeserializeObject<List<SpecialRate>>(Resources.specialrates).Single(r => r.Weekend);

            return EntryResponse.FromSpecialRate(weekendRate, request);
        }

        private static IEntryResponse CalculateWeekdayParkingFee(IEntryRequest request)
        {
            var specialRates = JsonConvert.DeserializeObject<List<SpecialRate>>(Resources.specialrates).Where(r => !r.Weekend);

            var applicableSpecialRate = specialRates.SingleOrDefault(r => request.EntryTime.TimeOfDay >= TimeSpan.Parse(r.EntryPeriodStart) &&
                                                         request.EntryTime.TimeOfDay <= TimeSpan.Parse(r.EntryPeriodEnd) &&
                                                         request.ExitTime.TimeOfDay >= TimeSpan.Parse(r.ExitPeriodStart) &&
                                                         request.ExitTime.TimeOfDay <= TimeSpan.Parse(r.ExitPeriodEnd));

            if (applicableSpecialRate != null)
            {
                return EntryResponse.FromSpecialRate(applicableSpecialRate, request);
            }

            // Standard Rates
            var standardRates = JsonConvert.DeserializeObject<List<StandardRate>>(Resources.standardrates);

            var applicableStandardRate = standardRates.Where(r => request.ExitTime.TimeOfDay.Subtract(request.EntryTime.TimeOfDay).TotalMinutes > r.MinimumHours * 60 &&
                                                                            request.ExitTime.TimeOfDay.Subtract(request.EntryTime.TimeOfDay).TotalMinutes <= r.MaximumHours * 60);

            return EntryResponse.FromStandardRate(applicableStandardRate.SingleOrDefault(), request);
        }
    }
}
