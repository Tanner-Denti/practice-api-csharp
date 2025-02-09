using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using sample_api.Data.Entities;

namespace sample_api.Data.Configurations;

public class VendorConfiguration : IEntityTypeConfiguration<Vendor>
{
    public void Configure(EntityTypeBuilder<Vendor> builder)
    {
        builder.ToTable("vendor");

        builder.HasKey(e => e.Name);

        builder.Property(e => e.Name)
            .HasColumnName("name")
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(e => e.PhoneNumber)
            .HasColumnName("phonenumber")
            .HasMaxLength(50);

        builder.Property(e => e.Street)
            .HasColumnName("street")
            .HasMaxLength(50);
            
        builder.Property(e => e.City)
            .HasColumnName("city")
            .HasMaxLength(50);

        builder.Property(e => e.State)
            .HasColumnName("state")
            .HasMaxLength(50);
            
        builder.Property(e => e.PostalCode)
            .HasColumnName("postalcode")
            .HasMaxLength(10)
            .IsRequired();
    }
}