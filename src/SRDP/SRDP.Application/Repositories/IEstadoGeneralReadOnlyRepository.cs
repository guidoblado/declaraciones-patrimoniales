using SRDP.Domain.Enumerations;
using SRDP.Domain.Reportes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRDP.Application.Repositories
{
    public interface IEstadoGeneralReadOnlyRepository
    {
        Task<ICollection<EstadoGeneral>> GetFromGestion(int gestion, int estado);
    }
}
