using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace pruebas
{
    class Program
    {
        static void Main(string[] args)
        {

            //List<string> discosLocales = Directory.GetLogicalDrives().ToList();

            //foreach (var discos in discosLocales)
            //{
            //    Console.WriteLine(discos);
            //}

            //Console.ReadKey();

            //string disco = @"c:\";

            //List<string> carpetas = Directory.GetDirectories(disco).ToList();

            //foreach (var carpeta in carpetas)
            //{
            //    Console.WriteLine(carpeta);
            //}








            //string miJson = "{'Cantidad':10,'Marca':'Quilmes'}";

            //Cerveza vieja = JsonSerializer.Deserialize<Cerveza>(miJson);

            
            string miJson = "{\"Cantidad\":900,\"Marca\":\"otra\"}";


            string path = @"c:\pruebas\texto.txt";
            
            if (!File.Exists(path))
            {
                File.CreateText(path);
            }

            using (StreamWriter strWriter = File.AppendText(path))
            {
                strWriter.WriteLine(miJson);
            }

            //*******************************************************************

            List<Cerveza> listaCerveza = new List<Cerveza>();
            using (StreamReader strReader = File.OpenText(path))
            {
                string linea = "";
                while ((linea = strReader.ReadLine()) != null)
                {
                    Cerveza cervezaItem = JsonSerializer.Deserialize<Cerveza>(linea);
                    listaCerveza.Add(cervezaItem);
                }
            }
            

          



            //using (StreamReader sr = File.OpenText(path))
            //{
            //    string s = "";
            //    while ((s = sr.ReadLine()) != null)
            //    {
            //        Console.WriteLine(s);
            //    }
            //}
        }
    }
}
