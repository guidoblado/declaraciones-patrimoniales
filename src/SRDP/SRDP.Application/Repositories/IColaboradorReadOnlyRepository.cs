using SRDP.Domain.Reportes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRDP.Application.Repositories
{
    public interface IColaboradorReadOnlyRepository
    {
        Task<Colaborador> GetByCodigo(int codigo);
    }
}
