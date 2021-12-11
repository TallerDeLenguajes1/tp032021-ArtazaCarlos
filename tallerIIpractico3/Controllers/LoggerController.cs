using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace tallerIIpractico3.Controllers
{
    public class LoggerController : Controller
    {
        private readonly Logger logger = LogManager.GetCurrentClassLogger();

        public IActionResult IndexLogger()
        {
            return View();
        }


        //*********************************ERRORES EN CADETES************************************
        public IActionResult ErrorCreateCadete()
        {
            return View();
        }

        public IActionResult ErrorUpdateCadete()
        {
            return View();
        }

        //*********************************ERRORES EN CLIENTES************************************

        public IActionResult ErrorCreateCliente()
        {
            return View();
        }

        public IActionResult ErrorCreateDuplicado()
        {
            return View();

        }

        public IActionResult ErrorUpdateCliente()
        {
            return View();
        }

        public IActionResult ErrorDeleteCliente()
        {
            return View();
        }

        //*********************************ERRORES EN PEDIDOS************************************

        public IActionResult ErrorCreatePedido()
        {
            return View();
        }

        public IActionResult ErrorUpdatePedido()
        {
            return View();
        }

        public IActionResult ErrorAlPagarCadete()
        {
            return View();
        }

        public IActionResult ErrorDeleteCadete()
        {
            return View();
        }

        //*********************************ERRORES EN USUARIOS************************************

        public IActionResult ErrorCreateUsuario()
        {
            return View();
        }

        public IActionResult ErrorDeleteUsuario()
        {
            return View();
        }

        public IActionResult ErrorUpdateUsuario()
        {
            return View();
        }
    }
}
