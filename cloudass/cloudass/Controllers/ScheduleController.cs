using Microsoft.AspNetCore.Mvc;

namespace cloudass.Controllers
{
    public class AppointmentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
