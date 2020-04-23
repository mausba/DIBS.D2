using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace DIBS.D2
{
    /// <summary>
    /// Used to get the cardtype used in a transaction.
    /// </summary>
    public class CardType
    {
        /// <summary>
        /// Post URL
        /// </summary>
        public static Uri Uri = new Uri("https://payment.architrade.com/cardtype.pml");

        /// <summary>
        /// If multiple departments utilize the same DIBS account, it may be practical to keep the transactions separate at DIBS. 
        /// An account name may be inserted in this field, to separate transactions at DIBS.
        /// </summary>
        public string Account { get; set; }

        /// <summary>
        /// The unique Nets identification number for the transaction.
        /// The transact is a as minimum 6-digit integer, e.g. transact=123456.
        /// </summary>
        public long Transact { get; set; }



        /// <summary>
        /// CardType  is used for getting the credit card type used for a specific transaction.
        /// </summary>
        /// <param name="Transact">The unique Nets identification number for the transaction.</param>
        public CardType(long Transact)
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
        public async Task<CardTypeResponse> Post()
        {
            var content = new StringContent(ToQueryString(), DIBSClient.Options.Encoding, "application/x-www-form-urlencoded");
            var response = await DIBSClient.HttpClient.PostAsync(Uri, content);
            var paymentResponse = new CardTypeResponse(response.Content.ReadAsStringAsync().Result);

            return paymentResponse;
        }
    }
}
