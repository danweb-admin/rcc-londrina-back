using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using RccManager.Domain.Entities;
using RccManager.Domain.Interfaces.Repositories;
using RccManager.Infra.Context;

namespace RccManager.Infra.Repositories
{
	public class ServoTempRepository : BaseRepository<ServoTemp>, IServoTempRepository
    {
        private readonly DbSet<ServoTemp> dbSet;

        public ServoTempRepository(AppDbContext _context) : base(_context)
        {
            dbSet = _context.Set<ServoTemp>();
        }

        public async Task<IEnumerable<ServoTemp>> GetAll(Guid grupoOracaoId)
        {
            return await dbSet
                    .Where(x => x.GrupoOracaoId == grupoOracaoId)
                    .OrderBy(x => x.Name).ToListAsync();
        }

        public async Task<ServoTemp> GetById(Guid id)
        {
            return await dbSet.FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}

