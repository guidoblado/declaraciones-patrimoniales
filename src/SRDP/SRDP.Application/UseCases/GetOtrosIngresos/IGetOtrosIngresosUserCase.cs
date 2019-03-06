using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRDP.Application.UseCases.GetOtrosIngresos
{
    public interface IGetOtrosIngresosUserCase
    {
        Task<OtroIngresoOutput> Execute(Guid otroIngresoID);
        Task<ICollection<OtroIngresoOutput>> ExecuteList(Guid declaracionID);
    }
}
