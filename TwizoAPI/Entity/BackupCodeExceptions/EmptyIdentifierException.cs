namespace TwizoAPI.Entity.BackupCodeExceptions
{
    /**
    * This file is part of the Twizo C# API.
    * 
    * (c) Twizo - info@twizo.com
    * 
    * For the full copyright and license information, please view the LICENSE file that was distributed with this source code.
    */

    /// <summary>
    /// Backup code empty identifier exception class.
    /// </summary>
    public class EmptyIdentifierException : EntityException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EmptyIdentifierException"/> class.
        /// </summary>
        public EmptyIdentifierException() : base("No identifier supplied for backup code", ErrorCode.BACKUP_CODE_FAILED) { }
    }
}
