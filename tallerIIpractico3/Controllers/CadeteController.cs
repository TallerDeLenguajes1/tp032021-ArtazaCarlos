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
    public class CadeteController : Controller
    {
        private readonly Db db;
        private readonly IMapper mapper;
        private readonly Logger logger = LogManager.GetCurrentClassLogger();


        public CadeteController(Db Db, IMapper mapper)
        {
            db = Db;
            this.mapper = mapper;
        }

        public IActionResult IndexCadete()
        {
            Usuario userDb = db.UsuarioDb.UsuarioByUser(HttpContext.Session.GetString("user"));
            if (userDb != null)
            {
                UsuarioViewModel userVM = mapper.Map<UsuarioViewModel>(userDb);
                List<Cadete> cadetesDb = db.CadeteDb.ReadCadetes();
                var cadetesVM = mapper.Map<List<CadeteViewModel>>(cadetesDb);
                foreach (var item in cadetesVM)
                {
                    item.Pedidos = mapper.Map<List<PedidoViewModel>>(db.PedidoDb.GetPedidosImpagos(item.Id));
                }
                return View(new Tuple<UsuarioViewModel, List<CadeteViewModel>>(userVM, cadetesVM));
            }
            return RedirectToAction("IndexUsuario", "Usuario");
        }

        //**************************************AGREGAR CADETE**************************************

        public IActionResult CreateCadete()
        {
            return View(new CadeteViewModel());
        }

        [HttpPost]
        public IActionResult SaveCadete(CadeteViewModel cadeteVM)
        {
            if (ModelState.IsValid)
            {
                Cadete cadeteDb = mapper.Map<Cadete>(cadeteVM);
                db.CadeteDb.SaveCadete(cadeteDb);
                return RedirectToAction("IndexCadete");
            }
            return RedirectToAction("ErrorCreateCadete", "Logger");
        }

        //***************************************MODIFICAR CADETE************************************

        [HttpGet]
        public IActionResult FormUpdateCadete(CadeteViewModel cadeteVM)
        {
            return View(cadeteVM);
        }

        [HttpPost]
        public IActionResult ModificarCadete(CadeteViewModel cadeteUpdateVM)
        {
            if (ModelState.IsValid)
            {
                Cadete cadeteDb = mapper.Map<Cadete>(cadeteUpdateVM);
                db.CadeteDb.UpdateCadete(cadeteDb);
                return RedirectToAction("IndexCadete");
            }
            return RedirectToAction("ErrorUpdateCadete", "Logger");
        }

        ////****************************************PAGAR A CADETE*****************************************

        [HttpGet]
        public IActionResult PagarACadete(CadeteViewModel cadeteVM)
        {
            var pedidosVM = mapper.Map<List<PedidoViewModel>>(db.PedidoDb.GetPedidosImpagos(cadeteVM.Id));
            cadeteVM.Pedidos = pedidosVM;
            return View(cadeteVM);
        }


        //***************************************ELIMINAR CADETE************************************
        [HttpGet]
        public IActionResult DeleteView(CadeteViewModel cadeteVM)
        {
            var pedidosVM = mapper.Map<List<PedidoViewModel>>(db.PedidoDb.GetPedidosImpagos(cadeteVM.Id));
            if (pedidosVM.Count() > 0)
            {
                return RedirectToAction("DeleteViewPendientes", cadeteVM);
            }
            
            return View(cadeteVM);
        }

        [HttpGet]
        public IActionResult DeleteCadete(int cadeteId)
        {
            if (db.CadeteDb.DeleteCadete(cadeteId))
            {
                return RedirectToAction("IndexCadete");
            }
            return RedirectToAction("ErrorDeleteCadete", "Logger");
        }



        //***************************************METODOS FROM DETELE VIEW************************************

        [HttpGet]
        public IActionResult DeleteViewPendientes(CadeteViewModel cadeteVM)
        {
            var pedidosVM = mapper.Map<List<PedidoViewModel>>(db.PedidoDb.GetPedidosImpagos(cadeteVM.Id));
            cadeteVM.Pedidos = pedidosVM;
            return View(cadeteVM);
        }


        [HttpGet]
        public IActionResult PagarACadeteFromDelete(CadeteViewModel cadeteVM)
        {
            var pedidosVM = mapper.Map<List<PedidoViewModel>>(db.PedidoDb.GetPedidosImpagos(cadeteVM.Id));
            cadeteVM.Pedidos = pedidosVM;
            return View(cadeteVM);
        }



    }
}
