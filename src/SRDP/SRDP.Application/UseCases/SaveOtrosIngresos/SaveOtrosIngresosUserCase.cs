using SRDP.Application.Repositories;
using SRDP.Domain.OtrosIngresos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRDP.Application.UseCases.SaveOtrosIngresos
{
    public class SaveOtrosIngresosUserCase : ISaveOtrosIngresosUserCase
    {
        private IOtroIngresoWriteOnlyRepository _otroIngresoWriteOnlyRepository;

        public SaveOtrosIngresosUserCase(IOtroIngresoWriteOnlyRepository otroIngresoWriteOnlyRepository)
        {
            _otroIngresoWriteOnlyRepository = otroIngresoWriteOnlyRepository;
        }
        public async Task DeleteOtroIngreso(Guid id)
        {
            await _otroIngresoWriteOnlyRepository.Delete(id);
        }

        public async Task<OtroIngresoOutput> Execute(Guid otroIngresoID, Guid declaracionID, string concepto, decimal ingresoMensual)
        {
            if (otroIngresoID == null || otroIngresoID == Guid.Empty)
                await _otroIngresoWriteOnlyRepository.Add(new OtroIngreso(declaracionID, concepto, ingresoMensual));
            else
                await _otroIngresoWriteOnlyRepository.Update(OtroIngreso.Load(otroIngresoID, declaracionID, concepto, ingresoMensual));
            return new OtroIngresoOutput(otroIngresoID, declaracionID, concepto, ingresoMensual);
        }
    }
}
