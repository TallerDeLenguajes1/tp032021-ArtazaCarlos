using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace tallerIIpractico3.Controllers
{
    public class LoggerController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
