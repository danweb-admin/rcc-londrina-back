using System;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using RccManager.Domain.Entities;
using RccManager.Domain.Helpers;
using RccManager.Domain.Interfaces.Repositories;
using RccManager.Infra.Context;

namespace RccManager.Infra.Repositories
{
    public class BaseRepository<T> : IRepository<T> where T : BaseEntity
    {
        protected readonly AppDbContext context;
        private DbSet<T> dbSet;

        public BaseRepository(AppDbContext _context)
        {
            this.context = _context;
            dbSet = context.Set<T>();
        }

        public async Task<bool> Delete(Guid id)
        {
            var result = await dbSet.SingleOrDefaultAsync(x => x.Id.Equals(id));

            if (result == null)
                return false;

            dbSet.Remove(result);

            await context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> EnableDisable(Guid id)
        {
            
            var result = await dbSet.SingleOrDefaultAsync(x => x.Id.Equals(id));

            result.CreatedAt = result.CreatedAt;
            result.UpdatedAt = Helpers.DateTimeNow();
            result.Active = !result.Active;

            dbSet.Update(result);
            await context.SaveChangesAsync();

            return true;
            
        }

        public async Task<IEnumerable<T>> GetAll(string search)
        {
            return await dbSet.ToListAsync();
        }

        public async Task<T> GetById(Guid id)
        {
            
            return await dbSet.Include("DecanatoSetor").SingleOrDefaultAsync(x => x.Id.Equals(id));
            
        }

        public async Task<T> Insert(T entity)
        {
            
            if (entity.Id == Guid.Empty)
                entity.Id = Guid.NewGuid();

            entity.CreatedAt = entity.CreatedAt == null ? Helpers.DateTimeNow() : entity.CreatedAt;
            dbSet.Add(entity);

            await context.SaveChangesAsync();

            return entity;
            

        }

        public Task<IQueryable<T>> Query(Expression<Func<T, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public async Task<T> Update(T entity)
        {
            
            var result = await dbSet.SingleOrDefaultAsync(x => x.Id.Equals(entity.Id));

            if (result == null)
                return null;

            entity.CreatedAt = result.CreatedAt;
            entity.UpdatedAt = Helpers.DateTimeNow();

            context.Entry(result).CurrentValues.SetValues(entity);
            await context.SaveChangesAsync();

            return entity;   
        }
    }
}

