using LittleConqueror.Infrastructure.Entities.DatabaseEntities;
using Microsoft.EntityFrameworkCore;

namespace LittleConqueror.Infrastructure;

public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
{
    #region DBSETS
    public DbSet<CityEntity> Cities { get; set; }
    public DbSet<UserEntity> Users { get; set; }
    public DbSet<TerritoryEntity> Territories { get; set; }
    #endregion

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserEntity>(entity =>
        {
            entity.HasKey(user => user.Id);
            entity.Property(user => user.Id).ValueGeneratedOnAdd();
            entity.HasOne(user => user.Territory).WithOne(territory => territory.Owner);
        });

        modelBuilder.Entity<CityEntity>(entity =>
        {
            entity.HasKey(city => city.Id);
            entity.Property(city => city.Id).ValueGeneratedOnAdd();
        });

        modelBuilder.Entity<TerritoryEntity>(entity =>
        {
            entity.HasKey(territory => territory.Id);
            entity.Property(territory => territory.Id).ValueGeneratedOnAdd();
            entity.HasOne(territory => territory.Owner)
                .WithOne(user => user.Territory);
            entity.HasMany(territory => territory.Cities);
        });
    }
}