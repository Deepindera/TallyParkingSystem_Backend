using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using TallyParkingSystem.Controllers;
using TallyParkingSystem.Model;

namespace TallyParkingSystem.Tests
{
    [TestFixture]
    [ExcludeFromCodeCoverage]
    public class ParkingRateControllerTests
    {
        [Test]
        public void Post_Returns_IEntryResponse()
        {
            //Arrange
            var controller = new ParkingRateController();
            var entryRequest = new EntryRequest
            {
                EntryTime = DateTime.Now.AddHours(1),
                ExitTime = DateTime.Now.AddHours(1.1),
                RegistrationNo = "Test"
            };

            //Act
            var actionResult = controller.Post(entryRequest);
            
            //Assert            
            Assert.IsInstanceOf<OkObjectResult>(actionResult.Result);
            var result = (EntryResponse)((OkObjectResult)actionResult.Result).Value;
            Assert.IsInstanceOf<EntryResponse>(result);            
            Assert.AreEqual(200, ((OkObjectResult)actionResult.Result).StatusCode);            
            Assert.AreEqual(entryRequest.EntryTime, result.EntryTime);            
            Assert.AreEqual(entryRequest.ExitTime, result.ExitTime);            
            Assert.AreEqual(entryRequest.RegistrationNo, result.RegistrationNo);            
            Assert.IsTrue(result.Amount > 0);            
        

        }
    }
}
