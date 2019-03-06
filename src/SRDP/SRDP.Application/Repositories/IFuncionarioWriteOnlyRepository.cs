using SRDP.Domain.Entities;
using System.Threading.Tasks;

namespace SRDP.Application.Repositories
{
    public interface IFuncionarioWriteOnlyRepository
    {
        Task Add(Funcionario funcionario);
        Task Update(Funcionario funcionario);
    }
}
