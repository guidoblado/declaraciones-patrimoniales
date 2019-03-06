using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRDP.Domain.Entities
{
    public class Vehiculo
    {
        public int VehiculoID { get; set; }
        public string Marca { get; set; }
        public decimal ValorAproximado { get; set; }
        public decimal Gravamen { get; set; }
    }
}
