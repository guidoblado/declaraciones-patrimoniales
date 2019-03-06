using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRDP.Application.UseCases.SaveOtrosIngresos
{
    public interface ISaveOtrosIngresosUserCase
    {
        Task<OtroIngresoOutput> Execute(Guid otroIngresoID, Guid declaracionID, string concepto, decimal ingresoMensual);
        Task DeleteOtroIngreso(Guid id);
    }
}
