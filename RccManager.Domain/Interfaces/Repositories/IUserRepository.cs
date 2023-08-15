using System;
using RccManager.Domain.Entities;

namespace RccManager.Domain.Interfaces.Repositories
{
	public interface IUserRepository : IRepository<User>
    {
		Task<User> GetByEmail(string email);
        Task<User> GetByName(string name);
    }
}

