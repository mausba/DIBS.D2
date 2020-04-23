using System.Web;

namespace DIBS.D2
{
    /// <summary>
    /// Response returned by Pay Info.
    /// </summary>
    public class PayInfoResponse
    {
        /// <summary>
        /// This is the action code returned by the acquirer.
        /// </summary>
        public string ActionCode { get; set; }

        /// <summary>
        /// The smallest unit of an amount in the selected currency.
        /// </summary>
        public long Amount { get; set; }

        /// <summary>
        /// Returnes the approvalcode from the acquire if available.
        /// </summary>
        public string ApprovalCode { get; set; }

        /// <summary>
        /// When the transaction is captured by a batch job, this parameter contains the ID of the batch job.
        /// </summary>
        public int Batch { get; set; }

        /// <summary>
        /// Currency is defined using the ISO4217 standard.
        /// </summary>
        public Currency Currency { get; set; }

        /// <summary>
        /// When calcfee is used, the calculated fee is returned so it can be shown on the receipt.
        /// </summary>
        public int Fee { get; set; }

        /// <summary>
        /// The shop’s order number for this particular purchase. It can be seen later when a payment is captured,
        /// and will in some instances appear on the customer’s bank statement (both numerals and letters may be used).
        /// </summary>
        public string OrderId { get; set; }

        /// <summary>
        /// Full response returned by post request.
        /// </summary>
        public string Response { get; set; }

        /// <summary>
        /// Status of the payment in the DIBS backend.
        /// </summary>
        public TransactionStatus Status { get; set; }

        /// <summary>
        /// The unique DIBS identification number for the transaction.
        /// </summary>
        public long Transact { get; set; }



        /// <summary>
        /// Creates a new Pay Info Response.
        /// </summary>
        /// <param name="response"></param>
        public PayInfoResponse(string response)
        {
            var queryString = HttpUtility.ParseQueryString(response);

            Response = response;

            if (!string.IsNullOrEmpty(queryString["actioncode"]))
            {
                ActionCode = queryString["actioncode"];
            }

            if (!string.IsNullOrEmpty(queryString["amount"]))
            {
                Amount = long.Parse(queryString["amount"]);
            }

            if (!string.IsNullOrEmpty(queryString["approvalcode"]))
            {
                ApprovalCode = queryString["approvalcode"];
            }

            if (!string.IsNullOrEmpty(queryString["batch"]))
            {
                Batch = int.Parse(queryString["batch"]);
            }

            if (!string.IsNullOrEmpty(queryString["currency"]))
            {
                Currency = new Currency(queryString["currency"]);
            }

            if (!string.IsNullOrEmpty(queryString["fee"]))
            {
                Fee = int.Parse(queryString["fee"]);
            }

            if (!string.IsNullOrEmpty(queryString["orderid"]))
            {
                OrderId = queryString["orderid"];
            }

            if (!string.IsNullOrEmpty(queryString["status"]))
            {
                Status = new TransactionStatus(int.Parse(queryString["status"]));
            }

            if (!string.IsNullOrEmpty(queryString["transact"]))
            {
                Transact = long.Parse(queryString["transact"]);
            }
        }
    }
}
