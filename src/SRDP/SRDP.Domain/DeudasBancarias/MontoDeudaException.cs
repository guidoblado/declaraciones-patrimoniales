using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRDP.Domain.DeudasBancarias
{
    public sealed class MontoDeudaException : DomainException
    {
        internal MontoDeudaException(string message)
            : base(message)
        {

        }
    }
}
