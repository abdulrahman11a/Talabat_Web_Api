using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Talabat.core.Entites;
using Talabat.core.Entitys.Order_Aggregate;

namespace Talabat.Repository.Data.DataSeed
{
    public static  class StoreContextDataSeed
    {
        public static async Task DataSeedAsync(StoreDbcontext dbcontext)
        {
            // Seed Product Brands
            if (!dbcontext.ProductBrands.Any())
            {


            var brandsData = await File.ReadAllTextAsync("../Talabat.Repository/Data/DataSeed/JasonFile/brands.json");
            var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);
            if (brands?.Count() > 0)
            {
                await dbcontext.Set<ProductBrand>().AddRangeAsync(brands);
                await dbcontext.SaveChangesAsync();
            }
            }

            // Seed Product Types
            if (!dbcontext.ProductType.Any())
            {

                var typeData = await File.ReadAllTextAsync("../Talabat.Repository/Data/DataSeed/JasonFile/types.json");
                var types = JsonSerializer.Deserialize<List<ProductType>>(typeData);
                if (types?.Count() > 0)
                {
                    await dbcontext.Set<ProductType>().AddRangeAsync(types);
                    await dbcontext.SaveChangesAsync();
                }
            }
            // Seed Products
            if (!dbcontext.products.Any())
            {

                var productData = await File.ReadAllTextAsync("../Talabat.Repository/Data/DataSeed/JasonFile/products.json");
                var products = JsonSerializer.Deserialize<List<Product>>(productData);
                if (products?.Count() > 0)
                {
                    await dbcontext.Set<Product>().AddRangeAsync(products);
                    await dbcontext.SaveChangesAsync();
                }
            }

            // Seed delivery

            if (!dbcontext.DeliveryMethod.Any())
            {

                var DeliveryMethodData = await File.ReadAllTextAsync("../Talabat.Repository/Data/DataSeed/JasonFile/delivery.json");
                var DeliveryMethodDataSerializer = JsonSerializer.Deserialize<List<DeliveryMethod>>(DeliveryMethodData);
                if (DeliveryMethodDataSerializer?.Count() > 0)
                {

                    await dbcontext.Set<DeliveryMethod>().AddRangeAsync(DeliveryMethodDataSerializer);
                    await dbcontext.SaveChangesAsync();
                }
            }
        }
    }
}
