using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace DIBS.D2
{
    /// <summary>
    /// Used to get the original callback.
    /// </summary>
    public class Callback
    {
        /// <summary>
        /// Post URL
        /// </summary>
        public static Uri Uri = new Uri("https://payment.architrade.com/cgi-adm/callback.cgi");

        /// <summary>
        /// The unique Nets identification number for the transaction.
        /// The transact is a as minimum 6-digit integer, e.g. transact=123456.
        /// </summary>
        public long Transact { get; set; }



        /// <summary>
        /// Allows customers to pull for callback information.
        /// </summary>
        /// <param name="Transact">The unique Nets identification number for the transaction.</param>
        public Callback(long Transact)
        {
            this.Transact = Transact;
        }

        /// <summary>
        /// Return object as a query string.
        /// </summary>
        /// <returns></returns>
        public string ToQueryString()
        {
            var data = new Dictionary<string, string>
            {
                { "merchant", DIBSClient.Options.Merchant.ToString() },
                { "transact", Transact.ToString() }
            };

            var postData = "";
            foreach (KeyValuePair<string, string> entry in data)
            {
                postData += "&" + entry.Key + "=" + entry.Value;
            }
            postData = postData.TrimStart('&');

            return postData;
        }

        /// <summary>
        /// Post request
        /// </summary>
        /// <returns></returns>
        public async Task<CallbackResponse> Post()
        {
            var content = new StringContent(ToQueryString(), DIBSClient.Options.Encoding, "application/x-www-form-urlencoded");

            var login = DIBSClient.Options.Encoding.GetBytes(DIBSClient.Options.ApiLogin + ":" + DIBSClient.Options.ApiPassword);
            DIBSClient.HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(login));

            var response = await DIBSClient.HttpClient.PostAsync(Uri, content).ConfigureAwait(false);
            var paymentResponse = new CallbackResponse(response.Content.ReadAsStringAsync().Result);

            DIBSClient.HttpClient.DefaultRequestHeaders.Authorization = null;

            return paymentResponse;
        }
    }
}
