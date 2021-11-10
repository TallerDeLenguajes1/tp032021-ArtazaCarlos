using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using tallerIIpractico3.entities;
using tallerIIpractico3.Models.Db;
using Rotativa.AspNetCore;
using NLog;


namespace tallerIIpractico3.Controllers
{
    public class CadeteController : Controller
    {
        private readonly DbSqlite db;

        public CadeteController(DbSqlite Db)
        {
            db = Db;
        }

        public IActionResult Index()
        {
            return View(db.CadeteDb.ReadCadetes());
        }

        public IActionResult CadeteList()
        {
            return View(db.CadeteDb.ReadCadetes());
        }

        public IActionResult CreateCadete()
        {            
            return View(new Cadete());
        }

        //**************************************AGREGAR CADETE**************************************
        public IActionResult SaveCadete(Cadete cadete)
        {
            db.CadeteDb.SaveCadete(cadete);
            return RedirectToAction("Index");
        }

        //***************************************MODIFICAR CADETE************************************
        public IActionResult FormUpdateCadete(int id)
        {
            return View(db.CadeteDb.CadeteById(id));
        }

        public IActionResult modificarCadete(Cadete cadeteUpdate)
        {

            return RedirectToAction("Index");
        }

        ////***************************************ELIMINAR CADETE************************************
        //public IActionResult ConfirmarEliminarCadete(int id)
        //{
        //    Cadete cadeteAEliminar = _DB.consultarUnCadete(id);
        //    return View(cadeteAEliminar);
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
