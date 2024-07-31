using LittleConqueror.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

namespace LittleConqueror.Persistence;

public class DataContext : DbContext
{
    #region DBSETS
    public DbSet<CityEntity> Cities { get; set; }
    public DbSet<TerritoryEntity> Territories { get; set; }
    public DbSet<UserEntity> Users { get; set; }
    #endregion
    
    public DataContext(DbContextOptions<DataContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
    }
}