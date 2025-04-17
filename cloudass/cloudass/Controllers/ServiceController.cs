using Microsoft.AspNetCore.Mvc;

namespace cloudass.Controllers
{
    public class ServicesController : Controller
    {
        public IActionResult Teeth_cleaning()
        {
            return View();
        }

        public IActionResult Teeth_Bracing()
        {
            return View();
        }

        public IActionResult Tooth_Fillings()
        {
            return View();
        }
    }
}
