using Core.Entities.Concrete;
using Entity.Concrete;
using Entity.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Repository.Concrete.Contexts;

public class EntityFrameworkContext(IOptions<DatabaseSettings> databaseOptions) : DbContext
{
    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<OperationClaim> OperationClaims { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<UserOperationClaim> UserOperationClaims { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer($"Server={databaseOptions.Value.DataSource};Database={databaseOptions.Value.InitialCatalog};Trusted_Connection={databaseOptions.Value.TrustedConnection}");
    }


}