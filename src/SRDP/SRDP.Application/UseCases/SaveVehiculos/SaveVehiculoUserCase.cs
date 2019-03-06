using SRDP.Application.Repositories;
using SRDP.Domain.Vehiculos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRDP.Application.UseCases.SaveVehiculos
{
    public class SaveVehiculoUserCase : ISaveVehiculoUserCase
    {
        private IVehiculoWriteOnlyRepository _vehiculoWriteOnlyRepository;

        public SaveVehiculoUserCase(IVehiculoWriteOnlyRepository vehiculoWriteOnlyRepository)
        {
            _vehiculoWriteOnlyRepository = vehiculoWriteOnlyRepository;
        }

        public async Task DeleteVehiculo(Guid id)
        {
            await _vehiculoWriteOnlyRepository.Delete(id);
        }

        public async Task<VehiculoOutput> Execute(Guid vehiculoID, Guid declaracionID, string marca, string tipoVehiculo, string anio, decimal valorAproximado, decimal saldoDeudor, string banco)
        {
            if (vehiculoID == null || vehiculoID == Guid.Empty)
                await _vehiculoWriteOnlyRepository.Add(new Vehiculo(declaracionID, marca,   tipoVehiculo, anio, valorAproximado, saldoDeudor, banco));
            else
                await _vehiculoWriteOnlyRepository.Update(Vehiculo.Load(vehiculoID, declaracionID, marca, tipoVehiculo, anio, valorAproximado, saldoDeudor, banco));
            return new VehiculoOutput(vehiculoID, declaracionID, marca, tipoVehiculo, anio, valorAproximado, saldoDeudor, banco);
        }
    }
}
