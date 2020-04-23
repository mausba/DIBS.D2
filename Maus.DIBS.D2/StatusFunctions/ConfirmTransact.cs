using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace DIBS.D2
{
    /// <summary>
    /// Used to confirm that the transaction is registrered at Nets.
    /// </summary>
    public class ConfirmTransact
    {
        /// <summary>
        /// Post URL
        /// </summary>
        public static Uri Uri = new Uri("https://payment.architrade.com/cgi-bin/confirmtransact.cgi");

        /// <summary>
        /// If multiple departments utilize the same DIBS account, it may be practical to keep the transactions separate at DIBS. 
        /// An account name may be inserted in this field, to separate transactions at DIBS.
        /// </summary>
        public string Account { get; set; }

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
        /// The unique Nets identification number for the transaction.
        /// The transact is a as minimum 6-digit integer, e.g. transact=123456.
        /// </summary>
        public long Transact { get; set; }



        /// <summary>
        /// ConfirmTransact is used for confirming that a particular transaction was successfully registered on the DIBS server.
        /// </summary>
        /// <param name="Currency"></param>
        /// <param name="OrderId"></param>
        /// <param name="Transact"></param>
        public ConfirmTransact(Currency Currency, string OrderId, long Transact)
        {
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
                { "currency", Currency.Value },
                { "merchant", DIBSClient.Options.Merchant.ToString() },
                { "orderid", OrderId },
                { "transact", Transact.ToString() }
            };

            if (!string.IsNullOrEmpty(Account))
            {
                data.Add("account", Account);
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
        public async Task<ConfirmTransactResponse> Post()
        {
            var content = new StringContent(ToQueryString(), DIBSClient.Options.Encoding, "application/x-www-form-urlencoded");
            var response = await DIBSClient.HttpClient.PostAsync(Uri, content);
            var paymentResponse = new ConfirmTransactResponse(response.Content.ReadAsStringAsync().Result);

            return paymentResponse;
        }
    }
}
