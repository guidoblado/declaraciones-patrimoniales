using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRDP.Domain.ValueObjects
{
    public class TipoCuenta : ValueObject
    {
        public string Codigo { get; }
        public string Valor { get; }

        private TipoCuenta()
        { }


        public TipoCuenta(string codigo, string valor)
        {
            Codigo = codigo;
            Valor = valor;
        }
        public override string ToString()
        {
            return $"{Codigo}-{Valor}";
        }
        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Codigo;
            yield return Valor;
        }
    }
}
