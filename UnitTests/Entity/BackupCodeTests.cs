using Microsoft.VisualStudio.TestTools.UnitTesting;
using TwizoAPI.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwizoAPI.Responses;
using TwizoAPI.Client;
using TwizoAPI.Entity.ValidationExceptions;
using TwizoAPI.Entity.BackupCodeExceptions;

namespace UnitTests.Entity
{
    [TestClass()]
    public class BackupCodeTests
    {
        [TestMethod()]
        public void CreateTest()
        {
            //Arrange
            TwizoClient client = new TwizoClient(TwizoTests.apiKey, TwizoTests.apiHost);
            BackupCode backup = new BackupCode(client);
            backup.identifier = "myIdentifier";

            //Act
            backup.Delete();
            Response response = backup.Create();

            //Assert
            Assert.AreEqual(response.statusCode, 201);
            Assert.IsTrue(response.body.Count > 0);
        }

        [TestMethod()]
        public void PopulateTest()
        {
            //Arrange
            TwizoClient client = new TwizoClient(TwizoTests.apiKey, TwizoTests.apiHost);
            BackupCode backup = new BackupCode(client);
            backup.identifier = "myIdentifierPopulateTest";

            //Act
            backup.Delete();
            backup.Create();
            var newBackup = new BackupCode(client);
            newBackup.Populate(backup.identifier);

            //Assert
            Assert.AreEqual(newBackup.identifier, backup.identifier);
            Assert.AreEqual(newBackup.amountOfCodesLeft, backup.amountOfCodesLeft);
        }

        [TestMethod()]
        [ExpectedException(typeof(EntityException))]
        public void PopulateTestNull()
        {
            //Arrange
            TwizoClient client = new TwizoClient(TwizoTests.apiKey, TwizoTests.apiHost);
            BackupCode backup = new BackupCode(client);

            //Act
            backup.Populate(null);
        }

        [TestMethod()]
        [ExpectedException(typeof(EntityException))]
        public void PopulateTestInvalidIdentifier()
        {
            //Arrange
            TwizoClient client = new TwizoClient(TwizoTests.apiKey, TwizoTests.apiHost);
            BackupCode backup = new BackupCode(client);

            //Act
            backup.Populate("myIdentifier");
        }

        [TestMethod()]
        public void UpdateTest()
        {
            //Arrange
            TwizoClient client = new TwizoClient(TwizoTests.apiKey, TwizoTests.apiHost);
            BackupCode backup = new BackupCode(client);
            backup.identifier = "myIdentifier";

            //Act
            //backup.Create();
            Response response = backup.Update();

            //Assert
            Assert.AreEqual(response.statusCode, 200);
            Assert.IsTrue(response.body.Count > 0);
        }

        [TestMethod()]
        [ExpectedException(typeof(EmptyTokenException))]
        public void VerifyTest()
        {
            //Arrange
            TwizoClient client = new TwizoClient(TwizoTests.apiKey, TwizoTests.apiHost);
            BackupCode backup = new BackupCode(client);

            //Act
            backup.Verify(null, "myIdentifier");
        }

        [TestMethod()]
        [ExpectedException(typeof(EmptyIdentifierException))]
        public void VerifyTest2()
        {
            //Arrange
            TwizoClient client = new TwizoClient(TwizoTests.apiKey, TwizoTests.apiHost);
            BackupCode backup = new BackupCode(client);

            //Act
            backup.Verify("myToken");
        }
    }
}