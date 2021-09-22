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
        const string pathCadetes = @"c:\pruebas\dbTemporal_cadetes.txt";
        const string pathPedidos = @"c:\pruebas\dbTemporal_pedidos.txt";

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
            }
            
            return listaCadetes;
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
            }

            return listaPedidos;
        }
    }
}
