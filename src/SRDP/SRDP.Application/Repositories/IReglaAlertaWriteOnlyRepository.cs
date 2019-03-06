using SRDP.Domain.Alertas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRDP.Application.Repositories
{
    public interface IReglaAlertaWriteOnlyRepository
    {
        Task Add(ReglaAlerta reglaAlerta);
        Task Update(ReglaAlerta reglaAlerta);
        Task Delete(Guid id);
    }
}
