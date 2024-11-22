using CMS_WebApps.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CMS_WebApps.Data
{
    public class CMS_WebAppsContext : IdentityDbContext<IdentityUser>
    {
        public CMS_WebAppsContext(DbContextOptions<CMS_WebAppsContext> options)
            : base(options)
        {
        }

        // Add DbSet for your custom models
        public DbSet<ClaimViewModel> Claims { get; set; }
       

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Customize ASP.NET Identity tables if necessary
            builder.Entity<IdentityUser>(entity =>
            {
                entity.ToTable("Users"); // Rename the default Identity table
            });

            builder.Entity<IdentityRole>(entity =>
            {
                entity.ToTable("Roles"); // Rename the default roles table
            });

            // Configure your custom models
            builder.Entity<ClaimViewModel>(entity =>
            {
                entity.Property(e => e.Total).HasColumnType("decimal(18, 2)");
                entity.Property(e => e.Status).HasDefaultValue("Pending");
            });

           
        }
    }
}
