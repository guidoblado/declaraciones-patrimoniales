using SRDP.Domain.OtrosIngresos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRDP.Application.Repositories
{
    public interface IOtroIngresoWriteOnlyRepository
    {
        Task Add(OtroIngreso otroIngreso);
        Task Update(OtroIngreso otroIngreso);
        Task Delete(Guid id);
    }
}
