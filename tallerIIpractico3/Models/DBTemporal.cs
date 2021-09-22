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
        const string path = @"c:\pruebas\dbTemporal.txt";

        private Cadeteria cadeteria;

        public Cadeteria Cadeteria { get => cadeteria; set => cadeteria = value; }

        public DBTemporal()
        {
            Cadeteria = new Cadeteria();
        }

        public void guardarEnArchivo(Cadete cadeteObj)
        {
            string miJson = JsonSerializer.Serialize(cadeteObj);

            if (!File.Exists(path))
            {
                StreamWriter archivo = File.CreateText(path);
                archivo.Close();
            }
            using (StreamWriter strWriter = File.AppendText(path))
            {
                strWriter.WriteLine(miJson);
            }
            
        }

        public List<Cadete> leerDeArchivo()
        {
            List<Cadete> listaCadetes = new List<Cadete>();
            string linea = "";
            if (File.Exists(path))
            {
                using (StreamReader strReader = File.OpenText(path))
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
    }
}
