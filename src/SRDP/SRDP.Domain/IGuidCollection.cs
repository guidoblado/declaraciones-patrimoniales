using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRDP.Domain
{
    internal interface IGuidCollection
    {
        IReadOnlyCollection<Guid> GetIds();
        void Add(Guid ID);
    }
}
