//Return data type, IActionResult returns all types of data
//View Result returns HTML view
//return View();

//JsonResult returns JSON file
//return Json(data);

//ContentResult returns plain text content
//return Content("plain text");

//RedirectsResult redirects URL 
//return Redirect("url");

//RedirectActionResult redirect to action
//return RedirectToAction("ActionName");

//StatusCodeResult returns HTTP status code
//return StatusCode(404);

/*------------------------------------------------------------------*/

//Action parameters (way for action to get input)
//can be from URL, query and form submission




using System.Diagnostics;
using cloudass.Models;
using Microsoft.AspNetCore.Mvc;

namespace cloudass.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
             return View();
            //When run view, route to same name of file in cshtml
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
