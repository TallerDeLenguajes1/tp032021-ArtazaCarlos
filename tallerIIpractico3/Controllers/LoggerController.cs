using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace tallerIIpractico3.Controllers
{
    public class LoggerController : Controller
    {
        private readonly Logger logger = LogManager.GetCurrentClassLogger();

        public IActionResult Index()
        {
            return View();
        }
    }
}
