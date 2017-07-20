using TwizoAPI.Responses;

namespace TwizoAPI.Client
{
    /**
    * This file is part of the Twizo C# API.
    * 
    * (c) Twizo - info@twizo.com
    * 
    * For the full copyright and license information, please view the LICENSE file that was distributed with this source code.
    */

    /// <summary>
    /// Client exception class.
    /// </summary>
    public class ClientException : TwizoException
    {
        /// <summary>Gets the server response.</summary>
        public Response response { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ClientException"/> class.
        /// </summary>
        /// <param name="message">The error message.</param>
        /// <param name="errorCode">The <see cref="ErrorCode"/>.</param>
        /// <param name="response">The server response.</param>
        public ClientException(string message, ErrorCode errorCode, Response response = null) : base(message, errorCode)
        {
            this.response = response;
        }
    }
}
