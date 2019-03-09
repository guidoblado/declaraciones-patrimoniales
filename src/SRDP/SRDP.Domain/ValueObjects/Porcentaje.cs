using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRDP.Domain.ValueObjects
{
    public class Porcentaje : ValueObject
    {
        public decimal Valor { get; private set; }

        private Porcentaje() { }

        public static Porcentaje For(decimal valor)
        {
            var porcentaje = new Porcentaje();


            try
            {
                if (valor < 0 || valor > 100)
                    throw new DomainException("Valor de porcentaje " + valor.ToString() + " inválido");
                if (valor > 1 && valor <= 100)
                    valor = valor / 100;
                porcentaje.Valor = valor;
            }
            catch (Exception ex)
            {
                throw new DomainException(ex.Message);
            }

            return porcentaje;
        }
        
        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Valor;
        }
    }
}
