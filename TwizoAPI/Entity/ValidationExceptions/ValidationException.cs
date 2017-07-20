using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace TwizoAPI.Entity.ValidationExceptions
{
    /**
    * This file is part of the Twizo C# API.
    * 
    * (c) Twizo - info@twizo.com
    * 
    * For the full copyright and license information, please view the LICENSE file that was distributed with this source code.
    */

    /// <summary>
    /// Entity validation exception class.
    /// </summary>
    public class ValidationException : EntityException
    {
        /// <summary>Gets a list of the <see cref="ErrorField"/> objects in this validation exception.</summary>
        public List<ErrorField> errorFields { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationException"/> class.
        /// </summary>
        /// <param name="validationFields">The validation messages returned by the server.</param>
        /// <param name="parameters">The parameters sent to the server.</param>
        public ValidationException(Dictionary<string, object> validationFields, Dictionary<string, object> parameters) : base(GetExceptionMessages(validationFields, parameters), ErrorCode.VALIDATION_ERRORS)
        {
            errorFields = CreateErrorFields(validationFields, parameters);
        }

        /// <summary>
        /// Parse all validation messages from the API and combine them in a string message.
        /// </summary>
        /// <param name="validationFields">The validation messages returned by the server.</param>
        /// <param name="parameters">The parameters sent to the server.</param>
        /// <returns>Validation error message.</returns>
        private static string GetExceptionMessages(Dictionary<string, object> validationFields, Dictionary<string, object> parameters)
        {
            List<string> messages = new List<string>();
            foreach (ErrorField error in CreateErrorFields(validationFields, parameters))
            {
                messages.Add($"Validation error for field {error.name} : {error.message}");
            }
            return String.Join(", ", messages);
        }

        /// <summary>
        /// Parse all validation messages from the API and create a list of <see cref="ErrorField"/> objects.
        /// </summary>
        /// <param name="validationFields">The validation messages returned by the server.</param>
        /// <param name="parameters">The parameters sent to the server.</param>
        /// <returns>List of <see cref="ErrorField"/> objects representing the validation errors received.</returns>
        private static List<ErrorField> CreateErrorFields(Dictionary<string, object> validationFields, Dictionary<string, object> parameters)
        {
            List<ErrorField> errors = new List<ErrorField>();
            foreach (KeyValuePair<string, object> validationField in validationFields)
            {
                string field = validationField.Key;
                JArray jarray = new JArray();
                //Try to parse multiple errors
                try { jarray = JArray.Parse(validationField.Value.ToString()); }
                //Parse single error
                catch { jarray.Add(JToken.Parse(validationField.Value.ToString())); }
                foreach (var fieldError in jarray)
                {
                    string value;
                    try
                    {
                        value = parameters[field].ToString();
                    }
                    catch (NullReferenceException)
                    {
                        value = null;
                    }
                    errors = AddErrorField(
                        errors,
                        field,
                        value,
                        JsonConvert.DeserializeObject<Dictionary<string, object>>(fieldError.ToString()));
                }
            }
            return errors;
        }

        /// <summary>
        /// Add an <see cref="ErrorField"/> to the list of ErrorFields.
        /// </summary>
        /// <param name="errors">Current list of ErrorFields.</param>
        /// <param name="name">Name of ErrorField.</param>
        /// <param name="value">Value of ErrorField.</param>
        /// <param name="message">Message of ErrorField.</param>
        /// <param name="arrayIndex">Array index of ErrorField.</param>
        /// <returns>List of <see cref="ErrorField"/> objects, including the new ErrorField.</returns>
        private static List<ErrorField> AddErrorField(List<ErrorField> errors, string name, string value, Dictionary<string, object> message, int? arrayIndex = null)
        {
            errors.Add(new ErrorField(name, value, message.Keys.First(), message.Values.First().ToString(), arrayIndex));
            return errors;
        }
    }
}
