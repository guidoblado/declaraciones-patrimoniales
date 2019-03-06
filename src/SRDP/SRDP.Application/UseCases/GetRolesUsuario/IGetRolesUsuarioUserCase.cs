using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRDP.Application.UseCases.GetRolesUsuario
{
    public interface IGetRolesUsuarioUserCase
    {
        Task<List<String>> Execute(string username);
        List<String> ExecuteSync(string username);
    }
}
