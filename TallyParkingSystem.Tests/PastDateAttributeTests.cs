using System;
using System.Diagnostics.CodeAnalysis;
using NUnit.Framework;
using TallyParkingSystem.Model;

namespace TallyParkingSystem.Tests
{
    [TestFixture]
    [ExcludeFromCodeCoverage]
    public class PastDateAttributeTests
    {
        [Test]
        public void PastDateAttribute_Returns_False_When_Date_Is_Not_In_Past()
        {
            //Arrange
            var entryRequest = new EntryRequest
            {
                EntryTime = DateTime.Now.AddHours(1),
                ExitTime = DateTime.Now,
                RegistrationNo = "Test"
            };
            
            var pastDateAttribute = new PastDateAttribute();

            // Act           
            var result = pastDateAttribute.IsValid(entryRequest.EntryTime);

            //Assert
            Assert.IsFalse(result);

        }

        [Test]
        public void PastDateAttribute_Returns_True_When_Date_Is_In_Past()
        {
            //Arrange
            var entryRequest = new EntryRequest
            {
                EntryTime = DateTime.Now.AddHours(-1),
                ExitTime = DateTime.Now,
                RegistrationNo = "Test"
            };

            var pastDateAttribute = new PastDateAttribute();

            // Act           
            var result = pastDateAttribute.IsValid(entryRequest.EntryTime);

            //Assert
            Assert.IsTrue(result);

        }
    }
}
