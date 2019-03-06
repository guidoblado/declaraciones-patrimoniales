using SRDP.Domain.Alertas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRDP.Application.Repositories
{
    public interface IReglaAlertaReadOnlyRepository
    {
        Task<ReglaAlerta> Get(Guid id);
        Task<ICollection<ReglaAlerta>> GetByGestion(int gestion);
    }
}
