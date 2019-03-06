using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRDP.Application.UseCases.GetDepositos
{
    public interface IGetDepositosUserCase
    {
        Task<DepositoOutput> Execute(Guid depositoID);
        Task<ICollection<DepositoOutput>> ExecuteList(Guid declaracionID);
    }
}
