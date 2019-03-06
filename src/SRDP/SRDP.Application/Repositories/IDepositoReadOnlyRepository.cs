using SRDP.Domain.Depositos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRDP.Application.Repositories
{
    public interface IDepositoReadOnlyRepository
    {
        Task<DepositoMayor10K> Get(Guid depositoID);
        Task<ICollection<DepositoMayor10K>> GetByDeclaracion(Guid declaracionID);
    }
}
