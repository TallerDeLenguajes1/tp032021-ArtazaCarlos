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


        public List<Cadete> ReadCadetes()
        {
            List<Cadete> ListaDeCadetes = new List<Cadete>();
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
                        Cadete cadeteTemp = new Cadete();
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



        public void SaveCadete(Cadete cadete)
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

        public void UpdateCadete(Cadete cadeteUpdate)
        {
            string queryString = @"UPDATE Cadetes
                                                SET 
                                                    nombre = '@nombre',
                                                    direccion = '@direccion',
                                                    telefono = '@telefono',
                                                    vehiculo = '@vehiculo'
                                                WHERE 
                                                    cadeteId = '@id';";

            using (var conexion = new SQLiteConnection(connectionString))
            {
                using (SQLiteCommand command = new SQLiteCommand(queryString, conexion))
                {
                    command.Parameters.AddWithValue("@nombre", cadeteUpdate.Nombre);
                    command.Parameters.AddWithValue("@direccion", cadeteUpdate.Direccion);
                    command.Parameters.AddWithValue("@telefono", cadeteUpdate.Telefono);
                    command.Parameters.AddWithValue("@vehiculo", cadeteUpdate.Vehiculo);
                    conexion.Open();
                    command.ExecuteNonQuery();
                }
                conexion.Close();
            }
        }

        public Cadete CadeteById(int id)
        {
            Cadete cadeteId = new Cadete();
            string queryString = @"SELECT * FROM Cadetes WHERE cadeteId = @id;";

            using (var conexion = new SQLiteConnection(connectionString))
            {
                using (SQLiteCommand command = new SQLiteCommand(queryString, conexion))
                {
                    command.Parameters.AddWithValue("@id", id);
                    conexion.Open();
                    command.ExecuteNonQuery();
                    //Cadete cadeteTemp = new Cadete();
                    SQLiteDataReader dataReader = command.ExecuteReader();
                    while (dataReader.Read())
                    {
                        cadeteId.Id = Convert.ToInt32(dataReader["cadeteId"]);
                        cadeteId.Nombre = dataReader["nombre"].ToString();
                        cadeteId.Direccion = dataReader["direccion"].ToString();
                        cadeteId.Telefono = dataReader["telefono"].ToString();
                        cadeteId.Vehiculo = dataReader["vehiculo"].ToString();
                    }
                    
                    }
                    conexion.Close();
                }
            return cadeteId;
        }

    }
}
