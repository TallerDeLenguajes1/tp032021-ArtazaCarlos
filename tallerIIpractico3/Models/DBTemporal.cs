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

        //###################################### CADETES ######################################

        //*********************************AGREGAR UN CADETES**********************************

        public void guardarCadete(string nom, string dir, string tel)
        {
            int id = leerArchivoCadete().Count() + 1;
            Cadete cadeteObj = new Cadete(id, nom, dir, tel);
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

        public void guardarPedido(Pedido pedido)
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

        public void ModificarArchivoPedido(List<Pedido> nuevaLista)
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
    }
}
