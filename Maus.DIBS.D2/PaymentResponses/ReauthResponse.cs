using System.Web;

namespace DIBS.D2
{
    /// <summary>
    /// Response returned by Reauth.
    /// </summary>
    public class ReauthResponse
    {
        public bool IsSuccess
        {
            get { return Status.ToUpper() == "ACCEPTED"; }
        }

        public string Response { get; set; }

        public AuthorizationStatus Result { get; set; }

        public string Status { get; set; }



        public ReauthResponse(string response)
        {
            var queryString = HttpUtility.ParseQueryString(response);

            Response = response;

            if (!string.IsNullOrEmpty(queryString["result"]))
            {
                Result = new AuthorizationStatus(int.Parse(queryString["result"]));
            }

            if (!string.IsNullOrEmpty(queryString["status"]))
            {
                Status = queryString["status"];
            }
        }
    }
}
