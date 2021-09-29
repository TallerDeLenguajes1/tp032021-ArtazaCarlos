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
        static int id = 0;
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
 
        //****************************************MODIFICAR//ELIMINAR******************************************
        public IActionResult FormModificarCadete(int id)
        {
            Cadete cadeteAModificar = _DB.ConsultarCadete(id);
            return View(cadeteAModificar);
        }

        public IActionResult modificarCadete(int id, string nom, string dir, string tel)
        {
            List<Cadete> cadeteLista = _DB.leerArchivoCadete();

            Cadete cadeteAModificar = cadeteLista.Find(x => x.Id == id);
            cadeteAModificar.Nombre = nom;
            cadeteAModificar.Direccion = dir;
            cadeteAModificar.Telefono = tel;

            _DB.ModificarArchivoCadete(cadeteLista);
            return RedirectToAction("Index");
        }

        //*************************************************
        public IActionResult ConfirmarEliminarCadete(int id)
        {
            Cadete cadeteAModificar = _DB.ConsultarCadete(id);
            return View(cadeteAModificar);
        }

        public IActionResult EliminarCadete(int id)
        {
            List<Cadete> cadeteLista = _DB.leerArchivoCadete();
            Cadete cadeteABorrar = cadeteLista.Find(x => x.Id == id);
            cadeteLista.Remove(cadeteABorrar);

            _DB.ModificarArchivoCadete(cadeteLista);
            return RedirectToAction("Index");
        }

        //**************************************AGREGAR CADETE****************************************************
        public IActionResult agregarCadete(string nom, string dir, string tel)
        {
            Cadete cadete_ = new Cadete(id, nom, dir, tel);
            id++;
            _DB.guardarCadete(cadete_);
            return RedirectToAction("Index");
        }

        //****************************************PAGAR A CADETE**************************************************

        public IActionResult PagarACadete(int id)
        {
            Cadete cadeteAPagar = _DB.ConsultarCadete(id);
            //List<Pedido> pedidosDelCadete = cadeteAPagar.Pedidos;
            //foreach (var item in pedidosDelCadete)
            //{
            //    if ((item.Est == Estado.En_camino) || (item.Est == Estado.No_entregado))
            //    {
            //        pedidosDelCadete.Remove(item);
            //    }
            //    cadeteAPagar.Pedidos.Clear();
            //    cadeteAPagar.Pedidos = pedidosDelCadete;
            //}
            cadeteAPagar.PagoReciente1 = cadeteAPagar.Pedidos.Count() * 100;
            return View(cadeteAPagar);
        }

        public IActionResult ConfirmarPago(int id)
        {
            List<Cadete> cadeteLista = _DB.leerArchivoCadete();
            Cadete cadetePagado = cadeteLista.Find(x => x.Id == id);
            cadetePagado.Pedidos.Clear();

            _DB.ModificarArchivoCadete(cadeteLista);
            return RedirectToAction("Index");
        }


        public IActionResult ImprimirPdf(int id)
        {
            Cadete cadeteAPagar = _DB.ConsultarCadete(id);
            return new ViewAsPdf("PagarACadete", cadeteAPagar);
        }
    }
}
