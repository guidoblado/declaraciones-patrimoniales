using SRDP.Application.Repositories;
using SRDP.Domain.Declaraciones;
using SRDP.Domain.Enumerations;
using SRDP.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRDP.Persitence.InMemoryDataAccess.Repositories
{
    public class DeclaracionRepository : IDeclaracionWriteOnlyRepository, IDeclaracionReadOnlyRepository
    {
        private readonly Context _context;

        public DeclaracionRepository(Context context)
        {
            _context = context;
        }


        public async Task Add(Declaracion declaracion)
        {
            _context.Declaraciones.Add(declaracion);
            await Task.CompletedTask;
        }

        public async Task<Declaracion> Get(Guid declaracionID, bool loadDeclaracionAnterior = false)
        {
            var declaracion = _context.Declaraciones
                .Where(c => c.ID == declaracionID)
                .SingleOrDefault();
            return await Task.FromResult<Declaracion>(declaracion);
        }

        public Task<Declaracion> Get(Gestion gestion, int funcionarioID)
        {
            throw new NotImplementedException();
        }

        public async Task<ICollection<Declaracion>> GetByGestion(Gestion gestion)
        {
            var declaraciones = _context.Declaraciones
                .Where(c => c.Gestion == gestion)
                .ToList();
            return await Task.FromResult<List<Declaracion>>(declaraciones);
        }

        public Task<Guid> GetDeclaracionAnteriorID(Guid declaracionID)
        {
            throw new NotImplementedException();
        }
        public async Task<ICollection<Guid>> GetDeclaracionesAnterioresIDs(Guid declaracionID)
        {
            throw new NotImplementedException();
        }


        public async Task Update(Declaracion declaracion)
        {
            var declaracionAnterior = _context.Declaraciones
                .Where(c => c.ID == declaracion.ID)
                .SingleOrDefault();
            declaracionAnterior = declaracion;
            await Task.CompletedTask;
        }

        public Task UpdateEstado(Guid declaracionID, EstadoDeclaracion estado)
        {
            throw new NotImplementedException();
        }
    }
}
