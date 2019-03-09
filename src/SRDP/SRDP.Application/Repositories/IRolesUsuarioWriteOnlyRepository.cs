using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRDP.Application.Repositories
{
    public interface IRolesUsuarioWriteOnlyRepository
    {
        Task Add(string userName, int funcionarioID);
        Task Delete(string username);
    }
}
