using AutoMapper;
using tallerIIpractico3.Models;
using tallerIIpractico3.Models.Entities;
using tallerIIpractico3.ViewModel;


namespace sistemaDeCadeteria
{
    public class PerfilDeMapeo : Profile
    {
        public PerfilDeMapeo()
        {
            CreateMap<Usuario, UsuarioViewModel>().ReverseMap();
        }
        
    }
}
