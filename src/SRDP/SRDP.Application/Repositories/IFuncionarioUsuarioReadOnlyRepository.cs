using SRDP.Domain.Funcionarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRDP.Application.Repositories
{
    public interface IFuncionarioUsuarioReadOnlyRepository
    {
        Task<FuncionarioUsuario> Get(string usuario);
        Task<ICollection<FuncionarioUsuario>> GetAll();
    }
}
