using SRDP.Application.Repositories;
using SRDP.Domain.Depositos;
using SRDP.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRDP.Persitence.InMemoryDataAccess.Repositories
{
    public class DepositoRepository : IDepositoReadOnlyRepository
    {
        private readonly Context _context;

        public DepositoRepository(Context context)
        {
            _context = context;
        }
        public async Task Add(DepositoMayor10K deposito)
        {
            _context.Depositos.Add(deposito);
            await Task.CompletedTask;
        }

        public async Task Delete(DepositoMayor10K deposito)
        {
            var depositoAnterior = _context.Depositos
                .Where(c => c.ID == deposito.ID && c.DeclaracionID == deposito.DeclaracionID)
                .SingleOrDefault();
            _context.Depositos.Remove(depositoAnterior);
            await Task.CompletedTask;
        }

        public async Task Update(DepositoMayor10K deposito)
        {
            var depositoAnterior = _context.Depositos
                .Where(c => c.ID == deposito.ID && c.DeclaracionID == deposito.DeclaracionID)
                .SingleOrDefault();
            depositoAnterior = deposito;
            await Task.CompletedTask;
        }

        public async Task<DepositoMayor10K> Get(Guid depositoID)
        {
            var deposito = _context.Depositos
                .Where(c => c.ID == depositoID)
                .SingleOrDefault();
            return await Task.FromResult<DepositoMayor10K>(deposito);
        }

        public async Task<ICollection<DepositoMayor10K>> GetByDeclaracion(Guid declaracionID)
        {
            var depositos = _context.Depositos
            .Where(c => c.DeclaracionID == declaracionID)
            .ToList();
            return await Task.FromResult<List<DepositoMayor10K>>(depositos);
        }
    }
}
