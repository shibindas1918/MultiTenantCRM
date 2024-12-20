using Microsoft.EntityFrameworkCore;
using MultiTenantCRM.Models;

namespace MultiTenantCRM.Data
{
    public class ApplicationDbContext :DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) :base(options) 
        {
            
        }
        public DbSet<Tenant> Tenants { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Contact> Contacts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.entity<tenant>()
                .hasmany(t => t.user)
                .withone(u => u.tenant)
                .hasforeignkey(u => u.tenantid);

            modelbuilder.entity<tenant>()
                .hasmany(t => t.customers)
                .withone(c => c.tenant)
                .hasforeignkey(c => c.tenantid);

            modelbuilder.entity<customer>()
                .hasmany(c => c.orders)
                .withone(o => o.customer)
                .hasforeignkey(o => o.customerid);
        }

    }
}
