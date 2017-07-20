using System;
using System.Collections.Generic;
using TwizoAPI.Client;
using TwizoAPI.Entity;

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
    /// Main Twizo class, used to create entities.
    /// </summary>
    public class Twizo : ITwizo
    {
        private readonly Factory factory;

        /// <summary>
        /// Initializes a new instance of the <see cref="Twizo"/> class.
        /// </summary>
        /// <param name="apiKey">Your Twizo API key.</param>
        /// <param name="apiHost">The Twizo host of your choice.</param>
        /// <exception cref="ArgumentNullException">Thrown when the provided apiKey or apiHost are null.</exception>
        public Twizo(string apiKey, string apiHost)
        {
            factory = new Factory(new TwizoClient(apiKey, apiHost));
        }

        //-------------------------VERIFICATION------------------------
        /// <summary>
        /// Create an sms verification for the supplied recipient.
        /// </summary>
        /// <param name="recipient">The number that should receive the token.</param>
        /// <returns><see cref="Verification"/> object with the supplied recipient.</returns>
        public Verification CreateVerification(string recipient)
        {
            return factory.CreateVerification(recipient);
        }

        /// <summary>
        /// Create a call verification for the supplied recipient.
        /// </summary>
        /// <param name="recipient">The number that should receive the token.</param>
        /// <returns><see cref="Verification"/> object with the supplied recipient.</returns>
        public Verification CreateVerificationCall(string recipient)
        {
            return factory.CreateVerificationCall(recipient);
        }

        /// <summary>
        /// Get the verification status for the supplied message id
        /// </summary>
        /// <param name="messageId">The message id of the verification.</param>
        /// <returns><see cref="Verification"/> object with the supplied message id</returns>
        /// <exception cref="EntityException">Thrown when no message id was supplied.</exception>
        /// <exception cref="EntityException">Thrown when the server returns a non-success http status code or an invalid response.</exception>
        public Verification GetVerification(string messageId)
        {
            Verification verification = factory.CreateEmptyVerification();
            verification.Populate(messageId);
            return verification;
        }

        /// <summary>
        /// Verify the supplied token and return the verification object when successfully verified; otherwise return an exception.
        /// </summary>
        /// <param name="token">The token to be verified.</param>
        /// <param name="messageId">The message id of the verification.</param>
        /// <returns><see cref="Verification"/> object with the supplied message id</returns>
        /// <exception cref="Entity.VerificationExceptions.EmptyTokenException">Thrown when the supplied token is empty or null.</exception>
        /// <exception cref="Entity.VerificationExceptions.EmptyMessageIdException">Thrown when the message id is empty or null.</exception>
        /// <exception cref="EntityException">Thrown when the server returns a non-success http status code or an invalid response.</exception>
        /// <exception cref="Entity.VerificationExceptions.VerificationException">Thrown when the returned error code is a verification error.</exception>
        public Verification GetTokenResult(string token, string messageId)
        {
            Verification verification = factory.CreateEmptyVerification();
            verification.Verify(token, messageId);
            return verification;
        }

        /// <summary>
        /// Verify the supplied token; return <c>true</c> when successful and <c>false</c> when there is an error.
        /// </summary>
        /// <param name="token">The token to be verified.</param>
        /// <param name="messageId">The message id of the verification.</param>
        /// <returns><c>true</c> if verification was succesful, otherwise <c>false</c>.</returns>
        public Boolean VerifyToken(string token, string messageId)
        {
            try
            {
                Verification verification = GetTokenResult(token, messageId);
                return (verification.statusCode == (int)VerificationStatusCode.SUCCESS);
            }
            catch (EntityException)
            {
                return false;
            }
        }

        //-------------------------BACKUP CODE------------------------
        /// <summary>
        /// Create backup codes for the supplied identifier.
        /// </summary>
        /// <param name="identifier">The identifier.</param>
        /// <returns><see cref="BackupCode"/> object with the supplied identifier.</returns>
        public BackupCode CreateBackupCode(string identifier)
        {
            return factory.CreateBackupCode(identifier);
        }

        /// <summary>
        /// Get the backup code status for the supplied identifier.
        /// </summary>
        /// <param name="identifier">The identifier.</param>
        /// <returns><see cref="BackupCode"/> object with the supplied identifier.</returns>
        /// <exception cref="EntityException">Thrown when the supplied identifier is empty or null.</exception>
        /// <exception cref="EntityException">Thrown when the server returns a non-success http status code or an invalid response.</exception>
        public BackupCode GetBackupCode(string identifier)
        {
            BackupCode backupCode = factory.CreateEmptyBackupCode();
            backupCode.Populate(identifier);
            return backupCode;
        }

        //-----------------------------SMS-----------------------------
        /// <summary>
        /// Create an sms with the supplied body, recipient and sender.
        /// </summary>
        /// <param name="body">The body of the sms.</param>
        /// <param name="recipient">The recipient of the sms.</param>
        /// <param name="sender">The sender of the sms.</param>
        /// <returns><see cref="Sms"/> object with the supplied parameters.</returns>
        public Sms CreateSms(string recipient, string body, string sender)
        {
            return factory.CreateSms(recipient, body, sender);
        }

        /// <summary>
        /// Create an sms with the supplied body, recipients and sender.
        /// </summary>
        /// <param name="body">The body of the sms.</param>
        /// <param name="recipients">Array with the recipients of the sms.</param>
        /// <param name="sender">The sender of the sms.</param>
        /// <returns><see cref="Sms"/> object with the supplied parameters.</returns>
        public Sms CreateSms(string[] recipients, string body, string sender)
        {
            return factory.CreateSms(recipients, body, sender);
        }

        /// <summary>
        /// Get the sms status for the supplied message id.
        /// </summary>
        /// <param name="messageId">The message id of the sms.</param>
        /// <returns><see cref="Sms"/> object with the supplied message id.</returns>
        /// <exception cref="EntityException">Thrown when no message id was supplied.</exception>
        /// <exception cref="EntityException">Thrown when the server returns a non-success http status code or an invalid response.</exception>
        public Sms GetSms(string messageId)
        {
            Sms sms = factory.CreateEmptySms();
            sms.Populate(messageId);
            return sms;
        }

        /// <summary>
        /// Get the sms polling results; returns results only for messages which have result type polling enabled.
        /// </summary>
        /// <returns>List of <see cref="Sms"/> objects.</returns>
        /// <exception cref="EntityException">Thrown when the server returns a non-success http status code or an invalid response.</exception>
        public List<Sms> GetSmsResults()
        {
            List<Sms> result = new List<Sms>();

            Poll poll = factory.CreateSmsPoll();
            poll.Send();

            List<Dictionary<string, object>> messages = poll.GetMessages();

            foreach (Dictionary<string, object> message in messages)
            {
                Sms sms = factory.CreateEmptySms();
                sms.FillObject(message);
                result.Add(sms);
            }

            poll.Delete();

            return result;
        }

        //------------------------NUMBER LOOKUP------------------------
        /// <summary>
        /// Create a number lookup for the supplied number.
        /// </summary>
        /// <param name="number">The number to be lookup up.</param>
        /// <returns><see cref="NumberLookup"/> object with the supplied numbers.</returns>
        public NumberLookup CreateNumberLookup(string number)
        {
            return factory.CreateNumberLookup(number);
        }

        /// <summary>
        /// Create a number lookup for the supplied numbers.
        /// </summary>
        /// <param name="numbers">Array with the numbers to be lookup up.</param>
        /// <returns><see cref="NumberLookup"/> object with the supplied numbers.</returns>
        public NumberLookup CreateNumberLookup(string[] numbers)
        {
            return factory.CreateNumberLookup(numbers);
        }

        /// <summary>
        /// Get the number lookup status for the supplied message id.
        /// </summary>
        /// <param name="messageId">The message id of the NumberLookup.</param>
        /// <returns><see cref="NumberLookup"/> object with the supplied message id.</returns>
        /// <exception cref="EntityException">Thrown when no message id was supplied.</exception>
        /// <exception cref="EntityException">Thrown when the server returns a non-success http status code or an invalid response.</exception>
        public NumberLookup GetNumberLookup(string messageId)
        {
            NumberLookup numberLookup = factory.CreateEmptyNumberLookup();
            numberLookup.Populate(messageId);
            return numberLookup;
        }

        /// <summary>
        /// Get the number lookup polling results; returns results only for messages which have result type polling enabled.
        /// </summary>
        /// <returns>List of <see cref="NumberLookup"/> objects.</returns>
        /// <exception cref="EntityException">Thrown when the server returns a non-success http status code or an invalid response.</exception>
        public List<NumberLookup> GetNumberLookupResults()
        {
            List<NumberLookup> result = new List<NumberLookup>();

            Poll poll = factory.CreateNumberLookupPoll();
            poll.Send();

            List<Dictionary<string, object>> messages = poll.GetMessages();

            foreach (Dictionary<string, object> message in messages)
            {
                NumberLookup NumberLookup = factory.CreateEmptyNumberLookup();
                NumberLookup.FillObject(message);
                result.Add(NumberLookup);
            }

            poll.Delete();

            return result;
        }

        //------------------------WIDGET SESSION------------------------
        /// <summary>
        /// Create a widget session with the supplied allowed types, recipient and backup code identifier.
        /// </summary>
        /// <param name="allowedTypes">Array with the allowed types.</param>
        /// <param name="recipient">The recipient (optional).</param>
        /// <param name="backupCodeIdentifier">The backup code identifier (optional).</param>
        /// <returns><see cref="WidgetSession"/> object with the supplied parameters.</returns>
        public WidgetSession CreateWidgetSession(string[] allowedTypes, string recipient = null, string backupCodeIdentifier = null)
        {
            return factory.CreateWidgetSession(allowedTypes, recipient, backupCodeIdentifier);
        }

        /// <summary>
        /// Get the widget session status for the supplied session token.
        /// </summary>
        /// <param name="sessionToken">The session token.</param>
        /// <param name="recipient">The recipient (optional).</param>
        /// <param name="backupCodeIdentifier">The backup code identifier (optional).</param>
        /// <returns><see cref="WidgetSession"/> with the supplied parameters.</returns>
        /// <exception cref="EntityException">Thrown when the session token is empty or null.</exception>
        /// <exception cref="EntityException">Thrown when the recipient and backup code identifier are empty or null.</exception>
        /// <exception cref="EntityException">Thrown when the <see cref="AbstractEntity.parameters"/> of this entity could not be serialized.</exception>
        /// <exception cref="Entity.ValidationExceptions.ValidationException">Thrown when incorrect parameters were sent to the server.</exception>
        /// <exception cref="EntityException">Thrown when the server returns a non-success http status code or an invalid response.</exception>
        public WidgetSession GetWidgetSession(string sessionToken, string recipient = null, string backupCodeIdentifier = null)
        {
            WidgetSession widgetSession = factory.CreateEmptyWidgetSession();
            widgetSession.Populate(sessionToken, recipient, backupCodeIdentifier);
            return widgetSession;
        }

        //---------------------------OTHER---------------------------
        /// <summary>
        /// Create a balance object.
        /// </summary>
        /// <returns><see cref="Balance"/> object including balance data.</returns>
        /// <exception cref="EntityException">Thrown when loading the balance data has failed.</exception>
        public Balance CreateBalance()
        {
            return factory.CreateBalance();
        }
    }
}
