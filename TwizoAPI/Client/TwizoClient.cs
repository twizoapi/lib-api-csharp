using System;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using TwizoAPI.Responses;
using Newtonsoft.Json;

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
    /// Twizo http client class.
    /// </summary>
    public class TwizoClient : AbstractClient
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TwizoClient"/> class.
        /// </summary>
        /// <param name="apiKey">Your Twizo API key.</param>
        /// <param name="apiHost">The Twizo host of your choice.</param>
        public TwizoClient(string apiKey, string apiHost) : base(apiKey, apiHost) { }

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
        public override Response SendRequest(string location, string verb, object parameters = null)
        {
            Uri requestUri = new Uri(GetUrl(location));
            HttpRequestMessage request = new HttpRequestMessage(new HttpMethod(verb), requestUri);

            request.Headers.Add("Accept", "application/json");

            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic",
                    Convert.ToBase64String(Encoding.UTF8.GetBytes($"{API_USERNAME}:{apiKey}")));

            if (parameters != null)
            {
                string data = JsonConvert.SerializeObject(parameters);
                if (!String.IsNullOrEmpty(data) && data != "{}")
                {
                    request.Content = new StringContent(
                        data,
                        Encoding.UTF8,
                        "application/json"
                    );
                }
            }
            try
            {
                return Send(request).Result;
            }
            catch (AggregateException e)
            {
                throw e.InnerException;
            }
        }

        /// <summary>
        /// Send the <see cref="HttpRequestMessage"/> and process the server response.
        /// </summary>
        /// <param name="request">The <see cref="HttpRequestMessage"/> to be sent.</param>
        /// <returns>The server response.</returns>
        /// <exception cref="ClientException">Thrown when an invalid json was received from the server.</exception>
        /// <exception cref="ClientException">Thrown when the server returns a non-success http status code.</exception>
        private async Task<Response> Send(HttpRequestMessage request)
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage HttpResponse = await client.SendAsync(request).ConfigureAwait(false);

                string json = await HttpResponse.Content.ReadAsStringAsync();
                int statusCode = (int)HttpResponse.StatusCode;

                Response response = GenerateResponse(statusCode, json);

                if (!HttpResponse.IsSuccessStatusCode)
                {
                    throw new ClientException("Error while sending request to API: " + HttpResponse.ReasonPhrase, ErrorCode.SERVER_UNAVAILABLE, response);
                }

                return response;
            }
        }
    }
}
