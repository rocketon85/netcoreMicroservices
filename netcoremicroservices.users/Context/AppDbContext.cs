using Microsoft.EntityFrameworkCore;
using NetCoreMicroservices.Users.Domains;

namespace NetCoreMicroservices.Users.Context
{
    public class AppDbContext : DbContext
    {
        
        public DbSet<UserDomain> Users { get; set; }
  

        public AppDbContext()
        {

        }
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
            modelBuilder.Entity<UserDomain>().HasData(
                new UserDomain() { Id=1,  Name="Bruno", Password="123" },
                new UserDomain() { Id = 2, Name ="Alfredo", Password="222" }
            );

            modelBuilder.Entity<UserDomain>().HasIndex(
                entity => new { entity.Name }
            ).IsUnique();
        }
    }
}
