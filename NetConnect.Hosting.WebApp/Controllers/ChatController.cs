using Microsoft.AspNetCore.Http.Connections;
using Microsoft.AspNetCore.Mvc;

namespace NetConnect.Hosting.WebApp.Controllers
{
    public class ChatController : BaseController
    {
        public IActionResult Index()
        {
            //Connect(HttpTransportType.WebSockets);
            return View();
        }
    }
}