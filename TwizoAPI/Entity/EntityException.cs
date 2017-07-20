namespace TwizoAPI.Entity
{
    /**
    * This file is part of the Twizo C# API.
    * 
    * (c) Twizo - info@twizo.com
    * 
    * For the full copyright and license information, please view the LICENSE file that was distributed with this source code.
    */

    /// <summary>
    /// Entity exception class.
    /// </summary>
    public class EntityException : TwizoException
    {
        /// <summary>Gets the rest status code.</summary>
        public int? statusCode { get; }

        /// <summary>Gets the json error code.</summary>
        public int? jsonErrorCode { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="EntityException"/> class.
        /// </summary>
        /// <param name="message">The error message.</param>
        /// <param name="errorCode">The error code.</param>
        /// <param name="statusCode">The rest status code.</param>
        /// <param name="jsonErrorCode">The json error code.</param>
        public EntityException(string message, ErrorCode errorCode, int? statusCode = null, int? jsonErrorCode = null) : base(message, errorCode)
        {
            this.statusCode = statusCode;
            this.jsonErrorCode = jsonErrorCode;
        }
    }
}
