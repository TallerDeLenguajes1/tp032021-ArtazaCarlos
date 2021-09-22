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
        private readonly DBTemporal dB;

        public PedidoController(ILogger<PedidoController> logger, DBTemporal DB )
        {
            _logger = logger;
            dB = DB;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult addPedido(string obs, string est, int dni, string nom, string dir, string tel)
        {
            //Pedido pedido_ = new Pedido(nro, obs, est, dni, nom, dir, tel);
            //int cant_Cadetes = dB.Cadeteria.Cadetes.Count(); //cantidad de cadetes en la lista

            //Random r = new Random();
            //int id_cadete = r.Next(0, cant_Cadetes + 1); //elijo al aleatoriamente un cadete

            //Cadete cadeteSeleccionado = dB.Cadeteria.Cadetes.Find(x => x.Id == id_cadete);
            //if(cadeteSeleccionado  != null) cadeteSeleccionado.Pedidos.Add(pedido_);
            return Redirect("Index");
        }

        public IActionResult ListaPedidos()
        {
            return View(dB.Cadeteria.Pedidos);
        }
    }
}
