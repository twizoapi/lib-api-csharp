using Microsoft.VisualStudio.TestTools.UnitTesting;
using TwizoAPI.Entity.BackupCodeExceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwizoAPI;
using TwizoAPI.Entity;
using TwizoAPI.Responses;

namespace UnitTests.Entity.BackupCodeExceptions
{
    [TestClass()]
    public class BackupCodeExceptionTests
    {
        [TestMethod()]
        public void BackupCodeExceptionTest1()
        {
            //Arrange
            EntityException entityexception =
                new EntityException("This is a test exception", ErrorCode.INVALID_FIELDS, 200, 103);

            //Act
            BackupCodeException backupexception = new BackupCodeException(entityexception);

            //Assert
            Assert.AreEqual(backupexception.Message, "Invalid token");
        }

        [TestMethod()]
        public void BackupCodeExceptionTest2()
        {
            //Arrange
            EntityException entityexception =
                new EntityException("This is a test exception", ErrorCode.INVALID_FIELDS, 200, 999);

            //Act
            BackupCodeException backupexception = new BackupCodeException(entityexception);

            //Assert
            Assert.AreEqual(backupexception.Message, "This is a test exception");
        }

        [TestMethod()]
        public void IsBackupCodeExceptionTest()
        {
            //Arrange
            BackupCodeException exception = new BackupCodeException(new EntityException(
                "This is a test message", ErrorCode.INVALID_FIELDS, RestStatusCodes.REST_CLIENT_ERROR_UNPROCESSABLE_ENTITY, 1));

            //Assert
            Assert.IsTrue(BackupCodeException.IsBackupCodeException(exception));
        }
    }
}