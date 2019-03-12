using SRDP.Application.Repositories;
using SRDP.Domain.Declaraciones;
using SRDP.Domain.Enumerations;
using SRDP.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRDP.Application.UseCases.UpdateDeclaracion
{
    public class UpdateDeclaracionUserCase : IUpdateDeclaracionUserCase
    {
        private readonly IDeclaracionWriteOnlyRepository _declaracionWriteOnlyRepository;

        public UpdateDeclaracionUserCase(IDeclaracionWriteOnlyRepository declaracionWriteOnlyRepository)
        {
            _declaracionWriteOnlyRepository = declaracionWriteOnlyRepository;
        }

        public async Task CloseDeclaracion(Guid declaracionID)
        {
            await _declaracionWriteOnlyRepository.UpdateEstado(declaracionID, EstadoDeclaracion.Completado);
        }
    }
}
