namespace TwizoAPI.Entity.VerificationExceptions
{
    /**
    * This file is part of the Twizo C# API.
    * 
    * (c) Twizo - info@twizo.com
    * 
    * For the full copyright and license information, please view the LICENSE file that was distributed with this source code.
    */

    /// <summary>
    /// Verification empty message id exception class.
    /// </summary>
    public class EmptyMessageIdException : EntityException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EmptyMessageIdException"/> class.
        /// </summary>
        public EmptyMessageIdException() : base("No message ID supplied for verification", ErrorCode.VERIFICATION_FAILED) { }
    }
}
