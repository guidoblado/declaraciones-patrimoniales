using SRDP.Domain.Declaraciones;
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
        Task<Declaracion> Get(int gestion, int funcionarioID);
        Task<ICollection<Declaracion>> GetByGestion(int gestion);

    }
}
