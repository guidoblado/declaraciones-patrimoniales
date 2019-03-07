using SRDP.Application.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRDP.Application.UseCases.GetGestiones
{
    public class GetGestionesUserCase : IGetGestionesUserCase
    {
        private readonly IGestionReadOnlyRepository _gestionReadOnlyRepository;

        public GetGestionesUserCase(IGestionReadOnlyRepository gestionReadOnlyRepository)
        {
            _gestionReadOnlyRepository = gestionReadOnlyRepository;
        }

        public async Task<GestionOutput> Execute(int gestion)
        {
            var result = await _gestionReadOnlyRepository.Get(gestion);
            return result;
        }

        public async Task<ICollection<GestionOutput>> ExecuteList()
        {
            var resultList = await _gestionReadOnlyRepository.GetAll();
            return resultList;
        }

        public async Task<GestionOutput> GestionVigente()
        {
            var resultList = await _gestionReadOnlyRepository.GetAll();
            var gestionVigente = resultList.Where(c => c.Vigente).FirstOrDefault();
            if (gestionVigente == null) return null;
            return gestionVigente;
        }

        public async Task<int> SiguienteGestion()
        {
            var resultList = await _gestionReadOnlyRepository.GetAll();
            var siguiente = resultList.Max(c => c.Gestion) + 1;
            return siguiente;
        }
    }
}
