using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRDP.Application.UseCases.SaveInmuebles
{
    public interface ISaveInmueblesUserCase
    {
        Task<InmuebleOutput> Execute(Guid inmuebleID, Guid declaracionID, string direccion, string tipoDeInmueble, decimal porcentajeParticipacion,
                decimal valorComercial, decimal saldoHipoteca, string banco);
        Task DeleteInmueble(Guid id);
    }
}
