using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using OtoServisSatis.Entities;
using System;

namespace OtoServisSatis.Data
{
    public class DatabaseContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=localhost;Database=OtoServisSatisDB;integrated security=true;TrustServerCertificate=true");
            optionsBuilder.UseLazyLoadingProxies();
            base.OnConfiguring(optionsBuilder);
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Make> Makes { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<Slider> Sliders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            modelBuilder.Entity<Make>()
                .Property(p => p.Name)
                .IsRequired()
                .HasColumnType("varchar(50)");

            modelBuilder.Entity<Role>()
                .Property(p => p.Name)
                .IsRequired()
                .HasColumnType("varchar(50)");


            modelBuilder.Entity<Role>().HasData(new Role
            {
                Id = 1,
                Name = "Admin",
            });

           
            modelBuilder.Entity<User>().HasData(new User
            {
                Id = 1,
                Name = "Admin",
                Surname = "User",
                DateAdded = DateTime.Now,
                Email = "adminuser@satis.com",
                IsActive = true,
                Password = "123456",
                Phone = "+905319677149",
                UserName = "AdminUser",
                RoleId = 1
            });

           

            // Sale → Vehicle
            modelBuilder.Entity<Sale>()
                .HasOne(s => s.Vehicle)
                .WithMany()
                .HasForeignKey(s => s.VehicleId)
                .OnDelete(DeleteBehavior.Restrict);

            // Sale → Customer
            modelBuilder.Entity<Sale>()
                .HasOne(s => s.Customer)
                .WithMany()
                .HasForeignKey(s => s.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);

            // Vehicle → Make
            modelBuilder.Entity<Vehicle>()
                .HasOne(v => v.Make)
                .WithMany()
                .HasForeignKey(v => v.MakeId)
                .OnDelete(DeleteBehavior.Restrict);

            // Customer → Vehicle
            modelBuilder.Entity<Customer>()
                .HasOne(c => c.Vehicle)
                .WithMany()
                .HasForeignKey(c => c.VehicleId)
                .OnDelete(DeleteBehavior.Restrict);

            // DECIMAL Hassasiyet Ayarları
            modelBuilder.Entity<Vehicle>()
                .Property(v => v.Price)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Sale>()
                .Property(s => s.SalePrice)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Service>()
                .Property(s => s.ServiceCost)
                .HasPrecision(18, 2);

            base.OnModelCreating(modelBuilder);
        }
    }
}
