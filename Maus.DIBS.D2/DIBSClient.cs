using System.Net.Http;

namespace DIBS.D2
{
    /// <summary>
    /// Provides a base class for sending HTTP requests to DIBS.
    /// </summary>
    public class DIBSClient
    {
        /// <summary>
        /// Provides a base class for sending HTTP requests and receiving HTTP responses from a resource identified by a URI.
        /// </summary>
        public static readonly HttpClient HttpClient = new HttpClient();

        /// <summary>
        /// DIBS main configuration
        /// </summary>
        public static DIBSOptions Options = new DIBSOptions();
    }
}