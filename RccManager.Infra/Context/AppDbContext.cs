using System;
using Microsoft.EntityFrameworkCore;
using RccManager.Domain.Entities;
using RccManager.Infra.Mappings;

namespace RccManager.Infra.Context;

public class AppDbContext : DbContext
{
	public DbSet<DecanatoSetor> Decanatos { get; set; }

	public AppDbContext()
	{

	}

	public AppDbContext(DbContextOptions<AppDbContext> options) : base (options)
	{

	}

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
		if (!optionsBuilder.IsConfigured)
		{
			optionsBuilder.UseSqlServer("Server=localhost, 1433;Initial Catalog=RccManager;User ID=SA;Password=P@ssw0rd");
		}
	}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);
		modelBuilder.Entity<DecanatoSetor>(new DecanatoSetorMap().Configure);
	}
}

