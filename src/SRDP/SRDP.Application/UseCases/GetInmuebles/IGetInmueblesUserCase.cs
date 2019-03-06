using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRDP.Application.UseCases.GetInmuebles
{
    public interface IGetInmueblesUserCase
    {
        Task<InmuebleOutput> Execute(Guid inmuebleID);
        Task<ICollection<InmuebleOutput>> ExecuteList(Guid declaracionID);
    }
}
