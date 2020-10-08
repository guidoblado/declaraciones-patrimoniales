using SRDP.Application.Repositories;
using SRDP.Domain.Declaraciones;
using SRDP.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRDP.Application.UseCases.GetAlertaIndividual
{
    public class GetAlertaIndividualUserCase : IGetAlertaIndividualUserCase
    {
        private IAlertaIndividualReadOnlyRepository _alertaIndividualReadOnlyRepository;
        private IDeclaracionReadOnlyRepository _declaracionReadOnlyRepository;
        private IGestionReadOnlyRepository _gestionReadOnlyRepository;

        public GetAlertaIndividualUserCase(IAlertaIndividualReadOnlyRepository alertaIndividualReadOnlyRepository, IDeclaracionReadOnlyRepository declaracionReadOnlyRepository, IGestionReadOnlyRepository gestionReadOnlyRepository)
        {
            _alertaIndividualReadOnlyRepository = alertaIndividualReadOnlyRepository;
            _declaracionReadOnlyRepository = declaracionReadOnlyRepository;
            _gestionReadOnlyRepository = gestionReadOnlyRepository;
        }

        public async Task<ICollection<AlertaIndividualOutput>> ExecuteList(int gestion, decimal monto, string operador, decimal porcentaje)
        {
            var declaracionesAlerta = await _alertaIndividualReadOnlyRepository.GetFromGestion(gestion);
            var gestionOutput = await _gestionReadOnlyRepository.Get(gestion);
            var result = new List<AlertaIndividualOutput>();
            foreach (var item in declaracionesAlerta)
            {
                var declaracionActual = await _declaracionReadOnlyRepository.Get(item.DeclaracionID);
                Declaracion declaracionAnterior = null;
                try
                {
                    if (item.DeclaracionAnteriorID == null)
                        declaracionAnterior = Declaracion.Load(Guid.NewGuid(), item.FuncionarioID, Gestion.For(gestionOutput.Anio, gestionOutput.FechaInicio, gestionOutput.FechaFinal, gestionOutput.Vigente),
                            DateTime.Now, Domain.Enumerations.EstadoDeclaracion.Completado, new DepositoCollection(), new DeudaBancariaCollection(), new InmuebleCollection(),
                            new OtroIngresoCollection(), new ValorNegociableCollection(), new VehiculoCollection(), null);
                    else
                        declaracionAnterior = await _declaracionReadOnlyRepository.Get(item.DeclaracionAnteriorID);

                    var declaracion = Declaracion.Load(declaracionActual.ID, declaracionActual.FuncionarioID, declaracionActual.Gestion,
                        declaracionActual.FechaLlenado, declaracionActual.Estado, declaracionActual.Depositos, declaracionActual.DeudasBancarias, declaracionActual.Inmuebles,
                        declaracionActual.OtrosIngresos, declaracionActual.ValoresNegociables, declaracionActual.Vehiculos, declaracionAnterior);

                    if (declaracionAnterior.Depositos.ValorNeto > 0)
                        item.SetMontosDepositos(declaracionActual.Depositos.ValorNeto, declaracionAnterior.Depositos.ValorNeto,
                            declaracionActual.Depositos.ValorNeto - declaracionAnterior.Depositos.ValorNeto,
                            (declaracionActual.Depositos.ValorNeto - declaracionAnterior.Depositos.ValorNeto) / declaracionAnterior.Depositos.ValorNeto * 100);

                    if (declaracionAnterior.DeudasBancarias.ValorNeto > 0)
                        item.SetMontosDeudaBancaria(declaracionActual.DeudasBancarias.ValorNeto, declaracionAnterior.DeudasBancarias.ValorNeto,
                            declaracionActual.DeudasBancarias.ValorNeto - declaracionAnterior.DeudasBancarias.ValorNeto,
                            (declaracionActual.DeudasBancarias.ValorNeto - declaracionAnterior.DeudasBancarias.ValorNeto) / declaracionAnterior.DeudasBancarias.ValorNeto * 100);

                    if (declaracionAnterior.Inmuebles.ValorNeto > 0)
                        item.SetMontosInmuebles(declaracionActual.Inmuebles.ValorNeto, declaracionAnterior.Inmuebles.ValorNeto,
                        declaracionActual.Inmuebles.ValorNeto - declaracionAnterior.Inmuebles.ValorNeto,
                        (declaracionActual.Inmuebles.ValorNeto - declaracionAnterior.Inmuebles.ValorNeto) / declaracionAnterior.Inmuebles.ValorNeto * 100);

                    if (declaracionAnterior.OtrosIngresos.ValorNeto > 0)
                        item.SetMontosOtrosIngresos(declaracionActual.OtrosIngresos.ValorNeto, declaracionAnterior.OtrosIngresos.ValorNeto,
                            declaracionActual.OtrosIngresos.ValorNeto - declaracionAnterior.OtrosIngresos.ValorNeto,
                            (declaracionActual.OtrosIngresos.ValorNeto - declaracionAnterior.OtrosIngresos.ValorNeto) / declaracionAnterior.OtrosIngresos.ValorNeto * 100);

                    if (declaracionAnterior.ValoresNegociables.ValorNeto > 0)
                        item.SetMontosValoresNegociables(declaracionActual.ValoresNegociables.ValorNeto, declaracionAnterior.ValoresNegociables.ValorNeto,
                            declaracionActual.ValoresNegociables.ValorNeto - declaracionAnterior.ValoresNegociables.ValorNeto,
                            (declaracionActual.ValoresNegociables.ValorNeto - declaracionAnterior.ValoresNegociables.ValorNeto) / declaracionAnterior.ValoresNegociables.ValorNeto * 100);

                    if (declaracionAnterior.Vehiculos.ValorNeto > 0)
                        item.SetMontosVehiculos(declaracionActual.Vehiculos.ValorNeto, declaracionAnterior.Vehiculos.ValorNeto,
                            declaracionActual.Vehiculos.ValorNeto - declaracionAnterior.Vehiculos.ValorNeto,
                            (declaracionActual.Vehiculos.ValorNeto - declaracionAnterior.Vehiculos.ValorNeto) / declaracionAnterior.Vehiculos.ValorNeto * 100);

                    item.SetMontosPatrimonioTotal(declaracion.PatrimonioNeto, declaracion.PatrimonioNetoGestionAnterior,
                        declaracion.DiferenciaPatrimonio, declaracion.VariacionPorcentual);

                    result.Add(item);
                }
                catch (Exception ex)
                {
                    var exception = new InvalidOperationException(String.Format("Error en Declaraion '{0}' FuncionarioID '{1}'", item.DeclaracionID, declaracionActual.FuncionarioID), ex);
                    throw exception;
                }
            }

            if (monto > 0 && porcentaje > 0 && !String.IsNullOrEmpty(operador))
            {
                var condicionAlerta = new CondicionAlerta(operador);
                switch (condicionAlerta.Operador)
                {
                    case Domain.Enumerations.OperadorAlerta.Y:
                        result = result.Where(c => c.DiferenciaPatrimonio > monto && c.VariacionPorcentual > porcentaje).ToList();
                        break;
                    case Domain.Enumerations.OperadorAlerta.O:
                        result = result.Where(c => c.DiferenciaPatrimonio > monto || c.VariacionPorcentual > porcentaje).ToList();
                        break;
                    default:
                        break;
                }
            }
            return result;
        }
    }
}
