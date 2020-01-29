using Microsoft.AspNetCore.Mvc;

namespace FEWebsite.API.Helpers
{
    public class StatusCodeResultReturnObject
    {
        public StatusCodeResultReturnObject(StatusCodeResult statusCodeResult, string response)
        {
            this.Title = statusCodeResult.ToString();
            this.StatusCode = statusCodeResult.StatusCode;
            this.Response = response;
        }

        public string Title { get; set; }

        public int StatusCode { get; set; }

        public string Response { get; set; }
    }
}
