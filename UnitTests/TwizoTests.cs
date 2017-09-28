using Microsoft.VisualStudio.TestTools.UnitTesting;
using TwizoAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwizoAPI.Entity;

namespace UnitTests
{
    [TestClass()]
    public class TwizoTests
    {
        public static string apiKey = "";
        public static string apiHost = "";

        [TestMethod()]
        public void CreateVerificationTest()
        {
            //Arrange
            string recipient = "601151174973";
            Twizo twizo = new Twizo(apiKey, apiHost);

            //Act
            var verification = twizo.CreateVerification(recipient);

            //Assert
            Assert.AreEqual(verification.recipient, recipient);
        }

        [TestMethod()]
        public void CreateVerificationTestNull()
        {
            //Arrange
            string recipient = null;
            Twizo twizo = new Twizo(apiKey, apiHost);

            //Act
            var verification = twizo.CreateVerification(recipient);

            //Assert
            Assert.AreEqual(verification.recipient, recipient);
        }

        [TestMethod()]
        public void CreateVerificationCallTest()
        {
            //Arrange
            string recipient = "601151174973";
            Twizo twizo = new Twizo(apiKey, apiHost);

            //Act
            var verification = twizo.CreateVerificationCall(recipient);

            //Assert
            Assert.AreEqual(verification.recipient, recipient);
            Assert.AreEqual(verification.type, Verification.TYPE_CALL);
        }

        [TestMethod()]
        public void GetVerificationTest()
        {
            //Arrange
            string recipient = "601151174973";
            Twizo twizo = new Twizo(apiKey, apiHost);

            //Act
            var verification = twizo.CreateVerification(recipient);
            verification.Send();
            string messageId = verification.messageId;
            var newVerficiation = twizo.GetVerification(messageId);

            //Assert
            Assert.AreEqual(verification.messageId, newVerficiation.messageId);
            Assert.AreEqual(verification.recipient, newVerficiation.recipient);
        }

        [TestMethod()]
        [ExpectedException(typeof(EntityException))]
        public void GetVerificationTestNull()
        {
            //Arrange
            string messageId = null;
            Twizo twizo = new Twizo(apiKey, apiHost);

            //Act
            twizo.GetVerification(messageId);
        }

        [TestMethod()]
        public void CreateBackupCodeTest()
        {
            //Arrange
            string identifier = "myIdentifier";
            Twizo twizo = new Twizo(apiKey, apiHost);

            //Act
            var backup = twizo.CreateBackupCode(identifier);

            //Assert
            Assert.AreEqual(backup.identifier, identifier);
        }

        [TestMethod()]
        public void CreateBackupCodeTestNull()
        {
            //Arrange
            string identifier = null;
            Twizo twizo = new Twizo(apiKey, apiHost);

            //Act
            var backup = twizo.CreateBackupCode(identifier);

            //Assert
            Assert.AreEqual(backup.identifier, identifier);
        }

        [TestMethod()]
        public void GetBackupCodeTest()
        {
            //Arrange
            string identifier = "myIdentifier";
            Twizo twizo = new Twizo(apiKey, apiHost);

            //Act
            try
            {
                twizo.CreateBackupCode(identifier);
            }
            catch { }
            var backup = twizo.GetBackupCode(identifier);

            //Assert
            Assert.AreEqual(backup.identifier, identifier);
        }

        [TestMethod()]
        public void CreateSmsTest()
        {
            //Arrange
            string recipient = "601151174973";
            string body = "This is a unit test";
            string sender = "Unit tester";
            Twizo twizo = new Twizo(apiKey, apiHost);

            //Act
            var sms = twizo.CreateSms(recipient, body, sender);

            //Assert
            Assert.AreEqual(sms.recipient, recipient);
            Assert.AreEqual(sms.body, body);
            Assert.AreEqual(sms.sender, sender);
        }

        [TestMethod()]
        public void CreateSmsTestNull()
        {
            //Arrange
            string recipient = null;
            string body = "This is a unit test";
            string sender = "Unit tester";
            Twizo twizo = new Twizo(apiKey, apiHost);

            //Act
            var sms = twizo.CreateSms(recipient, body, sender);

            //Assert
            Assert.AreEqual(sms.recipient, recipient);
            Assert.AreEqual(sms.body, body);
            Assert.AreEqual(sms.sender, sender);
        }

        [TestMethod()]
        public void GetSmsTest()
        {
            //Arrange
            string recipient = "601151174973";
            string body = "This is a unit test";
            string sender = "Unit tester";
            Twizo twizo = new Twizo(apiKey, apiHost);

            //Act
            var sms = twizo.CreateSms(recipient, body, sender);
            sms.Send();
            string messageId = sms.messageId;
            var newSms = twizo.GetSms(messageId);

            //Assert
            Assert.AreEqual(sms.messageId, newSms.messageId);
            Assert.AreEqual(sms.recipient, newSms.recipient);
        }

        [TestMethod()]
        public void GetSmsResultsTest()
        {
            //Arrange
            Twizo twizo = new Twizo(apiKey, apiHost);

            //Act
            var results = twizo.GetSmsResults();

            //Assert
            Assert.IsNotNull(results);
        }

        [TestMethod()]
        public void CreateNumberLookupTest()
        {
            //Arrange
            string[] numbers = new[] {"601151174973"};

            Twizo twizo = new Twizo(apiKey, apiHost);

            //Act
            var lookup = twizo.CreateNumberLookup(numbers);

            //Assert
            Assert.AreEqual(lookup.numbers, numbers);
        }

        [TestMethod()]
        public void CreateNumberLookupTestNull()
        {
            //Arrange
            string[] numbers = null;
            Twizo twizo = new Twizo(apiKey, apiHost);

            //Act
            var lookup = twizo.CreateNumberLookup(numbers);

            //Assert
            Assert.AreEqual(lookup.numbers, numbers);
        }

        [TestMethod()]
        public void GetNumberLookupTest()
        {
            //Arrange
            string recipient = "601151174973";
            Twizo twizo = new Twizo(apiKey, apiHost);

            //Act
            var lookup = twizo.CreateNumberLookup(recipient);
            lookup.Send();
            string messageId = lookup.messageId;
            var newLookup = twizo.GetNumberLookup(messageId);

            //Assert
            Assert.AreEqual(lookup.messageId, newLookup.messageId);
        }

        [TestMethod()]
        public void GetNumberLookupResultsTest()
        {
            //Arrange
            Twizo twizo = new Twizo(apiKey, apiHost);

            //Act
            var results = twizo.GetNumberLookupResults();

            //Assert
            Assert.IsNotNull(results);
        }

        [TestMethod()]
        public void CreateWidgetSessionTest()
        {
            //Arrange
            string[] allowedTypes = new[] { WidgetSession.TYPE_SMS, WidgetSession.TYPE_BACKUP_CODE };
            string recipient = "601151174973";
            Twizo twizo = new Twizo(apiKey, apiHost);

            //Act
            var session = twizo.CreateWidgetSession(allowedTypes, recipient);

            //Assert
            Assert.AreEqual(session.allowedTypes, allowedTypes);
            Assert.AreEqual(session.recipient, recipient);
        }

        [TestMethod()]
        public void CreateWidgetSessionTestNull()
        {
            //Arrange
            string[] allowedTypes = new[] { WidgetSession.TYPE_SMS, WidgetSession.TYPE_BACKUP_CODE };
            string recipient = null;
            Twizo twizo = new Twizo(apiKey, apiHost);

            //Act
            var session = twizo.CreateWidgetSession(allowedTypes, recipient);

            //Assert
            Assert.AreEqual(session.allowedTypes, allowedTypes);
            Assert.AreEqual(session.recipient, recipient);
        }

        [TestMethod()]
        public void GetWidgetSessionTest()
        {
            //Arrange
            string recipient = "601151174973";
            string[] allowedTypes = new[] {WidgetSession.TYPE_SMS};
            Twizo twizo = new Twizo(apiKey, apiHost);

            //Act
            var session = twizo.CreateWidgetSession(allowedTypes, recipient);
            session.Create();
            string token = session.sessionToken;
            var newSession = twizo.GetWidgetSession(token, recipient);

            //Assert
            Assert.AreEqual(session.sessionToken, newSession.sessionToken);
            Assert.AreEqual(session.recipient, newSession.recipient);
        }

        [TestMethod()]
        public void CreateBalanceTest()
        {
            //Arrange
            Twizo twizo = new Twizo(apiKey, apiHost);

            //Act
            var balance = twizo.CreateBalance();

            //Assert
            Assert.IsNotNull(balance);
        }


    }
}