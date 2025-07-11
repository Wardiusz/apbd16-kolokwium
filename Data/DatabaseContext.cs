using Microsoft.EntityFrameworkCore;
using Kolokwium_APBD.Models;

namespace Kolokwium_APBD.Data;

public class DatabaseContext : DbContext
{
    public DbSet<Nursery> Nurseries { get; set; }
    public DbSet<TreeSpecies> TreeSpecies { get; set; }
    public DbSet<SeedlingBatch> SeedlingBatches { get; set; }
    public DbSet<Responsible> Responsibles { get; set; }
    public DbSet<Employee> Employees { get; set; }
    
    protected DatabaseContext()
    {
    }

    public DatabaseContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Nursery>().HasData(new List<Nursery>()
        {
            new Nursery() { NurseryId = 1, Name = "Green Forest Nursery", EstablishedDate = DateTime.Parse("2020-01-01") },
            new Nursery() { NurseryId = 2, Name = "Another Forest Whatever Nursery", EstablishedDate = DateTime.Parse("2013-05-02")  },
            new Nursery() { NurseryId = 3, Name = "Dementia Forest Nursery", EstablishedDate = DateTime.Parse("2009-02-03")  },
        });
        
        modelBuilder.Entity<TreeSpecies>().HasData(new List<TreeSpecies>()
        {
            new TreeSpecies() { SpeciesId = 1, LatinName = "Juglans regia", GrowthTimeInYears = 7 },
            new TreeSpecies() { SpeciesId = 2, LatinName = "Betula pendula", GrowthTimeInYears = 9 },
            new TreeSpecies() { SpeciesId = 3, LatinName = "Robinia fertilis", GrowthTimeInYears = 5 },
        });
        
        modelBuilder.Entity<Employee>().HasData(new List<Employee>()
        {
            new Employee() { EmployeeId = 1, FirstName = "John", LastName = "Kowalski", HireDate = DateTime.Parse("2023-02-03") },
            new Employee() { EmployeeId = 2, FirstName = "Jane", LastName = "Lis", HireDate = DateTime.Parse("2022-06-08") },
            new Employee() { EmployeeId = 3, FirstName = "Julie", LastName = "Mas", HireDate = DateTime.Parse("2021-08-02") },
        });
        
        modelBuilder.Entity<Responsible>().HasData(new List<Responsible>()
        {
            new Responsible() { BatchId = 1, EmployeeId = 1, Role = "Supervisor" },
            new Responsible() { BatchId = 2, EmployeeId = 3, Role = "Tree climber" },
            new Responsible() { BatchId = 3, EmployeeId = 2, Role = "Planter" },
        });
        
        modelBuilder.Entity<SeedlingBatch>().HasData(new List<SeedlingBatch>()
        {
            new SeedlingBatch() { BatchId = 1, Quantity = 500, SownDate = DateTime.Parse("2024-05-01"), ReadyDate = DateTime.Parse("2025-05-02"), NurseryId = 1, SpeciesId = 3} ,
            new SeedlingBatch() { BatchId = 2, Quantity = 5000, SownDate = DateTime.Parse("2020-05-02"), ReadyDate = null, NurseryId = 1, SpeciesId = 3 },
            new SeedlingBatch() { BatchId = 3, Quantity = 100, SownDate = DateTime.Parse("2025-05-03"), ReadyDate = null, NurseryId = 2, SpeciesId = 2 },
            new SeedlingBatch() { BatchId = 4, Quantity = 1000, SownDate = DateTime.Parse("2023-05-04"), ReadyDate = DateTime.Parse("2025-10-08"), NurseryId = 3, SpeciesId = 1 },
        });

    }
}