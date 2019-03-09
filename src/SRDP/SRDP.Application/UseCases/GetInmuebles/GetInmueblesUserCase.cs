using SRDP.Application.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRDP.Application.UseCases.GetInmuebles
{
    public class GetInmueblesUserCase : IGetInmueblesUserCase
    {
        private IInmuebleReadOnlyRepository _inmuebleReadOnlyRepository;

        public GetInmueblesUserCase(IInmuebleReadOnlyRepository inmuebleReadOnlyRepository)
        {
            _inmuebleReadOnlyRepository = inmuebleReadOnlyRepository;
        }

        public async Task<InmuebleOutput> Execute(Guid inmuebleID)
        {
            var inmueble = await _inmuebleReadOnlyRepository.Get(inmuebleID);
            if (inmueble == null) return null;

            return new InmuebleOutput(inmueble.ID, inmueble.DeclaracionID, inmueble.Direccion, inmueble.TipoDeInmueble, inmueble.PorcentajeParticipacion.Valor,
                inmueble.ValorComercial, inmueble.SaldoHipoteca, inmueble.Banco);
        }

        public async Task<ICollection<InmuebleOutput>> ExecuteList(Guid declaracionID)
        {
            var inmuebles = await _inmuebleReadOnlyRepository.GetByDeclaracion(declaracionID);

            var outputList = new List<InmuebleOutput>();

            if (inmuebles == null) return outputList;

            foreach (var inmueble in inmuebles)
            {
                outputList.Add(new InmuebleOutput(inmueble.ID, inmueble.DeclaracionID, inmueble.Direccion, inmueble.TipoDeInmueble, inmueble.PorcentajeParticipacion.Valor,
                    inmueble.ValorComercial, inmueble.SaldoHipoteca, inmueble.Banco));
            }
            return outputList;
        }
    }
}
