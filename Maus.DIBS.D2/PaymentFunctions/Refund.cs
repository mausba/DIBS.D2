using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace DIBS.D2
{
    /// <summary>
    /// Used to partly or full refund a captured transaction.
    /// </summary>
    public class Refund
    {
        /// <summary>
        /// Post URL
        /// </summary>
        public static Uri Uri = new Uri("https://payment.architrade.com/cgi-adm/refund.cgi");

        /// <summary>
        /// The smallest unit of an amount in the selected currency.
        /// </summary>
        public long Amount { get; set; }

        /// <summary>
        /// Currency is defined using the ISO4217 standard.
        /// </summary>
        public Currency Currency { get; set; }

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
        /// Creates a new Refund.
        /// </summary>
        /// <param name="Amount">The smallest unit of an amount in the selected currency.</param>
        /// <param name="Currency">Currency is defined using the ISO4217 standard.</param>
        /// <param name="OrderId">The shops order number for a particular purchase.</param>
        /// <param name="Transact">The unique Nets identification number for the transaction.</param>
        public Refund(long Amount, Currency Currency, string OrderId, long Transact)
        {
            this.Amount = Amount;
            this.Currency = Currency;
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
                { "currency", Currency.Code },
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
        public async Task<RefundResponse> Post()
        {
            var content = new StringContent(ToQueryString(), DIBSClient.Options.Encoding, "application/x-www-form-urlencoded");

            var login = DIBSClient.Options.Encoding.GetBytes(DIBSClient.Options.ApiLogin + ":" + DIBSClient.Options.ApiPassword);
            DIBSClient.HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(login));

            var response = await DIBSClient.HttpClient.PostAsync(Uri, content).ConfigureAwait(false);
            var paymentResponse = new RefundResponse(response.Content.ReadAsStringAsync().Result);

            DIBSClient.HttpClient.DefaultRequestHeaders.Authorization = null;

            return paymentResponse;
        }
    }
}
