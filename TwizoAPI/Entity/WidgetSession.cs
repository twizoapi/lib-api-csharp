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
    /// Widget session class.
    /// </summary>
    public class WidgetSession : AbstractEntity
    {
        /// <summary>Widget session type for call.</summary>
        public const string TYPE_CALL = "call";

        /// <summary>Wdiget session type for sms.</summary>
        public const string TYPE_SMS = "sms";

        /// <summary>Wdiget session type for backup code.</summary>
        public const string TYPE_BACKUP_CODE = "backupcode";

        /// <summary>Numberic token type.</summary>
        public const string TOKEN_TYPE_NUMERIC = "numeric";

        /// <summary>Alphanumeric token type.</summary>
        public const string TOKEN_TYPE_ALPHANUMERIC = "alphanumeric";

        /// <summary>Gets the application used for sending the verification.</summary>
        public string applicationTag { get; private set; }

        /// <summary>Gets or sets the backup code identifier.</summary>
        public string backupCodeIdentifier { get; set; }

        /// <summary>Gets the datetime the verification was received by the API. Datetime is in ISO-8601 format.</summary>
        public string createdDateTime { get; private set; }

        /// <summary>Gets the language for this session.</summary>
        public string language { get; private set; }

        /// <summary>Gets the session token, a unique id identifying this widget verification session.</summary>
        public string sessionToken { get; private set; }

        /// <summary>Gets the session status.</summary>
        public string status { get; private set; }

        /// <summary>Gets the status code.</summary>
        public int statusCode { get; private set; }

        private string[] _allowedTypes;
        /// <summary>Gets or sets the allowed types. Valid types are <see cref="WidgetSession.TYPE_CALL"/>, <see cref="WidgetSession.TYPE_SMS"/> and <see cref="WidgetSession.TYPE_BACKUP_CODE"/>.</summary>
        public string[] allowedTypes
        {
            get => _allowedTypes;
            set { _allowedTypes = value; AddParameter("allowedTypes", value); }
        }

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

        private string _tag;
        /// <summary>Gets or sets the tag for this session. Free text parameter you can use for your own reference.</summary>
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
        /// <summary>Gets or sets the token type. Valid types are <see cref="WidgetSession.TOKEN_TYPE_NUMERIC"/> and <see cref="WidgetSession.TOKEN_TYPE_ALPHANUMERIC"/>.</summary>
        public string tokenType
        {
            get => _tokenType;
            set { _tokenType = value; AddParameter("tokenType", value); }
        }

        private int _validity;
        /// <summary>Gets or sets the tag for this session. Free text parameter you can use for your own reference.</summary>
        public int validity
        {
            get => _validity;
            set { _validity = value; AddParameter("validity", value); }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="WidgetSession"/> class.
        /// </summary>
        /// <param name="client"><see cref="AbstractClient"/> object, used to send and receive data.</param>
        public WidgetSession(AbstractClient client) : base(client) { }

        /// <summary>Get the location to which the server call should be made.</summary>
        protected override string GetCreateUrl()
        {
            return "widget/session";
        }

        /// <summary>
        /// Create a new widget session and return the server response.
        /// </summary>
        /// <returns><see cref="Response"/> object with the server response.</returns>
        /// <exception cref="EntityException">Thrown when the <see cref="AbstractEntity.parameters"/> of the widget session could not be serialized.</exception>
        /// <exception cref="ValidationExceptions.ValidationException">Thrown when incorrect parameters were sent to the server.</exception>
        /// <exception cref="EntityException">Thrown when the server returns a non-success http status code or an invalid response.</exception>
        public Response Create()
        {
            return SendApiCall(ACTION_SUBMIT, GetCreateUrl());
        }

        /// <summary>
        /// Get the status for widget with the supplied parameters.
        /// </summary>
        /// <param name="sessionToken">The session token of the widget session.</param>
        /// <param name="recipient">The recipient of the widget session (optional).</param>
        /// <param name="backupCodeIdentifier">The backup code identifier (optional).</param>
        /// <exception cref="EntityException">Thrown when the session token is empty or null.</exception>
        /// <exception cref="EntityException">Thrown when the recipient and backup code identifier are empty or null.</exception>
        /// <exception cref="EntityException">Thrown when the <see cref="AbstractEntity.parameters"/> of this entity could not be serialized.</exception>
        /// <exception cref="ValidationExceptions.ValidationException">Thrown when incorrect parameters were sent to the server.</exception>
        /// <exception cref="EntityException">Thrown when the server returns a non-success http status code or an invalid response.</exception>
        public void Populate(string sessionToken, string recipient = null, string backupCodeIdentifier = null)
        {
            if (String.IsNullOrEmpty(sessionToken))
            {
                throw new EntityException("No session token supplied", ErrorCode.NO_MESSAGE_ID_SUPPLIED);
            }
            if (String.IsNullOrEmpty(recipient) && String.IsNullOrEmpty(backupCodeIdentifier))
            {
                throw new EntityException("No recipient or backup code identifier supplied", ErrorCode.INVALID_FIELDS);
            }

            string param = "";
            if (!String.IsNullOrEmpty(recipient))
            {
                param += "recipient=" + recipient;
            }

            if (!String.IsNullOrEmpty(backupCodeIdentifier))
            {
                if (!String.IsNullOrEmpty(recipient))
                {
                    param += "?";
                }
                param += "backupCodeIdentifier=" + backupCodeIdentifier;
            }

            SendApiCall(ACTION_RETRIEVE, $"{GetCreateUrl()}/{sessionToken}?{param}");
        }
    }
}
