using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.Connections;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR.Client;
using NetConnect.Hosting.WebApp.Models;

namespace NetConnect.Hosting.WebApp.Controllers
{
    public class HomeController : BaseController
    {
        public async Task<IActionResult> Index()
        {
           // Connect(HttpTransportType.WebSockets);
            return View();
        }

        public IActionResult About()
        {
            Connect(HttpTransportType.WebSockets);
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
