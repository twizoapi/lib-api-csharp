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
    /// Retrieve information about your balance.
    /// </summary>
    public class Balance : AbstractEntity
    {
        /// <summary>Gets the curent balance of the wallet.</summary>
        public decimal credit { get; private set; }

        /// <summary>Gets the currency type of the wallet.</summary>
        public string currencyCode { get; private set; }

        /// <summary>Gets the amount of free verifications left for this month.</summary>
        public int freeVerification { get; private set; }

        /// <summary>Gets the name of the wallet.</summary>
        public string wallet { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Balance"/> class.
        /// </summary>
        /// <param name="client"><see cref="AbstractClient"/> object, used to send and receive data.</param>
        public Balance(AbstractClient client) : base(client) { }

        /// <summary>Get the location to which the server call should be made.</summary>
        protected override string GetCreateUrl()
        {
            return "wallet/getbalance";
        }

        /// <summary>
        /// Load the balance data from the server.
        /// </summary>
        /// <exception cref="EntityException">Thrown when the server returns a non-success http status code or an invalid response.</exception>
        public void LoadData()
        {
            SendApiCall(ACTION_RETRIEVE, GetCreateUrl());
        }
    }
}
