using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace DIBS.D2
{
    /// <summary>
    /// Used to get the status of the connection to various acquirers.
    /// </summary>
    public class Status
    {
        /// <summary>
        /// Post URL
        /// </summary>
        public static Uri Uri = new Uri("https://payment.architrade.com/status.pml");

        /// <summary>
        /// The name of the acquirer for which you want to know the current status.
        /// It´s also possible to state "all". In this case Status will return the status of all connected acquirers.
        /// </summary>
        public string Acquirer { get; set; }

        /// <summary>
        /// Should be declared to receive the returned message in simple text format.
        /// </summary>
        public bool TextReply { get; set; } = true;



        public Status(string Acquirer, bool TextReply)
        {
            this.Acquirer = Acquirer;
            this.TextReply = TextReply;
        }

        /// <summary>
        /// Return object as a query string.
        /// </summary>
        /// <returns></returns>
        public string ToQueryString()
        {
            var data = new Dictionary<string, string>
            {
                { "acquirer", Acquirer },
                { "textreply", TextReply ? "true" : "false" },
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
        public async Task<StatusResponse> Post()
        {
            var content = new StringContent(ToQueryString(), DIBSClient.Options.Encoding, "application/x-www-form-urlencoded");
            var response = await DIBSClient.HttpClient.PostAsync(Uri, content);
            var paymentResponse = new StatusResponse(response.Content.ReadAsStringAsync().Result);

            return paymentResponse;
        }
    }
}
