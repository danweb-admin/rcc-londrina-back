using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Microsoft.EntityFrameworkCore;
using RccManager.Domain.Entities;
using RccManager.Domain.Helpers;
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

        public async Task<bool> Disable(Guid id)
        {
            var result = await dbSet.SingleOrDefaultAsync(x => x.Id.Equals(id));

            result.CreatedAt = result.CreatedAt;
            result.UpdatedAt = Helpers.DateTimeNow();
            result.Active = !result.Active;

            dbSet.Update(result);
            await context.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<ServoTemp>> GetAll(Guid grupoOracaoId)
        {
            return await dbSet
                    .Where(x => x.GrupoOracaoId == grupoOracaoId)
                    .OrderBy(x => x.Name).ToListAsync();
        }

        public async Task<IEnumerable<ServoTemp>> GetAll()
        {
            return await dbSet
                    .Include(x => x.GrupoOracao)
                    .Include(x => x.GrupoOracao.ParoquiaCapela.DecanatoSetor)
                    .OrderBy(x => x.Name).ToListAsync();
        }

        public async Task<IEnumerable<ServoTemp>> GetByCpfGrupoOracao(Guid grupoOracaoId, string cpf)
        {
            return await dbSet.Include(x => x.GrupoOracao).Where(x =>
                                                  x.Cpf == cpf &&
                                                  x.GrupoOracaoId == grupoOracaoId).ToListAsync();
        }

            public async Task<ServoTemp> GetById(Guid id)
        {
            return await dbSet.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<ServoTemp>> GetByNameCpfEmail(string name, string cpf, string email)
        {
            return await dbSet.Include(x => x.GrupoOracao).Where(x => x.Name == name &&
                                                  x.Cpf == cpf &&
                                                  x.Email == email).ToListAsync();
        }

        public bool ValidateServoTemp(string name, string birthday, string cpf, string email, string cellPhone)
        {
            return dbSet.Any(x => x.Name == name &&
                                             x.CellPhone == cellPhone &&
                                             x.Email == email &&
                                             x.Cpf == cpf);
                
        }
    }
}

