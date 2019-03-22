using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRDP.Application.UseCases.CreateGestion
{
    public interface ICreateGestionUserCase
    {
        Task<GestionOutput> Add(int anio, DateTime fechInicio, DateTime fechaFinal, bool vigente);
        Task<IDictionary<string, int>> ImportFromPreviousGestion(int anioGestionAnterior, int anioGestionNueva);
    }
}
