using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using RccManager.Domain.Entities;
using RccManager.Infra.Mappings;

namespace RccManager.Infra.Context;

public class AppDbContext : DbContext
{
    public DbSet<DecanatoSetor> Decanatos { get; set; }
    public DbSet<ParoquiaCapela> ParoquiasCapelas { get; set; }
    public DbSet<GrupoOracao> GrupoOracoes { get; set; }
    public DbSet<User> Users { get; set; }


    public AppDbContext()
    {

    }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new DecanatoSetorMap());
        modelBuilder.ApplyConfiguration(new ParoquiaCapelaMap());
        modelBuilder.ApplyConfiguration(new GrupoOracaoMap());
        modelBuilder.ApplyConfiguration(new UserMap());
    }
}
