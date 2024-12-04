using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.core.Entitys.Order_Aggregate;
using Talabat.core.Entitys.Order_Aggregate.Enum_Order_Aggregate;

namespace Talabat.Infrastructure.Configurations
{
    internal class OrderConfig : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.OwnsOne(O => O.ShippingAddress, ShippingAddress => ShippingAddress.WithOwner());

            builder.Property(O => O.orderStatus).HasConversion(

                 storeStatus => storeStatus.ToString(),

                 UseStatus => (OrderStatus)Enum.Parse(typeof(OrderStatus), UseStatus)


                );

            builder.HasOne(O => O.DeliveryMethod).WithMany().
                OnDelete(DeleteBehavior.SetNull);
            builder.Property(o => o.Tax)
             .HasColumnType("decimal(18,2)");
            builder.Property(o => o.SubTotal)
             .HasColumnType("decimal(18,2)");
            builder.Property(o => o.Discount)
             .HasColumnType("decimal(18,2)");



        }
    }
}
