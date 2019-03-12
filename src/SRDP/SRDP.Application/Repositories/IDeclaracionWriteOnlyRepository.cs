using SRDP.Domain.Declaraciones;
using SRDP.Domain.Depositos;
using SRDP.Domain.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRDP.Application.Repositories
{
    public interface IDeclaracionWriteOnlyRepository
    {
        Task Add(Declaracion declaracion);
        Task Update(Declaracion declaracion);
        Task UpdateEstado(Guid declaracionID, EstadoDeclaracion estado);
    }
}
