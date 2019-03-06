using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SRDP.Application.Repositories;
using SRDP.Domain.Entities;
using SRDP.Domain.Funcionarios;

namespace SRDP.Persitence.InMemoryDataAccess.Repositories
{
    public class FuncionarioRepository : IFuncionarioReadOnlyRepository
    {
        private readonly Context _context;

        public FuncionarioRepository(Context context)
        {
            _context = context;
        }

        public Task<Domain.Funcionarios.Funcionario> Get(Guid funcionarioID)
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<Domain.Funcionarios.Funcionario>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<Domain.Funcionarios.Funcionario> GetByCodigo(int codigo)
        {
            throw new NotImplementedException();
        }

        public Task<int> GetFuncionarioID(string usuario)
        {
            throw new NotImplementedException();
        }
    }
}
