using Microsoft.AspNetCore.Mvc;

namespace FEWebsite.API.Helpers
{
    public class StatusCodeResultReturnObject
    {
        public StatusCodeResultReturnObject(StatusCodeResult statusCodeResult)
        {
            this.Title = statusCodeResult.ToString();
            this.StatusCode = statusCodeResult.StatusCode;
        }

        public string Title { get; set; }

        public int StatusCode { get; set; }

        public string Response { get; set; }
    }
}
