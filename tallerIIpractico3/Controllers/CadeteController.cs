using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using tallerIIpractico3.entities;
using tallerIIpractico3.Models;
using Rotativa.AspNetCore;



namespace tallerIIpractico3.Controllers
{
    public class CadeteController : Controller
    {
        private readonly ILogger<CadeteController> _logger;
        private readonly DBTemporal _DB;

        public CadeteController(ILogger<CadeteController> logger, DBTemporal DB)
        {
            _logger = logger;
            _DB = DB;
        }

        public IActionResult Index()
        {
            return View(_DB.leerArchivoCadete());
        }

        public IActionResult CreateCadete()
        {            
            return View();
        }

        //**************************************AGREGAR CADETE**************************************
        public IActionResult agregarCadete(string nom, int dni, string dir, string tel)
        {
            _DB.guardarCadete(nom, dni, dir, tel);
            return RedirectToAction("Index");
        }

        //***************************************MODIFICAR CADETE************************************
        public IActionResult FormModificarCadete(int id)
        {
            Cadete cadeteAModificar = _DB.consultarUnCadete(id);
            return View(cadeteAModificar);
        }

        public IActionResult modificarCadete(int id, string nom, string dir, string tel)
        {
            _DB.modificarCadete(id, nom, dir, tel);
            return RedirectToAction("Index");
        }

        //***************************************ELIMINAR CADETE************************************
        public IActionResult ConfirmarEliminarCadete(int id)
        {
            Cadete cadeteAEliminar = _DB.consultarUnCadete(id);
            return View(cadeteAEliminar);
        }

        public IActionResult eliminarCadete(int id)
        {
            _DB.eliminarCadete(id);
            return RedirectToAction("Index");
        }

        

        //****************************************PAGAR A CADETE*******************************************

        public IActionResult PagarACadete(int id)
        {
            return View(_DB.cargarPagoAlCadete(id));
        }

        public IActionResult ConfirmarPago(int id)
        {
            _DB.limpiarListaPedidoDelCadete(id);
            return RedirectToAction("Index");
        }


        public IActionResult ImprimirPdf(int id)
        {
            Cadete cadete = _DB.consultarUnCadete(id);
            return new ViewAsPdf("PagarACadete", cadete);
        }

    }
}
