using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MVC_Eurobanknoten.Models;
using WebApplication4.Models;


namespace Banknotenprüfung.Controllers
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
            if (string.IsNullOrWhiteSpace(serialNumber))
            {
                ViewBag.Result = "Bitte geben Sie eine Seriennummer ein.";
                return View("Index");
            }

            Seriennummerprüfer checker = new Seriennummerprüfer();
            string result = checker.CheckSerial(serialNumber);
            ViewBag.Result = result;
            return View("Index");
        }

        [HttpPost]
        public IActionResult Listenprüfung(string serialNumbers)
        {

            if (string.IsNullOrWhiteSpace(serialNumbers))
            {
                ViewBag.Result = "Bitte geben Sie eine Seriennummer ein.";
                return View("Index");
            }
            Seriennummerprüfer checker = new Seriennummerprüfer();
            string[] serialNumberArray = serialNumbers.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            List<string> results = new List<string>();
            foreach (string serialNumber in serialNumberArray)
            {
                string result = checker.CheckSerial(serialNumber);
                results.Add(result);
            }
            ViewBag.Results = results;
            return View("Index");
        }

        [HttpPost]
        public IActionResult Dateiprüfung(IFormFile datei)
        {
            if(datei == null || datei.Length == 0)
            {
                ViewBag.Result = "Bitte laden Sie eine Datei hoch.";
                return View("Index");
            }

            Seriennummerprüfer checker = new Seriennummerprüfer();

            List<string> results = new List<string>();

            if (datei != null && datei.Length > 0)
            {
                using (var reader = new StreamReader(datei.OpenReadStream()))
                {
                    while (!reader.EndOfStream)
                    {
                        string serialNumber = reader.ReadLine();
                        string result = checker.CheckSerial(serialNumber);
                        results.Add(result);
                    }
                }
            }
            ViewBag.Results = results;
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
