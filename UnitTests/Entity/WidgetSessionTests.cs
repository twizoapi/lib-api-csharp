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
    public class WidgetSessionTests
    {
        [TestMethod()]
        public void CreateTest()
        {
            //Arrange
            TwizoClient client = new TwizoClient(TwizoTests.apiKey, TwizoTests.apiHost);
            WidgetSession session = new WidgetSession(client);
            session.recipient = "601151174973";
            session.bodyTemplate = "This is a unit test %token%";
            session.tag = "Unit tester";
            session.allowedTypes = new[] {WidgetSession.TYPE_SMS};

        //Act
            Response response = session.Create();

            //Assert
            Assert.AreEqual(response.statusCode, 201);
            Assert.IsTrue(response.body.Count > 0);
        }

        [TestMethod()]
        public void PopulateTest()
        {
            //Arrange
            TwizoClient client = new TwizoClient(TwizoTests.apiKey, TwizoTests.apiHost);
            WidgetSession session = new WidgetSession(client);
            session.recipient = "601151174973";
            session.bodyTemplate = "This is a unit test %token%";
            session.tag = "Unit tester";
            session.allowedTypes = new[] { WidgetSession.TYPE_SMS };

            //Act
            session.Create();
            var newSession = new WidgetSession(client);
            newSession.Populate(session.sessionToken, session.recipient);

            //Assert
            Assert.AreEqual(newSession.sessionToken, session.sessionToken);
            Assert.AreEqual(newSession.bodyTemplate, session.bodyTemplate);
            Assert.AreEqual(newSession.tag, session.tag);
        }

        [TestMethod()]
        [ExpectedException(typeof(EntityException))]
        public void PopulateTestNull()
        {
            //Arrange
            TwizoClient client = new TwizoClient(TwizoTests.apiKey, TwizoTests.apiHost);
            WidgetSession session = new WidgetSession(client);

            //Act
            session.Populate(null);
        }

        [TestMethod()]
        [ExpectedException(typeof(EntityException))]
        public void PopulateTestInvalidIdentifier()
        {
            //Arrange
            TwizoClient client = new TwizoClient(TwizoTests.apiKey, TwizoTests.apiHost);
            WidgetSession session = new WidgetSession(client);

            //Act
            session.Populate("myMessageId");
        }
    }
}