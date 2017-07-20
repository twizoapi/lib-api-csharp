using Microsoft.VisualStudio.TestTools.UnitTesting;
using TwizoAPI.Entity.VerificationExceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwizoAPI;
using TwizoAPI.Entity;
using TwizoAPI.Responses;

namespace UnitTests.Entity.VerificationExceptions
{
    [TestClass()]
    public class VerificationExceptionTests
    {
        [TestMethod()]
        public void VerificationExceptionTest1()
        {
            //Arrange
            EntityException entityexception = new EntityException("This is a test exception", ErrorCode.INVALID_FIELDS, 200, 101);

            //Act
            VerificationException verificaitonexception = new VerificationException(entityexception);

            //Assert
            Assert.AreEqual(verificaitonexception.Message, "The verification was already completed");
        }

        [TestMethod()]
        public void VerificationExceptionTest2()
        {
            //Arrange
            EntityException entityexception = new EntityException("This is a test exception", ErrorCode.INVALID_FIELDS, 200, 999);

            //Act
            VerificationException verificationexception = new VerificationException(entityexception);

            //Assert
            Assert.AreEqual(verificationexception.Message, "Verification of token failed");
        }

        [TestMethod()]
        public void IsVerificationExceptionTest()
        {
            //Arrange
            VerificationException exception = new VerificationException(new EntityException(
                "This is a test message", ErrorCode.INVALID_FIELDS, RestStatusCodes.REST_CLIENT_ERROR_UNPROCESSABLE_ENTITY, 101));

            //Assert
            Assert.IsTrue(VerificationException.IsVerificationException(exception));
        }
    }
}