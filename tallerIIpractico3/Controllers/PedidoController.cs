using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using tallerIIpractico3.entities;

namespace tallerIIpractico3.Controllers
{
    public class PedidoController : Controller
    {
        static int nro = 0;
        private readonly ILogger<PedidoController> _logger;
        private readonly List<Cadete> cadetes;
        private readonly List<Cadete> pedidos;

        public PedidoController(ILogger<PedidoController> logger, List<Cadete> cadetes, List<Cadete> pedidos)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public void addPedido(string obs, string est, int dni, string nom, string dir, string tel)
        {
            Pedido pedido_ = new Pedido(nro, obs, est, dni, nom, dir, tel);
            int cant_Cadetes = cadetes.Count(); //cantidad de cadetes en la lista

            Random r = new Random();
            int id_cadete = r.Next(0, cant_Cadetes + 1); //elijo al aleatoriamente un cadete

            Cadete resultado = cadetes.Find(x => x.Id == id_cadete);
            resultado.Pedidos.Add(pedido_);
            Response.Redirect("https://localhost:44374/");
        }

        public IActionResult ListaPedidos()
        {
            return View(pedidos);
        }
    }
}
