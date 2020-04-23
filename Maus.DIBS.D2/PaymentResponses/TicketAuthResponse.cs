using System.Web;

namespace DIBS.D2
{
    /// <summary>
    /// Response returned by Ticket Auth.
    /// </summary>
    public class TicketAuthResponse
    {
        public string ApprovalCode { get; set; }

        public string AuthKey { get; set; }

        public string CardType { get; set; }

        public int Fee { get; set; }

        public bool IsSuccess
        {
            get { return Status.ToUpper() == "ACCEPTED"; }
        }

        public string Message { get; set; }

        public AuthorizationStatus Reason { get; set; }

        public string Response { get; set; }

        public string Status { get; set; }

        public long Transact { get; set; }



        public TicketAuthResponse(string response)
        {
            var queryString = HttpUtility.ParseQueryString(response);

            Response = response;

            if (!string.IsNullOrEmpty(queryString["approvalcode"]))
            {
                ApprovalCode = queryString["approvalcode"];
            }

            if (!string.IsNullOrEmpty(queryString["authkey"]))
            {
                AuthKey = queryString["authkey"];
            }

            if (!string.IsNullOrEmpty(queryString["cardtype"]))
            {
                CardType = queryString["cardtype"];
            }

            if (!string.IsNullOrEmpty(queryString["fee"]))
            {
                Fee = int.Parse(queryString["fee"]);
            }

            if (!string.IsNullOrEmpty(queryString["message"]))
            {
                Message = queryString["message"];
            }

            if (!string.IsNullOrEmpty(queryString["reason"]))
            {
                Reason = new AuthorizationStatus(int.Parse(queryString["reason"]));
            }

            if (!string.IsNullOrEmpty(queryString["status"]))
            {
                Status = queryString["status"];
            }

            if (!string.IsNullOrEmpty(queryString["transact"]))
            {
                Transact = long.Parse(queryString["transact"]);
            }
        }
    }
}
