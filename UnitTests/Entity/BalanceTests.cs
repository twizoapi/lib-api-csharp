using Microsoft.VisualStudio.TestTools.UnitTesting;
using TwizoAPI.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwizoAPI;

namespace UnitTests.Entity
{
    [TestClass()]
    public class BalanceTests
    {
        [TestMethod()]
        public void LoadDataTest()
        {
            //Arrange
            Twizo twizo = new Twizo(TwizoTests.apiKey, TwizoTests.apiHost);

            //Act
            var balance = twizo.CreateBalance();
            balance.LoadData();

            //Assert
            Assert.IsNotNull(balance.credit);
            Assert.IsNotNull(balance.freeVerification);
            Assert.IsNotNull(balance.currencyCode);
            Assert.IsNotNull(balance.wallet);
        }
    }
}