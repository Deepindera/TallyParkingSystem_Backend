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
            if (GetDaysDifference(request.EntryTimeLocal, request.ExitTimeLocal) > 2)
            {
                return false;
            }

            return Constants.WeekendDays.Contains(request.EntryTimeLocal.DayOfWeek.ToString()) && Constants.WeekendDays.Contains(request.ExitTimeLocal.DayOfWeek.ToString());
        }

        private static IEntryResponse CalculateWeekendParkingFee(IEntryRequest request)
        {
            var weekendRate = JsonConvert.DeserializeObject<List<SpecialRate>>(Resources.specialrates).Single(r => r.Weekend);

            return EntryResponse.FromSpecialRate(weekendRate, request);
        }

        private static IEntryResponse CalculateWeekdayParkingFee(IEntryRequest request)
        {

            var daysDifference = GetDaysDifference(request.EntryTimeLocal, request.ExitTimeLocal);

            if (daysDifference < 2)
            {
                var specialRates = JsonConvert.DeserializeObject<List<SpecialRate>>(Resources.specialrates).Where(r => !r.Weekend);

                var applicableSpecialRate = specialRates.SingleOrDefault(r => request.EntryTimeLocal.TimeOfDay >= TimeSpan.Parse(r.EntryPeriodStart) &&
                                                                              request.EntryTimeLocal.TimeOfDay <= TimeSpan.Parse(r.EntryPeriodEnd) &&
                                                                              request.ExitTimeLocal.TimeOfDay >= TimeSpan.Parse(r.ExitPeriodStart) &&
                                                                              request.ExitTimeLocal.TimeOfDay <= TimeSpan.Parse(r.ExitPeriodEnd));

                if (applicableSpecialRate != null)
                {
                    return EntryResponse.FromSpecialRate(applicableSpecialRate, request);
                }

            }

            // Standard Rates
            StandardRate applicableStandardRate;
            var standardRates = JsonConvert.DeserializeObject<List<StandardRate>>(Resources.standardrates);
            
            if (daysDifference == 0)
            {
                applicableStandardRate = standardRates.SingleOrDefault(r => request.ExitTimeLocal.TimeOfDay.Subtract(request.EntryTimeLocal.TimeOfDay).TotalMinutes > r.MinimumHours * 60 &&
                                                                                request.ExitTimeLocal.TimeOfDay.Subtract(request.EntryTimeLocal.TimeOfDay).TotalMinutes <= r.MaximumHours * 60);
            }
            else
            {
                applicableStandardRate = standardRates.OrderByDescending(r => r.MaximumHours).FirstOrDefault();
                applicableStandardRate.TotalPrice = applicableStandardRate.TotalPrice * daysDifference;
            }


            return EntryResponse.FromStandardRate(applicableStandardRate, request);
        }

        private static int GetDaysDifference(DateTime date1, DateTime date2)
        {
            var days = (date1 - date2).TotalDays;

            if(days < 0)
            {
                days = days * -1;
            }

            return days < 1 ? 0 : (int)Math.Ceiling(days);            
        }
    }
}
