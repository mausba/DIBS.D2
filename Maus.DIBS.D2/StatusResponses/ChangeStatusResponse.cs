using System.Web;

namespace DIBS.D2
{
    // <summary>
    /// Response returned by Change Status.
    /// </summary>
    public class ChangeStatusResponse
    {
        public string Response { get; set; }



        public ChangeStatusResponse(string response)
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
