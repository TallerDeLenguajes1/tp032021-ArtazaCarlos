using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsApp
{
    public partial class pedido : Form
    {
        private static int index = 0;
        private List<ObjPedido> ordenes = new List<ObjPedido>();

        public pedido()
        {
            InitializeComponent();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            ObjPedido objPedido = new ObjPedido(index ,txtObs.Text, txtNom.Text, txtDir.Text, txtTel.Text);
            ordenes.Add(objPedido);
            index++;
        }
    }

    class ObjPedido
    {
        int id;
        private string fechaHora;
        private string obs;
        private Cliente cliente;
        

        public int Id { get => id; set => id = value; }
        public string FechaHora { get => fechaHora; set => fechaHora = value; }
        public string Obs { get => obs; set => obs = value; }
        internal Cliente Cliente { get => cliente; set => cliente = value; }


        public ObjPedido()
        {
        }
        public ObjPedido(int id, string obs_, string nom, string dir, string tel)
        {
            Id = id;
            Cliente = new Cliente(nom, dir, tel);
            string FechaHora = DateTime.Now.ToString();
            Obs = obs_;
        }
    }

    class Cliente
    {
        private string nombre;
        private string direcccion;
        private string telefono;

        public string Nombre { get => nombre; set => nombre = value; }
        public string Direcccion { get => direcccion; set => direcccion = value; }
        public string Telefono { get => telefono; set => telefono = value; }

        public Cliente(string nom, string dir, string tel)
        {
            Nombre = nom;
            Direcccion = dir;
            Telefono = tel;
        }

        public Cliente()
        {
        }
    }
}
