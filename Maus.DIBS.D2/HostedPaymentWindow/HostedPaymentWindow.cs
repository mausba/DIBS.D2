using System;
using System.Collections.Generic;

namespace DIBS.D2
{
    /// <summary>
    /// The responsive D2 Hosted Payment Window - FlexWin - provides the possibility of high customization and a wide variety of functionalities.
    /// </summary>
    public class HostedPaymentWindow
    {
        /// <summary>
        /// Post URL
        /// </summary>
        public static Uri Uri = new Uri("https://payment.architrade.com/paymentweb/start.action");

        /// <summary>
        /// The URL of the page to be displayed if the purchase is approved.
        /// </summary>
        public string AcceptUrl { get; set; }

        /// <summary>
        /// The smallest unit of an amount in the selected currency.
        /// </summary>
        public long Amount { get; set; }

        /// <summary>
        /// An optional (but recommended) ”server-to-server” call. Can be used for many purposes. 
        /// The most important being that you will be able to register the order in your own system 
        /// without depending on the customers browser hitting a specific page of the shop.
        /// </summary>
        public string CallbackUrl { get; set; }

        /// <summary>
        /// The URL of the page to be displayed if the purchase is cancelled.
        /// </summary>
        public string CancelUrl { get; set; }

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
        /// Specifies which of the pre-built decorators to use.
        /// </summary>
        public Decorator Decorator { get; set; }

        /// <summary>
        /// This parameter determines the language in which the page will be displayed.
        /// </summary>
        public Language Lang { get; set; }

        /// <summary>
        /// The shops order number for a particular purchase. This is the cross reference between 
        /// the shop and Nets (both numerals and letters may be used). The actual length of the OrderId 
        /// shown on the bank settlement report may differ from acquirer to acquirer.
        /// </summary>
        public string OrderId { get; set; }

        /// <summary>
        /// Identifies the authorisation as a ticket authorisation rather than a normal transaction. 
        /// Please note that the pre-authorised transaction is NOT available among the transactions in the Nets Administration interface.
        /// NOTE: You cannot use "capturenow" along with "preauth".
        /// </summary>
        public bool Preauth { get; set; }

        /// <summary>
        /// When this field is declared, the transaction is not dispatched to the card issuer, 
        /// but is instead handled by the Nets test environment.
        /// </summary>
        public bool Test { get; set; }



        /// <summary>
        /// Creates a new Hosted Payment Window.
        /// </summary>
        /// <param name="AcceptUrl">The URL of the page to be displayed if the purchase is approved.</param>
        /// <param name="Amount">The smallest unit of an amount in the selected currency.</param>
        /// <param name="Currency">Currency is defined using the ISO4217 standard.</param>
        /// <param name="OrderId">The shops order number for a particular purchase.</param>
        public HostedPaymentWindow(string AcceptUrl, int Amount, Currency Currency, string OrderId)
        {
            this.AcceptUrl = AcceptUrl;
            this.Amount = Amount;
            this.Currency = Currency;
            this.OrderId = OrderId;
        }

        /// <summary>
        /// Return object as a query string.
        /// </summary>
        /// <returns></returns>
        public string ToQueryString()
        {
            var data = new Dictionary<string, string>
            {
                { "accepturl ", AcceptUrl },
                { "amount", Amount.ToString() },
                { "currency", Currency.Code },
                { "merchant", DIBSClient.Options.Merchant.ToString() },
                { "orderid", OrderId }
            };

            if (!string.IsNullOrEmpty(CallbackUrl))
            {
                data.Add("callbackurl", CallbackUrl);
            }

            if (!string.IsNullOrEmpty(CancelUrl))
            {
                data.Add("cancelurl", CancelUrl);
            }

            if(CaptureNow && !Preauth)
            {
                data.Add("capturenow", "true");
            }

            if (Decorator != null)
            {
                data.Add("decorator", Decorator.Value);
            }

            if(Lang != null)
            {
                data.Add("lang", Lang.Value);
            }

            if(Preauth && !CaptureNow)
            {
                data.Add("preauth", "true");
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
    }
}