using SRDP.Domain.ValoresNegociables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRDP.Application.Repositories
{
    public interface IValorNegociableWriteOnlyRepository
    {
        Task Add(ValorNegociableMayor10K valorNegociable);
        Task Update(ValorNegociableMayor10K valorNegociable);
        Task Delete(Guid id);
    }
}
