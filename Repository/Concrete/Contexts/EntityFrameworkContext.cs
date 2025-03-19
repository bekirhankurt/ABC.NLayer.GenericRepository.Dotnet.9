using Entity.Concrete;
using Microsoft.EntityFrameworkCore;

namespace Repository.Concrete.Contexts;

public class EntityFrameworkContext : DbContext
{
    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=Northwind;Trusted_Connection=true");
    }


}