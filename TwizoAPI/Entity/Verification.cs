using System;
using TwizoAPI.Entity.VerificationExceptions;
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
    /// Verification status codes.
    /// </summary>
    public enum VerificationStatusCode
    {
        NO_STATUS,
        SUCCESS,
        REJECTED,
        EXPIRED,
        FAILED
    }

    /// <summary>
    /// Create a verification and verify a token.
    /// </summary>
    public class Verification : AbstractEntity
    {
        /// <summary>Verification type for call.</summary>
        public const string TYPE_CALL = "call";

        /// <summary>Verification type for sms.</summary>
        public const string TYPE_SMS = "sms";

        /// <summary>Numeric token type.</summary>
        public const string TOKEN_TYPE_NUMERIC = "numeric";

        /// <summary>Alphanumeric token type.</summary>
        public const string TOKEN_TYPE_ALPHANUMERIC = "alphanumeric";

        /// <summary>Gets the application used for sending the verification.</summary>
        public string applicationTag { get; private set; }

        /// <summary>Gets the datetime the verification was received by the API. Datetime is in ISO-8601 format.</summary>
        public string createdDateTime { get; private set; }

        /// <summary>Gets the language for this verification.</summary>
        public string language { get; private set; }

        /// <summary>Gets the message id, a unique id identifying this verification.</summary>
        public string messageId { get; private set; }

        /// <summary>Gets the reason code if the verification was rejected.</summary>
        public int reasonCode { get; private set; }

        /// <summary>Gets the sales price for this verification.</summary>
        public float salesPrice { get; private set; }

        /// <summary>Gets the currency for the sale of this verification.</summary>
        public float salesPriceCurrencyCode { get; private set; }

        /// <summary>Gets the status of this verification.</summary>
        public string status { get; private set; }

        /// <summary>Gets the <see cref="VerificationStatusCode"/> of this verification.</summary>
        public int statusCode { get; private set; }

        /// <summary>Gets the datetime until which this verification is valid.</summary>
        public string validUntilDateTime { get; private set; }

        private string _bodyTemplate;
        /// <summary>Gets or sets the body template in case the verification is sent by sms.</summary>
        public string bodyTemplate
        {
            get => _bodyTemplate;
            set { _bodyTemplate = value; AddParameter("bodyTemplate", value); }
        }

        private int _dcs;
        /// <summary>Gets or sets the dcs, used to specify the character set of the body template.</summary>
        public int dcs
        {
            get => _dcs;
            set { _dcs = value; AddParameter("dcs", value); }
        }

        private string _recipient;
        /// <summary>Gets or sets the recipient. Single phone number in international format.</summary>
        public string recipient
        {
            get => _recipient;
            set { _recipient = value; AddParameter("recipient", value); }
        }

        private string _sender;
        /// <summary>Gets or sets the sender of the verification.</summary>
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

        private string _sessionId;
        /// <summary>Gets or sets the session id, a unique id for the verification session.</summary>
        public string sessionId
        {
            get => _sessionId;
            set { _sessionId = value; AddParameter("sessionId", value); }
        }

        private string _tag;
        /// <summary>Gets or sets the tag for this verification. Free text parameter you can use for your own reference.</summary>
        public string tag
        {
            get => _tag;
            set { _tag = value; AddParameter("tag", value); }
        }

        private int _tokenLength;
        /// <summary>Gets or sets the token length.</summary>
        public int tokenLength
        {
            get => _tokenLength;
            set { _tokenLength = value; AddParameter("tokenLength", value); }
        }

        private string _tokenType;
        /// <summary>Gets or sets token type. Valid types are <see cref="Verification.TOKEN_TYPE_NUMERIC"/> and <see cref="Verification.TOKEN_TYPE_ALPHANUMERIC"/>.</summary>
        public string tokenType
        {
            get => _tokenType;
            set { _tokenType = value; AddParameter("tokenType", value); }
        }

        private string _type;
        /// <summary>Gets or sets the type. Valid types are <see cref="Verification.TYPE_CALL"/> and <see cref="Verification.TYPE_SMS"/>.</summary>
        public string type
        {
            get => _type;
            set { _type = value; AddParameter("type", value); }
        }

        private int _validity;
        /// <summary>Gets or sets the validity of the token in seconds.</summary>
        public int validity
        {
            get => _validity;
            set { _validity = value; AddParameter("validity", value); }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Verification"/> class.
        /// </summary>
        /// <param name="client"><see cref="AbstractClient"/> object, used to send and receive data.</param>
        public Verification(AbstractClient client) : base(client) { }

        /// <summary>Get the location to which the server call should be made.</summary>
        protected override string GetCreateUrl()
        {
            return "verification/submit";
        }

        /// <summary>
        /// Send a new verification token to the recipient and return the server response.
        /// </summary>
        /// <returns><see cref="Response"/> object with the server response.</returns>
        /// <exception cref="EntityException">Thrown when the <see cref="AbstractEntity.parameters"/> of the verification could not be serialized.</exception>
        /// <exception cref="ValidationExceptions.ValidationException">Thrown when incorrect parameters were sent to the server.</exception>
        /// <exception cref="EntityException">Thrown when the server returns a non-success http status code or an invalid response.</exception>
        public Response Send()
        {
            return SendApiCall(ACTION_SUBMIT, GetCreateUrl());
        }

        /// <summary>
        /// Verify the supplied token with 
        /// </summary>
        /// <param name="token">The token to be verified.</param>
        /// <param name="messageId">The message id of the verification (optional).</param>
        /// <exception cref="EmptyTokenException">Thrown when the supplied token is empty or null.</exception>
        /// <exception cref="EmptyMessageIdException">Thrown when the message id is empty or null.</exception>
        /// <exception cref="EntityException">Thrown when the server returns a non-success http status code or an invalid response.</exception>
        /// <exception cref="VerificationException">Thrown when the returned error code is a verification error.</exception>
        public void Verify(string token, string messageId = null)
        {             
            if (!String.IsNullOrEmpty(messageId))
            {
                this.messageId = messageId;
            }

            if (String.IsNullOrEmpty(token))
            {
                throw new EmptyTokenException();
            }

            if (String.IsNullOrEmpty(messageId))
            {
                throw new EmptyMessageIdException();
            }

            try
            {
                string location = $"{GetCreateUrl()}/{this.messageId}?token={token}";
                SendApiCall(ACTION_RETRIEVE, location);
            }
            catch (EntityException e)
            {
                if (VerificationException.IsVerificationException(e))
                {
                    throw new VerificationException(e);
                }
                else
                {
                    throw;
                }
            }
        }   
    }
}
