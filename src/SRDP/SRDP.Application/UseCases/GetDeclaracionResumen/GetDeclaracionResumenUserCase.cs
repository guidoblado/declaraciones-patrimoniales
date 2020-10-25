using SRDP.Application.Repositories;
using SRDP.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRDP.Application.UseCases.GetDeclaracionResumen
{
    public class GetDeclaracionResumenUserCase : IGetDeclaracionResumenUserCase
    {
        private readonly IDeclaracionReadOnlyRepository _declaracionReadOnlyRepository;
        private readonly IGestionReadOnlyRepository _gestionReadOnlyRepository;

        public GetDeclaracionResumenUserCase(IDeclaracionReadOnlyRepository declaracionReadOnlyRepository,
            IGestionReadOnlyRepository gestionReadOnlyRepository)
        {
            _declaracionReadOnlyRepository = declaracionReadOnlyRepository;
            _gestionReadOnlyRepository = gestionReadOnlyRepository;
        }
        public async Task<DeclaracionResumenOutput> Execute(int anio, int funcionarioID)
        {
            var gestion = await _gestionReadOnlyRepository.Get(anio);

            if (gestion == null)
                throw new ApplicationException("La gestión '" + anio.ToString() + "' no existe.");

            var declaracionActual = await _declaracionReadOnlyRepository.Get(Gestion.For(gestion.Anio, gestion.FechaInicio, gestion.FechaFinal, gestion.Vigente), funcionarioID);

            if (declaracionActual == null)
                throw new ApplicationException("No exsite Declarion para el FuncionarioID '" + funcionarioID.ToString() + "' en la gestión '" + anio.ToString() + "'");
            
            var declaraciones = await _declaracionReadOnlyRepository.GetDeclaracionesResumen(anio, funcionarioID);

            var declaracionesAnteriores = new List<DeclaracionResumenOutput>();

            foreach (var declaracionAnterior in declaraciones.Where(c=> c.ID != declaracionActual.ID))
            {
                declaracionesAnteriores.Add(new DeclaracionResumenOutput(
                    declaracionAnterior.ID, declaracionAnterior.Gestion, declaracionAnterior.Estado.ToString(),
                    new List<DeclaracionResumenOutput>()));
            }

            return new DeclaracionResumenOutput(
                declaracionActual.ID, declaracionActual.Gestion.Anio, declaracionActual.Estado.ToString(),
                declaracionesAnteriores);
        }
    }
}
