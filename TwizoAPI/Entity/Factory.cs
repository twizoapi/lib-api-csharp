using System;

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
    /// The factory class is used to create new entities.
    /// </summary>
    public class Factory
    {
        private readonly AbstractClient client;

        /// <summary>
        /// Initializes a new instance of the <see cref="Factory"/> class.
        /// </summary>
        /// <param name="client"><see cref="AbstractClient"/> object, used to send and receive data.</param>
        public Factory(AbstractClient client)
        {
            this.client = client;
        }

        //-------------------------VERIFICATION------------------------
        /// <summary>
        /// Create an empty <see cref="Verification"/> object.
        /// </summary>
        /// <returns>Empty <see cref="Verification"/> object.</returns>
        internal Verification CreateEmptyVerification()
        {
            return new Verification(client);
        }

        /// <summary>
        /// Create an sms <see cref="Verification"/> object with the supplied recipient.
        /// </summary>
        /// <param name="recipient">The number that should receive the token.</param>
        /// <returns><see cref="Verification"/> object with the supplied recipient.</returns>
        internal Verification CreateVerification(string recipient)
        {
            Verification verification = CreateEmptyVerification();
            verification.recipient = recipient;
            return verification;
        }

        /// <summary>
        /// Create a call <see cref="Verification"/> object with the supplied recipient.
        /// </summary>
        /// <param name="recipient">The number that should receive the token.</param>
        /// <returns><see cref="Verification"/> object with the supplied recipient.</returns>
        internal Verification CreateVerificationCall(string recipient)
        {
            Verification verification = CreateVerification(recipient);
            verification.type = Verification.TYPE_CALL;
            return verification;
        }

        //-------------------------BACKUP CODE------------------------
        /// <summary>
        /// Create an empty <see cref="BackupCode"/> object.
        /// </summary>
        /// <returns>Empty <see cref="BackupCode"/> object.</returns>
        internal BackupCode CreateEmptyBackupCode()
        {
            return new BackupCode(client);
        }

        /// <summary>
        /// Create backup codes for the supplied identifier.
        /// </summary>
        /// <param name="identifier">The identifier.</param>
        /// <returns><see cref="BackupCode"/> object with the supplied identifier.</returns>
        internal BackupCode CreateBackupCode(string identifier)
        {
            BackupCode backupCode = CreateEmptyBackupCode();
            backupCode.identifier = identifier;
            return backupCode;
        }

        //-----------------------------SMS-----------------------------
        /// <summary>
        /// Create an empty <see cref="Sms"/> object.
        /// </summary>
        /// <returns>Empty <see cref="Sms"/> object.</returns>
        internal Sms CreateEmptySms()
        {
            return new Sms(client);
        }

        /// <summary>
        /// Create an sms with the supplied body, recipient and sender.
        /// </summary>
        /// <param name="body">The body of the sms.</param>
        /// <param name="recipient">The recipient of the sms.</param>
        /// <param name="sender">The sender of the sms.</param>
        /// <returns><see cref="Sms"/> object with the supplied parameters.</returns>
        internal Sms CreateSms(string recipient, string body, string sender)
        {
            Sms sms = CreateEmptySms();
            sms.recipient = recipient;
            sms.body = body;
            sms.sender = sender;
            return sms;
        }

        /// <summary>
        /// Create an sms with the supplied body, recipients and sender.
        /// </summary>
        /// <param name="body">The body of the sms.</param>
        /// <param name="recipients">Array with the recipients of the sms.</param>
        /// <param name="sender">The sender of the sms.</param>
        /// <returns><see cref="Sms"/> object with the supplied parameters.</returns>
        internal Sms CreateSms(string[] recipients, string body, string sender)
        {
            Sms sms = CreateEmptySms();
            sms.recipients = recipients;
            sms.body = body;
            sms.sender = sender;
            return sms;
        }

        /// <summary>
        /// Create a new <see cref="Poll"/> object with type <see cref="Poll.TYPE_SMS"/>.
        /// </summary>
        /// <returns><see cref="Poll"/> object.</returns>
        internal Poll CreateSmsPoll()
        {
            return new Poll(client, Poll.TYPE_SMS);
        }

        //------------------------NUMBER LOOKUP------------------------
        /// <summary>
        /// Create an empty <see cref="NumberLookup"/> object.
        /// </summary>
        /// <returns>Empty <see cref="NumberLookup"/> object.</returns>
        internal NumberLookup CreateEmptyNumberLookup()
        {
            return new NumberLookup(client);
        }

        /// <summary>
        /// Create a number lookup for the supplied number.
        /// </summary>
        /// <param name="number">The number to be lookup up.</param>
        /// <returns><see cref="NumberLookup"/> object with the supplied numbers.</returns>
        internal NumberLookup CreateNumberLookup(string number)
        {
            string[] numbers = new string[] { number };
            return CreateNumberLookup(numbers);
        }

        /// <summary>
        /// Create a number lookup for the supplied numbers.
        /// </summary>
        /// <param name="numbers">Array with the numbers to be lookup up.</param>
        /// <returns><see cref="NumberLookup"/> object with the supplied numbers.</returns>
        internal NumberLookup CreateNumberLookup(string[] numbers)
        {
            NumberLookup numberLookup = CreateEmptyNumberLookup();
            numberLookup.numbers = numbers;
            return numberLookup;
        }

        /// <summary>
        /// Create a new <see cref="Poll"/> object with type <see cref="Poll.TYPE_NUMBER_LOOKUP"/>.
        /// </summary>
        /// <returns><see cref="Poll"/> object.</returns>
        internal Poll CreateNumberLookupPoll()
        {
            return new Poll(client, Poll.TYPE_NUMBER_LOOKUP);
        }

        //------------------------WIDGET SESSION------------------------
        /// <summary>
        /// Create an empty <see cref="WidgetSession"/> object.
        /// </summary>
        /// <returns>Empty <see cref="WidgetSession"/> object.</returns>
        internal WidgetSession CreateEmptyWidgetSession()
        {
            return new WidgetSession(client);
        }

        /// <summary>
        /// Create a widget session with the supplied allowed types, recipient and backup code identifier.
        /// </summary>
        /// <param name="allowedTypes">Array with the allowed types.</param>
        /// <param name="recipient">The recipient (optional).</param>
        /// <param name="backupCodeIdentifier">The backup code identifier (optional).</param>
        /// <returns><see cref="WidgetSession"/> object with the supplied parameters.</returns>
        internal WidgetSession CreateWidgetSession(string[] allowedTypes, string recipient = null, string backupCodeIdentifier = null)
        {
            WidgetSession widgetSession = CreateEmptyWidgetSession();
            widgetSession.allowedTypes = allowedTypes;
            if (!String.IsNullOrEmpty(recipient))
            {
                widgetSession.recipient = recipient;
            }
            if (!String.IsNullOrEmpty(backupCodeIdentifier))
            {
                widgetSession.backupCodeIdentifier = backupCodeIdentifier;
            }

            return widgetSession;
        }

        //---------------------------OTHER---------------------------
        /// <summary>
        /// Create a balance object and retrieve its data.
        /// </summary>
        /// <returns><see cref="Balance"/> object including balance data.</returns>
        /// <exception cref="EntityException">Thrown when loading the balance data has failed.</exception>
        internal Balance CreateBalance()
        {
            return new Balance(client);
        }
    }
}
