using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using tallerIIpractico3.entities;
using tallerIIpractico3.Models;
using Rotativa.AspNetCore;
using NLog;


namespace tallerIIpractico3.Controllers
{
    public class CadeteController : Controller
    {

        public CadeteController()
        {
            
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult CadeteList()
        {
            return View();
        }

        public IActionResult CreateCadete()
        {            
            return View(new Cadete());
        }

        //**************************************AGREGAR CADETE**************************************
        public IActionResult SaveCadete(Cadete cadete)
        {
            //_DB.guardarCadete(nom, dni, dir, tel);
            return RedirectToAction("Index");
        }

        //***************************************MODIFICAR CADETE************************************
        //public IActionResult FormModificarCadete(int id)
        //{
        //    Cadete cadeteAModificar = _DB.consultarUnCadete(id);
        //    return View(cadeteAModificar);
        //}

        //public IActionResult modificarCadete(int id, string nom, string dir, string tel)
        //{
        //    if (_DB.modificarCadete(id, nom, dir, tel))
        //    {
        //        return RedirectToAction("Index");
        //    }
        //    else
        //    {
        //        return RedirectToAction("Index", "Logger");
        //    }
        //}

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
