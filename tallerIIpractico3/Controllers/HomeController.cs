using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NLog;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using tallerIIpractico3.entities;
using tallerIIpractico3.Models;

namespace tallerIIpractico3.Controllers
{
    public class HomeController : Controller
    {
        private readonly Logger logger = LogManager.GetCurrentClassLogger();

        public HomeController()
        {

        }

        public IActionResult Index()
        {
            try
            {

            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
                throw;
            }
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
