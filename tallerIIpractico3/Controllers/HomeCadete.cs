using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using tallerIIpractico3.entities;


namespace tallerIIpractico3.Controllers
{
    public class HomeCadete : Controller
    {
        static int id = 0;
        private readonly ILogger<HomeCadete> _logger;
        private readonly List<Cadete> cadetes;

        public HomeCadete(ILogger<HomeCadete> logger, List<Cadete> cadetes)
        {
            _logger = logger;
            this.cadetes = cadetes;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
