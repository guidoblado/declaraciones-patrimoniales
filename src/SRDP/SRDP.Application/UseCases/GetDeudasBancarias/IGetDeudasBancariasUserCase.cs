using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRDP.Application.UseCases.GetDeudasBancarias
{
    public interface IGetDeudasBancariasUserCase
    {
        Task<DeudaBancariaOutput> Execute(Guid dudaBancariaID);
        Task<ICollection<DeudaBancariaOutput>> ExecuteList(Guid declaracionID);
    }
}
