using SRDP.Domain.Vehiculos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRDP.Application.Repositories
{
    public interface IVehiculoWriteOnlyRepository
    {
        Task Add(Vehiculo vehiculo);
        Task Update(Vehiculo vehiculo);
        Task Delete(Guid id);
    }
}
