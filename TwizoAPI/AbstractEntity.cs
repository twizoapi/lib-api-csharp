using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using TwizoAPI.Client;
using TwizoAPI.Entity;
using TwizoAPI.Entity.ValidationExceptions;
using TwizoAPI.Responses;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

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
    /// Result types for sms and numberlookup.
    /// </summary>
    public enum ResultType
    {
        NO_RESULTS,
        CALLBACK,
        POLLING,
        CALLBACK_AND_POLLING
    }

    /// <summary>
    /// Abstract parent class of all entity objects.
    /// </summary>
    public abstract class AbstractEntity
    {
        /// <summary>Http verb GET, used to retrieve server data.</summary>
        protected const string ACTION_RETRIEVE = "GET";
        /// <summary>Http verb POST, used to submit server data.</summary>
        protected const string ACTION_SUBMIT = "POST";
        /// <summary>Http verb DELETE, used to remove server data.</summary>
        protected const string ACTION_REMOVE = "DELETE";
        /// <summary>Http verb PUT, used to update server data.</summary>
        protected const string ACTION_UPDATE = "PUT";

        /// <summary><see cref="AbstractClient"/> object, used to send and receive data.</summary>
        protected AbstractClient client;

        /// <summary>The parameters to be sent to the server.</summary>
        protected Dictionary<string, object> parameters;

        /// <summary>
        /// Initializes a new instance of the <see cref="AbstractEntity"/> clas.
        /// </summary>
        /// <param name="client"><see cref="AbstractClient"/> object, used to send and receive data.</param>
        protected AbstractEntity(AbstractClient client)
        {
            this.client = client;
            parameters = new Dictionary<string, object>();
        }

        /// <summary>Get the location to which the server call should be made.</summary>
        protected abstract string GetCreateUrl();

        /// <summary>
        /// Add a parameter to be sent to the server.
        /// </summary>
        /// <param name="key">The property name.</param>
        /// <param name="value">The property value.</param>
        protected void AddParameter(string key, object value)
        {
            if (PropertyExists(key))
            {
                parameters[key] = value;
            }
        }

        /// <summary>
        /// Detect invalid json fields in the entity and throws an exception when found.
        /// </summary>
        /// <exception cref="EntityException">Thrown when the <see cref="AbstractEntity.parameters"/> of this entity could not be serialized.</exception>
        protected void DetectInvalidJsonFields()
        {
            try
            {
                JsonConvert.SerializeObject(parameters);
            }
            catch (JsonSerializationException)
            {
                List<string> invalidFields = new List<string>();
                foreach (KeyValuePair<string, object> parameter in parameters)
                {
                    try
                    {
                        JsonConvert.SerializeObject(parameter);
                    }
                    catch (JsonSerializationException)
                    {
                        invalidFields.Add(parameter.Key);
                    }
                }
                throw new EntityException("Invalid data found in field(s): " + String.Join(", ", invalidFields), ErrorCode.INVALID_FIELDS);
            }
        }

        /// <summary>
        /// Send an API call to the <see cref="AbstractClient"/>.
        /// </summary>
        /// <param name="verb">The http verb to be used with the API call.</param>
        /// <param name="location">The URL to which the API call should be made.</param>
        /// <returns><see cref="Response"/> object with the server response.</returns>
        /// <exception cref="EntityException">Thrown when the <see cref="AbstractEntity.parameters"/> of this entity could not be serialized.</exception>
        /// <exception cref="ValidationException">Thrown when incorrect parameters were sent to the server.</exception>
        /// <exception cref="EntityException">Thrown when the server returns a non-success http status code or an invalid response.</exception>
        protected Response SendApiCall(string verb, string location)
        {
            try
            {
                DetectInvalidJsonFields();

                Response response = (verb == ACTION_SUBMIT || verb == ACTION_UPDATE) ? client.SendRequest(location, verb, parameters) : client.SendRequest(location, verb);

                FillObject(response.body);

                return response;
            }
            catch (ClientException e)
            {
                if (e.response != null)
                {
                    Dictionary<string, object> items = GetItems(e.response.body);
                    if (items.ContainsKey("validation_messages"))
                    {
                        throw new ValidationException(
                            JsonConvert.DeserializeObject<Dictionary<string, object>>(items["validation_messages"].ToString()),
                            parameters);
                    }
                    else {
                        int? statusCode = null;
                        if (items.ContainsKey("errorCode"))
                        {
                            statusCode = int.Parse(items["errorCode"].ToString());
                        }
                        throw new EntityException(
                            "Exception received from API client: " + e.Message,
                            e.errorCode,
                            e.response.statusCode,
                            statusCode);
                    }
                }
                else
                {
                    throw new EntityException("Exception received from API client: " + e.Message, e.errorCode);
                }
            }
                
        }

        /// <summary>
        /// Fill the entity properties using the API response.
        /// </summary>
        /// <param name="fields">Deserialized API response.</param>
        internal virtual void FillObject(Dictionary<string, object> fields)
        {
            if (fields.ContainsKey("_embedded"))
            {
                fields = GetItems(fields);
            }

            if (fields.Count > 0)
            {
                Type type = GetType();
                foreach (KeyValuePair<string, object> field in fields)
                {
                    PropertyInfo property = type.GetProperty(field.Key);
                    if (property != null)
                    {
                        try
                        {
                            property.SetValue(this, Convert.ChangeType(field.Value, property.PropertyType), null);
                        }
                        catch
                        {
                            try
                            {
                                var jarray = JArray.Parse(field.Value.ToString());
                                property.SetValue(this, jarray.ToObject<string[]>());
                            }
                            catch { }
                        }
                    }
                }
                
            }
        }

        /// <summary>
        /// Find the embedded items from an API response.
        /// </summary>
        /// <param name="fields">Deserialized API response.</param>
        /// <returns>If found, returns embedded items. Otherwise, returns <paramref name="fields"/></returns>
        protected Dictionary<string, object> GetItems(Dictionary<string, object> fields)
        {
            Dictionary<string, object> embedded = new Dictionary<string, object>();
            if (fields.ContainsKey("_embedded"))
                embedded = JsonConvert.DeserializeObject<Dictionary<string, object>>(fields["_embedded"].ToString());
            if (embedded.ContainsKey("items"))
            {
                var jarray = JArray.Parse(embedded["items"].ToString());
                return JsonConvert.DeserializeObject<Dictionary<string, object>>(jarray[0].ToString());
            }
            else
            {
                return fields;
            }
        }

        /// <summary>
        /// Get the entity status for the supplied message id.
        /// </summary>
        /// <param name="messageId">The message id of the entity.</param>
        /// <exception cref="EntityException">Thrown when no message id was supplied.</exception>
        /// <exception cref="EntityException">Thrown when the server returns a non-success http status code or an invalid response.</exception>
        public void Populate(string messageId)
        {
            if (String.IsNullOrEmpty(messageId))
            {
                throw new EntityException("No message ID supplied", ErrorCode.NO_MESSAGE_ID_SUPPLIED);
            }
            SendApiCall(ACTION_RETRIEVE, $"{GetCreateUrl()}/{messageId}");
        }

        /// <summary>
        /// Check if this entity contains a property with the given name.
        /// </summary>
        /// <param name="propertyName">The name of the entity.</param>
        /// <returns><c>true</c> if the property exists, otherwise <c>false</c></returns>
        private bool PropertyExists(string propertyName)
        {
            Type type = GetType();
            PropertyInfo property = type.GetProperty(propertyName);
            return (property != null);
        }
    }
}
