using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRDP.Domain.ValueObjects
{
    public class Cedula : ValueObject
    {
        public string Numero { get; }
        public string Extension { get; }

        private Cedula()
        { }

        public Cedula(string valor)
        {
            //TODO: Parse with the apropiate rules for CI
            Numero = valor.Substring(0, valor.Trim().Length - 2);
            Extension = valor.Substring(valor.Trim().Length - 2);
        }

        public override string ToString()
        {
            return$"{Numero} {Extension}"; 
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Numero;
            yield return Extension;
        }
    }
}
