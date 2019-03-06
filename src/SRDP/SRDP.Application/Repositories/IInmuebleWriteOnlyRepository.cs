using SRDP.Domain.Inmuebles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRDP.Application.Repositories
{
    public interface IInmuebleWriteOnlyRepository
    {
        Task Add(Inmueble inmueble);
        Task Update(Inmueble inmueble);
        Task Delete(Guid id);
    }
}
