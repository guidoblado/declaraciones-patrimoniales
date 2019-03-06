using SRDP.Application.SearchParameters;
using SRDP.Domain.Reportes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRDP.Application.UseCases.GetHistoricoIndividual
{
    public interface IGetHistoricoIndividualUserCase
    {
        Task<ICollection<HistoricoIndividualOutput>> ExecuteList(int gestion, FiltroFuncionario filtro);
    }
}
