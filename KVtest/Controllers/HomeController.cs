using KVtest.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace KVtest.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UnServicio _unServicio;

        public HomeController(ILogger<HomeController> logger, UnServicio unServicio)
        {
            _logger = logger;
            _unServicio = unServicio;
        }

        public IActionResult Index()
        {
            ViewBag.Texto = _unServicio.Texto;

            return View();
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