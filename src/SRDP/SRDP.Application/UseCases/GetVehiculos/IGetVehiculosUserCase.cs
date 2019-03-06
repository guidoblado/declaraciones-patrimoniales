using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRDP.Application.UseCases.GetVehiculos
{
    public interface IGetVehiculosUserCase
    {
        Task<VehiculoOutput> Execute(Guid vehiculoID);
        Task<ICollection<VehiculoOutput>> ExecuteList(Guid declaracionID);
    }
}
