using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRDP.Persitence.Entities
{
    internal class OtroIngreso
    {
        public Guid ID { get; set; }
        public Guid DeclaracionID { get; set; }
        public string Concepto { get; set; }
        public decimal IngresoMensual { get; set; }
    }
}
