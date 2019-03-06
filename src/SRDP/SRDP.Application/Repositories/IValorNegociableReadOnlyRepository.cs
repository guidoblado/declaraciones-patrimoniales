using SRDP.Domain.ValoresNegociables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRDP.Application.Repositories
{
    public interface IValorNegociableReadOnlyRepository
    {
        Task<ValorNegociableMayor10K> Get(Guid valorNegociableID);
        Task<ICollection<ValorNegociableMayor10K>> GetByDeclaracion(Guid declaracionID);
    }
}
