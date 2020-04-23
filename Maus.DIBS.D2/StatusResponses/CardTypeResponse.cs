using System.Web;

namespace DIBS.D2
{
    /// <summary>
    /// Response returned by Card Type.
    /// </summary>
    public class CardTypeResponse
    {
        public string Response { get; set; }



        public CardTypeResponse(string response)
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
