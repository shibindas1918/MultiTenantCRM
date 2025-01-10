using Microsoft.EntityFrameworkCore;
using MultiTenantCRM.Models;

namespace MultiTenantCRM.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Tenant> Tenants { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Contact> Contacts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //Tenant Configuration
            modelBuilder.Entity<Tenant>().HasKey(t => t.TenantId);
            modelBuilder.Entity<Tenant>().Property(t => t.Name).IsRequired().HasMaxLength(100);

            //User Configuration
            modelBuilder.Entity<User>().HasKey(u => u.UserId);
            modelBuilder.Entity<User>().Property(u => u.Email).IsRequired().HasMaxLength(150);
            modelBuilder.Entity<User>().HasOne(u => u.Tenant)
                                        .WithMany(t => t.Users)
                                        .HasForeignKey(u => u.TenantId);

            //Customer Configuration
            modelBuilder.Entity<Contact>().HasKey(c => c.ContactId);
            modelBuilder.Entity<Contact>().Property(c => c.Name).IsRequired().HasMaxLength(100);
            modelBuilder.Entity<Contact>().HasOne(c => c.Tenant)
                                            .WithMany(t => t.Customers)
                                            .HasForeignKey(c => c.TenantId);
            // Seeding tenants
            modelBuilder.Entity<Tenant>().HasData(
                 new Tenant { TenantId = 1, Name = "Tenant A", Domain = "tenant-a.com" },
                 new Tenant { TenantId = 2, Name = "Tenant B", Domain = "tenant-b.com" },
                 new Tenant { TenantId = 3, Name = "Tenant c", Domain = "tenant-b.com" }
                );

            // Seeding  users
            modelBuilder.Entity<User>().HasData(
                new User { UserId = 1, Name = "Admin A", Email = "admin@tenant-a.com", TenantId = 1 },
                new User { UserId = 2, Name = "Admin B", Email = "admin@tenant-b.com", TenantId = 2 },
                new User { UserId = 3, Name = "Admin c", Email = "admin@tenant-b.com", TenantId = 3 }
                );

            //modelBuilder.entity<tenant>()
            //    .hasmany(t => t.user)
            //    .withone(u => u.tenant)
            //    .hasforeignkey(u => u.tenantid);

            //modelbuilder.entity<tenant>()
            //    .hasmany(t => t.customers)
            //    .withone(c => c.tenant)
            //    .hasforeignkey(c => c.tenantid);

            //modelbuilder.entity<customer>()
            //    .hasmany(c => c.orders)
            //    .withone(o => o.customer)
            //    .hasforeignkey(o => o.customerid);
        }

    }
}
