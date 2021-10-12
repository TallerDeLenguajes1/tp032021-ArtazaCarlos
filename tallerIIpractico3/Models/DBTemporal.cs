using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using tallerIIpractico3.entities;
using System.Text.Json;
using System.Text.Json.Serialization;
using NLog;

namespace tallerIIpractico3.Models
{
    public class DBTemporal
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private const string carpeta = @"TP3DB";
        private const string pathCadetesTMP = @"TP3DB\_DB.cadetes.tmp.txt";
        private const string pathPedidosTMP = @"TP3DB\_DB.Pedidos.tmp.txt";
        private const string pathCadetes = @"TP3DB\_DB.cadetes.txt";
        private const string pathPedidos = @"TP3DB\_DB.pedidos.txt";
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
            try
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
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
            }
        }
        //*********************************MODIFICAR CADETES**********************************
        public bool modificarCadete(int id, string nom, string dir, string tel)
        {
            try
            {
                List<Cadete> cadeteLista = leerArchivoCadete();
                Cadete cadeteAModificar = cadeteLista.Find(x => x.Id == id);
                cadeteAModificar.Nombre = nom;
                cadeteAModificar.Direccion = dir;
                cadeteAModificar.Telefono = tel;
                ModificarArchivoCadete(cadeteLista);
                return true;
            }
            catch (Exception ex)
            {
                logger.Fatal(ex.ToString());
                return false;
            }  
        }
        //*********************************ELIMINAR CADETES**********************************

        public bool eliminarCadete(int id)
        {
            List<Cadete> cadeteLista = leerArchivoCadete();
            Cadete cadeteABorrar = cadeteLista.Find(x => x.Id == id);
            if (cadeteABorrar != null)
            {
                cadeteLista.Remove(cadeteABorrar);
                ModificarArchivoCadete(cadeteLista);
                return true;
            }
            else
            {
                return false;
            } 
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

            try
            {
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
            }
            catch (Exception ex)
            {
                logger.Fatal(ex.ToString());
            }
            return cadete;
        }

        public bool limpiarListaPedidoDelCadete(int id)
        {
            return borrarPedidosFinalizados(id);
        }

        private bool borrarPedidosFinalizados(int idCadete)
        {
            List<Cadete> listaCadete = leerArchivoCadete();
            Cadete cadete = listaCadete.Find(x => x.Id == idCadete);
            List<Pedido> listaTemporalEntregados = new List<Pedido>();
            try
            {
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
                return true;
            }
            catch (Exception ex)
            {
                logger.Fatal(ex.ToString());
                return false;
            } 
        }
        //*********************************LEER ARCHIVO DE CADETES**********************************

        public List<Cadete> leerArchivoCadete()
        {
            List<Cadete> listaCadetes = new List<Cadete>();
            string linea = "";
            try
            {
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
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
                return listaCadetes;
            }
        }

        public Cadete consultarUnCadete(int id)
        {
            Cadete cadete = new Cadete();
            int b = 0;
            string linea = "";
            try
            {
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
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
                return cadete;
            }
        }

        //***************************FUNCIONES ADICIONALES PARA CADETES**********************

        private void ModificarArchivoCadete(List<Cadete> nuevaLista)
        {
            try
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
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
            } 
        }
        //###################################### PEDIDOS ######################################

        //*********************************AGREGAR PEDIDO **********************************
        public bool guardarPedido(int nro, string obs, Estado est, string nom, string dir, string tel, int idCadete)
        {
            try
            {
                List<Cadete> cadeteLista = leerArchivoCadete();
                Cadete cadeteSeleccionado = cadeteLista.Find(x => x.Id == idCadete);
                Pedido pedido = new Pedido(nro, obs, est, cadeteSeleccionado.Nombre, nom, dir, tel);
                cadeteSeleccionado.Pedidos.Add(pedido);
                ModificarArchivoCadete(cadeteLista);
                agregarPedidoAlArchivo(pedido);
                return true;
            }
            catch (Exception ex)
            {
                logger.Fatal(ex.ToString());
                return false;
            }
        }

        //*********************************LEER PEDIDOS **********************************
        public List<Pedido> leerArchivoPedido()
        {
            List<Pedido> listaPedidos = new List<Pedido>();
            string linea = "";
            try
            {
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
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
                return listaPedidos;
            }
        }
        //***************************FUNCIONES ADICIONALES PARA PEDIDOS**********************

        public List<Pedido> busquedaFiltrada(DateTime fechaInicial, DateTime fechaFinal)
        {
            List<Pedido> listaFiltrada = new List<Pedido>();
            try
            {
                List<Pedido> listaCompleta = leerArchivoPedido();
                foreach (Pedido item in listaCompleta)
                {
                    if ((item.FechaHora.Date >= fechaInicial.Date) && (item.FechaHora.Date <= fechaFinal.Date))
                    {
                        listaFiltrada.Add(item);
                    }
                }
                return listaFiltrada;
            }
            catch (Exception ex)
            {
                logger.Fatal(ex.ToString());
                return listaFiltrada;
            }  
        }

        private void ModificarArchivoPedido(List<Pedido> nuevaLista)
        {
            try
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
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
            }
        }

        private void agregarPedidoAlArchivo(Pedido pedido)
        {
            try
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
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
            } 
        }

        public bool modificarArchivoCadetePedido(int nroPedido, Estado estadoPedido)
        {
            List<Pedido> pedidoLista = leerArchivoPedido();
            List<Cadete> cadeteLista = leerArchivoCadete();
            try
            {
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
                return true;
            }
            catch (Exception ex)
            {
                logger.Fatal(ex.ToString());
                return false;
            }
            
        }
    }
}
