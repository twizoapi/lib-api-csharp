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

namespace UnitTests.Entity
{
    [TestClass()]
    public class SmsTests
    {
        [TestMethod()]
        public void SendSimpleTest()
        {
            //Arrange
            TwizoClient client = new TwizoClient(TwizoTests.apiKey, TwizoTests.apiHost);
            Sms sms = new Sms(client);
            sms.recipients = new[] { "601151174973" };
            sms.body = "This is a unit test";
            sms.sender = "Unit tester";

            //Act
            Response response = sms.SendSimple();

            //Assert
            Assert.AreEqual(response.statusCode, 201);
            Assert.IsTrue(response.body.Count > 0);
        }

        [TestMethod()]
        [ExpectedException(typeof(ValidationException))]
        public void SendSimpleTestNull()
        {
            //Arrange
            TwizoClient client = new TwizoClient(TwizoTests.apiKey, TwizoTests.apiHost);
            Sms sms = new Sms(client);
            sms.recipients = null;
            sms.body = "This is a unit test";
            sms.sender = "Unit tester";

            //Act
            Response response = sms.SendSimple();
        }

        [TestMethod()]
        public void SendTest()
        {
            //Arrange
            TwizoClient client = new TwizoClient(TwizoTests.apiKey, TwizoTests.apiHost);
            Sms sms = new Sms(client);
            sms.recipients = new[] { "601151174973" };
            sms.body = "This is a unit test";
            sms.sender = "Unit tester";

            //Act
            Response response = sms.Send();

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
            Sms sms = new Sms(client);
            sms.recipients = null;
            sms.body = "This is a unit test";
            sms.sender = "Unit tester";

            //Act
            Response response = sms.Send();
        }

        [TestMethod()]
        public void PopulateTest()
        {
            //Arrange
            TwizoClient client = new TwizoClient(TwizoTests.apiKey, TwizoTests.apiHost);
            Sms sms = new Sms(client);
            sms.recipients = new[] { "601151174973"};
            sms.sender = "Unit tester";
            sms.body = "This is a unit test";

            //Act
            Response response = sms.Send();
            string messageId = sms.messageId;
            var newSms = new Sms(client);
            newSms.Populate(messageId);

            //Assert
            Assert.AreEqual(newSms.messageId, sms.messageId);
            Assert.AreEqual(newSms.recipient, sms.recipient);
            Assert.AreEqual(newSms.body, sms.body);
            Assert.AreEqual(newSms.sender, sms.sender);
        }

        [TestMethod()]
        [ExpectedException(typeof(EntityException))]
        public void PopulateTestNull()
        {
            //Arrange
            TwizoClient client = new TwizoClient(TwizoTests.apiKey, TwizoTests.apiHost);
            Sms sms = new Sms(client);

            //Act
            sms.Populate(null);
        }

        [TestMethod()]
        [ExpectedException(typeof(EntityException))]
        public void PopulateTestInvalidMessageId()
        {
            //Arrange
            TwizoClient client = new TwizoClient(TwizoTests.apiKey, TwizoTests.apiHost);
            Sms sms = new Sms(client);

            //Act
            sms.Populate("myMessageId");
        }
    }
}