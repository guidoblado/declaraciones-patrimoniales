using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRDP.Domain.ValoresNegociables
{
    public class MontoValorNegociableException : DomainException
    {
        internal MontoValorNegociableException(string message)
            : base(message)
        {
        }
    }
}
