using TwizoAPI.Responses;

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
    /// Perform a lookup on one or more numbers.
    /// </summary>
    public class NumberLookup : AbstractEntity
    {
        /// <summary>Gets the application used for sending the verification.</summary>
        public string applicationTag { get; private set; }

        /// <summary>Gets the country code. A two-digit code of the country of the operator the number is subscribed to.</summary>
        public string countryCode { get; private set; }

        /// <summary>Gets the datetime the numberlookup was received by the API. Datetime is in ISO-8601 format.</summary>
        public string createdDateTime { get; private set; }

        /// <summary>Gets the imsi (International Mobile Subscriber Identity) of the SIM card the number is linked to.</summary>
        public string imsi { get; private set; }

        /// <summary>Gets information on whether the number is ported to another operator than the operator who issued the number.</summary>
        public string isPorted { get; private set; }

        /// <summary>Gets information on whether the number is currently roaming.</summary>
        public string isRoaming { get; private set; }

        /// <summary>Gets the message id, a unique id identifying this numberlookup.</summary>
        public string messageId { get; private set; }

        /// <summary>Gets the msc (Mobile Switching Center) of the operator the number is currently registered to.</summary>
        public string msc { get; private set; }

        /// <summary>Gets the network code of the operator the number is subscribed to.</summary>
        public string networkCode { get; private set; }

        /// <summary>Gets the name of the operator the number is subscribed to.</summary>
        public string Operator { get; private set; }

        /// <summary>Gets the reason code if the numberlookup was rejected or could not be performed.</summary>
        public int reasonCode { get; private set; }

        /// <summary>Gets the timestamp when the numberlookup was performed.</summary>
        public string resultTimestamp { get; private set; }

        /// <summary>Gets the sales price for this numberlookup.</summary>
        public float salesPrice { get; private set; }

        /// <summary>Gets the currency for the sale of this numberlookup.</summary>
        public float salesPriceCurrencyCode { get; private set; }

        /// <summary>Gets the status of this numberlookup.</summary>
        public string status { get; private set; }

        /// <summary>Gets the status code of this numberlookup.</summary>
        public int statusCode { get; private set; }

        /// <summary>Gets the datetime until which this numberlookup is valid.</summary>
        public string validUntilDateTime { get; private set; }

        private string _callbackUrl;
        /// <summary>Gets or sets the callback url. If set, status updates will be submitted to this url</summary>
        public string callbackUrl
        {
            get => _callbackUrl;
            set { _callbackUrl = value; AddParameter("callbackUrl", value); }
        }

        private string[] _numbers;
        /// <summary>Gets or sets the array of numbers to be lookup up. Minimum of 1, maximum of 1000 numers.</summary>
        public string[] numbers
        {
            get => _numbers;
            set { _numbers = value; AddParameter("numbers", value); }
        }

        private int _resultType;
        /// <summary>Gets or sets the <see cref="ResultType"/> of the numberlookup.</summary>
        public int resultType
        {
            get => _resultType;
            set { _resultType = value; AddParameter("resultType", value); }
        }

        private string _tag;
        /// <summary>Gets or sets the tag for this numberlookup. Free text parameter you can use for your own reference.</summary>
        public string tag
        {
            get => _tag;
            set { _tag = value; AddParameter("tag", value); }
        }

        private int _validity;
        /// <summary>Gets or sets the validity of the numberlookup in seconds.</summary>
        public int validity
        {
            get => _validity;
            set { _validity = value; AddParameter("validity", value); }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NumberLookup"/> class.
        /// </summary>
        /// <param name="client"><see cref="AbstractClient"/> object, used to send and receive data.</param>
        public NumberLookup(AbstractClient client) : base(client) { }

        /// <summary>Get the location to which the server call should be made.</summary>
        protected override string GetCreateUrl()
        {
            return "numberlookup/submit";
        }

        /// <summary>
        /// Send a new numberlookup with the supplied parameters and return the server response.
        /// </summary>
        /// <returns><see cref="Response"/> object with the server response.</returns>
        /// <exception cref="EntityException">Thrown when the <see cref="AbstractEntity.parameters"/> of the numberlookup could not be serialized.</exception>
        /// <exception cref="ValidationExceptions.ValidationException">Thrown when incorrect parameters were sent to the server.</exception>
        /// <exception cref="EntityException">Thrown when the server returns a non-success http status code or an invalid response.</exception>
        public Response Send()
        {
            Response response = SendApiCall(ACTION_SUBMIT, GetCreateUrl());

            if (GetItems(response.body).Count == 0)
            {
                throw new EntityException("Invalid response returned from server", ErrorCode.INVALID_RESPONSE);
            }

            return response;
        }
    }
}
