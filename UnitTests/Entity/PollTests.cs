using Microsoft.VisualStudio.TestTools.UnitTesting;
using TwizoAPI.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwizoAPI.Client;
using TwizoAPI.Responses;

namespace UnitTests.Entity
{
    [TestClass()]
    public class PollTests
    {
        [TestMethod()]
        public void SendTest()
        {
            //Arrange
            TwizoClient client = new TwizoClient(TwizoTests.apiKey, TwizoTests.apiHost);
            Poll poll = new Poll(client, Poll.TYPE_SMS);

            //Act
            Response response = poll.Send();

            //Assert
            Assert.AreEqual(response.statusCode, 200);
            Assert.IsTrue(response.body.Count > 0);
        }
    }
}