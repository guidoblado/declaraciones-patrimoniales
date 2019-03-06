using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRDP.Persitence.Entities
{
    public class ReglaAlerta
    {
        public Guid ID { get; set; }
        public int Gestion { get; set; }
        public string Descripcion { get; set; }
        public decimal Porcentaje { get; set; }
        public string Operador { get; set; }
        public decimal Monto { get; set; }
    }
}
