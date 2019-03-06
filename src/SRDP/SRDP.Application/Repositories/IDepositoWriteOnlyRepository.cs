using SRDP.Domain.Depositos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRDP.Application.Repositories
{
    public interface IDepositoWriteOnlyRepository
    {
        Task Add(DepositoMayor10K deposito);
        Task Update(DepositoMayor10K deposito);
        Task Delete(Guid id);
    }
}
