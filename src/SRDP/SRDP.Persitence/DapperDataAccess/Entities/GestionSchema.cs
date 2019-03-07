using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRDP.Persitence.Entities
{
    public class GestionSchema
    {
        public int Gestion { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFinal { get; set; }
        public bool Vigente { get; set; }
    }
}
