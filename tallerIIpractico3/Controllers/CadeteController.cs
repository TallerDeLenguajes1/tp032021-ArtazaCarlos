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
        public IActionResult agregarCadete(string nom, string dir, string tel)
        {
            _DB.guardarCadete(nom, dir, tel);
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

        

        //****************************************PAGAR A CADETE**************************************************

        public IActionResult PagarACadete(int id)
        {
            List<Cadete> listaCadete = _DB.leerArchivoCadete();
            Cadete cadete = listaCadete.Find(x => x.Id == id);
            controlDePedidosEntregados(id);
            cadete.Pago = cadete.CantidadDeEntregados * 100;
            // _DB.ModificarArchivoCadete(listaCadete);
            return View(cadete);
        }

        public IActionResult ConfirmarPago(int id)
        {
            List<Cadete> cadeteLista = _DB.leerArchivoCadete();
            Cadete cadetePagado = cadeteLista.Find(x => x.Id == id);
            borrarPedidosFinalizados(cadetePagado);

            // _DB.ModificarArchivoCadete(cadeteLista);
            return RedirectToAction("Index");
        }


        public IActionResult ImprimirPdf(int id)
        {
            Cadete cadete = _DB.consultarUnCadete(id);
            return new ViewAsPdf("PagarACadete", cadete);
        }

        public void controlDePedidosEntregados(int id)
        {
            List<Cadete> listaCadete = _DB.leerArchivoCadete();
            Cadete cadete = listaCadete.Find(x => x.Id == id);
            List<Pedido> listaTemporalEntregados = new List<Pedido>();

            foreach (var item in cadete.Pedidos)
            {
                if (item.Est != Estado.No_entregado)
                {
                    listaTemporalEntregados.Add(item);
                }
                if (item.Est == Estado.Entregado)
                {
                    cadete.CantidadDeEntregados++;
                }
            }
            cadete.Pedidos.Clear();
            cadete.Pedidos = listaTemporalEntregados;
            //_DB.ModificarArchivoCadete(listaCadete);
        }

        public void borrarPedidosFinalizados(Cadete cadete)
        {
            List<Cadete> listaCadete = _DB.leerArchivoCadete();
            List<Pedido> listaTemporalEntregados = new List<Pedido>();

            foreach (var item in cadete.Pedidos)
            {
                if (item.Est == Estado.En_camino)
                {
                    listaTemporalEntregados.Add(item);
                }
            }
            cadete.Pedidos.Clear();
            cadete.Pedidos = listaTemporalEntregados;
            cadete.CantidadDeEntregados = 0;
            // _DB.ModificarArchivoCadete(listaCadete);
        }
    }
}
