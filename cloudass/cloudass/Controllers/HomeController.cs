using System.Diagnostics;
using cloudass.Models;
using cloudass.Models.DbTable;
using cloudass.Services;
using Microsoft.AspNetCore.Mvc;

namespace cloudass.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IDentalService _dentalService;

        public HomeController(ILogger<HomeController> logger, IDentalService dentalService)
        {
            _logger = logger;
            _dentalService = dentalService;
        }

        public async Task<IActionResult> Index()
        {
            var services = await _dentalService.getAllServiceAsync();
            return View(services);
        }


    }
}
