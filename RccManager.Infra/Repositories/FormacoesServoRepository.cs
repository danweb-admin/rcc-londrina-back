using System;
using Microsoft.EntityFrameworkCore;
using RccManager.Domain.Entities;
using RccManager.Domain.Interfaces.Repositories;
using RccManager.Infra.Context;

namespace RccManager.Infra.Repositories
{
    public class FormacoesServoRepository : BaseRepository<FormacoesServo>, IFormacoesServoRepository
    {
        private DbSet<FormacoesServo> dbSet;

        public FormacoesServoRepository(AppDbContext _context) : base(_context)
        {
            dbSet = context.Set<FormacoesServo>();

        }

        public async Task<IEnumerable<FormacoesServo>> GetAll(Guid servoId)
        {
            return await dbSet
                        .Include(x => x.Servo)
                        .Include(x => x.Formacao)
                        .Include(x => x.Usuario)
                        .Where(x => x.ServoId == servoId).ToListAsync();
        }

        public async Task<bool> GetByServoAndFormacao(Guid formacaoId, Guid servoId)
        {
            return await dbSet.AnyAsync(x => x.FormacaoId == formacaoId && x.ServoId == servoId);
        }
    }
}

