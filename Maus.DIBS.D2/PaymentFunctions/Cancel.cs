using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace DIBS.D2
{
    /// <summary>
    /// Used to cancel a transaction.
    /// </summary>
    public class Cancel
    {
        /// <summary>
        /// Post URL
        /// </summary>
        public static Uri Uri = new Uri("https://payment.architrade.com/cgi-adm/cancel.cgi");

        /// <summary>
        /// The shop’s order number for this particular purchase. It can be seen later when a payment is captured,
        /// and will in some instances appear on the customer’s bank statement (both numerals and letters may be used).
        /// </summary>
        public string OrderId { get; set; }

        /// <summary>
        /// Should be declared to receive the returned message in simple text format.
        /// </summary>
        public bool TextReply { get; set; } = true;

        /// <summary>
        /// The unique Nets identification number for the transaction.
        /// </summary>
        public long Transact { get; set; }



        /// <summary>
        /// Creates a new Cancel.
        /// </summary>
        /// <param name="OrderId">The shops order number for a particular purchase.</param>
        /// <param name="Transact">The unique Nets identification number for the transaction.</param>
        public Cancel(string OrderId, long Transact)
        {
            this.OrderId = OrderId;
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
                { "orderid", OrderId },
                { "textreply", TextReply ? "true" : "false" },
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
        public async Task<CancelResponse> Post()
        {
            var content = new StringContent(ToQueryString(), DIBSClient.Options.Encoding, "application/x-www-form-urlencoded");

            var login = DIBSClient.Options.Encoding.GetBytes(DIBSClient.Options.ApiLogin + ":" + DIBSClient.Options.ApiPassword);
            DIBSClient.HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(login));

            var response = await DIBSClient.HttpClient.PostAsync(Uri, content).ConfigureAwait(false);
            var paymentResponse = new CancelResponse(response.Content.ReadAsStringAsync().Result);

            DIBSClient.HttpClient.DefaultRequestHeaders.Authorization = null;

            return paymentResponse;
        }
    }
}
