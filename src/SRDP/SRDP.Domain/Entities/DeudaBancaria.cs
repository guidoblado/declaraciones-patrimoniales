using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRDP.Domain.Entities
{
    public class DeudaBancaria
    {
        public int DeudaBancariaID { get; set; }
        public string Institucion { get; set; }
        public decimal Monto { get; set; }
    }
}
