using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Threading.Tasks;
using tallerIIpractico3.entities;

namespace tallerIIpractico3.Models.Db
{
    public class RepositorioCadete
    {
        private readonly string connectionString;

        public RepositorioCadete(string ConnectionString)
        {
            connectionString = ConnectionString;
        }


        public List<Cadete> CadeteList()
        {
            List<Cadete> ListaDeCadetes = new List<Cadete>();
            Cadete cadeteTemp = new Cadete();
            string queryString = @"SELECT
                                        cadeteId,
                                        nombre,
                                        direccion,
                                        telefono,
                                        vehiculo
                                FROM Cadetes;";

            using (var conexion = new SQLiteConnection(connectionString))
            {
                using (SQLiteCommand command = new SQLiteCommand(queryString, conexion))
                {
                    conexion.Open();
                    command.ExecuteNonQuery();
                    SQLiteDataReader CadeteFilas = command.ExecuteReader();
                    while (CadeteFilas.Read())
                    {
                        cadeteTemp.Id = Convert.ToInt32(CadeteFilas["cadeteId"]);
                        cadeteTemp.Nombre = CadeteFilas["nombre"].ToString();
                        cadeteTemp.Direccion = CadeteFilas["direccion"].ToString();
                        cadeteTemp.Telefono = CadeteFilas["telefono"].ToString();
                        cadeteTemp.Vehiculo = CadeteFilas["vehiculo"].ToString();
                        ListaDeCadetes.Add(cadeteTemp);
                    }
                    conexion.Close();
                }
            }

            return ListaDeCadetes;
        }



        public void addCadete(Cadete cadete)
        {
            string queryString = @"INSERT INTO Cadetes (
                                                            nombre,
                                                            direccion,
                                                            telefono,
                                                            vehiculo
                                                        )
                                                        VALUES(
                                                            @nombre,
                                                            @direccion,
                                                            @telefono,
                                                            @vehiculo
                                                        )";

            using (var conexion = new SQLiteConnection(connectionString))
            {
                using (SQLiteCommand command = new SQLiteCommand(queryString, conexion))
                {
                    command.Parameters.AddWithValue("@nombre", cadete.Nombre);
                    command.Parameters.AddWithValue("@direccion", cadete.Direccion);
                    command.Parameters.AddWithValue("@telefono", cadete.Telefono);
                    command.Parameters.AddWithValue("@vehiculo", cadete.Vehiculo);
                    conexion.Open();
                    command.ExecuteNonQuery();
                    conexion.Close();
                }
            }

        }

    }
}
