using SRDP.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRDP.Domain.Entities
{
    public class Deposito
    {
        public int DepositoID { get; set; }
        public string  Institucion { get; set; }
        public decimal Saldo { get; set; }
        public TipoCuenta TipoDeCuenta { get; set; }
    }
}
