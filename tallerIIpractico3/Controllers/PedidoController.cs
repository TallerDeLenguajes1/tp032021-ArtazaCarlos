using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using tallerIIpractico3.entities;
using tallerIIpractico3.Models;

namespace tallerIIpractico3.Controllers
{
    public class PedidoController : Controller
    {
        static int nro = 0;
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

        public IActionResult crearPedido(string obs, string est, int dni, string nom, string dir, string tel, int id)
        {
            Pedido pedido = new Pedido(nro, obs, est, dni, nom, dir, tel);

            List<Cadete> cadeteLista = _DB.leerArchivoCadete();

            Cadete cadeteSeleccionado = cadeteLista.Find(x => x.Id == id);
            cadeteSeleccionado.Pedidos.Add(pedido);

            _DB.ModificarArchivoCadete(cadeteLista);

            nro++;
            _DB.guardarPedido(pedido);
            return Redirect("Index");
        }

        public IActionResult ListaPedidos()
        {
            return View(_DB.leerArchivoPedido());
        }
    }
}
