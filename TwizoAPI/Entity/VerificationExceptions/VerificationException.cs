using System;
using System.Linq;
using TwizoAPI.Responses;

namespace TwizoAPI.Entity.VerificationExceptions
{
    /**
    * This file is part of the Twizo C# API.
    * 
    * (c) Twizo - info@twizo.com
    * 
    * For the full copyright and license information, please view the LICENSE file that was distributed with this source code.
    */

    /// <summary>
    /// Verification exception class.
    /// </summary>
    public class VerificationException : EntityException
    {
        /// <summary>
        /// Verification error codes.
        /// </summary>
        public enum VerificationError
        {
            TOKEN_ALREADY_VERIFIED = 101,
            TOKEN_EXPIRED = 102,
            TOKEN_INVALID = 103,
            TOKEN_FAILED = 104
        }

        /// <summary>
        /// Initalizes a new instance of the <see cref="VerificationException"/> class.
        /// </summary>
        /// <param name="e">Underlying <see cref="EntityException"/>.</param>
        public VerificationException(EntityException e) : base(GetMessage(e), ErrorCode.VERIFICATION_FAILED, e.statusCode, e.jsonErrorCode) { }

        /// <summary>
        /// Get the error message based on the error code.
        /// </summary>
        /// <param name="e">Underlying <see cref="EntityException"/></param>
        /// <returns>Error message.</returns>
        private static string GetMessage(EntityException e)
        {
            switch (e.jsonErrorCode)
            {
                case (int)VerificationError.TOKEN_ALREADY_VERIFIED:
                    return "The verification was already completed";
                case (int)VerificationError.TOKEN_EXPIRED:
                    return "Verification expired";
                case (int)VerificationError.TOKEN_INVALID:
                    return "Invalid token supplied";
                case (int)VerificationError.TOKEN_FAILED:
                    return "Verification was not sent";
                default:
                    return "Verification of token failed";
            }
        }

        /// <summary>
        /// Get a list of all valid json error codes for verification failures.
        /// </summary>
        /// <returns>Array of json error codes.</returns>
        protected static int?[] GetVerificationJsonErrorCodes()
        {
            return Enum.GetValues(typeof(VerificationError))
                .Cast<int>()
                .ToArray()
                .Cast<int?>()
                .ToArray();
        }

        /// <summary>
        /// Test if the <see cref="EntityException"/> is the result of a verification exception.
        /// </summary>
        /// <param name="e">The <see cref="EntityException"/> to test.</param>
        /// <returns><c>true</c> if the exception is a verification exception, otherwise <c>false</c>.</returns>
        public static Boolean IsVerificationException(EntityException e)
        {
            int?[] verificationCodes = new int?[] {
                RestStatusCodes.REST_CLIENT_ERROR_UNPROCESSABLE_ENTITY,
                RestStatusCodes.REST_CLIENT_ERROR_LOCKED
            };
            int?[] jsonErrorCodes = GetVerificationJsonErrorCodes();

            if (verificationCodes.Contains(e.statusCode) && jsonErrorCodes.Contains(e.jsonErrorCode))
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
