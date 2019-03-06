using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SRDP.Domain.Funcionarios;

namespace SRDP.Application.Repositories
{
    public interface IFuncionarioReadOnlyRepository
    {
        Task<Funcionario> Get(Guid funcionarioID);
        Task<Funcionario> GetByCodigo(int codigo);
        Task<ICollection<Funcionario>> GetAll();
        Task<int> GetFuncionarioID(string usuario);
    }
}
