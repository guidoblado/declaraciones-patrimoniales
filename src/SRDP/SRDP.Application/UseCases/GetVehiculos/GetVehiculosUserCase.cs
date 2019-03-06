using SRDP.Application.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRDP.Application.UseCases.GetVehiculos
{
    public class GetVehiculosUserCase : IGetVehiculosUserCase
    {
        private IVehiculoReadOnlyRepository _vehiculoReadOnlyRepository;

        public GetVehiculosUserCase(IVehiculoReadOnlyRepository vehiculoReadOnlyRepository)
        {
            _vehiculoReadOnlyRepository = vehiculoReadOnlyRepository;
        }

        public async Task<VehiculoOutput> Execute(Guid vehiculoID)
        {
            var vehiculo = await _vehiculoReadOnlyRepository.Get(vehiculoID);
            if (vehiculo == null) return null;

            return new VehiculoOutput(vehiculo.ID, vehiculo.DeclaracionID, vehiculo.Marca, vehiculo.TipoVehiculo, vehiculo.Anio,
                vehiculo.ValorAproximado, vehiculo.SaldoDeudor, vehiculo.Banco);
        }

        public async Task<ICollection<VehiculoOutput>> ExecuteList(Guid declaracionID)
        {
            var vehiculos = await _vehiculoReadOnlyRepository.GetByDeclaracion(declaracionID);

            var outputList = new List<VehiculoOutput>();

            if (vehiculos == null) return outputList;

            foreach (var vehiculo in vehiculos)
            {
                outputList.Add(new VehiculoOutput(vehiculo.ID, vehiculo.DeclaracionID, vehiculo.Marca, vehiculo.TipoVehiculo, vehiculo.Anio,
                vehiculo.ValorAproximado, vehiculo.SaldoDeudor, vehiculo.Banco));
            }
            return outputList;
        }
    }
}
