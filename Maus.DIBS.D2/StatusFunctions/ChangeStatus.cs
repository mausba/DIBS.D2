using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace DIBS.D2
{
    /// <summary>
    /// Used to change the status of a transaction.
    /// </summary>
    public class ChangeStatus
    {
        /// <summary>
        /// Post URL
        /// </summary>
        public static Uri Uri = new Uri("https://payment.architrade.com/cgi-adm/changestatus.cgi");

        /// <summary>
        /// If multiple departments utilize the same DIBS account, it may be practical to keep the transactions separate at DIBS. 
        /// An account name may be inserted in this field, to separate transactions at DIBS.
        /// </summary>
        public string Account { get; set; }

        /// <summary>
        /// Action must take on either: “new”, “matched”, “canceled”, or “captured” depending on the 
        /// status you want the transaction to have afterwards.
        /// </summary>
        public string Action { get; set; }

        /// <summary>
        /// The smallest unit of an amount in the selected currency, following ISO4217.
        /// </summary>
        public int Amount { get; set; }

        /// <summary>
        /// Use this to change status from either Pending or Declined to New, Cancelled or Captured.
        /// </summary>
        public bool Force { get; set; }

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
        /// The transact is a as minimum 6-digit integer, e.g. transact=123456.
        /// </summary>
        public long Transact { get; set; }



        /// <summary>
        /// The purpose of ChangeStatus is to allow changing the status of a specific transaction on the DIBS server, 
        /// without using the DIBS administration interface.
        /// </summary>
        public ChangeStatus(string Action, int Amount, bool TextReply, long Transact)
        {
            this.Action = Action;
            this.Amount = Amount;
            this.TextReply = TextReply;
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
                { "action", Action },
                { "amount", Amount.ToString() },
                { "merchant", DIBSClient.Options.Merchant.ToString() },
                { "textreply", TextReply ? "yes" : "no" },
                { "transact", Transact.ToString() }
            };

            if (!string.IsNullOrEmpty(Account))
            {
                data.Add("account", Account);
            }

            if (Force)
            {
                data.Add("force", "true");
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
        public async Task<ChangeStatusResponse> Post()
        {
            var content = new StringContent(ToQueryString(), DIBSClient.Options.Encoding, "application/x-www-form-urlencoded");

            var login = DIBSClient.Options.Encoding.GetBytes(DIBSClient.Options.ApiLogin + ":" + DIBSClient.Options.ApiPassword);
            DIBSClient.HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(login));

            var response = await DIBSClient.HttpClient.PostAsync(Uri, content).ConfigureAwait(false);
            var paymentResponse = new ChangeStatusResponse(response.Content.ReadAsStringAsync().Result);

            DIBSClient.HttpClient.DefaultRequestHeaders.Authorization = null;

            return paymentResponse;
        }
    }
}
