using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using tallerIIpractico3.entities;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace tallerIIpractico3.Models
{
    public class DBTemporal
    {
        const string carpeta = @"c:\TP3DB";
        const string pathCadetesTMP = @"c:\TP3DB\_DB.cadetes.tmp.txt";
        const string pathPedidosTMP = @"c:\TP3DB\_DB.Pedidos.tmp.txt";
        const string pathCadetes = @"c:\TP3DB\_DB.cadetes.txt";
        const string pathPedidos = @"c:\TP3DB\_DB.pedidos.txt";
        public DBTemporal()
        {
        }

        //###################################### CADETES #######################################

        //*********************************AGREGAR UN CADETES**********************************

        public void guardarCadete(string nom, int dni, string dir, string tel)
        {
            int id = leerArchivoCadete().Count() + 1;
            Cadete cadeteObj = new Cadete(id, nom, dni, dir, tel);
            guardarCadeteEnArchivo(cadeteObj);
        }

        private void guardarCadeteEnArchivo(Cadete cadeteObj)
        {
            string miJson = JsonSerializer.Serialize(cadeteObj);

            if (!File.Exists(pathCadetes))
            {
                Directory.CreateDirectory(carpeta);
                StreamWriter archivo = File.CreateText(pathCadetes);
                archivo.Close();
                archivo.Dispose();
            }
            using (StreamWriter strWriter = File.AppendText(pathCadetes))
            {
                strWriter.WriteLine(miJson);
                strWriter.Close();
                strWriter.Dispose();
            }
        }

        //*********************************MODIFICAR CADETES**********************************
        public void modificarCadete(int id, string nom, string dir, string tel)
        {
            List<Cadete> cadeteLista = leerArchivoCadete();
            Cadete cadeteAModificar = cadeteLista.Find(x => x.Id == id);
            cadeteAModificar.Nombre = nom;
            cadeteAModificar.Direccion = dir;
            cadeteAModificar.Telefono = tel;

            ModificarArchivoCadete(cadeteLista);
        }

        //*********************************ELIMINAR CADETES**********************************

        public void eliminarCadete(int id)
        {
            List<Cadete> cadeteLista = leerArchivoCadete();
            Cadete cadeteABorrar = cadeteLista.Find(x => x.Id == id);
            cadeteLista.Remove(cadeteABorrar);

            ModificarArchivoCadete(cadeteLista);
        }

        //*********************************PAGAR A UN CADETE**********************************
        public Cadete cargarPagoAlCadete(int id)
        {
            return controlDePedidosEntregados(id);
        }

        private Cadete controlDePedidosEntregados(int id)
        {
            List<Cadete> listaCadete = leerArchivoCadete();
            Cadete cadete = listaCadete.Find(x => x.Id == id);
            List<Pedido> listaTemporalEntregados = new List<Pedido>();
            cadete.CantidadDeEntregados = 0;

            foreach (Pedido pedido in cadete.Pedidos)
            {
                if (pedido.Est != Estado.No_entregado)
                {
                    listaTemporalEntregados.Add(pedido);
                }
                if (pedido.Est == Estado.Entregado)
                {
                    cadete.CantidadDeEntregados++;
                }
            }
            cadete.Pedidos.Clear();
            cadete.Pedidos = listaTemporalEntregados;
            cadete.Pago = cadete.CantidadDeEntregados * 100;
            ModificarArchivoCadete(listaCadete);

            return cadete;
        }

        public void limpiarListaPedidoDelCadete(int id)
        {
            borrarPedidosFinalizados(id);
        }

        private void borrarPedidosFinalizados(int idCadete)
        {
            List<Cadete> listaCadete = leerArchivoCadete();
            Cadete cadete = listaCadete.Find(x => x.Id == idCadete);
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
            cadete.Pago = 0;
            ModificarArchivoCadete(listaCadete);
        }

        //*********************************LEER ARCHIVO DE CADETES**********************************

        public List<Cadete> leerArchivoCadete()
        {
            List<Cadete> listaCadetes = new List<Cadete>();
            string linea = "";
            if (File.Exists(pathCadetes))
            {
                using (StreamReader strReader = File.OpenText(pathCadetes))
                {
                    while ((linea = strReader.ReadLine()) != null)
                    {
                        Cadete cadeteObj = JsonSerializer.Deserialize<Cadete>(linea);
                        listaCadetes.Add(cadeteObj);
                    }
                    strReader.Close();
                    strReader.Dispose();
                }
                return listaCadetes;
            }

            return listaCadetes;
        }

        public Cadete consultarUnCadete(int id)
        {
            Cadete cadete = new Cadete();
            int b = 0;
            string linea = "";
            if (File.Exists(pathCadetes))
            {
                using (StreamReader strReader = File.OpenText(pathCadetes))
                {
                    while (((linea = strReader.ReadLine()) != null) || (b == 0))
                    {
                        Cadete cadeteObj = JsonSerializer.Deserialize<Cadete>(linea);
                        if (cadeteObj.Id == id)
                        {
                            b = 1;
                            cadete = cadeteObj;
                        }
                    }
                }
            }

            return cadete;
        }

        //***************************FUNCIONES ADICIONALES PARA CADETES**********************

        private void ModificarArchivoCadete(List<Cadete> nuevaLista)
        {
        StreamWriter archivo = File.CreateText(pathCadetesTMP);
        archivo.Close();
        foreach (Cadete item in nuevaLista)
        {
            string miJson = JsonSerializer.Serialize(item);
            using (StreamWriter strWriter = File.AppendText(pathCadetesTMP))
            {
                strWriter.WriteLine(miJson);
                strWriter.Close();
                strWriter.Dispose();
            }
        }
        File.Delete(pathCadetes);
        File.Move(pathCadetesTMP, pathCadetes);
        }


        //###################################### PEDIDOS ######################################

        //*********************************AGREGAR PEDIDO **********************************
        public void guardarPedido(int nro, string obs, Estado est, string nom, string dir, string tel, int idCadete)
        {
            List<Cadete> cadeteLista = leerArchivoCadete();
            Cadete cadeteSeleccionado = cadeteLista.Find(x => x.Id == idCadete);
            Pedido pedido = new Pedido(nro, obs, est, cadeteSeleccionado.Nombre, nom, dir, tel);
            cadeteSeleccionado.Pedidos.Add(pedido);
            ModificarArchivoCadete(cadeteLista);
            agregarPedidoAlArchivo(pedido);

        }

        //*********************************LEER PEDIDOS **********************************
        public List<Pedido> leerArchivoPedido()
        {
            List<Pedido> listaPedidos = new List<Pedido>();
            string linea = "";
            if (File.Exists(pathPedidos))
            {
                using (StreamReader strReader = File.OpenText(pathPedidos))
                {
                    while ((linea = strReader.ReadLine()) != null)
                    {
                        Pedido pedidoObj = JsonSerializer.Deserialize<Pedido>(linea);
                        listaPedidos.Add(pedidoObj);
                    }
                    strReader.Close();
                    strReader.Dispose();
                }
                return listaPedidos;
            }

            return listaPedidos;
        }


        //***************************FUNCIONES ADICIONALES PARA PEDIDOS**********************
        private void ModificarArchivoPedido(List<Pedido> nuevaLista)
        {
            StreamWriter archivo = File.CreateText(pathPedidosTMP);
            archivo.Close();
            foreach (Pedido item in nuevaLista)
            {
                string miJson = JsonSerializer.Serialize(item);
                using (StreamWriter strWriter = File.AppendText(pathPedidosTMP))
                {
                    strWriter.WriteLine(miJson);
                    strWriter.Close();
                    strWriter.Dispose();
                }
            }
            File.Delete(pathPedidos);
            File.Move(pathPedidosTMP, pathPedidos);

        }

        private void agregarPedidoAlArchivo(Pedido pedido)
        {
            string miJson = JsonSerializer.Serialize(pedido);

            if (!File.Exists(pathPedidos))
            {
                StreamWriter archivo = File.CreateText(pathPedidos);
                archivo.Close();
                archivo.Dispose();
            }
            using (StreamWriter strWriter = File.AppendText(pathPedidos))
            {
                strWriter.WriteLine(miJson);
                strWriter.Close();
                strWriter.Dispose();
            }
        }

        public void modificarArchivoCadetePedido(int nroPedido, Estado estadoPedido)
        {
            List<Pedido> pedidoLista = leerArchivoPedido();
            List<Cadete> cadeteLista = leerArchivoCadete();

            //modifico el estado del pedido en la lista de pedidos
            Pedido pedidoAModificar = pedidoLista.Find(x => x.Nro == nroPedido);
            pedidoAModificar.Est = estadoPedido;

            //modifico el estado del pedido en la lista de cadetes
            foreach (Cadete cadete in cadeteLista)
            {
                foreach (Pedido pedido in cadete.Pedidos)
                {
                    if (pedido.Nro == nroPedido)
                    {
                        pedido.Est = estadoPedido;
                    }
                }
            }

            ModificarArchivoPedido(pedidoLista);
            ModificarArchivoCadete(cadeteLista);
        }
    }
}
