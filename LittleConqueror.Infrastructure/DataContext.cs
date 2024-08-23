using LittleConqueror.AppService.Domain.Models.Entities;
using LittleConqueror.AppService.Domain.Models.Entities.ActionEntities;
using LittleConqueror.AppService.DrivenPorts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Newtonsoft.Json.Linq;
using Action = LittleConqueror.AppService.Domain.Models.Entities.ActionEntities.Action;

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
    public DbSet<Resources> Resources { get; set; }
    
    // ActionEntities
    public DbSet<Action> Actions { get; set; }
    
    public DbSet<Agricole> ActionsAgricoles { get; set; }
    public DbSet<Miniere> ActionsMiniere { get; set; }
    public DbSet<Militaire> ActionsMilitaires { get; set; }
    public DbSet<Diplomatique> ActionsDiplomatiques { get; set; }
    public DbSet<Espionnage> ActionsEspionnages { get; set; }
    public DbSet<Technologique> ActionsTechnologiques { get; set; }
    #endregion

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var geoJsonConverter = new ValueConverter<JToken, string>(
            v => v.ToString(),
            v => JToken.Parse(v));
        
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(user => user.Id);
            entity.Property(user => user.Id).ValueGeneratedOnAdd();
            entity.HasOne(user => user.Territory)
                .WithOne(territory => territory.Owner)
                .HasForeignKey<Territory>(territory => territory.OwnerId);
            entity.HasOne(user => user.Resources)
                .WithOne(resources => resources.User)
                .HasForeignKey<Resources>(resources => resources.UserId);
        });
        
        modelBuilder.Entity<Resources>(
            entity =>
            {
                entity.HasKey(resources => resources.Id);
                entity.Property(resources => resources.Id).ValueGeneratedOnAdd();
                entity.HasOne(resources => resources.User)
                    .WithOne(user => user.Resources);
            });

        modelBuilder.Entity<City>(entity =>
        {
            entity.HasKey(city => city.Id);
            entity.Property(city => city.Id);
            entity.Property(city => city.Geojson).HasConversion(geoJsonConverter);
            entity.HasOne(city => city.Territory)
                .WithMany(territory => territory.Cities)
                .HasForeignKey(city => city.TerritoryId)
                .IsRequired(false);

            entity.HasOne(city => city.Action)
                .WithOne(action => action.City)
                .HasForeignKey<Action>(action => action.Id)
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
            
            // UserId is unique
            entity.HasIndex(authUser => authUser.UserId).IsUnique();
            
            entity.HasData(new AuthUser
            {
                Id = -1,
                Username = "admin",
                Hash = passwordHasher.EnhancedHashPassword("aDxGschD3vCe"),
                Role = "Admin"
            });
        });

        modelBuilder.Entity<Action>(entity =>
        {
            // primary key is the same as the city id
            entity.HasKey(action => action.Id);
            entity.HasOne(action => action.City)
                .WithOne(city => city.Action)
                .HasForeignKey<Action>(action => action.Id)
                .IsRequired();
        });
        
        modelBuilder.Entity<Agricole>().ToTable("ActionsAgricoles");
        modelBuilder.Entity<Miniere>().ToTable("ActionsMiniere");
        modelBuilder.Entity<Militaire>().ToTable("ActionsMilitaires");
        modelBuilder.Entity<Diplomatique>().ToTable("ActionsDiplomatiques");
        modelBuilder.Entity<Espionnage>().ToTable("ActionsEspionnages");
        modelBuilder.Entity<Technologique>().ToTable("ActionsTechnologiques");
    }
}