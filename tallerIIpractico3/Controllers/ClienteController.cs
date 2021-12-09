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
using tallerIIpractico3.ViewModel;
using AutoMapper;

namespace tallerIIpractico3.Controllers
{
    public class ClienteController : Controller
    {
        private readonly Db db;
        private readonly IMapper mapper;
        private readonly Logger logger = LogManager.GetCurrentClassLogger();

        public ClienteController(Db Db, IMapper mapper)
        {
            db = Db;
            this.mapper = mapper;
        }


        public IActionResult IndexCliente()
        {
            var clienteVM = mapper.Map<List<ClienteViewModel>>(db.ClienteDb.ReadCliente());
            return View(clienteVM);
        }

        public IActionResult CreateView()
        {
            return View(new ClienteViewModel());
        }

        [HttpPost]
        public IActionResult CreateCliente(ClienteViewModel clienteVM)
        {
            if (ModelState.IsValid)
            {
                Cliente clienteDb = mapper.Map<Cliente>(clienteVM);
                db.ClienteDb.SaveCliente(clienteDb);
                return RedirectToAction("IndexCliente");
            }
            return RedirectToAction("ErrorCreateCliente", "Logger");
        }

        [HttpGet]
        public IActionResult EditView(ClienteViewModel clienteVM)
        {
            return View(clienteVM);
        }

        [HttpPost]
        public IActionResult UpdateCliente(ClienteViewModel clienteVM)
        {
            if (ModelState.IsValid)
            {
                Cliente clienteDb = mapper.Map<Cliente>(clienteVM);
                db.ClienteDb.UpdateCliente(clienteDb);
                return RedirectToAction("IndexCliente");
            }
            return RedirectToAction("ErrorUpdateCliente", "Logger");
        }

        public IActionResult DeleteView(ClienteViewModel clienteVM)
        {
            return View(clienteVM);
        }

      
        public IActionResult DeteleCliente(int clienteId)
        {
            if (db.ClienteDb.DeleteCliente(clienteId))
            {
                return RedirectToAction("IndexCliente");
            }
            return RedirectToAction("ErrorDeleteCliente", "Logger");
        }


    }
}
