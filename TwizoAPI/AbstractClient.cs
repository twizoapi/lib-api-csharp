using System;
using System.Collections.Generic;
using TwizoAPI.Client;
using TwizoAPI.Responses;
using Newtonsoft.Json;

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
    /// Abstract client class to connect to the API server.
    /// </summary>
    public abstract class AbstractClient
    { 
        /// <summary>The API username to be used.</summary>
        protected const string API_USERNAME = "twizo";
        /// <summary>The API version to be used.</summary>
        protected const string API_VERSION = "v1";

        /// <summary>The provided Twizo API key.</summary>
        protected string apiKey;
        /// <summary>The provided Twizo host.</summary>
        protected string apiHost;

        /// <summary>
        /// Initializes a new instance of the <see cref="AbstractClient"/> class.
        /// </summary>
        /// <param name="apiKey">Your Twizo API key.</param>
        /// <param name="apiHost">The Twizo host of your choice.</param>
        /// <exception cref="ArgumentNullException">Thrown when the provided apiKey or apiHost are null.</exception>
        protected AbstractClient(string apiKey, string apiHost)
        {
            this.apiKey = apiKey ?? throw new ArgumentNullException(nameof(apiKey), "The apiKey parameter can not be null.");
            this.apiHost = apiHost ?? throw new ArgumentNullException(nameof(apiHost), "The apiHost parameter can not be null.");
        }

        /// <summary>
        /// Get the full URL for a given location.
        /// </summary>
        /// <param name="location">The API location to be used.</param>
        /// <returns>Full URL to Twizo server.</returns>
        protected string GetUrl(string location)
        {
            return $"https://{apiHost}/{API_VERSION}/{location}";
        }

        /// <summary>
        /// Deserialize the server response json and creates a <see cref="Response"/> object with its data.
        /// </summary>
        /// <param name="statusCode">The http status code returned by the server.</param>
        /// <param name="json">The json returned by the server.</param>
        /// <returns><see cref="Response"/> object with the server reponse.</returns>
        /// <exception cref="ClientException">Thrown when an invalid json was received from the server.</exception>
        /// <exception cref="ClientException">Thrown when the server returns a non-success http status code.</exception>
        protected Response GenerateResponse(int statusCode, string json)
        {
            if (String.IsNullOrEmpty(json) && statusCode == RestStatusCodes.REST_SUCCESS_NO_CONTENT)
            {
                return new Response(new Dictionary<string, object>(), statusCode);
            }
            else
            {
                Dictionary<string, object> body;
                try
                {
                    body = JsonConvert.DeserializeObject<Dictionary<string, object>>(json);
                }
                catch (JsonSerializationException)
                {
                    throw new ClientException("Error while sending request to API; Received invalid json: " + json, ErrorCode.SERVICE_UNAVAILABLE);
                }

                Response response = new Response(body, statusCode);
                ValidateServerResponse(response);

                return response;
            }
        }

        /// <summary>
        /// Validate the response from the server and throws appropriate exception if the call was not successful.
        /// </summary>
        /// <param name="response"><see cref="Response"/> object created from the server response.</param>
        /// <exception cref="ClientException">Thrown when the server returns a non-success http status code.</exception>
        protected void ValidateServerResponse(Response response)
        {
            switch (response.statusCode)
            {
                case RestStatusCodes.REST_CLIENT_ERROR_UNAUTHORIZED:
                    throw new ClientException("You have provided an invalid API key", ErrorCode.INVALID_APPLICATION_SECRET, response);
                case RestStatusCodes.REST_CLIENT_ERROR_FORBIDDEN:
                    throw new ClientException("Your account is not enabled for the service", ErrorCode.INVALID_APPLICATION_SECRET, response);
                case RestStatusCodes.REST_CLIENT_ERROR_NOT_FOUND:
                    throw new ClientException("The requested entity was not found on the server", ErrorCode.INVALID_RESPONSE, response);
                case RestStatusCodes.REST_CLIENT_ERROR_TOO_MANY_REQUESTS:
                    throw new ClientException("You are sending too fast and your calls are throttled", ErrorCode.SERVER_UNAVAILABLE, response);
                case RestStatusCodes.REST_CLIENT_ERROR_UNPROCESSABLE_ENTITY:
                    throw new ClientException("One or more of the parameters supplied are invalid", ErrorCode.INVALID_FIELDS, response);
                case RestStatusCodes.REST_SUCCESS_OK:
                case RestStatusCodes.REST_SUCCESS_CREATED:
                    break;
                default:
                    throw new ClientException($"Unknown status code {response.statusCode} received from server", ErrorCode.INVALID_RESPONSE, response);
            }
        }

        /// <summary>
        /// Send a request to the Twizo server and return its response.
        /// </summary>
        /// <param name="location">The API location to which to request should be sent.</param>
        /// <param name="verb">The http verb to be used.</param>
        /// <param name="parameters">The parameters to be sent with the request (optional).</param>
        /// <returns><see cref="Response"/> object with the server response.</returns>
        /// <exception cref="ClientException">Thrown when an invalid json was received from the server.</exception>
        /// <exception cref="ClientException">Thrown when the server returns a non-success http status code.</exception>
        /// <exception cref="JsonSerializationException">Thrown when the parameters could not be serialized.</exception>
        public abstract Response SendRequest(string location, string verb, object parameters = null);
    }
}
