using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MVC_Eurobanknoten.Models;
using WebApplication4.Models;

namespace WebApplication4.Controllers
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
        }

        [HttpPost]
        public IActionResult Einzelprüfung(string serialNumber)
        {
            Seriennummerprüfer checker = new Seriennummerprüfer();
            string result = checker.CheckSerial(serialNumber);
            ViewBag.Result = result;
            return View("Index");
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
