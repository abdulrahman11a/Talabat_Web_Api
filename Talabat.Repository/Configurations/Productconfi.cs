using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Talabat.core.Entites;

namespace Talabat.Repository.Configurations
{
    public class Productconfi : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            #region Product Brand Configuration
            builder.HasOne(p => p.ProductBrand)
                .WithMany()
                .OnDelete(DeleteBehavior.Cascade);
            #endregion

            #region Product Type Configuration
            builder.HasOne(p => p.ProductType)
                .WithMany()
                .HasForeignKey(p => p.ProductTypeId) // Use the correct foreign key property here
                .OnDelete(DeleteBehavior.Cascade);
            #endregion

            builder.Property(p => p.PictureUrl)
                .IsRequired();

            builder.Property(p => p.Description)
                .IsRequired();

            builder.Property(p => p.Name)
                .HasMaxLength(100)
                .IsRequired();

            // Configure the Price property with column type
            builder.Property(p => p.Price)
                .HasColumnType("decimal(18,2)");
        }
    }
}
