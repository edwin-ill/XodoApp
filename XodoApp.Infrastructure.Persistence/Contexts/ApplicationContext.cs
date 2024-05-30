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
            #endregion
            #region "primary keys"
            modelBuilder.Entity<Vehicle>().HasKey(x => x.Id);
            modelBuilder.Entity<Dealership>().HasKey(x => x.Id);
            #endregion
            #region "property configurations"
            #region vehicle
            modelBuilder.Entity<Vehicle>()
                .Property(vehicle => vehicle.Price)
                .HasColumnType("decimal(18, 2)");
            modelBuilder.Entity<Vehicle>()
                .Property(vehicle => vehicle.CarMake)
                .IsRequired();
            modelBuilder.Entity<Vehicle>()
                .Property(vehicle => vehicle.Year)
                .IsRequired();
            modelBuilder.Entity<Vehicle>()
                .Property(vehicle => vehicle.Model)
                .IsRequired();
            modelBuilder.Entity<Vehicle>()
                .Property(vehicle => vehicle.TransmissionType)
                .IsRequired();
            modelBuilder.Entity<Vehicle>()
                .Property(vehicle => vehicle.Description)
                .IsRequired();
            modelBuilder.Entity<Vehicle>()
                .Property(vehicle => vehicle.ImageUrl)
                .IsRequired();
            modelBuilder.Entity<Vehicle>()
                .Property(vehicle => vehicle.VehicleType)
                .IsRequired();
            #endregion
            #region dealership
            modelBuilder.Entity<Dealership>()
                .Property(dealership => dealership.Name)
                .IsRequired();
            modelBuilder.Entity<Dealership>()
                .Property(dealership => dealership.Address)
                .IsRequired();
            modelBuilder.Entity<Dealership>()
                .Property(dealership => dealership.City)
                .IsRequired();
            modelBuilder.Entity<Dealership>()
                .Property(dealership => dealership.PhoneNumber)
                .IsRequired();
            modelBuilder.Entity<Dealership>()
                .Property(dealership => dealership.Email)
                .IsRequired();

            #endregion
            #endregion
            #region relationships
            modelBuilder.Entity<Vehicle>()
               .HasOne(v => v.Dealership)
               .WithMany(d => d.Vehicles)
               .HasForeignKey(v => v.DealershipId)
               .OnDelete(DeleteBehavior.Cascade);        

            #endregion
        }
    }
}
