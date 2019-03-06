using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRDP.Application.Repositories
{
    public interface IRolesUsuarioReadOnlyRepository
    {
        Task<List<string>> Get(string username);
    }
}
