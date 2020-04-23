using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace DIBS.D2
{
    /// <summary>
    /// Used to receive simple transaction information and status in Nets.
    /// </summary>
    public class TransInfo
    {
        /// <summary>
        /// Post URL
        /// </summary>
        public static Uri Uri = new Uri("https://payment.architrade.com/cgi-bin/transinfo.cgi");

        /// <summary>
        /// If multiple departments utilize the same DIBS account, it may be practical to keep the transactions separate at DIBS. 
        /// An account name may be inserted in this field, to separate transactions at DIBS.
        /// </summary>
        public string Account { get; set; }

        /// <summary>
        /// The smallest unit of an amount in the selected currency, following ISO4217.
        /// </summary>
        public int Amount { get; set; }

        /// <summary>
        /// Currency is defined using the ISO4217 standard.
        /// </summary>
        public Currency Currency { get; set; }

        /// <summary>
        /// The shop’s order number for this particular purchase. It can be seen later when a payment is captured,
        /// and will in some instances appear on the customer’s bank statement (both numerals and letters may be used).
        /// </summary>
        public string OrderId { get; set; }



        public TransInfo(int Amount, Currency Currency, string OrderId)
        {
            this.Amount = Amount;
            this.Currency = Currency;
            this.OrderId = OrderId;
        }

        /// <summary>
        /// Return object as query string.
        /// </summary>
        /// <returns></returns>
        public string ToQueryString()
        {
            var data = new Dictionary<string, string>
            {
                { "amount", Amount.ToString() },
                { "currency", Currency.Value },
                { "merchant", DIBSClient.Options.Merchant.ToString() },
                { "orderid", OrderId }
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
        public async Task<TransInfoResponse> Post()
        {
            var content = new StringContent(ToQueryString(), DIBSClient.Options.Encoding, "application/x-www-form-urlencoded");
            var response = await DIBSClient.HttpClient.PostAsync(Uri, content);
            var paymentResponse = new TransInfoResponse(response.Content.ReadAsStringAsync().Result);

            return paymentResponse;
        }
    }
}
