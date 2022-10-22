
namespace AbyssiniaPocketLaw.API.Persistance;

using AbyssiniaPocketLaw.API.Entities;
using Microsoft.EntityFrameworkCore;
public class PocketLawDbContext : DbContext
{
    public PocketLawDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Law> Laws { get; set; }
    public DbSet<Cassation> Cassations { get; set; }
    public DbSet<User> Users { get; set; }
}
