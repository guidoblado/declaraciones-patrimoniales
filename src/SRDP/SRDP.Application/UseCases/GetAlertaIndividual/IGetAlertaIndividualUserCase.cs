using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRDP.Application.UseCases.GetAlertaIndividual
{
    public interface IGetAlertaIndividualUserCase
    {
        Task<ICollection<AlertaIndividualOutput>> ExecuteList(int gestion, decimal monto, string operador, decimal porcentaje);

    }
}
