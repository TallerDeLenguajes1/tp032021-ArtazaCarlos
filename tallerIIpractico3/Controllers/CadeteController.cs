using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using tallerIIpractico3.entities;
using NLog;
using Microsoft.AspNetCore.Http;
using tallerIIpractico3.ViewModel;
using AutoMapper;
using tallerIIpractico3.Models.Entities;
using tallerIIpractico3.Models.Db;

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
            Usuario userDb = db.UsuarioDb.UsuarioByUserPass(
                HttpContext.Session.GetString("user"), HttpContext.Session.GetString("pass"));

            if (userDb != null)
            {
                UsuarioViewModel userVM = mapper.Map<UsuarioViewModel>(userDb);
                CadeteIndexViewModel cadeteIndexVM = new CadeteIndexViewModel();
                cadeteIndexVM.UserLog = userVM;

                List<Cadete> cadetesDb = db.CadeteDb.ReadCadetes();
                var cadetesVM = mapper.Map<List<CadeteViewModel>>(cadetesDb);
                foreach (var item in cadetesVM)
                {
                    item.Pedidos = mapper.Map<List<PedidoViewModel>>(db.PedidoDb.GetPedidosImpagos(item.Id));
                }
                cadeteIndexVM.Cadetes = cadetesVM;
                return View(cadeteIndexVM);
            }
            return RedirectToAction("IndexUsuario", "Usuario");
        }

        //**************************************AGREGAR CADETE**************************************

        public IActionResult CreateCadete()
        {
            Usuario userDb = db.UsuarioDb.UsuarioByUserPass(
                HttpContext.Session.GetString("user"), HttpContext.Session.GetString("pass"));

            if (userDb != null)
            {
                CadeteABMViewModel cadeteCreateVM = carcarModelosCadete(userDb, new CadeteViewModel());
                return View(cadeteCreateVM);
            }
            return RedirectToAction("IndexUsuario", "Usuario");
        }

        [HttpPost]
        public IActionResult SaveCadete(CadeteABMViewModel cadeteVM)
        {
            if (ModelState.IsValid)
            {
                Cadete cadeteDb = mapper.Map<Cadete>(cadeteVM.Cadete);
                db.CadeteDb.SaveCadete(cadeteDb);
                return RedirectToAction("IndexCadete");
            }
            return RedirectToAction("ErrorCreateCadete", "Logger");
        }

        //***************************************MODIFICAR CADETE************************************

        [HttpGet]
        public IActionResult FormUpdateCadete(CadeteViewModel cadeteVM)
        {
            if (ModelState.IsValid)
            {
                Usuario userDb = db.UsuarioDb.UsuarioByUserPass(
                    HttpContext.Session.GetString("user"), HttpContext.Session.GetString("pass"));

                if (userDb != null)
                {
                    CadeteABMViewModel cadeteUpdateVM = carcarModelosCadete(userDb, cadeteVM);
                    return View(cadeteUpdateVM);
                }
                return RedirectToAction("IndexUsuario", "Usuario");
            }
            return RedirectToAction("ErrorUpdateCadete", "Logger");
        }

        [HttpPost]
        public IActionResult ModificarCadete(CadeteABMViewModel cadeteUpdateVM)
        {
            if (ModelState.IsValid)
            {
                Cadete cadeteDb = mapper.Map<Cadete>(cadeteUpdateVM.Cadete);
                db.CadeteDb.UpdateCadete(cadeteDb);
                return RedirectToAction("IndexCadete");
            }
            return RedirectToAction("ErrorUpdateCadete", "Logger");
        }

        ////****************************************PAGAR A CADETE*****************************************

        [HttpGet]
        public IActionResult PagarACadete(CadeteViewModel cadeteVM)
        {
            if (ModelState.IsValid)
            {
                Usuario userDb = db.UsuarioDb.UsuarioByUserPass(
                    HttpContext.Session.GetString("user"), HttpContext.Session.GetString("pass"));

                if (userDb != null)
                {
                    var pedidosVM = mapper.Map<List<PedidoViewModel>>(db.PedidoDb.GetPedidosImpagos(cadeteVM.Id));
                    if (pedidosVM.Count() > 0)
                    {
                        CadeteABMViewModel cadetePagoVM = carcarModelosCadete(userDb, cadeteVM);
                        cadetePagoVM.Cadete.Pedidos = pedidosVM;
                        return View(cadetePagoVM);
                    }
                    return RedirectToAction("IndexCadete", "Cadete");
                }
                return RedirectToAction("IndexUsuario", "Usuario");
            }
            return RedirectToAction("ErrorAlPagarCadete", "Logger");

        }


        //***************************************ELIMINAR CADETE************************************
        [HttpGet]
        public IActionResult DeleteView(CadeteViewModel cadeteVM)
        {
            if (ModelState.IsValid)
            {
                Usuario userDb = db.UsuarioDb.UsuarioByUserPass(
                    HttpContext.Session.GetString("user"), HttpContext.Session.GetString("pass"));

                if (userDb != null)
                {
                    var pedidosVM = mapper.Map<List<PedidoViewModel>>(db.PedidoDb.GetPedidosImpagos(cadeteVM.Id));
                    if (pedidosVM.Count() > 0)
                    {
                        return RedirectToAction("DeleteViewPendientes", cadeteVM);
                    }
                    CadeteABMViewModel cadeteDeleteVM = carcarModelosCadete(userDb, cadeteVM);
                    return View(cadeteDeleteVM);
                }
                return RedirectToAction("IndexUsuario", "Usuario");
            }
            return RedirectToAction("ErrorDeleteCadete", "Logger");
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
            Usuario userDb = db.UsuarioDb.UsuarioByUserPass(
                    HttpContext.Session.GetString("user"), HttpContext.Session.GetString("pass"));

            if (userDb != null)
            {
                var pedidosVM = mapper.Map<List<PedidoViewModel>>(db.PedidoDb.GetPedidosImpagos(cadeteVM.Id));
                CadeteABMViewModel cadeteDeleteVM = carcarModelosCadete(userDb, cadeteVM);
                cadeteDeleteVM.Cadete.Pedidos = pedidosVM;
                return View(cadeteDeleteVM);
            }
            return RedirectToAction("IndexUsuario", "Usuario");
        }


        [HttpGet]
        public IActionResult PagarACadeteFromDelete(CadeteViewModel cadeteVM)
        {

            if (ModelState.IsValid)
            {
                Usuario userDb = db.UsuarioDb.UsuarioByUserPass(
                    HttpContext.Session.GetString("user"), HttpContext.Session.GetString("pass"));
                if (userDb != null)
                {
                    var pedidosVM = mapper.Map<List<PedidoViewModel>>(db.PedidoDb.GetPedidosImpagos(cadeteVM.Id));
                    CadeteABMViewModel cadetePagoVM = carcarModelosCadete(userDb, cadeteVM);
                    cadetePagoVM.Cadete.Pedidos = pedidosVM;
                    return View(cadetePagoVM);
                }

                return RedirectToAction("IndexUsuario", "Usuario");
            }
            return RedirectToAction("ErrorAlPagarCadete", "Logger");
        }


        //***************************************METODOS ADICIONALES************************************

        private CadeteABMViewModel carcarModelosCadete(Usuario userDb, CadeteViewModel cadeteVM)
        {
            UsuarioViewModel userVM = mapper.Map<UsuarioViewModel>(userDb);
            CadeteABMViewModel modeloCargadoVM = new CadeteABMViewModel();
            modeloCargadoVM.UserLog = userVM;
            modeloCargadoVM.Cadete = cadeteVM;
            return modeloCargadoVM;
        }


    }


}

