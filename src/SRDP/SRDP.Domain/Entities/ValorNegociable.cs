using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRDP.Domain.Entities
{
    public class ValorNegociable
    {
        public int ValorNegociableID { get; set; }
        public string Decripcion { get; set; }
        public decimal ValorAproximado { get; set; }
    }
}
