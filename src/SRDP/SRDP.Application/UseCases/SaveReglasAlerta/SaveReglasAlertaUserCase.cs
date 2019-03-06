using SRDP.Application.Repositories;
using SRDP.Domain.Alertas;
using SRDP.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRDP.Application.UseCases.SaveReglasAlerta
{
    public class SaveReglasAlertaUserCase : ISaveReglasAlertaUserCase
    {
        private IReglaAlertaWriteOnlyRepository _reglaAlertaWriteOnlyRepository;

        public SaveReglasAlertaUserCase(IReglaAlertaWriteOnlyRepository reglaAlertaWriteOnlyRepository)
        {
            _reglaAlertaWriteOnlyRepository = reglaAlertaWriteOnlyRepository;
        }

        public async Task DeleteRegla(Guid id)
        {
            await _reglaAlertaWriteOnlyRepository.Delete(id);
        }

        public async Task<ReglaAlertaOutput> Execute(Guid id, int gestion, string descripcion, decimal porcentaje, string operador, decimal monto)
        {
            if (id == null || id == Guid.Empty)
                await _reglaAlertaWriteOnlyRepository.Add(new ReglaAlerta(gestion, descripcion, porcentaje, new CondicionAlerta(operador), monto));
            else
                await _reglaAlertaWriteOnlyRepository.Update(ReglaAlerta.Load(id, gestion, descripcion, porcentaje, new CondicionAlerta(operador), monto));
            return new ReglaAlertaOutput(id, gestion, descripcion, porcentaje, operador, monto);
        }
    }
}
