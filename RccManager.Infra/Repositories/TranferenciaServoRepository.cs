using System;
using Microsoft.EntityFrameworkCore;
using RccManager.Domain.Entities;
using RccManager.Domain.Interfaces.Repositories;
using RccManager.Infra.Context;

namespace RccManager.Infra.Repositories
{
    public class TranferenciaServoRepository : BaseRepository<TransferenciaServo>, ITransferenciaServoRepository
    {
        private readonly DbSet<TransferenciaServo> dbSet;

        public TranferenciaServoRepository(AppDbContext _context) : base(_context)
        {
            dbSet = _context.Set<TransferenciaServo>();
        }

        public async Task<IEnumerable<TransferenciaServo>> GetAll()
        {
            return await dbSet.Include(x => x.Servo)
                            .Include(x => x.GrupoOracao)
                            .Include(x => x.GrupoOracaoAntigo)
                            .ToListAsync();
        }
    }
}

