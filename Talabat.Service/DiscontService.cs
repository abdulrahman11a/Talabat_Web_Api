using Talabat.core.Entitys.Order_Aggregate.Enum_Order_Aggregate;
using Talabat.core.Services.Contract;

namespace Talabat.Applacation
{
    public class DiscountService : IDiscount
    {
        public decimal ApplyDiscount(Discount discount, decimal subTotal, bool isMember = false)
        {
            // Define discount rates
            var discountRates = new Dictionary<Discount, decimal>
            {
                { Discount.NoDiscount, 0 },
                { Discount.Seasonal, 0.1m },       // 10%
                { Discount.Promotional, 0.2m }    // 20%
            };

            // Default subtotal
            var discountedTotal = subTotal;

            // Check for member-specific discount
            if (isMember)
            {
                discountedTotal *= 0.75m; // Apply 25% member discount
            }
            else if (discountRates.TryGetValue(discount, out var discountRate))
            {
                // Apply discount based on type
                discountedTotal *= (1 - discountRate);
            }

            return discountedTotal;
        }

    
    }
}
