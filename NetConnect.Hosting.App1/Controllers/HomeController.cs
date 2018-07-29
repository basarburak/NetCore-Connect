using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NetConnect.Hosting.App1.Models;
using NetConnect.Hosting.Core.Controllers;
using System.Diagnostics;

namespace NetConnect.Hosting.App1.Controllers
{
    [Authorize]
    public class HomeController : BaseController
    {
        public HomeController()
        {
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

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
