using System.Collections.Generic;
using tallerIIpractico3.entities;

namespace tallerIIpractico3.Models.Db
{
    public interface IRepositorioCadete
    {
        Cadete CadeteById(int id);
        bool DeleteCadete(int id);
        List<Cadete> ReadCadetes();
        void SaveCadete(Cadete cadete);
        void UpdateCadete(Cadete cadeteUpdate);
        
    }
}