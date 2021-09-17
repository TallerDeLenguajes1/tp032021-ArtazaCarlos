﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace tallerIIpractico3.entities
{
    public class Pedido
    {
        private int nro;
        private string obs;
        private Cliente cliente;
        private string estado;

        public int Nro { get => nro; set => nro = value; }
        public string Obs { get => obs; set => obs = value; }
        public Cliente Cliente { get => cliente; set => cliente = value; }
        public string Estado { get => estado; set => estado = value; }

        public Pedido(int num, string obs_, string est, int dni, string nom, string dir, string tel)
        {
            Cliente = new Cliente(dni, nom, dir, tel);
            Nro = num;
            Obs = obs_;
            Estado = est;

        }

        public Pedido()
        {
        }
    }
}