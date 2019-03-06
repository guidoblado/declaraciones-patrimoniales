using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SRDP.Application.Repositories;
using SRDP.Domain.Reportes;

namespace SRDP.Application.UseCases.GetCatalogos
{
    public class GetCatalogosUserCase : IGetCatalogosUserCase
    {
        private readonly ICatalogosReadOnlyRepository _catalogosReadOnlyRepository;

        public GetCatalogosUserCase(ICatalogosReadOnlyRepository catalogosReadOnlyRepository)
        {
            _catalogosReadOnlyRepository = catalogosReadOnlyRepository;
        }
        public async Task<Catalogos> Execute()
        {
            var result = await _catalogosReadOnlyRepository.GetCatalogos();
            return result;
        }
    }
}
