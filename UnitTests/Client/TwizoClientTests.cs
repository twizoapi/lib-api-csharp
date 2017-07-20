using Microsoft.VisualStudio.TestTools.UnitTesting;
using TwizoAPI.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwizoAPI.Responses;

namespace UnitTests.Client
{
    [TestClass()]
    public class TwizoClientTests
    {
        [TestMethod()]
        public void SendRequestTest()
        {
            //Arrange
            string location = "wallet/getbalance";
            string verb = "GET";
            TwizoClient client = new TwizoClient(TwizoTests.apiKey, TwizoTests.apiHost);

            //Act
            Response response =  client.SendRequest(location, verb);

            //Assert
            Assert.AreEqual(response.statusCode, 200);
        }
    }
}