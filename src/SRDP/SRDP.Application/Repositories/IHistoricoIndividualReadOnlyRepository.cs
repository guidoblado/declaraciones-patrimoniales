using SRDP.Domain.Enumerations;
using SRDP.Domain.Reportes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRDP.Application.Repositories
{
    public interface IHistoricoIndividualReadOnlyRepository
    {
        Task<ICollection<HistoricoIndividual>> GetFromGestion(int gestion);
        Task<ICollection<HistoricoIndividual>> GetTwoGestiones(int gestionActual, int gestionAnterior);
        Task<ICollection<HistoricoItem>> GetHistoricoItems(int gestion, RubroDeclaracion rubro);
    }
}
