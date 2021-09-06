using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ejercicioClase01_09
{
    class Cadete
    {
        private int id;
        private string nombre;
        private string direccion;
        private int telefono;
        private List<Pedido> pedidos;

        public int Id { get => id; set => id = value; }
        public string Nombre { get => nombre; set => nombre = value; }
        public string Direccion { get => direccion; set => direccion = value; }
        public int Telefono { get => telefono; set => telefono = value; }
        

        public Cadete(int dni, string nom, string dir, int tel)
        {
            Id = dni;
            Nombre = nom;
            Direccion = dir;
            Telefono = tel;

        }

        public Cadete()
        {
        }

        public void addOrder(Pedido ped)
        {
            pedidos.Add(ped);
        }
        public void removeOrder(Pedido ped)
        {
            pedidos.Remove(ped);
        }
    }
}
