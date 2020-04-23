using System.Web;

namespace DIBS.D2
{
    /// <summary>
    /// Response returned by Cancel.
    /// </summary>
    public class CancelResponse
    {
        public string CardType { get; set; }

        public bool IsSuccess
        {
            get { return Status.ToUpper() == "ACCEPTED"; }
        }

        public string Message { get; set; }

        public string Reason { get; set; }

        public string Response { get; set; }

        public HandlingStatus Result { get; set; }

        public string Status { get; set; }

        public long Transact { get; set; }



        public CancelResponse(string response)
        {
            var queryString = HttpUtility.ParseQueryString(response);

            Response = response;

            if (!string.IsNullOrEmpty(queryString["cardtype"]))
            {
                CardType = queryString["cardtype"];
            }

            if (!string.IsNullOrEmpty(queryString["message"]))
            {
                Message = queryString["message"];
            }

            if (!string.IsNullOrEmpty(queryString["reason"]))
            {
                Reason = queryString["reason"];
            }

            if (!string.IsNullOrEmpty(queryString["result"]))
            {
                Result = new HandlingStatus(int.Parse(queryString["result"]));
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
