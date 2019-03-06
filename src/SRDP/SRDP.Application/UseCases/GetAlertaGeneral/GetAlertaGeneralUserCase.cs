using SRDP.Application.Repositories;
using SRDP.Domain.Alertas;
using SRDP.Domain.Declaraciones;
using SRDP.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRDP.Application.UseCases.GetAlertaGeneral
{
    public class GetAlertaGeneralUserCase : IGetAlertaGeneralUserCase
    {
        private IAlertaGeneralReadOnlyRepository _alertaGeneralReadOnlyRepository;
        private IDeclaracionReadOnlyRepository _declaracionReadOnlyRepository;

        public GetAlertaGeneralUserCase(IAlertaGeneralReadOnlyRepository alertaGeneralReadOnlyRepository, IDeclaracionReadOnlyRepository declaracionReadOnlyRepository)
        {
            _alertaGeneralReadOnlyRepository = alertaGeneralReadOnlyRepository;
            _declaracionReadOnlyRepository = declaracionReadOnlyRepository;

        }
        public async Task<ICollection<AlertaGeneralOutput>> ExecuteList(int gestion, decimal monto, string operador, decimal porcentaje)
        {
            var declaracionesAlerta = await _alertaGeneralReadOnlyRepository.GetFromGestion(gestion);
            var result = new List<AlertaGeneralOutput >();
            foreach (var item in declaracionesAlerta)
            {
                var declaracionActual = await _declaracionReadOnlyRepository.Get(item.DeclaracionID);
                var declaracionAnterior = await _declaracionReadOnlyRepository.Get(item.DeclaracionAnteriorID);
                var declaracion = Declaracion.Load(declaracionActual.ID, declaracionActual.FuncionarioID, declaracionActual.Gestion,
                    declaracionActual.FechaLlenado, declaracionActual.Depositos, declaracionActual.DeudasBancarias, declaracionActual.Inmuebles,
                    declaracionActual.OtrosIngresos, declaracionActual.ValoresNegociables, declaracionActual.Vehiculos, declaracionAnterior);

                item.SetMontosPatrimonio(declaracion.PatrimonioNeto, declaracion.PatrimonioNetoGestionAnterior,
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
