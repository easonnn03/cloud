using Microsoft.AspNetCore.Mvc;

namespace cloudass.Controllers
{
    public class ServicesController : Controller
    {
        public IActionResult TeethCleaning()
        {
            return View();
        }

        public IActionResult TeethBracing()
        {
            return View();
        }

        public IActionResult ToothFillings()
        {
            return View();
        }

        public IActionResult ToothExtraction()
        {
            return View();
        }
    }
}
