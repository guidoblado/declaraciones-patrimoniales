using SRDP.Application.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRDP.Application.UseCases.UpdateGestiones
{
    public class UpdateGestionesUserCase : IUpdateGestionesUserCase
    {
        private readonly IGestionWriteOnlyRepository _gestionWriteOnlyRepository;

        public UpdateGestionesUserCase(IGestionWriteOnlyRepository gestionWriteOnlyRepository)
        {
            _gestionWriteOnlyRepository = gestionWriteOnlyRepository;
        }

        public async Task Delete(int gestion)
        {
            await _gestionWriteOnlyRepository.Delete(gestion);
        }

        public async Task SetAsActive(int gestion)
        {
            await _gestionWriteOnlyRepository.SetAsVigente(gestion);
        }

        public async Task<GestionOutput> Update(int gestion, DateTime fechaInicio, DateTime fechaFinal, bool vigente)
        {
            var gestionActualizada = new GestionOutput(gestion, fechaInicio, fechaFinal, vigente);
            await _gestionWriteOnlyRepository.Update(gestionActualizada);
            return gestionActualizada;
        }
    }
}
