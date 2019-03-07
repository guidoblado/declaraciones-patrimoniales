using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRDP.Application.UseCases.GetGestiones
{
    public interface IGetGestionesUserCase
    {
        Task<GestionOutput> Execute(int gestion);
        Task<ICollection<GestionOutput>> ExecuteList();
        Task<GestionOutput> GestionVigente();
        Task<int> SiguienteGestion();
    }
}
