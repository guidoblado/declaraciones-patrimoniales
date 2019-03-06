using SRDP.Domain.Reportes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRDP.Application.UseCases.GetCatalogos
{
    public interface IGetCatalogosUserCase
    {
        Task<Catalogos> Execute();
    }
}
