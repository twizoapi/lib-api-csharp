using System;
using System.Linq;
using TwizoAPI.Responses;

namespace TwizoAPI.Entity.BackupCodeExceptions
{
    /**
    * This file is part of the Twizo C# API.
    * 
    * (c) Twizo - info@twizo.com
    * 
    * For the full copyright and license information, please view the LICENSE file that was distributed with this source code.
    */

    /// <summary>
    /// Backup code exception class.
    /// </summary>
    public class BackupCodeException : EntityException
    {
        /// <summary>
        /// Backup code error codes.
        /// </summary>
        public enum BackupCodeError
        {
            NO_IDENTIFIER_SUPPLIED = 1,
            NO_TOKEN_SUPPLIED = 1,
            IDENTIFIER_TOO_LONG = 2,
            INVALID_TOKEN = 103
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BackupCodeException"/> class.
        /// </summary>
        /// <param name="e">Underlying <see cref="EntityException"/>.</param>
        public BackupCodeException(EntityException e) : base(GetMessage(e), ErrorCode.BACKUP_CODE_FAILED, e.statusCode, e.jsonErrorCode) { }

        /// <summary>
        /// Get the error message based on the error code.
        /// </summary>
        /// <param name="e">Underlying <see cref="EntityException"/></param>
        /// <returns>Error message.</returns>
        private static string GetMessage(EntityException e)
        {
            switch (e.jsonErrorCode)
            {
                case (int)BackupCodeError.INVALID_TOKEN:
                    return "Invalid token";
                case (int)BackupCodeError.IDENTIFIER_TOO_LONG:
                    return "Identifier is too long";
                default:
                    return e.Message;
            }
        }

        /// <summary>
        /// Get a list of all valid json error codes for backup code failures.
        /// </summary>
        /// <returns>Array of json error codes.</returns>
        protected static int?[] GetBackupCodeJsonErrorCodes()
        {
            return Enum.GetValues(typeof(BackupCodeError))
                .Cast<int>()
                .ToArray()
                .Cast<int?>()
                .ToArray();
        }

        /// <summary>
        /// Test if the <see cref="EntityException"/> is the result of a backup code exception.
        /// </summary>
        /// <param name="e">The <see cref="EntityException"/> to test.</param>
        /// <returns><c>true</c> if the exception is a backup code exception, otherwise <c>false</c>.</returns>
        public static Boolean IsBackupCodeException(EntityException e)
        {
            int?[] backupCodes = new int?[]
            {
                RestStatusCodes.REST_CLIENT_ERROR_CONFLICT,
                RestStatusCodes.REST_CLIENT_ERROR_UNPROCESSABLE_ENTITY,
                RestStatusCodes.REST_CLIENT_ERROR_LOCKED
            };
            int?[] jsonErrorCodes = GetBackupCodeJsonErrorCodes();

            if (backupCodes.Contains(e.statusCode) && jsonErrorCodes.Contains(e.jsonErrorCode))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
