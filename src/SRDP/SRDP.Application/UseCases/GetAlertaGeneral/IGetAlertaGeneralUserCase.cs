using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRDP.Application.UseCases.GetAlertaGeneral
{
    public interface IGetAlertaGeneralUserCase
    {
        Task<ICollection<AlertaGeneralOutput>> ExecuteList(int gestion, decimal monto, string operador, decimal porcentaje);
    }
}
