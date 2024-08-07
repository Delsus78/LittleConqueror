using LittleConqueror.AppService.Domain.Models.Entities;
using LittleConqueror.AppService.DrivenPorts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Newtonsoft.Json;

namespace LittleConqueror.Infrastructure;

public class DataContext(
    DbContextOptions<DataContext> options,
    IPasswordHasherPort passwordHasher) : DbContext(options)
{
    #region DBSETS
    public DbSet<City> Cities { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Territory> Territories { get; set; }
    public DbSet<AuthUser> AuthUsers { get; set; }
    #endregion

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var geoJsonConverter = new ValueConverter<Geojson, string>(
            v => JsonConvert.SerializeObject(v),
            v => JsonConvert.DeserializeObject<Geojson>(v) ?? new Geojson());
        
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(user => user.Id);
            entity.Property(user => user.Id).ValueGeneratedOnAdd();
            entity.HasOne(user => user.Territory)
                .WithOne(territory => territory.Owner)
                .HasForeignKey<Territory>(territory => territory.OwnerId);
        });

        modelBuilder.Entity<City>(entity =>
        {
            entity.HasKey(city => city.Id);
            entity.Property(city => city.Id).ValueGeneratedOnAdd();
            entity.Property(city => city.Geojson).HasConversion(geoJsonConverter);
            entity.HasOne(city => city.Territory)
                .WithMany(territory => territory.Cities)
                .HasForeignKey(city => city.TerritoryId)
                .IsRequired(false);
        });

        modelBuilder.Entity<Territory>(entity =>
        {
            entity.HasKey(territory => territory.Id);
            entity.Property(territory => territory.Id).ValueGeneratedOnAdd();
            entity.HasOne(territory => territory.Owner)
                .WithOne(user => user.Territory);
            entity.HasMany(territory => territory.Cities);
        });
        
        modelBuilder.Entity<AuthUser>(entity =>
        {
            entity.HasKey(authUser => authUser.Id);
            entity.Property(authUser => authUser.Id).ValueGeneratedOnAdd();
            entity.HasOne(authUser => authUser.User)
                .WithOne(user => user.AuthUser)
                .HasForeignKey<AuthUser>(authUser => authUser.UserId);
            
            entity.HasData(new AuthUser
            {
                Id = -1,
                Username = "admin",
                Hash = passwordHasher.EnhancedHashPassword("aDxGschD3vCe"),
                Role = "Admin"
            });
        });
    }
}