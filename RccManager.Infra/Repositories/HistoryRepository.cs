using System;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using RccManager.Domain.Entities;
using RccManager.Domain.Helpers;
using RccManager.Domain.Interfaces.Repositories;
using RccManager.Infra.Context;

namespace RccManager.Infra.Repositories
{
    public class HistoryRepository : BaseRepository<History>, IHistoryRepository
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IUserRepository userRepository;
        private DbSet<History> dbSet;

        public HistoryRepository(AppDbContext _context, IHttpContextAccessor _httpContextAccessor, IUserRepository _userRepository) : base(_context)
        {
            dbSet = context.Set<History>();
            httpContextAccessor = _httpContextAccessor;
            userRepository = _userRepository;
        }

        public async Task Add(string tableName, Guid recordId, string operation)
        {

            var userName = httpContextAccessor.HttpContext.User?.Identity.Name;
            if (string.IsNullOrEmpty(userName))
                userName = "administrador";
            var user = await userRepository.GetByName(userName);

            var history = new History
            {
                Id = Guid.NewGuid(),
                TableName = tableName,
                RecordId = recordId,
                Operation = operation,
                UserId = user.Id,
                OperationDate = Helpers.DateTimeNow()

            };
            dbSet.Add(history);
            await context.SaveChangesAsync();
            
        }

        public async Task<IEnumerable<History>> GetAll(string tableName, Guid recordId)
        {
            return await dbSet.Include(x => x.User).Where(x => x.TableName == tableName && x.RecordId == recordId)
                .OrderBy(x => x.OperationDate)
                .ToListAsync();
        }
    }
}

