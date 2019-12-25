using SRDP.Application.Repositories;
using SRDP.Domain.Declaraciones;
using SRDP.Domain.Enumerations;
using SRDP.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRDP.Application.UseCases.CloneDeclaracion
{
    public class CloneDeclaracionUserCase : ICloneDeclaracionUserCase
    {
        private readonly IDeclaracionReadOnlyRepository _declaracionReadOnlyRepository;
        private readonly IDeclaracionWriteOnlyRepository _declaracionWriteOnlyRepository;
        private readonly IGestionReadOnlyRepository _gestionReadOnlyRepository;
        private readonly IFuncionarioReadOnlyRepository _funcionarioReadOnlyRepository;
        
        private readonly IDepositoWriteOnlyRepository _depositoWriteOnlyRepository;
        private readonly IInmuebleWriteOnlyRepository _inmuebleWriteOnlyRepository;
        private readonly IVehiculoWriteOnlyRepository _vehiculoWriteOnlyRepository;
        private readonly IOtroIngresoWriteOnlyRepository _otroIngresoWriteOnlyRepository;
        private readonly IValorNegociableWriteOnlyRepository _valorNegociableWriteOnlyRepository;
        private readonly IDeudaBancariaWriteOnlyRepository _deudaBancariaWriteOnlyRepository;

        public CloneDeclaracionUserCase(IDeclaracionReadOnlyRepository declaracionReadOnlyRepository, IDeclaracionWriteOnlyRepository declaracionWriteOnlyRepository, IGestionReadOnlyRepository gestionReadOnlyRepository,
            IFuncionarioReadOnlyRepository funcionarioReadOnlyRepository, IDepositoWriteOnlyRepository depositoWriteOnlyRepository, IInmuebleWriteOnlyRepository inmuebleWriteOnlyRepository, IVehiculoWriteOnlyRepository vehiculoWriteOnlyRepository,
            IOtroIngresoWriteOnlyRepository otroIngresoWriteOnlyRepository, IValorNegociableWriteOnlyRepository valorNegociableWriteOnlyRepository, IDeudaBancariaWriteOnlyRepository deudaBancariaWriteOnlyRepository)
        {
            _declaracionReadOnlyRepository = declaracionReadOnlyRepository;
            _declaracionWriteOnlyRepository = declaracionWriteOnlyRepository;
            _gestionReadOnlyRepository = gestionReadOnlyRepository;
            _funcionarioReadOnlyRepository = funcionarioReadOnlyRepository;
            _depositoWriteOnlyRepository = depositoWriteOnlyRepository;
            _inmuebleWriteOnlyRepository = inmuebleWriteOnlyRepository;
            _vehiculoWriteOnlyRepository = vehiculoWriteOnlyRepository;
            _otroIngresoWriteOnlyRepository = otroIngresoWriteOnlyRepository;
            _valorNegociableWriteOnlyRepository = valorNegociableWriteOnlyRepository;
            _deudaBancariaWriteOnlyRepository = deudaBancariaWriteOnlyRepository;

        }

        public async Task Execute(Guid declaracionID)
        {
            var declaracion = await _declaracionReadOnlyRepository.Get(declaracionID, true);
            
            var gestiones = await _gestionReadOnlyRepository.GetAll();
            var gestionVigente = gestiones.Where(c => c.Vigente).FirstOrDefault();
            declaracion.CambiarEstado(EstadoDeclaracion.Anulada);
            await _declaracionWriteOnlyRepository.UpdateEstado(declaracionID, EstadoDeclaracion.Anulada);

            var declaracionAnterior = declaracion.DeclaracionAnterior != null ? 
                declaracion.DeclaracionAnterior : 
                Declaracion.Load(Guid.NewGuid(), declaracion.FuncionarioID, Gestion.For(gestionVigente.Anio, gestionVigente.FechaInicio, gestionVigente.FechaFinal, gestionVigente.Vigente), 
                DateTime.Now, EstadoDeclaracion.Pendiente, 
                new DepositoCollection(), new DeudaBancariaCollection(), new InmuebleCollection(), new OtroIngresoCollection(), 
                new ValorNegociableCollection(), new VehiculoCollection(), null);
            var declaracionNueva = Declaracion.Load(Guid.NewGuid(), declaracionAnterior.FuncionarioID, Gestion.For(gestionVigente.Anio, gestionVigente.FechaInicio, gestionVigente.FechaFinal, gestionVigente.Vigente),
                DateTime.Now, EstadoDeclaracion.Pendiente, declaracionAnterior.Depositos, declaracionAnterior.DeudasBancarias, declaracionAnterior.Inmuebles,
                declaracionAnterior.OtrosIngresos, declaracionAnterior.ValoresNegociables, declaracionAnterior.Vehiculos, declaracionAnterior);

            await _declaracionWriteOnlyRepository.Add(declaracionNueva);

            foreach (var item in declaracionAnterior.Depositos.GetItems())
            {
                await _depositoWriteOnlyRepository.Add(new Domain.Depositos.DepositoMayor10K(declaracionNueva.ID, item.InstitucionFinanciera, item.TipoDeCuenta, item.Saldo));
            }
            foreach (var item in declaracionAnterior.Inmuebles.GetItems())
            {
                await _inmuebleWriteOnlyRepository.Add(new Domain.Inmuebles.Inmueble(declaracionNueva.ID, item.Direccion, item.TipoDeInmueble, item.PorcentajeParticipacion,
                    item.ValorComercial, item.SaldoHipoteca, item.Banco));
            }
            foreach (var item in declaracionAnterior.Vehiculos.GetItems())
            {
                await _vehiculoWriteOnlyRepository.Add(new Domain.Vehiculos.Vehiculo(declaracionNueva.ID, item.Marca, item.TipoVehiculo, item.Anio, item.ValorAproximado, item.SaldoDeudor, item.Banco));
            }
            foreach (var item in declaracionAnterior.OtrosIngresos.GetItems())
            {
                await _otroIngresoWriteOnlyRepository.Add(new Domain.OtrosIngresos.OtroIngreso(declaracionNueva.ID, item.Concepto, item.IngresoMensual));
            }
            foreach (var item in declaracionAnterior.ValoresNegociables.GetItems())
            {
                await _valorNegociableWriteOnlyRepository.Add(new Domain.ValoresNegociables.ValorNegociableMayor10K(declaracionNueva.ID, item.Descripcion, item.TipoValor, item.ValorAproximado));
            }
            foreach (var item in declaracionAnterior.DeudasBancarias.GetItems())
            {
                await _deudaBancariaWriteOnlyRepository.Add(new Domain.DeudasBancarias.DeudaBancariaMayor10K(declaracionNueva.ID, item.InstitucionFinanciera, item.Monto, item.Tipo));
            }
        }
    }
}
