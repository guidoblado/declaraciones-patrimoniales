using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRDP.Domain.Depositos
{
    public sealed class MontoDepositoException : DomainException
    {
        internal MontoDepositoException(string message)
            : base(message)
        { }

    }
}
