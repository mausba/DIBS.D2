using System.Web;

namespace DIBS.D2
{
    /// <summary>
    /// Response returned by Refund.
    /// </summary>
    public class RefundResponse
    {
        public bool IsSuccess
        {
            get { return Status.ToUpper() == "ACCEPTED"; }
        }

        public string Response { get; set; }

        public HandlingStatus Result { get; set; }

        public string Status { get; set; }



        public RefundResponse(string response)
        {
            var queryString = HttpUtility.ParseQueryString(response);

            Response = response;

            if (!string.IsNullOrEmpty(queryString["result"]))
            {
                Result = new HandlingStatus(int.Parse(queryString["result"]));
            }

            if (!string.IsNullOrEmpty(queryString["status"]))
            {
                Status = queryString["status"];
            }
        }
    }
}
