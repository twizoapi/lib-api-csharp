using System;

namespace TwizoAPI
{
    /**
    * This file is part of the Twizo C# API.
    * 
    * (c) Twizo - info@twizo.com
    * 
    * For the full copyright and license information, please view the LICENSE file that was distributed with this source code.
    */

    /// <summary>
    /// Twizo error codes.
    /// </summary>
    public enum ErrorCode
    {
        /// <summary>The server is unavailable.</summary>
        SERVER_UNAVAILABLE = 1,
        /// <summary>An invalid or unknown response was received from the server.</summary>
        INVALID_RESPONSE,
        /// <summary>One or more errors occured while validating the supplied data.</summary>
        VALIDATION_ERRORS,
        /// <summary>The verification failed.</summary>
        VERIFICATION_FAILED,
        /// <summary>No client was found.</summary>
        NO_CLIENT_FOUND,
        /// <summary>One or more of the parameters supplied are invalid.</summary>
        INVALID_FIELDS,
        /// <summary>No message id was supplied.</summary>
        NO_MESSAGE_ID_SUPPLIED,
        /// <summary>The supplied api key is invalid.</summary>
        INVALID_APPLICATION_SECRET,
        /// <summary>An undefined field was requested.</summary>
        UNDEFINED_FIELD_ACCESSED,
        /// <summary>The requested service is unavailable.</summary>
        SERVICE_UNAVAILABLE,
        /// <summary>The requested entity was not found.</summary>
        ENTITY_NOT_FOUND,
        /// <summary>The supplied backup code is invalid.</summary>
        BACKUP_CODE_FAILED
    };

    /// <summary>
    /// General Twizo exception class.
    /// </summary>
    public abstract class TwizoException : Exception
    {
        /// <summary>Gets the <see cref="ErrorCode"/> of this exception.</summary>
        public ErrorCode errorCode { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="TwizoException"/> class.
        /// </summary>
        /// <param name="message">The error message.</param>
        /// <param name="errorCode">The <see cref="ErrorCode"/>.</param>
        protected TwizoException(string message, ErrorCode errorCode) : base(message)
        {
            this.errorCode = errorCode;
        }
    }
}
