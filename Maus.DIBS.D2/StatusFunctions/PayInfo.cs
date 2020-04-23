using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace DIBS.D2
{
    /// <summary>
    /// Used to get the basic transaction information.
    /// </summary>
    public class PayInfo
    {
        /// <summary>
        /// Post URL
        /// </summary>
        public static Uri Uri = new Uri("https://payment.architrade.com/cgi-adm/payinfo.cgi");

        /// <summary>
        /// The unique Nets identification number for the transaction.
        /// </summary>
        public long Transact { get; set; }



        /// <summary>
        /// Creates a new PayInfo.
        /// </summary>
        /// <param name="Transact">The unique Nets identification number for the transaction.</param>
        public PayInfo(long Transact)
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
        public async Task<PayInfoResponse> Post()
        {
            var content = new StringContent(ToQueryString(), DIBSClient.Options.Encoding, "application/x-www-form-urlencoded");

            var login = DIBSClient.Options.Encoding.GetBytes(DIBSClient.Options.ApiLogin + ":" + DIBSClient.Options.ApiPassword);
            DIBSClient.HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(login));

            var response = await DIBSClient.HttpClient.PostAsync(Uri, content);
            var paymentResponse = new PayInfoResponse(response.Content.ReadAsStringAsync().Result);

            DIBSClient.HttpClient.DefaultRequestHeaders.Authorization = null;

            return paymentResponse;
        }
    }
}
