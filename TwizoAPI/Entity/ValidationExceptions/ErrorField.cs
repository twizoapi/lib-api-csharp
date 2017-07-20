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
    /// Define an error field for use in validation exceptions.
    /// </summary>
    public class ErrorField
    {
        /// <summary>Gets the array index of this ErrorField.</summary>
        public int? arrayIndex { get; private set; }

        /// <summary>Gets the message of this ErrorField.</summary>
        public string message { get; private set; }

        /// <summary>Gets the name of this ErrorField.</summary>
        public string name { get; private set; }

        /// <summary>Gets the type of this ErrorField.</summary>
        public string type { get; private set; }

        /// <summary>Gets the value of this ErrorField.</summary>
        public string value { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ErrorField"/> class.
        /// </summary>
        /// <param name="name">Name of the ErrorField.</param>
        /// <param name="value">Value of the ErrorField.</param>
        /// <param name="type">Type of the ErrorField.</param>
        /// <param name="message">Message of the ErrorField.</param>
        /// <param name="arrayIndex">Array index of the ErrorField.</param>
        public ErrorField(string name, string value, string type, string message, int? arrayIndex = null)
        {
            this.name = name;
            this.value = value;
            this.type = type;
            this.message = message;
            this.arrayIndex = arrayIndex;
        }
    }
}
