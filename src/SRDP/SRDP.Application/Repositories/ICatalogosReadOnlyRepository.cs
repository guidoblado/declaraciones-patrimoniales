using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SRDP.Domain.Reportes;
namespace SRDP.Application.Repositories
{
    public interface ICatalogosReadOnlyRepository
    {
        Task<Catalogos> GetCatalogos();
    }
}
