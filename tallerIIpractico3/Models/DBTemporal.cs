using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tallerIIpractico3.entities;

namespace tallerIIpractico3.Models
{
    public class DBTemporal
    {
        public Cadeteria Cadeteria { get; set; } 

        public DBTemporal()
        {
            Cadeteria = new Cadeteria();
        }
    }
}
