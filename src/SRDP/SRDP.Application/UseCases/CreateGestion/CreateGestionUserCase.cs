using SRDP.Application.Repositories;
using SRDP.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRDP.Application.UseCases.CreateGestion
{
    public class CreateGestionUserCase : ICreateGestionUserCase
    {
        private readonly IGestionWriteOnlyRepository _gestionWriteOnlyRepository;

        public CreateGestionUserCase(IGestionWriteOnlyRepository gestionWriteOnlyRepository)
        {
            _gestionWriteOnlyRepository = gestionWriteOnlyRepository;
        }

        public async Task<GestionOutput> Add(int anio, DateTime fechaInicio, DateTime fechaFinal, bool vigente)
        {
            var gestion = Gestion.For(anio, fechaInicio, fechaFinal, vigente);
            var nuevaGestion = new GestionOutput(gestion.Anio, gestion.FechaInicio, gestion.FechaFinal, gestion.Vigente);
            await _gestionWriteOnlyRepository.Add(nuevaGestion);
            return nuevaGestion;
        }

        public async Task<IDictionary<string, int>> ImportFromPreviousGestion(int anioGestionAnterior, int anioGestionNueva)
        {
            var result = new Dictionary<string, int>();

            var nroDeclaraciones = await _gestionWriteOnlyRepository.ImportDeclaraciones(anioGestionAnterior, anioGestionNueva);
            result.Add("Declaraciones", nroDeclaraciones);
            var detalles = await _gestionWriteOnlyRepository.ImportDeclaracionesDetails(anioGestionNueva);

            foreach (var item in detalles)
            {
                result.Add(item.Key, item.Value);
            }
            return result;
        }
    }
}
