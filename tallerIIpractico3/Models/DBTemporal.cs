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

        private Cadeteria cadeteria;

        public Cadeteria Cadeteria { get => cadeteria; set => cadeteria = value; }

        public DBTemporal()
        {
            Cadeteria = new Cadeteria();
        }

        //*********************************CADETES*********************************************************************

        public void guardarCadete(Cadete cadeteObj)
        {
            string miJson = JsonSerializer.Serialize(cadeteObj);

            if (!File.Exists(pathCadetes))
            {
                Directory.CreateDirectory(carpeta);
                StreamWriter archivo = File.CreateText(pathCadetes);
                archivo.Close();
            }
            using (StreamWriter strWriter = File.AppendText(pathCadetes))
            {
                strWriter.WriteLine(miJson);
            }
            
        }


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
                }
                return listaCadetes;
            }
            
            return listaCadetes;
        }

        public void ModificarArchivoCadete(List<Cadete> nuevaLista)
        {
            StreamWriter archivo = File.CreateText(pathCadetesTMP);
            archivo.Close();
            foreach (Cadete item in nuevaLista)
            {
                string miJson = JsonSerializer.Serialize(item);
                using (StreamWriter strWriter = File.AppendText(pathCadetesTMP))
                {
                    strWriter.WriteLine(miJson);
                }
            }
            File.Delete(pathCadetes);
            File.Move(pathCadetesTMP, pathCadetes);

        }

        public Cadete ConsultarCadete(int id)
        {
            string linea = "";
            if (File.Exists(pathCadetes))
            {
                using (StreamReader strReader = File.OpenText(pathCadetes))
                {
                    while ((linea = strReader.ReadLine()) != null)
                    {
                        if (linea.Contains("\"Id\":" + id))
                        {
                            Cadete cadeteObj = JsonSerializer.Deserialize<Cadete>(linea);
                            return cadeteObj;
                        }
                    }
                }
            }
            
            return null;
        }

        //*********************************PEDIDOS*********************************************************************

        public void guardarPedido(Pedido pedido)
        {
            string miJson = JsonSerializer.Serialize(pedido);

            if (!File.Exists(pathPedidos))
            {
                StreamWriter archivo = File.CreateText(pathPedidos);
                archivo.Close();
            }
            using (StreamWriter strWriter = File.AppendText(pathPedidos))
            {
                strWriter.WriteLine(miJson);
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
                }
            }
            File.Delete(pathPedidos);
            File.Move(pathPedidosTMP, pathPedidos);

        }
    }
}
