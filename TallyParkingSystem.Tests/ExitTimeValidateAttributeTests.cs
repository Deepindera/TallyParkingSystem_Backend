using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using NUnit.Framework;
using TallyParkingSystem.Model;

namespace TallyParkingSystem.Tests
{
    [TestFixture]
    [ExcludeFromCodeCoverage]
    public class ExitTimeValidateAttributeTests
    {
        [Test]
        public void ExitTimeValidateAttribute_Returns_Success_When_ExitTime_GreaterThan_EntryTime()
        {
            //Arrange
            var entryRequest = new EntryRequest
            {
                EntryTime = DateTime.Now.AddHours(1),
                ExitTime = DateTime.Now.AddHours(1.1),
                RegistrationNo = "Test"
            };

            var validationContext = new ValidationContext(entryRequest);
            var exitTimeValidateAttribute = new ExitTimeValidateAttribute("EntryTime");
            var expectedResult = ValidationResult.Success;

            // Act           
            var result = exitTimeValidateAttribute.GetValidationResult(entryRequest.ExitTime, validationContext);

            //Assert
            Assert.AreEqual(expectedResult, result);

        }

        [Test]
        public void ExitTimeValidateAttribute_Returns_ValidationError_When_ExitTime_Not_GreaterThan_EntryTime()
        {
            //Arrange
            var entryRequest = new EntryRequest
            {
                EntryTime = DateTime.Now.AddHours(1),
                ExitTime = DateTime.Now.AddHours(0.9),
                RegistrationNo = "Test"
            };

            var validationContext = new ValidationContext(entryRequest);
            var exitTimeValidateAttribute = new ExitTimeValidateAttribute("EntryTime");

            // Act           
            var result = exitTimeValidateAttribute.GetValidationResult(entryRequest.ExitTime, validationContext);

            //Assert
            Assert.NotNull(result);
            Assert.AreEqual("EntryTime cannot be greater than exit time", result.ErrorMessage);

        }
    }
}
