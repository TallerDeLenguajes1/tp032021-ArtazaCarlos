using System.Collections.Generic;
using tallerIIpractico3.entities;

namespace tallerIIpractico3.Models.Db
{
    public interface IRepositorioCliente
    {
        Cliente ClienteById(int id);
        bool DeleteCliente(int id);
        List<Cliente> ReadCliente();
        void SaveCliente(Cliente cliente);
        void UpdateCliente(Cliente clienteUpdate);
        int GetClienteId(string nom, string dir);
        Cliente ClienteByNomTel(string nom, string tel);
        List<Cliente> BusquedaFiltrada(string busqueda);
        Cliente ClienteByNom(string nom);

    }
}