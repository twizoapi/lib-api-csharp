using System;
using System.Collections.Generic;
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
    /// Interface for Twizo.
    /// </summary>
    interface ITwizo
    {
        //-------------------------VERIFICATION------------------------

        /// <summary>
        /// Create a verification for the supplied recipient.
        /// </summary>
        /// <param name="recipient">The number that should receive the token.</param>
        /// <returns><see cref="Verification"/> object with the supplied recipient.</returns>
        Verification CreateVerification(string recipient);

        /// <summary>
        /// Get the verification status for the supplied message id.
        /// </summary>
        /// <param name="messageId">The message id of the verification.</param>
        /// <returns><see cref="Verification"/> object with the supplied message id</returns>
        /// <exception cref="EntityException">Thrown when no message id was supplied.</exception>
        /// <exception cref="EntityException">Thrown when the server returns a non-success http status code or an invalid response.</exception>
        Verification GetVerification(string messageId);

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
        Verification GetTokenResult(string token, string messageId);

        /// <summary>
        /// Verify the supplied token; return <c>true</c> when successful and <c>false</c> when there is an error.
        /// </summary>
        /// <param name="token">The token to be verified.</param>
        /// <param name="messageId">The message id of the verification.</param>
        /// <returns><c>true</c> if verification was succesful, otherwise <c>false</c>.</returns>
        Boolean VerifyToken(string token, string messageId);

        //-------------------------BACKUP CODE------------------------
        /// <summary>
        /// Create backup codes for the supplied identifier.
        /// </summary>
        /// <param name="identifier">The identifier.</param>
        /// <returns><see cref="BackupCode"/> object with the supplied identifier.</returns>
        BackupCode CreateBackupCode(string identifier);

        /// <summary>
        /// Get the backup code status for the supplied identifier.
        /// </summary>
        /// <param name="identifier">The identifier.</param>
        /// <returns><see cref="BackupCode"/> object with the supplied identifier.</returns>
        /// <exception cref="EntityException">Thrown when the supplied identifier is empty or null.</exception>
        /// <exception cref="EntityException">Thrown when the server returns a non-success http status code or an invalid response.</exception>
        BackupCode GetBackupCode(string identifier);

        //-----------------------------SMS-----------------------------
        /// <summary>
        /// Create an sms with the supplied body, recipients and sender.
        /// </summary>
        /// <param name="body">The body of the sms.</param>
        /// <param name="recipients">Array with the recipients of the sms.</param>
        /// <param name="sender">The sender of the sms.</param>
        /// <returns><see cref="Sms"/> object with the supplied parameters.</returns>
        Sms CreateSms(string[] recipients, string body, string sender);

        /// <summary>
        /// Get the sms status for the supplied message id.
        /// </summary>
        /// <param name="messageId">The message id of the sms.</param>
        /// <returns><see cref="Sms"/> object with the supplied message id.</returns>
        /// <exception cref="EntityException">Thrown when no message id was supplied.</exception>
        /// <exception cref="EntityException">Thrown when the server returns a non-success http status code or an invalid response.</exception>
        Sms GetSms(string messageId);

        /// <summary>
        /// Get the sms polling results; returns results only for messages which have result type polling enabled.
        /// </summary>
        /// <returns>List of <see cref="Sms"/> objects.</returns>
        /// <exception cref="EntityException">Thrown when the server returns a non-success http status code or an invalid response.</exception>
        List<Sms> GetSmsResults();

        //------------------------NUMBER LOOKUP------------------------
        /// <summary>
        /// Create a number lookup for the supplied numbers.
        /// </summary>
        /// <param name="numbers">Array with the numbers to be lookup up.</param>
        /// <returns><see cref="NumberLookup"/> object with the supplied numbers.</returns>
        NumberLookup CreateNumberLookup(string[] numbers);

        /// <summary>
        /// Get the number lookup status for the supplied message id.
        /// </summary>
        /// <param name="messageId">The message id of the NumberLookup.</param>
        /// <returns><see cref="NumberLookup"/> object with the supplied message id.</returns>
        /// <exception cref="EntityException">Thrown when no message id was supplied.</exception>
        /// <exception cref="EntityException">Thrown when the server returns a non-success http status code or an invalid response.</exception>
        NumberLookup GetNumberLookup(string messageId);

        /// <summary>
        /// Get the number lookup polling results; returns results only for messages which have result type polling enabled.
        /// </summary>
        /// <returns>List of <see cref="NumberLookup"/> objects.</returns>
        /// <exception cref="EntityException">Thrown when the server returns a non-success http status code or an invalid response.</exception>
        List<NumberLookup> GetNumberLookupResults();

        //------------------------WIDGET SESSION------------------------
        /// <summary>
        /// Create a widget session with the supplied allowed types, recipient and backup code identifier.
        /// </summary>
        /// <param name="allowedTypes">Array with the allowed types.</param>
        /// <param name="recipient">The recipient (optional).</param>
        /// <param name="backupCodeIdentifier">The backup code identifier (optional).</param>
        /// <returns><see cref="WidgetSession"/> object with the supplied parameters.</returns>
        WidgetSession CreateWidgetSession(string[] allowedTypes, string recipient = null, string backupCodeIdentifier = null);

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
        WidgetSession GetWidgetSession(string sessionToken, string recipient = null, string backupCodeIdentifier = null);

        //---------------------------OTHER---------------------------
        /// <summary>
        /// Create a balance object and retrieve its data.
        /// </summary>
        /// <returns><see cref="Balance"/> object including balance data.</returns>
        /// <exception cref="EntityException">Thrown when loading the balance data has failed.</exception>
        Balance CreateBalance();
    }
}
