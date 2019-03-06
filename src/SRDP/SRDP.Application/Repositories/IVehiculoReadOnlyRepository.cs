using SRDP.Domain.Vehiculos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRDP.Application.Repositories
{
    public interface IVehiculoReadOnlyRepository
    {
        Task<Vehiculo> Get(Guid vehiculoID);
        Task<ICollection<Vehiculo>> GetByDeclaracion(Guid declaracionID);
    }
}
