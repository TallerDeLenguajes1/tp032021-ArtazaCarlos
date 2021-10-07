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
    public class PedidoController : Controller
    {
        private readonly ILogger<PedidoController> _logger;
        private readonly DBTemporal _DB;

        public PedidoController(ILogger<PedidoController> logger, DBTemporal DB )
        {
            _logger = logger;
            _DB = DB;
        }

        public IActionResult Index()
        {
            return View(_DB.leerArchivoCadete());
        }

        public IActionResult crearPedido(string obs, Estado est, int dni, string nom, string dir, string tel, int id)
        {
            int nro = _DB.leerArchivoPedido().Count() + 1;
            Pedido pedido = new Pedido(nro, obs, est, dni, nom, dir, tel);

            List<Cadete> cadeteLista = _DB.leerArchivoCadete();

            Cadete cadeteSeleccionado = cadeteLista.Find(x => x.Id == id);
            cadeteSeleccionado.Pedidos.Add(pedido);

            //_DB.ModificarArchivoCadete(cadeteLista);
            _DB.guardarPedido(pedido);
            nro = _DB.leerArchivoPedido().Count() + 1;
            return RedirectToAction("Index"); ;
        }

        public IActionResult ModificarPedido(int nro, Estado est)
        {
            List<Pedido> pedidoLista = _DB.leerArchivoPedido();
            List<Cadete> cadeteLista = _DB.leerArchivoCadete();

            Pedido pedidoAModificar = pedidoLista.Find(x => x.Nro == nro);
            pedidoAModificar.Est = est;

            foreach (Cadete cadete in cadeteLista)
            {
                foreach (Pedido pedido in cadete.Pedidos)
                {
                    if (pedido.Nro == nro)
                    {
                        pedido.Est = est;
                    }
                    if (pedido.Est == Estado.Entregado)
                    {
                        cadete.CantidadDeEntregados++;
                    }
                }
            }

            _DB.ModificarArchivoPedido(pedidoLista);
            //_DB.ModificarArchivoCadete(cadeteLista);

            return RedirectToAction("ListaPedidos");
        }

        public IActionResult ListaPedidos()
        {
            return View(_DB.leerArchivoPedido());
        }

    }
}
