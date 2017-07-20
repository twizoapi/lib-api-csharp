using System;
using System.Collections.Generic;
using TwizoAPI.Responses;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

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
    /// Poll the results of all sms messages or number lookups of the past 24 hour, with a maximum of 10 reports.
    /// </summary>
    public class Poll : AbstractEntity
    {
        /// <summary>Poll type for sms</summary>
        public const string TYPE_SMS = "sms";
        /// <summary>Poll type for number lookup</summary>
        public const string TYPE_NUMBER_LOOKUP = "numberlookup";

        private readonly string type;
        private readonly List<Dictionary<string, object>> messages;

        /// <summary>Gets the batch id of the poll.</summary>
        public string batchId { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Poll"/> class.
        /// </summary>
        /// <param name="client"><see cref="AbstractClient"/> object, used to send and receive data.</param>
        /// <param name="type">The poll type, either <see cref="Poll.TYPE_SMS"/> or <see cref="Poll.TYPE_NUMBER_LOOKUP"/>.</param>
        public Poll(AbstractClient client, string type) : base(client)
        {
            this.type = type;
            messages = new List<Dictionary<string, object>>();
        }

        /// <summary>Get the location to which the server call should be made.</summary>
        protected override string GetCreateUrl()
        {
            return type + "/poll";
        }

        /// <summary>
        /// Create a poll entity on the server.
        /// </summary>
        /// <returns><see cref="Response"/> object with the server response.</returns>
        /// <exception cref="EntityException">Thrown when the server returns a non-success http status code or an invalid response.</exception>
        public Response Send()
        {
            return SendApiCall(ACTION_RETRIEVE, GetCreateUrl());
        }

        /// <summary>
        /// Get a list of the poll messages.
        /// </summary>
        /// <returns>The poll messages.</returns>
        public List<Dictionary<string, object>> GetMessages()
        {
            return messages;
        }

        /// <summary>
        /// Delete this poll from the server if the batch id is not <c>null</c>.
        /// </summary>
        /// <exception cref="EntityException">Thrown when the server returns a non-success http status code or an invalid response.</exception>
        public void Delete()
        {
            if (!String.IsNullOrEmpty(batchId))
            {
                SendApiCall(ACTION_REMOVE, $"{GetCreateUrl()}/{batchId}");
            }
        }

        /// <summary>
        /// Fill the entity properties using the API response.
        /// </summary>
        /// <param name="fields">Deserialized API response.</param>
        internal override void FillObject(Dictionary<string, object> fields)
        {
            base.FillObject(fields);

            if (String.IsNullOrEmpty(batchId))
            {
                batchId = null;
            }

            if (fields.ContainsKey("_embedded"))
            {
                var embedded = JsonConvert.DeserializeObject<Dictionary<string, object>>(fields["_embedded"].ToString());
                if (embedded.ContainsKey("messages"))
                {
                    var jarray = JArray.Parse(embedded["messages"].ToString());
                    foreach (var item in jarray)
                    {
                        messages.Add(JsonConvert.DeserializeObject<Dictionary<string, object>>(item.ToString()));
                    }
                }
            }
        }

    }
}
