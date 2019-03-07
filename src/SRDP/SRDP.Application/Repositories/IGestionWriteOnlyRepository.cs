using SRDP.Application.UseCases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRDP.Application.Repositories
{
    public interface IGestionWriteOnlyRepository
    {
        Task Add(GestionOutput gestion);
        Task Update(GestionOutput gestion);
        Task Delete(int gestion);
        Task SetAsVigente(int gestion);
    }
}
