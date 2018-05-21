using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers
{
    public class MainController : Controller
    {
        public IActionResult GetAngularPage()
        {
            return File("/index.html", "text/html");
        }

        public IActionResult ApiNotFound()
        {
            return NotFound();
        }
    }
}
