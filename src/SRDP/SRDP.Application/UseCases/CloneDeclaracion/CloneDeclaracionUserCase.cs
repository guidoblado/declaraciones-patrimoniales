using SRDP.Application.Repositories;
using SRDP.Domain.Declaraciones;
using SRDP.Domain.Enumerations;
using SRDP.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRDP.Application.UseCases.CloneDeclaracion
{
    public class CloneDeclaracionUserCase : ICloneDeclaracionUserCase
    {
        private readonly IDeclaracionReadOnlyRepository _declaracionReadOnlyRepository;
        private readonly IDeclaracionWriteOnlyRepository _declaracionWriteOnlyRepository;
        private readonly IGestionReadOnlyRepository _gestionReadOnlyRepository;
        private readonly IFuncionarioReadOnlyRepository _funcionarioReadOnlyRepository;

        public CloneDeclaracionUserCase(IDeclaracionReadOnlyRepository declaracionReadOnlyRepository, IDeclaracionWriteOnlyRepository declaracionWriteOnlyRepository, IGestionReadOnlyRepository gestionReadOnlyRepository,
            IFuncionarioReadOnlyRepository funcionarioReadOnlyRepository)
        {
            _declaracionReadOnlyRepository = declaracionReadOnlyRepository;
            _declaracionWriteOnlyRepository = declaracionWriteOnlyRepository;
            _gestionReadOnlyRepository = gestionReadOnlyRepository;
            _funcionarioReadOnlyRepository = funcionarioReadOnlyRepository;
        }

        public async Task Execute(Guid declaracionID)
        {
            var declaracion = await _declaracionReadOnlyRepository.Get(declaracionID, true);
            if (declaracion.DeclaracionAnterior == null)
                return;

            var gestiones = await _gestionReadOnlyRepository.GetAll();
            var gestionVigente = gestiones.Where(c => c.Vigente).FirstOrDefault();
            declaracion.CambiarEstado(EstadoDeclaracion.Anulada);
            await _declaracionWriteOnlyRepository.UpdateEstado(declaracionID, EstadoDeclaracion.Anulada);

            var declaracionAnterior = declaracion.DeclaracionAnterior;
            var declaracionNueva = Declaracion.Load(Guid.NewGuid(), declaracionAnterior.FuncionarioID, Gestion.For(gestionVigente.Anio, gestionVigente.FechaInicio, gestionVigente.FechaFinal, gestionVigente.Vigente),
                DateTime.Now, EstadoDeclaracion.Pendiente, declaracionAnterior.Depositos, declaracionAnterior.DeudasBancarias, declaracionAnterior.Inmuebles,
                declaracionAnterior.OtrosIngresos, declaracionAnterior.ValoresNegociables, declaracionAnterior.Vehiculos, declaracionAnterior);

            await _declaracionWriteOnlyRepository.Add(declaracionNueva);


        }
    }
}
