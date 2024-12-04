using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.core.Entitys.Order_Aggregate;

namespace Talabat.Infrastructure.Configurations
{
    internal class OrderItemConfig : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.OwnsOne(Or => Or.ProductItemOrdered, ProductItemOrdered => ProductItemOrdered.WithOwner());


            builder.Property(o => o.Price)
            .HasColumnType("decimal(18,2)");
        }
    }
}
