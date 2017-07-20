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
    public class NumberLookupTests
    {
        [TestMethod()]
        public void SendTest()
        {
            //Arrange
            TwizoClient client = new TwizoClient(TwizoTests.apiKey, TwizoTests.apiHost);
            NumberLookup lookup = new NumberLookup(client);
            lookup.numbers = new[] {"601151174973"};

            //Act
            Response response = lookup.Send();

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
            NumberLookup lookup = new NumberLookup(client);
            lookup.numbers = null;

            //Act
            Response response = lookup.Send();
        }

        [TestMethod()]
        public void PopulateTest()
        {
            //Arrange
            TwizoClient client = new TwizoClient(TwizoTests.apiKey, TwizoTests.apiHost);
            NumberLookup lookup = new NumberLookup(client);
            lookup.numbers = new[] { "601151174973" };
            lookup.validity = 100;
            lookup.tag = "Unit testing";

            //Act
            Response response = lookup.Send();
            string messageId = lookup.messageId;
            var newLookup = new NumberLookup(client);
            newLookup.Populate(messageId);

            //Assert
            Assert.AreEqual(newLookup.messageId, lookup.messageId);
            Assert.AreEqual(newLookup.validity, lookup.validity);
            Assert.AreEqual(newLookup.tag, lookup.tag);
        }

        [TestMethod()]
        [ExpectedException(typeof(EntityException))]
        public void PopulateTestNull()
        {
            //Arrange
            TwizoClient client = new TwizoClient(TwizoTests.apiKey, TwizoTests.apiHost);
            NumberLookup lookup = new NumberLookup(client);

            //Act
            lookup.Populate(null);
        }

        [TestMethod()]
        [ExpectedException(typeof(EntityException))]
        public void PopulateTestInvalidMessageId()
        {
            //Arrange
            TwizoClient client = new TwizoClient(TwizoTests.apiKey, TwizoTests.apiHost);
            NumberLookup lookup = new NumberLookup(client);

            //Act
            lookup.Populate("myMessageId");
        }
    }
}
