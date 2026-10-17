using System;
using Microsoft.EntityFrameworkCore;
using RccManager.Domain.Entities;
using RccManager.Domain.Interfaces.Repositories;
using RccManager.Infra.Context;

namespace RccManager.Infra.Repositories
{
    public class FormularioCadastroServoRepository : BaseRepository<FormularioCadastroServo>, IFormularioCadastroServoRepository
    {
        private readonly DbSet<FormularioCadastroServo> dbSet;

        public FormularioCadastroServoRepository(AppDbContext context)
            : base(context)
        {
            dbSet = context.Set<FormularioCadastroServo>();
        }

        public async Task<FormularioCadastroServo> GetByGrupoOracaoId(
            Guid grupoOracaoId)
        {
            return await dbSet.FirstOrDefaultAsync(
                x => x.GrupoOracaoId == grupoOracaoId);
        }

        public async Task<FormularioCadastroServo> GetByToken(
            string token)
        {
            return await dbSet
                .Include(x => x.GrupoOracao)
                .Include(x => x.GrupoOracao.ParoquiaCapela)
                .Include(x => x.GrupoOracao.ParoquiaCapela.DecanatoSetor)

                .FirstOrDefaultAsync(
                    x => x.Token == token && x.Active);
        }
    }
}

