using SRDP.Domain.Declaraciones;
using SRDP.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRDP.Application.Repositories
{
    public interface IDeclaracionReadOnlyRepository
    {
        Task<Declaracion> Get(Guid declaracionID);
        Task<Declaracion> Get(Gestion gestion, int funcionarioID);
        Task<ICollection<Declaracion>> GetByGestion(Gestion gestion);

    }
}
