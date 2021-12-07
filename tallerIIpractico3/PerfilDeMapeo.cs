using AutoMapper;
using tallerIIpractico3.entities;
using tallerIIpractico3.Models;
using tallerIIpractico3.Models.Entities;
using tallerIIpractico3.ViewModel;


namespace sistemaDeCadeteria
{
    public class PerfilDeMapeo : Profile
    {
        public PerfilDeMapeo()
        {
            //mapeo de Usuario
            CreateMap<Usuario, UsuarioViewModel>().ReverseMap();

            //mapeo de Cadete
            CreateMap<Cadete, CadeteViewModel>().ReverseMap();

            //mapeo de Pedido
            CreateMap<Pedido, PedidoViewModel>().ReverseMap();

            //mapeo de Cliente
            CreateMap<Cliente, ClienteViewModel>().ReverseMap();
        }

    }
}
