using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XodoApp.Core.Domain.Common;
using XodoApp.Core.Domain.Entities;
using XodoApp.Core.Domain.Enums;

namespace XodoApp.Infrastructure.Persistence.Contexts
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }
        public DbSet<Vehicle> Vehicles { get; set; }
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<AuditableBaseEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.Created = DateTime.Now;
                        entry.Entity.CreatedBy = "DefaultAppUser";
                        break;
                    case EntityState.Modified:
                        entry.Entity.LastModified = DateTime.Now;
                        entry.Entity.LastModifiedBy = "DefaultAppUser";
                        break;
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region tables
            modelBuilder.Entity<Vehicle>()
                .ToTable("Vehicles");
            modelBuilder.Entity<Dealership>()
                .ToTable("Dealerships");
            modelBuilder.Entity<VehicleImage>()
                .ToTable("VehicleImages");
            #endregion
            #region "primary keys"
            modelBuilder.Entity<Vehicle>().HasKey(x => x.Id);
            modelBuilder.Entity<Dealership>().HasKey(x => x.Id);
            modelBuilder.Entity<VehicleImage>().HasKey(x => x.Id);
            #endregion
            #region "property configurations"
            #region vehicle
            modelBuilder.Entity<Vehicle>()
                .Property(vehicle => vehicle.VIN)
                .HasMaxLength(17);
            modelBuilder.Entity<Vehicle>()
                .Property(vehicle => vehicle.EngineType)
                .HasMaxLength(12);
            modelBuilder.Entity<Vehicle>()
                .Property(vehicle => vehicle.Mileage)
                .HasMaxLength(8);
            modelBuilder.Entity<Vehicle>()
                .Property(vehicle => vehicle.Color)
                .HasMaxLength(28);
            modelBuilder.Entity<Vehicle>()
                .Property(vehicle => vehicle.Price)
                .HasColumnType("decimal(18, 2)");
            modelBuilder.Entity<Vehicle>()
                .Property(vehicle => vehicle.CarMake)
                .HasMaxLength(28)
                .IsRequired();
            modelBuilder.Entity<Vehicle>()
                .Property(vehicle => vehicle.Year)
                .HasMaxLength(4)
                .IsRequired();
            modelBuilder.Entity<Vehicle>()
                .Property(vehicle => vehicle.Model)
                .HasMaxLength(48)
                .IsRequired();
            modelBuilder.Entity<Vehicle>()
                .Property(vehicle => vehicle.TransmissionType)
                .HasMaxLength(12)
                .IsRequired();
            modelBuilder.Entity<Vehicle>()
                .Property(vehicle => vehicle.VehicleType)
                .HasMaxLength(16)
                .IsRequired();
            #endregion
            #region dealership
            modelBuilder.Entity<Dealership>()
                .Property(dealership => dealership.Name)
                .HasMaxLength(48)
                .IsRequired();
            modelBuilder.Entity<Dealership>()
                .Property(dealership => dealership.Address)
                .HasMaxLength(255)
                .IsRequired();
            modelBuilder.Entity<Dealership>()
                .Property(dealership => dealership.City)
                .HasMaxLength(64)
                .IsRequired();
            modelBuilder.Entity<Dealership>()
                .Property(dealership => dealership.PhoneNumber)
                .HasMaxLength(16)
                .IsRequired();
            modelBuilder.Entity<Dealership>()
                .Property(dealership => dealership.Email)
                .HasMaxLength(255)
                .IsRequired();
            #endregion
            #endregion
            #region relationships
            modelBuilder.Entity<Vehicle>()
               .HasOne(v => v.Dealership)
               .WithMany(d => d.Vehicles)
               .HasForeignKey(v => v.DealershipId)
               .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Vehicle>()
               .HasMany(v => v.VehicleImages)
               .WithOne(i => i.Vehicle)
               .HasForeignKey(i => i.VehicleId);
            #endregion
        }
    }
}
