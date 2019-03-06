using SRDP.Domain.OtrosIngresos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRDP.Application.Repositories
{
    public interface IOtroIngresoReadOnlyRepository
    {
        Task<OtroIngreso> Get(Guid otroIngresoID);
        Task<ICollection<OtroIngreso>> GetByDeclaracion(Guid declaracionID);
    }
}
