using System.Web;

namespace DIBS.D2
{
    /// <summary>
    /// Response returned by Trans Info.
    /// </summary>
    public class TransInfoResponse
    {
        public string Response { get; set; }



        public TransInfoResponse(string response)
        {
            var queryString = HttpUtility.ParseQueryString(response);

            Response = response;

            //if (!string.IsNullOrEmpty(queryString["result"]))
            //{
            //    Result = new HandlingReason(int.Parse(queryString["result"]));
            //}

            //if (!string.IsNullOrEmpty(queryString["status"]))
            //{
            //    Status = queryString["status"];
            //}
        }
    }
}
