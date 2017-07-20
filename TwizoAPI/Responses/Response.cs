using System.Collections.Generic;

namespace TwizoAPI.Responses
{
    /**
    * This file is part of the Twizo C# API.
    * 
    * (c) Twizo - info@twizo.com
    * 
    * For the full copyright and license information, please view the LICENSE file that was distributed with this source code.
    */

    /// <summary>
    /// Response from API server.
    /// </summary>
    public class Response : RestStatusCodes
    {
        /// <summary> Get the response body.</summary>
        public Dictionary<string, object> body { get; }

        /// <summary>Gets the http status code.</summary>
        public int statusCode { get; }

        /// <summary>
        /// Initializes a new instance fo the <see cref="Response"/> class.
        /// </summary>
        /// <param name="body">Deserialized API response.</param>
        /// <param name="statusCode">The http status code returned by the server.</param>
        public Response(Dictionary<string, object> body, int statusCode)
        {
            this.body = body;     
            this.statusCode = statusCode;
        }
    }
}
