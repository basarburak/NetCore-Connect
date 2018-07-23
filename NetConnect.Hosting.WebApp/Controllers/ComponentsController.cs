using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetConnect.Hosting.WebApp.Controllers
{
    public class ComponentsController : Controller
    {
        public IActionResult InvokeMessage()
        {
            // doesn't works: this component's view is NOT in Views/<controller>
            return ViewComponent("InvokeMessage");
        }
    }
}
