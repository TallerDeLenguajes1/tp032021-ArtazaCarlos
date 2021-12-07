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

        //private static DateTime fechaInicial;
        //private static DateTime fechaFinal;

        //public static DateTime FechaInicial { get => fechaInicial; set => fechaInicial = value; }
        //public static DateTime FechaFinal { get => fechaFinal; set => fechaFinal = value; }

        public PedidoController(Db Db, IMapper mapper)
        {
            db = Db;
            this.mapper = mapper;
        }

        public IActionResult IndexPedido()
        {
            return View(db.PedidoDb.ReadPedidos());
        }


//************************CREAR PEDIDO DESDE MENU******************************
        public IActionResult CreateView()
        {
            CreatePedidoViewModel modelosParaPedido = new CreatePedidoViewModel();
            List<Cadete> cadetes = db.CadeteDb.ReadCadetes();
            var cadetesModel = mapper.Map<List<CadeteViewModel>>(cadetes);
            modelosParaPedido.Cadetes = cadetesModel;

            return View(modelosParaPedido);
        }

     
        public IActionResult CreatePedido(CreatePedidoViewModel modelosParaPedido)
        {
            var clienteDb = mapper.Map<Cliente>(modelosParaPedido.Cliente);
            db.ClienteDb.SaveCliente(clienteDb);
            Cliente clienteWithId = db.ClienteDb.ClienteByNomTel(clienteDb.Nombre, clienteDb.Telefono);

            Pedido pedido = new(modelosParaPedido.PedidoObs);
            pedido.Cliente = clienteWithId;
            db.PedidoDb.SavePedido(pedido, modelosParaPedido.CadeteId);

            return RedirectToAction("IndexPedido");
        }

//************************CREAR PEDIDO DESDE CLIENTES CARGADOS******************************
        public IActionResult CreateViewFromCliente(Cliente cliente)
        {
            CreatePedidoViewModel modelosParaPedido = new CreatePedidoViewModel();

            List<Cadete> cadetes = db.CadeteDb.ReadCadetes();
            var cadetesModel = mapper.Map<List<CadeteViewModel>>(cadetes);
            var clienteVM = mapper.Map<ClienteViewModel>(cliente);

            modelosParaPedido.Cadetes = cadetesModel;
            modelosParaPedido.Cliente = clienteVM;

            return View(modelosParaPedido);
        }

        public IActionResult CreatePedidoFromCliente(CreatePedidoViewModel modelosParaPedido)
        {
            Pedido pedido = new(modelosParaPedido.PedidoObs);
            Cliente clienteDb = mapper.Map<Cliente>(modelosParaPedido.Cliente);
            pedido.Cliente = clienteDb;
            db.PedidoDb.SavePedido(pedido, modelosParaPedido.CadeteId);

            return RedirectToAction("IndexPedido");
        }


        public IActionResult UpdatePedido(int pedidoId, entities.Estado estadoPedido)
        {
            db.PedidoDb.UpdatePedido(pedidoId, estadoPedido.ToString());
            return RedirectToAction("IndexPedido");
        }


        //public IActionResult CreatePedido(string obs, string nom, string dir, string tel, int cadeteId)
        //{
        //    Cliente cliente = new(nom, dir, tel);
        //    db.ClienteDb.SaveCliente(cliente);
        //    Cliente clienteWithId = db.ClienteDb.ClienteByNomTel(nom, tel);
        //    Pedido pedido = new(obs, clienteWithId);
        //    db.PedidoDb.SavePedido(pedido, cadeteId);

        //    return RedirectToAction("IndexPedido");
        //}



        ////modifica el pedido desde un listado completo de pedidos
        //public IActionResult ModificarPedidoListaCompleta(int nroPedido, Estado estadoPedido)
        //{
        //    if (_DB.modificarArchivoCadetePedido(nroPedido, estadoPedido))
        //    {
        //        return RedirectToAction("ListaCompleta");
        //    }
        //    else
        //    {
        //        return RedirectToAction("Index", "Logger");
        //    } 
        //}
        ////modifica el pedido desde un listado filtrado de pedidos
        //public IActionResult ModificarPedidoListaFiltrada(int nroPedido, Estado estadoPedido)
        //{
        //    if (_DB.modificarArchivoCadetePedido(nroPedido, estadoPedido))
        //    {
        //        return RedirectToAction("ListaFiltrada2");
        //    }
        //    else
        //    {
        //        return RedirectToAction("Index", "Logger");
        //    }
        //}

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
