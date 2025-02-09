using Microsoft.EntityFrameworkCore;
using sample_api.Data.Configurations;
using sample_api.Data.Entities;

namespace sample_api.Data;

public class ApplicationDbContext : DbContext
{
    public DbSet<Vendor> Vendor { get; set; }
    
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new VendorConfiguration());
    }
}