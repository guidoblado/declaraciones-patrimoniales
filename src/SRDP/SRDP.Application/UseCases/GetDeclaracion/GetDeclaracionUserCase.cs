using SRDP.Application.Repositories;
using SRDP.Application.SearchParameters;
using SRDP.Domain.Depositos;
using SRDP.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRDP.Application.UseCases.GetDeclaracion
{
    public class GetDeclaracionUserCase : IGetDeclaracionUserCase
    {
        private readonly IDeclaracionReadOnlyRepository _declaracionReadOnlyRepository;
        private readonly IGestionReadOnlyRepository _gestionReadOnlyRepository;
        private readonly IFuncionarioReadOnlyRepository _funcionarioReadOnlyRepository;

        public GetDeclaracionUserCase(IDeclaracionReadOnlyRepository declaracionReadOnlyRepository, IGestionReadOnlyRepository gestionReadOnlyRepository,
            IFuncionarioReadOnlyRepository funcionarioReadOnlyRepository)
        {
            _declaracionReadOnlyRepository = declaracionReadOnlyRepository;
            _gestionReadOnlyRepository = gestionReadOnlyRepository;
            _funcionarioReadOnlyRepository = funcionarioReadOnlyRepository;
        }

        public async Task<DeclaracionOutput> Execute(Guid declaracionID)
        {
            var declaracion = await _declaracionReadOnlyRepository.Get(declaracionID);
            var funcionario = await _funcionarioReadOnlyRepository.GetByCodigo(declaracion.FuncionarioID);

            var depositos = new List<DepositoOutput>();
            foreach (var item in declaracion.Depositos.Items)
            {
                depositos.Add(new DepositoOutput(item.ID, item.DeclaracionID, item.InstitucionFinanciera, item.TipoDeCuenta, item.Saldo));
            }

            var deudasBancarias = new List<DeudaBancariaOutput>();
            foreach (var item in declaracion.DeudasBancarias.Items)
            {
                deudasBancarias.Add(new DeudaBancariaOutput(item.ID, item.DeclaracionID, item.InstitucionFinanciera, item.Monto, item.Tipo));
            }

            var inmuebles = new List<InmuebleOutput>();
            foreach (var item in declaracion.Inmuebles.Items)
            {
                inmuebles.Add(new InmuebleOutput(item.ID, item.DeclaracionID, item.Direccion, item.TipoDeInmueble, item.PorcentajeParticipacion.Valor, item.ValorComercial, item.SaldoHipoteca, item.Banco));
            }

            var otrosIngresos = new List<OtroIngresoOutput>();
            foreach (var item in declaracion.OtrosIngresos.Items)
            {
                otrosIngresos.Add(new OtroIngresoOutput(item.ID, item.DeclaracionID, item.Concepto, item.IngresoMensual));
            }

            var valoresNegociables = new List<ValorNegociableOutput>();
            foreach (var item in declaracion.ValoresNegociables.Items)
            {
                valoresNegociables.Add(new ValorNegociableOutput(item.ID, item.DeclaracionID, item.Descripcion, item.TipoValor, item.ValorAproximado));
            }

            var vehiculos = new List<VehiculoOutput>();
            foreach (var item in declaracion.Vehiculos.Items)
            {
                vehiculos.Add(new VehiculoOutput(item.ID, item.DeclaracionID, item.Marca, item.TipoVehiculo, item.Anio, item.ValorAproximado, item.SaldoDeudor, item.Banco));
            }
            var output = new DeclaracionOutput(declaracion, funcionario, depositos, deudasBancarias, inmuebles, otrosIngresos,
                valoresNegociables, vehiculos, declaracion.PatrimonioNeto);

            return output;
        }

        public async Task<DeclaracionOutput> Execute(int anioGestion, int funcionarioID)
        {
            var gestion = await _gestionReadOnlyRepository.Get(anioGestion);

            if (gestion == null)
                throw new ApplicationException("La gestión '" + anioGestion.ToString() + "' no existe.");

            var declaracion = await _declaracionReadOnlyRepository.Get(Gestion.For(gestion.Anio, gestion.FechaInicio, gestion.FechaFinal, gestion.Vigente), funcionarioID);
            var funcionario = await _funcionarioReadOnlyRepository.GetByCodigo(declaracion.FuncionarioID);
            var depositos = new List<DepositoOutput>();
            foreach (var item in declaracion.Depositos.Items)
            {
                depositos.Add(new DepositoOutput(item.ID, item.DeclaracionID, item.InstitucionFinanciera, item.TipoDeCuenta, item.Saldo));
            }

            var deudasBancarias = new List<DeudaBancariaOutput>();
            foreach (var item in declaracion.DeudasBancarias.Items)
            {
                deudasBancarias.Add(new DeudaBancariaOutput(item.ID, item.DeclaracionID, item.InstitucionFinanciera, item.Monto, item.Tipo));
            }

            var inmuebles = new List<InmuebleOutput>();
            foreach (var item in declaracion.Inmuebles.Items)
            {
                inmuebles.Add(new InmuebleOutput(item.ID, item.DeclaracionID, item.Direccion, item.TipoDeInmueble, item.PorcentajeParticipacion.Valor, item.ValorComercial, item.SaldoHipoteca, item.Banco));
            }

            var otrosIngresos = new List<OtroIngresoOutput>();
            foreach (var item in declaracion.OtrosIngresos.Items)
            {
                otrosIngresos.Add(new OtroIngresoOutput(item.ID, item.DeclaracionID, item.Concepto, item.IngresoMensual));
            }

            var valoresNegociables = new List<ValorNegociableOutput>();
            foreach (var item in declaracion.ValoresNegociables.Items)
            {
                valoresNegociables.Add(new ValorNegociableOutput(item.ID, item.DeclaracionID, item.Descripcion, item.TipoValor, item.ValorAproximado));
            }

            var vehiculos = new List<VehiculoOutput>();
            foreach (var item in declaracion.Vehiculos.Items)
            {
                vehiculos.Add(new VehiculoOutput(item.ID, item.DeclaracionID, item.Marca, item.TipoVehiculo, item.Anio, item.ValorAproximado, item.SaldoDeudor, item.Banco));
            }
            var output = new DeclaracionOutput(declaracion, funcionario, depositos, deudasBancarias, inmuebles, otrosIngresos,
                valoresNegociables, vehiculos, declaracion.PatrimonioNeto);

            return output;
        }

        public async Task<ICollection<DeclaracionOutput>> ExecuteList(int anioGestion)
        {
            var gestion = await _gestionReadOnlyRepository.Get(anioGestion);

            if (gestion == null)
                throw new ApplicationException("La gestión '" + anioGestion.ToString() + "' no existe.");

            var declaraciones = await _declaracionReadOnlyRepository.GetByGestion(Gestion.For(gestion.Anio, gestion.FechaInicio, gestion.FechaFinal, gestion.Vigente));
            var output = new List<DeclaracionOutput>();

            foreach (var declaracion in declaraciones)
            {
                var funcionario = await _funcionarioReadOnlyRepository.GetByCodigo(declaracion.FuncionarioID);

                if (funcionario == null)
                    continue;

                output.Add(new DeclaracionOutput(declaracion, funcionario, new List<DepositoOutput>(), new List<DeudaBancariaOutput>(),
                    new List<InmuebleOutput>(), new List<OtroIngresoOutput>(), new List<ValorNegociableOutput>(), new List<VehiculoOutput>(), 
                    declaracion.PatrimonioNeto));

            }
            
            return output;
        }
    }
}
