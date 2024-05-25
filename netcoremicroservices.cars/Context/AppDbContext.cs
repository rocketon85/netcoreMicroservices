using Microsoft.EntityFrameworkCore;
using NetCoreMicroservices.Cars.Domains;
using NetCoreMicroservices.Commons.Options;

namespace NetCoreMicroservices.Cars.Context
{
    public class AppDbContext : DbContext
    {
        public DbSet<CarDomain> Cars { get; set; }
  
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseInMemoryDatabase("app");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            //modelBuilder.Entity<FuelDomain>().HasData(
            //  new FuelDomain() { Id = 1, Name = "Gasoil" },
            //  new FuelDomain() { Id = 2, Name = "Electric" }
            //);
            //modelBuilder.Entity<BrandDomain>().HasData(
            //    new BrandDomain() { Id = 1, Name = "Honda" },
            //    new BrandDomain() { Id = 2, Name = "Ford" }
            //);
            //modelBuilder.Entity<ModelDomain>().HasData(
            //    new ModelDomain() { Id = 1, BrandId = 1, Name = "Pilot" },
            //    new ModelDomain() { Id = 2, BrandId = 1, Name = "XR" },
            //    new ModelDomain() { Id = 3, BrandId = 2, Name = "Mondeo" }
            //);
            modelBuilder.Entity<CarDomain>().HasData(
                new CarDomain() { Id = 1, Name = "Auto 1", BrandId = 1, ModelId = 1, FuelId = 1 },
                new CarDomain() { Id = 2, Name = "Auto 2", BrandId = 1, ModelId = 1, FuelId = 2 },
                new CarDomain() { Id = 3, Name = "Auto 3", BrandId = 1, ModelId = 2, FuelId = 2 }
            );

          //  modelBuilder.Entity<CarDomain>().HasIndex(
          //    entity => new { entity.Id }
          //).IsUnique();
        }
    }
}
