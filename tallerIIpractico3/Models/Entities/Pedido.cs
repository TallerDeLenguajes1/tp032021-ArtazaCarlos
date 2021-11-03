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
        private int nro;
        private DateTime fechaHora;
        private string obs;
        private Cliente cliente;
        private Estado est;
        private string nomCadete;
        public int Nro { get => nro; set => nro = value; }
        public DateTime FechaHora { get => fechaHora; set => fechaHora = value; }
        public string Obs { get => obs; set => obs = value; }
        public Cliente Cliente { get => cliente; set => cliente = value; }
        public Estado Est { get => est; set => est = value; }
        public string NomCadete { get => nomCadete; set => nomCadete = value; }

        public Pedido(int num, string obs_, Estado est, string nomCadete, string nom, string dir, string tel)
        {
            Cliente = new Cliente(nom, dir, tel);
            Nro = num;
            FechaHora = DateTime.Now;
            Obs = obs_;
            Est = est;
            NomCadete = nomCadete;
        }

        public Pedido()
        {
        }
    }
}
