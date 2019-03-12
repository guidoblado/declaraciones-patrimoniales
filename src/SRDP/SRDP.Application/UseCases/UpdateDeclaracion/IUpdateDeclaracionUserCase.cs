using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRDP.Application.UseCases.UpdateDeclaracion
{
    public interface IUpdateDeclaracionUserCase
    {
        Task CloseDeclaracion(Guid declaracionID);
    }
}
