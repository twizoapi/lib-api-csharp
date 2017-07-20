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
    /// Verification empty token exception class.
    /// </summary>
    public class EmptyTokenException : EntityException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EmptyTokenException"/> class.
        /// </summary>
        public EmptyTokenException() : base("Empty token supplied for verification", ErrorCode.VERIFICATION_FAILED) { }
    }
}
