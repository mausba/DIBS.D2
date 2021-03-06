﻿using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace DIBS.D2
{
    /// <summary>
    /// Used to get the status of a transaction status in Nets' system.
    /// </summary>
    public class TransStatus
    {
        /// <summary>
        /// Post URL
        /// </summary>
        public static Uri Uri = new Uri("http://payment.architrade.com/transstatus.pml");

        private static DateTime LastCall = DateTime.MinValue;

        /// <summary>
        /// If multiple departments utilize the same DIBS account, it may be practical to keep the transactions separate at DIBS. 
        /// An account name may be inserted in this field, to separate transactions at DIBS.
        /// </summary>
        public string Account { get; set; }

        /// <summary>
        /// The unique Nets identification number for the transaction.
        /// </summary>
        public long Transact { get; set; }



        /// <summary>
        /// Creates a new Trans Status.
        /// </summary>
        /// <param name="Transact">The unique Nets identification number for the transaction.</param>
        public TransStatus(long Transact)
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
        public async Task<TransactionStatus> Post()
        {
            if ((DateTime.Now - LastCall).TotalMilliseconds <= 500)
                await Task.Delay(500);

            var content = new StringContent(ToQueryString(), DIBSClient.Options.Encoding, "application/x-www-form-urlencoded");
            var response = await DIBSClient.HttpClient.PostAsync(Uri, content).ConfigureAwait(false);
            var paymentResponse = new TransactionStatus(int.Parse(response.Content.ReadAsStringAsync().Result));

            return paymentResponse;
        }
    }
}
