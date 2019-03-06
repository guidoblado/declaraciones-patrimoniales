using SRDP.Domain.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRDP.Domain.ValueObjects
{
    public class CondicionAlerta : ValueObject
    {
        public OperadorAlerta Operador { get; }
        public string OperadorSql { get; }

        private CondicionAlerta() { }

        public CondicionAlerta(string operador)
        {
            if (operador.Trim().ToUpper() == "Y")
            {
                Operador = OperadorAlerta.Y;
                OperadorSql = " AND ";
            }
            else if (operador.Trim().ToUpper() == "O")
            {
                Operador = OperadorAlerta.O;
                OperadorSql = " OR ";
            }
            else
            {
                throw new DomainException("Operador inválido '" + operador + "'");
            }
        }

        public override string ToString()
        {
            return Operador.ToString();
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Operador;
            yield return OperadorSql;
        }
    }
}
