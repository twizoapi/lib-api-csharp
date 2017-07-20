using System;
using TwizoAPI.Entity.BackupCodeExceptions;
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
    /// Create, retrieve, update, delete or verify backup codes for an identifier.
    /// </summary>
    public class BackupCode : AbstractEntity
    {
        /// <summary>Gets the amount of backup codes left.</summary>
        public int amountOfCodesLeft { get; private set; }

        /// <summary>Gets the array with the back codes.</summary>
        public string[] codes { get; private set; }

        /// <summary>Gets the datetime the backup codes were created. Datetime is in ISO-8601 format.</summary>
        public string createdDateTime { get; private set; }

        private string _identifier;
        /// <summary>Gets or sets the identifier of the backupCode.</summary>
        public string identifier
        {
            get => _identifier;
            set { _identifier = value; AddParameter("identifier", value); }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BackupCode"/> class.
        /// </summary>
        /// <param name="client"><see cref="AbstractClient"/> object, used to send and receive data.</param>
        public BackupCode(AbstractClient client) : base(client) { }

        /// <summary>Get the location to which the server call should be made.</summary>
        protected override string GetCreateUrl()
        {
            return "backupcode";
        }

        /// <summary>
        /// Create backup codes for the <see cref="BackupCode.identifier"/> and return the server response.
        /// </summary>
        /// <returns><see cref="Response"/> object with the server response.</returns>
        /// <exception cref="EntityException">Thrown when the <see cref="AbstractEntity.parameters"/> of this entity could not be serialized.</exception>
        /// <exception cref="ValidationExceptions.ValidationException">Thrown when incorrect parameters were sent to the server.</exception>
        /// <exception cref="EntityException">Thrown when the server returns a non-success http status code or an invalid response.</exception>
        public Response Create()
        {
            return SendApiCall(ACTION_SUBMIT, GetCreateUrl());
        }

        /// <summary>
        /// Create new backup codes for the <see cref="BackupCode.identifier"/> and return the server response.
        /// </summary>
        /// <returns><see cref="Response"/> object with the server response.</returns>
        /// <exception cref="EntityException">Thrown when the <see cref="AbstractEntity.parameters"/> of this entity could not be serialized.</exception>
        /// <exception cref="ValidationExceptions.ValidationException">Thrown when incorrect parameters were sent to the server.</exception>
        /// <exception cref="EntityException">Thrown when the server returns a non-success http status code or an invalid response.</exception>
        public Response Update()
        {
            return SendApiCall(ACTION_UPDATE, $"{GetCreateUrl()}/{identifier}");
        }

        /// <summary>
        /// Delete all backup codes for the <see cref="BackupCode.identifier"/>.
        /// </summary>
        /// <exception cref="EntityException">Thrown when the server returns a non-success http status code or an invalid response.</exception>
        public void Delete()
        {
            SendApiCall(ACTION_REMOVE, $"{GetCreateUrl()}/{identifier}");
        }

        /// <summary>
        /// Retrieve remaining backup codes for the <see cref="BackupCode.identifier"/>.
        /// </summary>
        /// <param name="identifier">The identifier.</param>
        /// <exception cref="EntityException">Thrown when the supplied identifier is empty or null.</exception>
        /// <exception cref="EntityException">Thrown when the server returns a non-success http status code or an invalid response.</exception>
        public new void Populate(string identifier)
        {
            if (String.IsNullOrEmpty(identifier))
            {
                throw new EntityException("No identifier supplied for backup code", (ErrorCode)BackupCodeException.BackupCodeError.NO_IDENTIFIER_SUPPLIED);
            }

            SendApiCall(ACTION_RETRIEVE, $"{GetCreateUrl()}/{identifier}");
        }

        /// <summary>
        /// Verify the supplied token.
        /// </summary>
        /// <param name="token">The token to be verified.</param>
        /// <param name="identifier">The identifier (optional).</param>
        /// <exception cref="EmptyTokenException">Thrown when the supplied token is empty or null.</exception>
        /// <exception cref="EmptyIdentifierException">Thrown when the identifier is empty or null.</exception>
        /// <exception cref="EntityException">Thrown when the server returns a non-success http status code or an invalid response.</exception>
        /// <exception cref="BackupCodeException">Thrown when the returned error code is a backupcode error.</exception>
        public void Verify(string token, string identifier = null)
        {
            identifier = (!String.IsNullOrEmpty(identifier)) ? identifier : this.identifier;

            if (String.IsNullOrEmpty(token))
            {
                throw new EmptyTokenException();
            }
            if (string.IsNullOrEmpty(identifier))
            {
                throw new EmptyIdentifierException();
            }

            SendApiCall(ACTION_RETRIEVE, $"{GetCreateUrl()}/{identifier}?token={token}");
        }

        /// <summary>
        /// Send an API call to the <see cref="AbstractClient"/>.
        /// </summary>
        /// <param name="verb">The http verb to be used with the API call.</param>
        /// <param name="location">The URL to which the API call should be made.</param>
        /// <returns><see cref="Response"/> object with the server response.</returns>
        /// <exception cref="EntityException">Thrown when the <see cref="AbstractEntity.parameters"/> of this entity could not be serialized.</exception>
        /// <exception cref="ValidationExceptions.ValidationException">Thrown when incorrect parameters were sent to the server.</exception>
        /// <exception cref="EntityException">Thrown when the server returns a non-success http status code or an invalid response.</exception>
        /// <exception cref="BackupCodeException">Thrown when the returned error code is a backupcode error.</exception>
        protected new Response SendApiCall(string verb, string location)
        {
            try
            {
                return base.SendApiCall(verb, location);
            }
            catch (EntityException e)
            {
                if (BackupCodeException.IsBackupCodeException(e))
                {
                    throw new BackupCodeException(e);
                }
                else
                {
                    throw;
                }
            }
        }
    }
}
