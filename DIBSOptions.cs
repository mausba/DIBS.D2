using System.Text;

namespace DIBS.D2
{
    /// <summary>
    /// DIBS main configuration
    /// </summary>
    public class DIBSOptions
    {
        /// <summary>
        /// Shop identification. The Merchant ID appears in the e-mail received from DIBS during registration with DIBS or on your contract.
        /// </summary>
        public long Merchant = 123456789;

        /// <summary>
        /// ApiLogin
        /// </summary>
        public string ApiLogin = "";

        /// <summary>
        /// ApiPassword
        /// </summary>
        public string ApiPassword = "";

        /// <summary>
        /// OrderIdPrefix
        /// </summary>
        public string OrderIdPrefix = "tst";

        /// <summary>
        /// Encoding
        /// </summary>
        public Encoding Encoding = Encoding.UTF8;
    }
}