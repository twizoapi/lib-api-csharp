using System;
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
    /// Send an sms to one or more recipients.
    /// </summary>
    public class Sms : AbstractEntity
    {
        // Type of sms: simple will automatically split large messages into multiple sms messages, advanced will reject messages that are too large.
        private const string TYPE_SIMPLE = "simple";
        private const string TYPE_ADVANCED = "advanced";

        /// <summary>Gets the application used for sending the sms.</summary>
        public string applicationTag { get; private set; }

        /// <summary>Gets the datetime the sms was received by the API. Datetime is in ISO-8601 format.</summary>
        public string createdDateTime { get; private set; }

        /// <summary>Gets the message id, a unique id identifying this sms.</summary>
        public string messageId { get; private set; }

        /// <summary>Gets the network code of the operator the number is subscribed to.</summary>
        public int networkCode { get; private set; }

        /// <summary>Gets the reason code if the sms was rejected or could not be sent.</summary>
        public int reasonCode { get; private set; }

        /// <summary>Gets the timestamp when the sms was performed.</summary>
        public string resultTimestamp { get; private set; }

        /// <summary>Gets the sales price for this sms.</summary>
        public float salesPrice { get; private set; }

        /// <summary>Gets the currency for the sale of this sms.</summary>
        public float salesPriceCurrencyCode { get; private set; }

        /// <summary>Gets the status of this sms.</summary>
        public string status { get; private set; }

        /// <summary>Gets the status code of this sms.</summary>
        public int statusCode { get; private set; }

        /// <summary>Gets the type of this sms. Either <see cref="Sms.TYPE_SIMPLE"/> or <see cref="Sms.TYPE_ADVANCED"/>.</summary>
        public string type { get; private set; }

        /// <summary>Gets the datetime until which this numberlookup is valid.</summary>
        public string validUntilDateTime { get; private set; }

        private string _body;
        /// <summary>Gets or sets the body of the sms.</summary>
        public string body
        {
            get => _body;
            set { _body = value; AddParameter("body", value); }
        }

        private string _callbackUrl;
        /// <summary>Gets or sets the callback url. If set, status updates will be submitted to this url</summary>
        public string callbackUrl
        {
            get => _callbackUrl;
            set { _callbackUrl = value; AddParameter("callbackUrl", value); }
        }

        private int _dcs;
        /// <summary>Gets or sets the dcs, used to specify the character set of the body.</summary>
        public int dcs
        {
            get => _dcs;
            set { _dcs = value; AddParameter("dcs", value); }
        }

        private int _pid;
        /// <summary>Gets or sets the pid. Can be used to send hidden sms.</summary>
        public int pid
        {
            get => _pid;
            set { _pid = value; AddParameter("pid", value); }
        }

        private string _recipient;
        /// <summary>Gets or sets the recipient. Single phone number in international format.</summary>
        public string recipient
        {
            get => _recipient;
            set { _recipient = value; recipients = new[] { value }; }
        }

        private string[] _recipients;
        /// <summary>Gets or sets recipients. Array of all the numbers this message was sent to.</summary>
        public string[] recipients
        {
            get => _recipients;
            set { _recipients = value; AddParameter("recipients", value); }
        }

        private int _resultType;
        /// <summary>Gets or sets the <see cref="ResultType"/> of the sms.</summary>
        public int resultType
        {
            get => _resultType;
            set { _resultType = value; AddParameter("resultType", value); }
        }

        private string _scheduledDelivery;
        /// <summary>Gets or sets the datetime of scheduled delivery. Datetime must be in ISO-8601 format.</summary>
        public string scheduledDelivery
        {
            get => _scheduledDelivery;
            set { _scheduledDelivery = value; AddParameter("scheduledDelivery", value); }
        }

        private string _sender;
        /// <summary>Gets or sets the sender of the sms.</summary>
        public string sender
        {
            get => _sender;
            set { _sender = value; AddParameter("sender", value); }
        }

        private int _senderNpi;
        /// <summary>Gets or sets the senderNpi.</summary>
        public int senderNpi
        {
            get => _senderNpi;
            set { _senderNpi = value; AddParameter("senderNpi", value); }
        }

        private int _senderTon;
        /// <summary>Gets or sets the senderTon.</summary>
        public int senderTon
        {
            get => _senderTon;
            set { _senderTon = value; AddParameter("senderTon", value); }
        }

        private string _tag;
        /// <summary>Gets or sets the tag for this sms. Free text parameter you can use for your own reference.</summary>
        public string tag
        {
            get => _tag;
            set { _tag = value; AddParameter("tag", value); }
        }

        private string _udh;
        /// <summary>Gets or sets the udh. Hexadecimal characters used to set concat message.</summary>
        public string udh
        {
            get => _udh;
            set { _udh = value; AddParameter("udh", value); }
        }

        private int _validity;
        /// <summary>Gets or sets the validity of the sms in seconds.</summary>
        public int validity
        {
            get => _validity;
            set { _validity = value; AddParameter("validity", value); }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Sms"/> class.
        /// </summary>
        /// <param name="client"><see cref="AbstractClient"/> object, used to send and receive data.</param>
        public Sms(AbstractClient client) : base(client)
        {
            type = TYPE_ADVANCED;
        }

        /// <summary>Get the location to which the server call should be made.</summary>
        protected override string GetCreateUrl()
        {
            if (type == TYPE_SIMPLE)
                return "sms/submitsimple";
            else
                return "sms/submit";
        }

        /// <summary>
        /// Checks whether the sms has been configured as binary message.
        /// </summary>
        /// <returns><c>true</c> if the sms is configured as binary message, otherwise <c>false</c>.</returns>
        protected Boolean isBinary()
        {
            bool binary = false;

            if ((dcs & 200) == 0)
            {
                binary = ((dcs & 4) > 0);
            }
            else if ((dcs & 248) == 240)
            {
                binary = ((dcs & 4) > 0);
            }
            return binary;
        }

        /// <summary>
        /// Send a new sms with the supplied parameters and return the server response.
        /// </summary>
        /// <returns><see cref="Response"/> object with the server response.</returns>
        /// <exception cref="EntityException">Thrown when the <see cref="AbstractEntity.parameters"/> of the sms could not be serialized.</exception>
        /// <exception cref="ValidationExceptions.ValidationException">Thrown when incorrect parameters were sent to the server.</exception>
        /// <exception cref="EntityException">Thrown when the server returns a non-success http status code or an invalid response.</exception>
        public Response Send()
        {
            if (isBinary())
            {
                body = (Convert.ToInt32(body, 2).ToString("X")).ToUpper();
            }

            Response response = SendApiCall(ACTION_SUBMIT, GetCreateUrl());

            if (GetItems(response.body).Count == 0)
            {
                throw new EntityException("Invalid reponse returned from server", ErrorCode.INVALID_RESPONSE);
            }

            return response;
        }

        /// <summary>
        /// Send a new sms with the supplied parameters and return the server response. Automatically split multi part sms messages.
        /// </summary>
        /// <returns><see cref="Response"/> object with the server response.</returns>
        /// <exception cref="EntityException">Thrown when the <see cref="AbstractEntity.parameters"/> of the sms could not be serialized.</exception>
        /// <exception cref="ValidationExceptions.ValidationException">Thrown when incorrect parameters were sent to the server.</exception>
        /// <exception cref="EntityException">Thrown when the server returns a non-success http status code or an invalid response.</exception>
        public Response SendSimple()
        {
            type = TYPE_SIMPLE;

            Response response = SendApiCall(ACTION_SUBMIT, GetCreateUrl());

            if (GetItems(response.body).Count == 0)
            {
                throw new EntityException("Invalid reponse returned from server", ErrorCode.INVALID_RESPONSE);
            }

            return response;
        }

        
    }
}
