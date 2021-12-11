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
using tallerIIpractico3.Models.Entities;

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
            Usuario userDb = db.UsuarioDb.UsuarioByUserPass(
                HttpContext.Session.GetString("user"), HttpContext.Session.GetString("pass"));
            if (userDb != null)
            {
                UsuarioViewModel userVM = mapper.Map<UsuarioViewModel>(userDb);
                ClienteIndexViewModel clienteIndexVM = new ClienteIndexViewModel();
                clienteIndexVM.UserLog = userVM;

                return View(clienteIndexVM);
            }
            return RedirectToAction("IndexUsuario", "Usuario");
        }

        public IActionResult IndexTodosLosClientes()
        {
            Usuario userDb = db.UsuarioDb.UsuarioByUserPass(
                HttpContext.Session.GetString("user"), HttpContext.Session.GetString("pass"));
            if (userDb != null)
            {
                UsuarioViewModel userVM = mapper.Map<UsuarioViewModel>(userDb);
                var clientesVM = mapper.Map<List<ClienteViewModel>>(db.ClienteDb.ReadCliente());
                ClienteIndexViewModel clienteIndexVM = new ClienteIndexViewModel();
                clienteIndexVM.Clientes = clientesVM;
                clienteIndexVM.UserLog = userVM;

                return View(clienteIndexVM);
            }
            return RedirectToAction("IndexUsuario", "Usuario");
        }


        public IActionResult IndexBusquedaFiltrada(string busqueda)
        {
            Usuario userDb = db.UsuarioDb.UsuarioByUserPass(
                HttpContext.Session.GetString("user"), HttpContext.Session.GetString("pass"));
            if (userDb != null)
            {
                UsuarioViewModel userVM = mapper.Map<UsuarioViewModel>(userDb);
                var clientesVM = mapper.Map<List<ClienteViewModel>>(db.ClienteDb.BusquedaFiltrada(busqueda));
                ClienteIndexViewModel clienteIndexVM = new ClienteIndexViewModel();
                clienteIndexVM.Clientes = clientesVM;
                clienteIndexVM.UserLog = userVM;

                return View(clienteIndexVM);
            }
            return RedirectToAction("IndexUsuario", "Usuario");
        }







        public IActionResult CreateView()
        {
            Usuario userDb = db.UsuarioDb.UsuarioByUserPass(
                HttpContext.Session.GetString("user"), HttpContext.Session.GetString("pass"));

            if (userDb != null)
            {
                UsuarioViewModel userVM = mapper.Map<UsuarioViewModel>(userDb);
                ClienteABMViewModel clienteCreateVM = new ClienteABMViewModel();
                clienteCreateVM.UserLog = userVM;
                clienteCreateVM.Cliente = new ClienteViewModel();
                return View(clienteCreateVM);
            }
            return RedirectToAction("IndexUsuario", "Usuario");
        }

        [HttpPost]
        public IActionResult CreateCliente(ClienteABMViewModel clienteCreateVM)
        {
            if (ModelState.IsValid)
            {
                if (db.ClienteDb.ClienteByNom(clienteCreateVM.Cliente.Nombre) == null)
                {
                    Cliente clienteDb = mapper.Map<Cliente>(clienteCreateVM.Cliente);
                    db.ClienteDb.SaveCliente(clienteDb);
                    return RedirectToAction("IndexCliente");
                }
                return RedirectToAction("ErrorCreateDuplicado", "Logger");

            }
            return RedirectToAction("ErrorCreateCliente", "Logger");
        }

        [HttpGet]
        public IActionResult EditView(ClienteViewModel clienteVM)
        {
            if (ModelState.IsValid)
            {
                Usuario userDb = db.UsuarioDb.UsuarioByUserPass(
                                HttpContext.Session.GetString("user"), HttpContext.Session.GetString("pass"));

                if (userDb != null)
                {
                    UsuarioViewModel userVM = mapper.Map<UsuarioViewModel>(userDb);
                    ClienteABMViewModel clienteUpdateVM = new ClienteABMViewModel();
                    clienteUpdateVM.UserLog = userVM;
                    clienteUpdateVM.Cliente = clienteVM;
                    return View(clienteUpdateVM);
                }
                return RedirectToAction("IndexUsuario", "Usuario");
            }
            return RedirectToAction("ErrorUpdateCliente", "Logger");
        }

        [HttpPost]
        public IActionResult UpdateCliente(ClienteABMViewModel clienteUpdateVM)
        {
            if (ModelState.IsValid)
            {
                Cliente clienteDb = mapper.Map<Cliente>(clienteUpdateVM.Cliente);
                db.ClienteDb.UpdateCliente(clienteDb);
                return RedirectToAction("IndexCliente");
            }
            return RedirectToAction("ErrorUpdateCliente", "Logger");
        }

        public IActionResult DeleteView(ClienteViewModel clienteVM)
        {
            if (ModelState.IsValid)
            {
                Usuario userDb = db.UsuarioDb.UsuarioByUserPass(
                                HttpContext.Session.GetString("user"), HttpContext.Session.GetString("pass"));

                if (userDb != null)
                {
                    UsuarioViewModel userVM = mapper.Map<UsuarioViewModel>(userDb);
                    ClienteABMViewModel clienteDeleteVM = new ClienteABMViewModel();
                    clienteDeleteVM.UserLog = userVM;
                    clienteDeleteVM.Cliente = clienteVM;
                    return View(clienteDeleteVM);
                }
                return RedirectToAction("IndexUsuario", "Usuario");
            }
            return RedirectToAction("ErrorDeleteCliente", "Logger");
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
