using Microsoft.EntityFrameworkCore;
using Talabat.core.Entites;
using Talabat.core.Entitys.Order_Aggregate;

namespace Talabat.Repository.Data
{
    public class StoreDbcontext:DbContext
    {
        public StoreDbcontext(DbContextOptions<StoreDbcontext>options):base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(StoreDbcontext).Assembly); 
        }
        public DbSet<Product> products { get; set; }
        public DbSet<ProductBrand> ProductBrands { get; set; }
        public DbSet<ProductType> ProductType { get; set; }
        #region Order Module DbSet
        public DbSet<DeliveryMethod> DeliveryMethod { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<OrderItem> OrderItem { get; set; } 
        #endregion

    }
}
