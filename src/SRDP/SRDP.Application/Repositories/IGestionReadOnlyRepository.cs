using SRDP.Application.UseCases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRDP.Application.Repositories
{
    public interface IGestionReadOnlyRepository
    {
        Task<GestionOutput> Get(int anio);
        Task<ICollection<GestionOutput>> GetAll();
    }
}
