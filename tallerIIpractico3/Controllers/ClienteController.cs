using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using tallerIIpractico3.entities;
using tallerIIpractico3.Models.Db;
using NLog;
using Microsoft.AspNetCore.Http;

namespace tallerIIpractico3.Controllers
{
    public class ClienteController : Controller
    {
        private readonly Db db;
        private readonly Logger logger = LogManager.GetCurrentClassLogger();

        public ClienteController(Db Db)
        {
            db = Db;
         
        }


        public IActionResult IndexCliente()
        {
            return View(db.ClienteDb.ReadCliente());
        }

        public IActionResult CreateView()
        {
            return View(new Cliente());
        }

        public IActionResult CreateCliente(Cliente cliente)
        {
            db.ClienteDb.SaveCliente(cliente);
            return RedirectToAction("IndexCliente");
        }

        public IActionResult EditView(Cliente cliente)
        {
            return View(cliente);
        }

        public IActionResult UpdateCliente(Cliente cliente)
        {
            db.ClienteDb.UpdateCliente(cliente);
            return RedirectToAction("IndexCliente");
        }

    }
}
