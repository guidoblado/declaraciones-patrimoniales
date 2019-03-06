using SRDP.Domain.DeudasBancarias;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRDP.Application.Repositories
{
    public interface IDeudaBancariaWriteOnlyRepository
    {
        Task Add(DeudaBancariaMayor10K deudaBancaria);
        Task Update(DeudaBancariaMayor10K deudaBancaria);
        Task Delete(Guid id);
    }
}
