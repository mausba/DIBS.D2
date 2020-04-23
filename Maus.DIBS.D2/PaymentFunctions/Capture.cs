using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace DIBS.D2
{
    /// <summary>
    /// Used to capture a transaction.
    /// </summary>
    public class Capture
    {
        /// <summary>
        /// Post URL
        /// </summary>
        public static Uri Uri = new Uri("https://payment.architrade.com/cgi-bin/capture.cgi");

        /// <summary>
        /// The smallest unit of an amount in the selected currency.
        /// </summary>
        public long Amount { get; set; }

        /// <summary>
        /// If force=true is posted, the Nets server will attempt to capture otherwise expired authorisations (more than 7 days old). 
        /// This is achieved through an initial re-authorisation of the original transaction, followed by a capture process.
        /// </summary>
        public bool Force { get; set; }

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
        /// Creates a new Capture.
        /// </summary>
        /// <param name="Amount">The smallest unit of an amount in the selected currency.</param>
        /// <param name="OrderId">The shops order number for a particular purchase.</param>
        /// <param name="Transact">The unique Nets identification number for the transaction.</param>
        public Capture(long Amount, string OrderId, long Transact)
        {
            this.Amount = Amount;
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
                { "amount", Amount.ToString() },
                { "merchant", DIBSClient.Options.Merchant.ToString() },
                { "orderid", OrderId },
                { "textreply", TextReply ? "true" : "false" },
                { "transact", Transact.ToString() }
            };

            if (Force)
            {
                data.Add("force", "true");
            }

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
        public async Task<CaptureResponse> Post()
        {
            var content = new StringContent(ToQueryString(), DIBSClient.Options.Encoding, "application/x-www-form-urlencoded");
            var response = await DIBSClient.HttpClient.PostAsync(Uri, content);
            var paymentResponse = new CaptureResponse(response.Content.ReadAsStringAsync().Result);

            return paymentResponse;
        }
    }
}