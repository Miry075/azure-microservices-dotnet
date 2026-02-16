
using Microsoft.EntityFrameworkCore;
using Wpm.Managemnt.Api.Entities;

namespace Wpm.Managemnt.Api.DataAccess;

public class ManagementDbContext(DbContextOptions<ManagementDbContext> options) : DbContext(options)
{
    public DbSet<Pet> Pets { get; set; }
    public DbSet<Breed> Breeds { get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        CreateUserData(modelBuilder);
        CreateBreedData(modelBuilder);
        CreatePetData(modelBuilder);
    }

    private void CreatePetData(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Pet>().HasData(
           [
                new Pet() {Id = 1,  Name = "Max", Age = 8, BreedId = 1},
                new Pet() {Id = 2,  Name = "Nina", Age = 3, BreedId = 2},
                new Pet() {Id = 3,  Name = "Kati", Age = 13, BreedId = 2},
           ]
       );
    }

    private void CreateUserData(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().HasData(
           [
                new User { Id = 1, Username = "john_doe", Email = "john@example.com", PasswordHash = "hashed_password_123", IsActive = true },
                new User { Id = 2, Username = "jane_smith", Email = "jane@example.com", PasswordHash = "hashed_password_456", IsActive = true }
           ]
       );
    }

    private void CreateBreedData(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Breed>().HasData(
           [
                new Breed(1, "Beagle"),
                new Breed(2, "Staffordshire Terrier")
           ]
       );
    }
}