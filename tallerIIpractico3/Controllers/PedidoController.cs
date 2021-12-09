using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using tallerIIpractico3.entities;
using tallerIIpractico3.Models.Db;
using NLog;
using tallerIIpractico3.ViewModel;
using AutoMapper;

namespace tallerIIpractico3.Controllers
{
    public class PedidoController : Controller
    {
        private readonly Db db;
        private readonly IMapper mapper;
        private readonly Logger logger = LogManager.GetCurrentClassLogger();


        public PedidoController(Db Db, IMapper mapper)
        {
            db = Db;
            this.mapper = mapper;
        }

        public IActionResult IndexPedido()
        {
            var pedidoVM = mapper.Map<List<PedidoViewModel>>(db.PedidoDb.ReadPedidos());
            return View(pedidoVM);
        }


//************************CREAR PEDIDO DESDE MENU******************************
        public IActionResult CreateView()
        {
            CreatePedidoViewModel modelosParaPedido = new CreatePedidoViewModel();
            List<Cadete> cadetesDb = db.CadeteDb.ReadCadetes();
            var cadetesVM = mapper.Map<List<CadeteViewModel>>(cadetesDb);
            modelosParaPedido.Cadetes = cadetesVM;

            return View(modelosParaPedido);
        }

        [HttpPost]
        public IActionResult CreatePedido(CreatePedidoViewModel modelosParaPedido)
        {
            if (ModelState.IsValid)
            {
                var clienteDb = mapper.Map<Cliente>(modelosParaPedido.Cliente);
                db.ClienteDb.SaveCliente(clienteDb);
                Cliente clienteWithId = db.ClienteDb.ClienteByNomTel(clienteDb.Nombre, clienteDb.Telefono);

                Pedido pedidoDb = new(modelosParaPedido.PedidoObs);
                pedidoDb.Cliente = clienteWithId;
                db.PedidoDb.SavePedido(pedidoDb, modelosParaPedido.CadeteId);

                return RedirectToAction("IndexPedido");
            }
            return RedirectToAction("ErrorCreatePedido", "Logger");
        }

//************************CREAR PEDIDO DESDE CLIENTES CARGADOS******************************
        [HttpGet]
        public IActionResult CreateViewFromCliente(ClienteViewModel clienteVM)
        {
            CreatePedidoViewModel modelosParaPedido = new CreatePedidoViewModel();

            List<Cadete> cadetesDb = db.CadeteDb.ReadCadetes();
            var cadetesVM = mapper.Map<List<CadeteViewModel>>(cadetesDb);

            modelosParaPedido.Cadetes = cadetesVM;
            modelosParaPedido.Cliente = clienteVM;

            return View(modelosParaPedido);
        }

        [HttpPost]
        public IActionResult CreatePedidoFromCliente(CreatePedidoViewModel modelosParaPedido)
        {
            if (ModelState.IsValid)
            {
                Pedido pedidoDb = new(modelosParaPedido.PedidoObs);
                Cliente clienteDb = mapper.Map<Cliente>(modelosParaPedido.Cliente);
                pedidoDb.Cliente = clienteDb;
                db.PedidoDb.SavePedido(pedidoDb, modelosParaPedido.CadeteId);
                return RedirectToAction("IndexPedido");
            }
            return RedirectToAction("ErrorCreatePedido", "Logger");
        }

        //************************UPDATE PEDIDO******************************

        [HttpPost]
        public IActionResult UpdatePedido(int pedidoId, entities.Estado estadoPedido)
        {
            if (db.PedidoDb.UpdatePedido(pedidoId, estadoPedido.ToString()))
            {
                return RedirectToAction("IndexPedido");
            }
            return RedirectToAction("ErrorUpdatePedido", "Logger");
        }


        //************************PAGAR PEDIDO********************************************

        [HttpGet]
        public IActionResult ConfirmarPago(int cadeteId)
        {
            if (db.PedidoDb.LiquidarPedido(cadeteId))
            {
                return RedirectToAction("IndexCadete", "Cadete");
            }
            return RedirectToAction("ErrorAlPagarCadete", "Logger");

        }



        //***************************************METODOS FROM DETELE VIEW************************************

        [HttpPost]
        public IActionResult UpdatePedidoFromDeleteView(int cadeteId, int pedidoId, entities.Estado estadoPedido)
        {
            if (db.PedidoDb.UpdatePedido(pedidoId, estadoPedido.ToString()))
            {
                CadeteViewModel cadeteVM = mapper.Map<CadeteViewModel>(db.CadeteDb.CadeteById(cadeteId));
                return RedirectToAction("DeleteViewPendientes", "Cadete", cadeteVM);
            }
            return RedirectToAction("ErrorUpdatePedido", "Logger");
        }


        [HttpGet]
        public IActionResult ConfirmarPagoFromDelete(int cadeteId)
        {
            if (db.PedidoDb.LiquidarPedido(cadeteId))
            {
                CadeteViewModel cadeteVM = mapper.Map<CadeteViewModel>(db.CadeteDb.CadeteById(cadeteId));
                return RedirectToAction("DeleteView", "Cadete", cadeteVM);
            }
            return RedirectToAction("ErrorAlPagarCadete", "Logger");

        }








        //public IActionResult ListaFiltrada(DateTime fechaInicial, DateTime fechaFinal)
        //{
        //    FechaInicial = fechaInicial;
        //    FechaFinal = fechaFinal;
        //    return View(_DB.busquedaFiltrada(fechaInicial, fechaFinal));
        //}

        ////posee la misma funcion que ListaFiltrada solo que sin argumentos, tomando los datos
        ////static FechaInicial y FechaFinal de la clase Pedido
        //public IActionResult ListaFiltrada2()
        //{
        //    return View(_DB.busquedaFiltrada(FechaInicial, FechaFinal));
        //}
    }
}
