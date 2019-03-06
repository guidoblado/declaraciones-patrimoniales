using SRDP.Application.Repositories;
using SRDP.Domain.Inmuebles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRDP.Application.UseCases.SaveInmuebles
{
    public class SaveInmueblesUserCase : ISaveInmueblesUserCase
    {
        private IInmuebleWriteOnlyRepository _inmuebleWriteOnlyRepository;

        public SaveInmueblesUserCase(IInmuebleWriteOnlyRepository inmuebleWriteOnlyRepository)
        {
            _inmuebleWriteOnlyRepository = inmuebleWriteOnlyRepository;
        }
        public async Task DeleteInmueble(Guid id)
        {
            await _inmuebleWriteOnlyRepository.Delete(id);
        }

        public async Task<InmuebleOutput> Execute(Guid inmuebleID, Guid declaracionID, string direccion, string tipoDeInmueble, decimal porcentajeParticipacion, decimal valorComercial, decimal saldoHipoteca, string banco)
        {
            if (inmuebleID == null || inmuebleID == Guid.Empty)
                await _inmuebleWriteOnlyRepository.Add(new Inmueble(declaracionID, direccion, tipoDeInmueble, porcentajeParticipacion, valorComercial, saldoHipoteca, banco));
            else
                await _inmuebleWriteOnlyRepository.Update(Inmueble.Load(inmuebleID, declaracionID, direccion, tipoDeInmueble, porcentajeParticipacion, valorComercial, saldoHipoteca, banco));
            return new InmuebleOutput(inmuebleID, declaracionID, direccion, tipoDeInmueble, porcentajeParticipacion, valorComercial, saldoHipoteca, banco);
        }
    }
}
     
