using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRDP.Application.UseCases.SaveDeposito
{
    public interface ISaveDepositoUserCase
    {
        Task<DepositoOutput> Execute(Guid depositoID, Guid declaracionID, string institucion, string tipoCuenta, decimal saldo );
        Task DeleteDeposito(Guid id);
    }
}
