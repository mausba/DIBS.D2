using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace DIBS.D2
{
    /// <summary>
    /// Used to re-authorize a transaction, if the authorization has expired.
    /// </summary>
    public class Reauth
    {
        /// <summary>
        /// Post URL
        /// </summary>
        public static Uri Uri = new Uri("https://payment.architrade.com/cgi-bin/reauth.cgi");

        /// <summary>
        /// If multiple departments utilize the same Nets account, it may be practical to keep the transactions separate at Nets. 
        /// An account name may be inserted in this field, to separate transactions at Nets.
        /// </summary>
        public string Account { get; set; }

        /// <summary>
        /// If this variable is set, all variables will be returned (as defined in the Nets administration).
        /// </summary>
        public bool FullReply { get; set; }

        /// <summary>
        /// Should be declared to receive the returned message in simple text format.
        /// </summary>
        public bool TextReply { get; set; } = true;

        /// <summary>
        /// The unique Nets identification number for the transaction.
        /// </summary>
        public long Transact { get; set; }



        /// <summary>
        /// Creates a new Reauth.
        /// </summary>
        /// <param name="Transact">The unique Nets identification number for the transaction.</param>
        public Reauth(long Transact)
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
                { "textreply", TextReply ? "true" : "false" },
                { "transact", Transact.ToString() }
            };

            if (!string.IsNullOrEmpty(Account))
            {
                data.Add("account", Account);
            }

            if (FullReply)
            {
                data.Add("fullreply", "true");
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
        public async Task<ReauthResponse> Post()
        {
            var content = new StringContent(ToQueryString(), DIBSClient.Options.Encoding, "application/x-www-form-urlencoded");
            var response = await DIBSClient.HttpClient.PostAsync(Uri, content);
            var paymentResponse = new ReauthResponse(response.Content.ReadAsStringAsync().Result);

            return paymentResponse;
        }
    }    
}