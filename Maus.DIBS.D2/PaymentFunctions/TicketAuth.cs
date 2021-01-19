using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace DIBS.D2
{
    /// <summary>
    /// Used to authorize a transaction on a ticket.
    /// </summary>
    public class TicketAuth
    {
        /// <summary>
        /// Post URL
        /// </summary>
        public static Uri Uri = new Uri("https://payment.architrade.com/cgi-ssl/ticket_auth.cgi");

        private static DateTime LastCall = DateTime.MinValue;

        /// <summary>
        /// The smallest unit of an amount in the selected currency.
        /// </summary>
        public long Amount { get; set; }

        /// <summary>
        /// If this field exists, a capture request is automatically carried out after the authorization, 
        /// following the normal capture process of the specific acquirer.
        /// </summary>
        public bool CaptureNow { get; set; }

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
        /// When this field is declared, the transaction is not dispatched to the card issuer, 
        /// but is instead handled by the Nets test environment.
        /// </summary>
        public bool Test { get; set; }

        /// <summary>
        /// Should be declared to receive the returned message in simple text format.
        /// </summary>
        public bool TextReply { get; set; } = true;

        /// <summary>
        /// The unique Ticket ID. Has the same format as a transaction number.
        /// </summary>
        public long Ticket { get; set; }



        /// <summary>
        /// Creates a new Ticket Auth.
        /// </summary>
        /// <param name="Amount">The smallest unit of an amount in the selected currency.</param>
        /// <param name="Currency">Currency is defined using the ISO4217 standard.</param>
        /// <param name="OrderId">The shops order number for a particular purchase.</param>
        /// <param name="Ticket">The unique Ticket ID. Has the same format as a transaction number.</param>
        public TicketAuth(long Amount, Currency Currency, string OrderId, long Ticket)
        {
            this.Amount = Amount;
            this.Currency = Currency;
            this.OrderId = OrderId;
            this.Ticket = Ticket;
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
                { "ticket", Ticket.ToString() }
            };

            if (CaptureNow)
            {
                data.Add("capturenow", "true");
            }

            if (Test)
            {
                data.Add("test", "true");
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
        public async Task<TicketAuthResponse> Post()
        {
            if ((DateTime.Now - LastCall).TotalMilliseconds <= 200)
                await Task.Delay(200);

            var content = new StringContent(ToQueryString(), DIBSClient.Options.Encoding, "application/x-www-form-urlencoded");
            var response = await DIBSClient.HttpClient.PostAsync(Uri, content).ConfigureAwait(false);
            var paymentResponse = new TicketAuthResponse(response.Content.ReadAsStringAsync().Result);

            return paymentResponse;
        }
    }
}
