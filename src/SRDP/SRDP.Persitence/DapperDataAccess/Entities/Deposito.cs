using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRDP.Persitence.Entities
{
    internal class Deposito
    {
        public Guid ID { get; private set; }
        public Guid DeclaracionID { get; private set; }
        public string InstitucionFinanciera { get; private set; }
        public string TipoCuenta { get; private set; }
        public decimal Saldo { get; private set; }
    }
}
