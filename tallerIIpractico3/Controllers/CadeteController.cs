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
    public class CadeteController : Controller
    {
        private readonly Db db;
        private readonly Logger logger = LogManager.GetCurrentClassLogger();


        public CadeteController(Db Db)
        {
            db = Db;
            
        }

        public IActionResult IndexCadete()
        {
            return View(db.CadeteDb.ReadCadetes());
        }

        public IActionResult CadeteList()
        {
            return View(db.CadeteDb.ReadCadetes());
        }

        public IActionResult CreateCadete()
        {
            string rol = HttpContext.Session.GetString("rol");
            if (rol == "admin")
            {
                return View(new Cadete());
            }
            return RedirectToAction("IndexLogger", "Logger");
        }

        //**************************************AGREGAR CADETE**************************************

        [HttpPost]
        public IActionResult SaveCadete(Cadete cadete)
        {
            db.CadeteDb.SaveCadete(cadete);
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

        ////***************************************ELIMINAR CADETE************************************
        
        [HttpGet]
        public IActionResult ConfirmarEliminarCadete(int id)
        {
            return View(db.CadeteDb.CadeteById(id));
        }

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

        //public IActionResult PagarACadete(int id)
        //{
        //    Cadete cadeteAPagar = _DB.cargarPagoAlCadete(id);
        //    if(cadeteAPagar != null)
        //    {
        //        return View(cadeteAPagar);
        //    }
        //    else
        //    {
        //        return RedirectToAction("Index", "Logger");
        //    } 
        //}

        //public IActionResult ConfirmarPago(int id)
        //{
        //    if (_DB.limpiarListaPedidoDelCadete(id))
        //    {
        //        return RedirectToAction("Index");
        //    }
        //    return RedirectToAction("Index", "Logger");
        //}


        //public IActionResult ImprimirPdf(int id)
        //{
        //    Cadete cadete = _DB.consultarUnCadete(id);
        //    return new ViewAsPdf("PagarACadete", cadete);
        //}

    }
}
