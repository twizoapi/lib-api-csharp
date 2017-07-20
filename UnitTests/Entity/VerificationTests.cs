using Microsoft.VisualStudio.TestTools.UnitTesting;
using TwizoAPI.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwizoAPI.Client;
using TwizoAPI.Responses;
using TwizoAPI.Entity.ValidationExceptions;
using TwizoAPI.Entity.VerificationExceptions;

namespace UnitTests.Entity
{
    [TestClass()]
    public class VerificationTests
    {
        [TestMethod()]
        public void SendTest()
        {
            //Arrange
            TwizoClient client = new TwizoClient(TwizoTests.apiKey, TwizoTests.apiHost);
            Verification verification = new Verification(client);
            verification.recipient = "601151174973";

            //Act
            Response response = verification.Send();

            //Assert
            Assert.AreEqual(response.statusCode, 201);
            Assert.IsTrue(response.body.Count > 0);
        }

        [TestMethod()]
        [ExpectedException(typeof(ValidationException))]
        public void SendTestNull()
        {
            //Arrange
            TwizoClient client = new TwizoClient(TwizoTests.apiKey, TwizoTests.apiHost);
            Verification verification = new Verification(client);
            verification.recipient = null;

            //Act
            Response response = verification.Send();
        }

        [TestMethod()]
        [ExpectedException(typeof(EmptyTokenException))]
        public void VerifyTest()
        {
            //Arrange
            TwizoClient client = new TwizoClient(TwizoTests.apiKey, TwizoTests.apiHost);
            Verification verification = new Verification(client);

            //Act
            verification.Verify(null, "myMessageId");
        }

        [TestMethod()]
        [ExpectedException(typeof(EmptyMessageIdException))]
        public void VerifyTest2()
        {
            //Arrange
            TwizoClient client = new TwizoClient(TwizoTests.apiKey, TwizoTests.apiHost);
            Verification verification = new Verification(client);

            //Act
            verification.Verify("myToken");
        }

        [TestMethod()]
        public void PopulateTest()
        {
            //Arrange
            TwizoClient client = new TwizoClient(TwizoTests.apiKey, TwizoTests.apiHost);
            Verification verification = new Verification(client);
            verification.recipient ="601151174973";
            verification.validity = 100;
            verification.tag = "Unit testing";

            //Act
            Response response = verification.Send();
            string messageId = verification.messageId;
            var newVerification = new Verification(client);
            newVerification.Populate(messageId);

            //Assert
            Assert.AreEqual(newVerification.messageId, verification.messageId);
            Assert.AreEqual(newVerification.validity, verification.validity);
            Assert.AreEqual(newVerification.tag, verification.tag);
        }

        [TestMethod()]
        [ExpectedException(typeof(EntityException))]
        public void PopulateTestNull()
        {
            //Arrange
            TwizoClient client = new TwizoClient(TwizoTests.apiKey, TwizoTests.apiHost);
            Verification verification = new Verification(client);

            //Act
            verification.Populate(null);
        }

        [TestMethod()]
        [ExpectedException(typeof(EntityException))]
        public void PopulateTestInvalidMessageId()
        {
            //Arrange
            TwizoClient client = new TwizoClient(TwizoTests.apiKey, TwizoTests.apiHost);
            Verification verification = new Verification(client);

            //Act
            verification.Populate("myMessageId");
        }
    }
}