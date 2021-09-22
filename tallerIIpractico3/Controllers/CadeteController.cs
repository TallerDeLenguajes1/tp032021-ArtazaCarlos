using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using tallerIIpractico3.entities;
using tallerIIpractico3.Models;

namespace tallerIIpractico3.Controllers
{
    public class CadeteController : Controller
    {
        static int id = 0;
        private readonly ILogger<CadeteController> _logger;
        private readonly DBTemporal _DB;

        public CadeteController(ILogger<CadeteController> logger, DBTemporal DB)
        {
            _logger = logger;
            _DB = DB;
        }

        public IActionResult Index()
        {
            return View(_DB.leerDeArchivo());
        }

        public IActionResult CreateCadete()
        {            
            return View();
        }

        public IActionResult agregarCadete(string nom, string dir, string tel)
        {
            Cadete cadete_ = new Cadete(id, nom, dir, tel);
            id++;
            _DB.Cadeteria.Cadetes.Add(cadete_); //que pasa si la lista y archivos no coinciden con la cantidad de datos
            _DB.guardarEnArchivo(cadete_);
            return Redirect("Index");
        }
    }
}
