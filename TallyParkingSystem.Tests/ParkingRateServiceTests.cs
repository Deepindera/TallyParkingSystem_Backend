using System;
using System.Diagnostics.CodeAnalysis;
using NUnit.Framework;
using TallyParkingSystem.Model;
using TallyParkingSystem.Services;

namespace TallyParkingSystem.Tests
{
    [TestFixture]
    [ExcludeFromCodeCoverage]
    public class ParkingRateServiceTests
    {        
        [TestCase("23/02/2019 00:01", "24/02/2019 23:59", 10, TestName = "Weekend Fee Test 1", Description = "Entry Saturday and Exit on Sunday")]
        [TestCase("24/02/2019 00:01", "24/02/2019 23:59", 10, TestName = "Weekend Fee Test 2", Description = "Entry Sunday and Exit on Sunday")]
        [TestCase("23/02/2019 00:01", "23/02/2019 23:59", 10, TestName = "Weekend Fee Test 3", Description = "Entry Saturday and Exit on Saturday")]
        [TestCase("11/02/2019 09:00", "11/02/2019 10:00", 5, TestName = "Weekday 1 Hour Entry Fee Test", Description = "1 Hour entry on weekday")]
        [TestCase("11/02/2019 09:00", "11/02/2019 9:30", 5, TestName = "Weekday 1/2 Hour Entry Fee Test", Description = "1/2 Hour entry on weekday")]
        [TestCase("11/02/2019 09:00", "11/02/2019 11:00", 10, TestName = "Weekday 2 Hours Entry Fee Test", Description = "2 Hours entry on weekday")]
        [TestCase("11/02/2019 09:00", "11/02/2019 11:01", 15, TestName = "Weekday 2+ Hours Entry Fee Test", Description = "2+ Hours entry on weekday")]
        [TestCase("11/02/2019 09:00", "11/02/2019 13:00", 20, TestName = "Weekday 4 Hours Entry Fee Test", Description = "4 Hours entry on weekday")]
        [TestCase("11/02/2019 09:01", "11/02/2019 21:00", 20, TestName = "Weekday 12 Hours Entry Test", Description = "12 Hours entry on weekday")]
        [TestCase("11/02/2019 09:00", "11/02/2019 21:00", 13, TestName = "Weekday 12 Hours Entry - Early Bird Fee Test", Description = "12 Hours entry on weekday")]
        [TestCase("11/02/2019 18:00", "12/02/2019 5:59", 6.50, TestName = "Weekday  Night  Fee Test", Description = "Night entry on weekday")]
        [TestCase("22/02/2019 23:59", "23/02/2019 5:00", 6.50, TestName = "Test to Apply Night Rate Instead of Weekend", Description = "Night entry on friday")]
        public void When_CalculateParkingFee_Called_ForEntryTime_Returns_CorrectRate(string entryTime, string exitTime, decimal expectedFee)
        {
            //arrange           
            var entryRequest = BuildEntryRequest(entryTime, exitTime);

            //act
            var result = ParkingRateService.CalculateParkingFee(entryRequest);

            //assert
            Assert.AreEqual(expectedFee, result.Amount, "The Fee is not as expected. Incorrect Rate Applied.");            
        }



        [Test]
        public void Mapping_StandardRate_ToEntryResponse_IsCorrect()
        {
            //arrange
            var standardRate = new StandardRate {TotalPrice = 10, Name = "Test Rate Name", Type = "Test Rate Type"};
            var entryRequest = BuildEntryRequest(DateTime.Now.AddHours(-1).ToString(), DateTime.Now.ToString());

            //act
            var result = EntryResponse.FromStandardRate(standardRate, entryRequest);

            //assert
            Assert.AreEqual(standardRate.TotalPrice, result.Amount, "Amount not match");
            Assert.AreEqual(standardRate.Type, result.RateType, "Rate Type not match");
            Assert.AreEqual(standardRate.Name, result.RateName, "Rate Name not match");
            Assert.AreEqual(entryRequest.RegistrationNo, result.RegistrationNo, "Registration No not match");
            Assert.AreEqual(entryRequest.EntryTime, result.EntryTime, "Entry Time not match");
            Assert.AreEqual(entryRequest.ExitTime, result.ExitTime, "Exit Time not match");
        }

        [Test]
        public void Mapping_SpecialRate_ToEntryResponse_IsCorrect()
        {
            //arrange
            var specialRate = new SpecialRate { TotalPrice = 10, Name = "Test Rate Name", Type = "Test Rate Type" };
            var entryRequest = BuildEntryRequest(DateTime.Now.AddHours(-1).ToString(), DateTime.Now.ToString());

            //act
            var result = EntryResponse.FromSpecialRate(specialRate, entryRequest);

            //assert
            Assert.AreEqual(specialRate.TotalPrice, result.Amount, "Amount not match");
            Assert.AreEqual(specialRate.Type, result.RateType, "Rate Type not match");
            Assert.AreEqual(specialRate.Name, result.RateName, "Rate Name not match");
            Assert.AreEqual(entryRequest.RegistrationNo, result.RegistrationNo, "Registration No not match");
            Assert.AreEqual(entryRequest.EntryTime, result.EntryTime, "Entry Time not match");
            Assert.AreEqual(entryRequest.ExitTime, result.ExitTime, "Exit Time not match");
        }

        private IEntryRequest BuildEntryRequest(string entryTime, string exitTime)
        {
            return new EntryRequest()
            {
                EntryTime = DateTime.Parse(entryTime),
                ExitTime = DateTime.Parse(exitTime)
            };
        }
    }
}
