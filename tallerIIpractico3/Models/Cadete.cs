using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace tallerIIpractico3.entities
{
    public class Cadete
    {
        private int id;
        private string nombre;
        private string direccion;
        private string telefono;
        private List<Pedido> pedidos;

        public int Id { get => id; set => id = value; }
        public string Nombre { get => nombre; set => nombre = value; }
        public string Direccion { get => direccion; set => direccion = value; }
        public string Telefono { get => telefono; set => telefono = value; }
        public List<Pedido> Pedidos { get => pedidos; set => pedidos = value; }

        public Cadete(int dni, string nom, string dir, string tel)
        {
            Id = dni;
            Nombre = nom;
            Direccion = dir;
            Telefono = tel;
            Pedidos = new List<Pedido>();
        }

        public Cadete()
        {
            Pedidos = new List<Pedido>();
        }

        public void addOrder(Pedido ped)
        {
            Pedidos.Add(ped);
        }
        public void removeOrder(Pedido ped)
        {
            Pedidos.Remove(ped);
        }
    }
}
