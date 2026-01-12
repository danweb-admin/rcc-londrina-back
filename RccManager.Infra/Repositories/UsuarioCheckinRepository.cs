using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using RccManager.Domain.Entities;
using RccManager.Domain.Interfaces.Repositories;
using RccManager.Infra.Context;

namespace RccManager.Infra.Repositories
{
    public class UsuarioCheckinRepository : BaseRepository<UsuariosCheckin>, IUsuarioCheckinRepository
    {
        private readonly DbSet<UsuariosCheckin> dbSet;

        public UsuarioCheckinRepository(AppDbContext _context) : base(_context)
        {
            dbSet = _context.Set<UsuariosCheckin>();
        }

        public new async Task<IEnumerable<UsuariosCheckin>> GetAll(string email)
        {
            if (string.IsNullOrEmpty(email))
                return await dbSet.Include(x => x.Evento).ToListAsync();

            return await dbSet.Where(x => x.Email == email).ToListAsync();
        }

        public async Task<bool> Login(string email, string senha)
        {
            return await dbSet.AnyAsync(x => x.Email == email && x.Senha == senha && x.Active);
        }
    }
}

