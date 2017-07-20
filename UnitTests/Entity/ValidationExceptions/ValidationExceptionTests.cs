using Microsoft.VisualStudio.TestTools.UnitTesting;
using TwizoAPI.Entity.ValidationExceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwizoAPI;

namespace UnitTests.Entity.ValidationExceptions
{
    [TestClass()]
    public class ValidationExceptionTests
    {
        [TestMethod()]
        public void ValidationExceptionTest()
        {
            //Arrange
            Twizo twizo = new Twizo(TwizoTests.apiKey, TwizoTests.apiHost);
            var verification = twizo.CreateVerification("601151174973");
            verification.tokenLength = 11;
            verification.tag = "This tag contains more than 30 characters.";
            verification.validity = 1;

            //Act
            try
            {
                verification.Send();
            }
            catch (ValidationException e)
            {
                var errors = e.errorFields;
                List<string> errorNames = errors.Select(x => x.name).ToList();

                //Assert
                Assert.IsTrue(e.Message.StartsWith("Validation error for field"));
                Assert.AreEqual(errors.Count, 3);
                Assert.IsTrue(errorNames.Contains("tag") && errorNames.Contains("tokenLength") && errorNames.Contains("validity"));
            }
        }
    }
}