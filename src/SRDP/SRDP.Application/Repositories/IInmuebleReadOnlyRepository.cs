using SRDP.Domain.Inmuebles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRDP.Application.Repositories
{
    public interface IInmuebleReadOnlyRepository
    {
        Task<Inmueble> Get(Guid inmuebleID);
        Task<ICollection<Inmueble>> GetByDeclaracion(Guid declaracionID);
    }
}
