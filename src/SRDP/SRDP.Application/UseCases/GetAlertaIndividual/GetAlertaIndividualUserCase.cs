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

        public GetAlertaIndividualUserCase(IAlertaIndividualReadOnlyRepository alertaIndividualReadOnlyRepository, IDeclaracionReadOnlyRepository declaracionReadOnlyRepository)
        {
            _alertaIndividualReadOnlyRepository = alertaIndividualReadOnlyRepository;
            _declaracionReadOnlyRepository = declaracionReadOnlyRepository;
        }

        public async Task<ICollection<AlertaIndividualOutput>> ExecuteList(int gestion, decimal monto, string operador, decimal porcentaje)
        {
            var declaracionesAlerta = await _alertaIndividualReadOnlyRepository.GetFromGestion(gestion);
            var result = new List<AlertaIndividualOutput>();
            foreach (var item in declaracionesAlerta)
            {
                var declaracionActual = await _declaracionReadOnlyRepository.Get(item.DeclaracionID);
                var declaracionAnterior = await _declaracionReadOnlyRepository.Get(item.DeclaracionAnteriorID);
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
