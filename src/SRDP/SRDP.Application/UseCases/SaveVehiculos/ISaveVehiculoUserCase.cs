using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRDP.Application.UseCases.SaveVehiculos
{
    public interface ISaveVehiculoUserCase
    {
        Task<VehiculoOutput> Execute(Guid vehiculoID, Guid declaracionID, string marca, string tipoVehiculo, string anio, decimal valorAproximado, decimal saldoDeudor, string banco);
        Task DeleteVehiculo(Guid id);
    }
}
