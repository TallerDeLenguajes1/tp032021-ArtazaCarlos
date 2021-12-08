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
            List<Cadete> cadetes = db.CadeteDb.ReadCadetes();
            cadetes.ForEach(cadete => cadete.Pedidos = db.PedidoDb.GetPedidosImpagos(cadete.Id));
            return View(cadetes);
        }

        public IActionResult CreateCadete()
        {
            return View(new CadeteViewModel());
        }

        //**************************************AGREGAR CADETE**************************************

        [HttpPost]
        public IActionResult SaveCadete(CadeteViewModel cadeteVM)
        {
            Cadete cadeteDb = mapper.Map<Cadete>(cadeteVM);
            db.CadeteDb.SaveCadete(cadeteDb);
            return RedirectToAction("IndexCadete");
        }

        //***************************************MODIFICAR CADETE************************************

        [HttpGet]
        public IActionResult FormUpdateCadete(int id)
        {
            return View(db.CadeteDb.CadeteById(id));
        }

        public IActionResult ModificarCadete(Cadete cadeteUpdate)
        {
            db.CadeteDb.UpdateCadete(cadeteUpdate);
            return RedirectToAction("IndexCadete");
        }


        public IActionResult PagarACadete(Cadete cadete)
        {
            cadete.Pedidos = db.PedidoDb.GetPedidosImpagos(cadete.Id);
            return View(cadete);
        }





        ////***************************************ELIMINAR CADETE************************************

        //[HttpGet]
        //public IActionResult ConfirmarEliminarCadete(int id)
        //{
        //    return View(db.CadeteDb.CadeteById(id));
        //}


        //public IActionResult eliminarCadete(int id)
        //{
        //    if (_DB.eliminarCadete(id))
        //    {
        //        return RedirectToAction("Index");
        //    }
        //    else
        //    {
        //        logger.Error("linea 81 Model/DBTemporal no encuentra al cadete en los archivos, datos dañados, renombrados o eliminados");
        //        return RedirectToAction("Index", "Logger");
        //    }
        //}

        ////****************************************PAGAR A CADETE*****************************************






    }
}
