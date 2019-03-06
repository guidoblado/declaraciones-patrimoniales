using SRDP.Application.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRDP.Application.UseCases.GetOtrosIngresos
{
    public class GetOtrosIngresosUserCase : IGetOtrosIngresosUserCase
    {
        private IOtroIngresoReadOnlyRepository _otroIngresoReadOnlyRepository;

        public GetOtrosIngresosUserCase(IOtroIngresoReadOnlyRepository otroIngresoReadOnlyRepository)
        {
            _otroIngresoReadOnlyRepository = otroIngresoReadOnlyRepository;
        }
        public async Task<OtroIngresoOutput> Execute(Guid otroIngresoID)
        {
            var otroIngreso = await _otroIngresoReadOnlyRepository.Get(otroIngresoID);
            if (otroIngreso == null) return null;

            return new OtroIngresoOutput(otroIngreso.ID, otroIngreso.DeclaracionID, otroIngreso.Concepto, otroIngreso.IngresoMensual);
        }

        public async Task<ICollection<OtroIngresoOutput>> ExecuteList(Guid declaracionID)
        {
            var otrosIngresos = await _otroIngresoReadOnlyRepository.GetByDeclaracion(declaracionID);

            var outputList = new List<OtroIngresoOutput>();

            if (otrosIngresos == null) return outputList;

            foreach (var otroIngreso in otrosIngresos)
            {
                outputList.Add(new OtroIngresoOutput(otroIngreso.ID, otroIngreso.DeclaracionID, otroIngreso.Concepto, otroIngreso.IngresoMensual));
            }
            return outputList;
        }
    }
}
