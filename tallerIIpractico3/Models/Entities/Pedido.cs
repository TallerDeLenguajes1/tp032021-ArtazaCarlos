using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace tallerIIpractico3.entities
{   
    public enum Estado
    {
        En_camino,
        Entregado,
        No_entregado,
    }
    public class Pedido
    {
        private int id;
        private DateTime fecha;
        private string observaciones;
        private Cliente cliente;
        private string estado;


        public int Id { get => id; set => id = value; }
        public DateTime Fecha { get => fecha; set => fecha = value; }
        public string Observaciones { get => observaciones; set => observaciones = value; }
        public Cliente Cliente { get => cliente; set => cliente = value; }
        public string Estado { get => estado; set => estado = value; }

        public Pedido()
        {
        }

        public Pedido(int id, DateTime fecha, string observaciones, Cliente cliente, Estado estado)
        {
            Id = id;
            Fecha = fecha;
            Observaciones = observaciones;
            Cliente = cliente;
            Estado = estado.ToString();
        }
    }
}
