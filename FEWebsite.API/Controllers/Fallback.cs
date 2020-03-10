using System.IO;

using Microsoft.AspNetCore.Mvc;

namespace FEWebsite.API.Controllers
{
    public class Fallback : Controller
    {
        public IActionResult Index()
        {
            return this.PhysicalFile(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot",
                "index.html"), "text/HTML");
        }
    }
}
